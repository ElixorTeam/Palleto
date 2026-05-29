/**
 * Numeric Input TypeScript interop module.
 * Handles input/blur/focus/keydown events in JS to minimize Blazor interop calls.
 */

import type { DotNetObjectType } from "../types/dotnet-object-type";
import type { InputUpdateMode } from "../types/input-update-mode.ts";

export interface NumericInputConfig {
    mode: InputUpdateMode;
	debounceMs: number;
	stepKeys: string[];
	allowDecimal: boolean;
	allowNegative: boolean;
	decimalSeparator: string;
}

interface NumericInputState {
	element: HTMLInputElement;
	dotNetRef: DotNetObjectType;
	config: NumericInputConfig;
	debounceTimer: number | null;
    rafId: number | null;
    pendingValue: string | null;
}

interface NumericInputInstance {
	state: NumericInputState;
	handleInput: () => void;
	handleBlur: () => void;
	handleFocus: () => void;
	handleKeyDown: (e: KeyboardEvent) => void;
	element: HTMLInputElement;
}

const instances = new Map<string, NumericInputInstance>();

/**
 * Initializes JS event handling for a numeric input element.
 */
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
        rafId: null,
        pendingValue: null,
	};

    const stepKeySet = new Set(config.stepKeys ?? []);


    const callOnInput = (value: string): void => {
        void dotNetRef.invokeMethodAsync("JsOnInput", value).catch(() => {});
    };

    const handleFocus = (): void => {
        void dotNetRef.invokeMethodAsync("JsOnFocus").catch(() => {});
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

    const handleKeyDown = (e: KeyboardEvent): void => {
        if (stepKeySet.has(e.key)) {
            e.preventDefault();
            void dotNetRef.invokeMethodAsync("JsOnKeyDown", e.key).catch(() => {});
        }
    };

    /**
	 * Sanitizes numeric input while preserving cursor position.
	 */
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
			} else {
				if (i < cursorPos) {
					removed++;
				}
			}
		}

		if (sanitized !== raw) {
			element.value = sanitized;

			const newPos = Math.max(
				0,
				Math.min(cursorPos - removed, sanitized.length),
			);

			try {
				element.setSelectionRange(newPos, newPos);
			} catch {
				// setSelectionRange not supported
			}
		}
	};

	const cancelPending = (): void => {
        if (state.debounceTimer !== null) {
            window.clearTimeout(state.debounceTimer);
            state.debounceTimer = null;
        }
        if (state.rafId !== null) {
            cancelAnimationFrame(state.rafId);
            state.rafId = null;
        }
	};

	const handleInput = (): void => {
		sanitizeInput();

		const value = element.value;

        if (config.mode === "onblur") {
            return;
        }

        if (config.mode === "immediate") {
            if (state.rafId !== null) {
                cancelAnimationFrame(state.rafId);
            }
            state.pendingValue = value;
            state.rafId = requestAnimationFrame(() => {
                state.rafId = null;
                callOnInput(state.pendingValue ?? "");
            });
            return;
        }

        if (config.mode === "debounced") {
            callOnInput(value);

            if (state.debounceTimer !== null) {
                window.clearTimeout(state.debounceTimer);
            }
            state.debounceTimer = window.setTimeout(() => {
                state.debounceTimer = null;
                callOnInput(element.value);
            }, config.debounceMs);
        }
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

/**
 * Updates configuration for an existing instance.
 */
export function updateConfig(
	instanceId: string,
	config: NumericInputConfig,
): void {
	const stored = instances.get(instanceId);

	if (stored) {
		stored.state.config = config;
	}
}

/**
 * Removes handlers and disposes instance state.
 */
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