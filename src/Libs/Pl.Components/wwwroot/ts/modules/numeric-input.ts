/**
 * Numeric Input TypeScript interop module.
 *
 * Modes (config.debouncing):
 *   - false: sanitize on input, commit only on blur (JsOnBlur)
 *   - true:  debounced JsOnInput while typing + JsOnBlur on blur
 */

import type { DotNetObjectType } from "../types/dotnet-object-type";

const STEP_KEYS = new Set([
	"ArrowUp",
	"ArrowDown",
	"PageUp",
	"PageDown",
	"Home",
	"End",
]);

export interface NumericInputConfig {
	debouncing: boolean;
	debounceMs: number;
	allowDecimal: boolean;
	allowNegative: boolean;
	decimalSeparator: string;
}

interface NumericInputState {
	element: HTMLInputElement;
	dotNetRef: DotNetObjectType;
	config: NumericInputConfig;
	debounceTimer: number | null;
}

interface NumericInputInstance {
	state: NumericInputState;
	handleInput: () => void;
	handleBlur: () => void;
	handleFocus: () => void;
	handleKeyDown: (event: KeyboardEvent) => void;
	element: HTMLInputElement;
}

const instances = new Map<string, NumericInputInstance>();

export function initialize(
	element: HTMLInputElement,
	dotNetRef: DotNetObjectType,
	instanceId: string,
	config: NumericInputConfig,
): void {
	if (!element || !dotNetRef) {
		return;
	}

	const state: NumericInputState = {
		element,
		dotNetRef,
		config,
		debounceTimer: null,
	};

	const sanitizeInput = (): void => {
		const cfg = state.config;
		const raw = element.value;
		const cursorPos = element.selectionStart ?? raw.length;

		let sanitized = "";
		let hasDecimal = false;
		let removed = 0;

		for (let i = 0; i < raw.length; i++) {
			const ch = raw[i];
			const decSep = cfg.decimalSeparator || ".";

			if (ch >= "0" && ch <= "9") {
				sanitized += ch;
			} else if (
				(ch === "." || ch === "," || ch === decSep) &&
				cfg.allowDecimal &&
				!hasDecimal
			) {
				sanitized += decSep;
				hasDecimal = true;
			} else if (ch === "-" && cfg.allowNegative && sanitized.length === 0) {
				sanitized += ch;
			} else if (i < cursorPos) {
				removed++;
			}
		}

		if (sanitized !== raw) {
			element.value = sanitized;
			const newPos = Math.max(0, Math.min(cursorPos - removed, sanitized.length));

			try {
				element.setSelectionRange(newPos, newPos);
			} catch {
				// setSelectionRange not supported for this input type
			}
		}
	};

	const cancelPending = (): void => {
		if (state.debounceTimer !== null) {
			window.clearTimeout(state.debounceTimer);
			state.debounceTimer = null;
		}
	};

	const handleInput = (): void => {
		sanitizeInput();

		if (!state.config.debouncing) {
			return;
		}

		cancelPending();
		state.debounceTimer = window.setTimeout(() => {
			state.debounceTimer = null;
			void dotNetRef.invokeMethodAsync("JsOnInput", element.value).catch(() => {});
		}, state.config.debounceMs);
	};

	const handleBlur = (): void => {
		cancelPending();
		void dotNetRef
			.invokeMethodAsync<string>("JsOnBlur", element.value)
			.then((displayValue) => {
				element.value = displayValue;
			})
			.catch(() => {});
	};

	const handleFocus = (): void => {
		void dotNetRef.invokeMethodAsync("JsOnFocus").catch(() => {});
	};

	const handleKeyDown = (event: KeyboardEvent): void => {
        if (!STEP_KEYS.has(event.key)) {
			return;
		}
		event.preventDefault();
		void dotNetRef.invokeMethodAsync("JsOnKeyDown", event.key).catch(() => {});
	};

	element.addEventListener("input", handleInput);
	element.addEventListener("blur", handleBlur);
	element.addEventListener("focus", handleFocus);
	element.addEventListener("keydown", handleKeyDown);

	instances.set(instanceId, {
		state,
		handleInput,
		handleBlur,
		handleFocus,
		handleKeyDown,
		element,
	});
}

export function updateConfig(
	instanceId: string,
	config: NumericInputConfig,
): void {
	const stored = instances.get(instanceId);

	if (stored) {
		stored.state.config = config;
	}
}

export function dispose(instanceId: string): void {
	const stored = instances.get(instanceId);

	if (!stored) {
		return;
	}

	stored.element.removeEventListener("input", stored.handleInput);
	stored.element.removeEventListener("blur", stored.handleBlur);
	stored.element.removeEventListener("focus", stored.handleFocus);
	stored.element.removeEventListener("keydown", stored.handleKeyDown);

	if (stored.state.debounceTimer !== null) {
		window.clearTimeout(stored.state.debounceTimer);
	}

	instances.delete(instanceId);
}
