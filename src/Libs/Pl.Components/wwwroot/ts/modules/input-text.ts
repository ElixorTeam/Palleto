/**
 * Text Input TypeScript interop module.
 * Handles input/change events in JS to minimize C# interop calls.
 *
 * Modes (config.debouncing):
 *   - false: updates fire only on blur/change (zero interop during typing)
 *   - true:  debounced JsOnInput while typing + JsOnChange on blur
 */

import type { DotNetObjectType } from "../types/dotnet-object-type";

export interface TextInputConfig {
	debouncing: boolean;
	debounceMs: number;
	hasCharacterCount?: boolean;
	characterCountSelector?: string;
	maxLength?: number | null;
	notifyOnBlur?: boolean;
}

interface TextInputState {
	element: HTMLInputElement | HTMLTextAreaElement;
	dotNetRef: DotNetObjectType;
	config: TextInputConfig;
	debounceTimer: number | null;
	rafId: number | null;
	pendingValue: string | null;
}

interface TextInputInstance {
	state: TextInputState;
	handleInput: () => void;
	handleChange: () => void;
	handleBlur?: () => void;
	element: HTMLInputElement | HTMLTextAreaElement;
}

const instances = new Map<string, TextInputInstance>();

/**
 * Initializes JS event handling for a text input or textarea element.
 */
export function initialize(
	element: HTMLInputElement | HTMLTextAreaElement,
	dotNetRef: DotNetObjectType,
	instanceId: string,
	config: TextInputConfig,
): void {
	if (!element || !dotNetRef) {
		return;
	}

	const state: TextInputState = {
		element,
		dotNetRef,
		config,
		debounceTimer: null,
		rafId: null,
		pendingValue: null,
	};

	const callOnInput = (value: string): void => {
		void dotNetRef.invokeMethodAsync("JsOnInput", value).catch(() => {});
	};

	const callOnChange = (value: string): void => {
		void dotNetRef.invokeMethodAsync("JsOnChange", value).catch(() => {});
	};

	const updateCharacterCount = (): void => {
		if (!config.hasCharacterCount || !config.characterCountSelector) {
			return;
		}

		const wrapper = element.closest("[data-textarea-wrapper]");
		if (!wrapper) {
			return;
		}

		const counter = wrapper.querySelector(config.characterCountSelector);
		if (!counter) {
			return;
		}

		const len = element.value.length;
		counter.textContent = config.maxLength
			? `${len}/${config.maxLength}`
			: `${len}`;
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
		const value = element.value;

		updateCharacterCount();

        if (!state.config.debouncing) {
            return;
        }


        if (state.debounceTimer !== null) {
            window.clearTimeout(state.debounceTimer);
        }
        state.debounceTimer = window.setTimeout(() => {
            state.debounceTimer = null;
            callOnInput(value);
        }, config.debounceMs);
	};

	const handleChange = (): void => {
		cancelPending();
		callOnChange(element.value);
	};

	element.addEventListener("input", handleInput);
	element.addEventListener("change", handleChange);

	const stored: TextInputInstance = {
		state,
		handleInput,
		handleChange,
		element,
	};

	// When notifyOnBlur is true, also listen for blur events so JsOnChange fires
	// even when the value hasn't changed (needed for editing/display toggle in InputField).
	// The change event fires before blur, so use a flag to prevent double-calling.
	if (config.notifyOnBlur) {
		let changeHandledBlur = false;

		const originalHandleChange = handleChange;
		const handleChangeForBlur = (): void => {
			changeHandledBlur = true;
			originalHandleChange();
		};

		const handleBlur = (): void => {
			if (!changeHandledBlur) {
                handleChange();
			}
			changeHandledBlur = false;
		};

		element.removeEventListener("change", handleChange);
		element.addEventListener("change", handleChangeForBlur);
		element.addEventListener("blur", handleBlur);

		stored.handleChange = handleChangeForBlur;
		stored.handleBlur = handleBlur;
	}

	instances.set(instanceId, stored);
}

/**
 * Updates the configuration for an existing instance.
 */
export function updateConfig(
	instanceId: string,
	config: TextInputConfig,
): void {
	const stored = instances.get(instanceId);

	if (stored) {
		stored.state.config = config;
	}
}

/**
 * Removes event handlers and cleans up state.
 */
export function dispose(instanceId: string): void {
	const stored = instances.get(instanceId);

	if (!stored) {
		return;
	}

	stored.element.removeEventListener("input", stored.handleInput);
	stored.element.removeEventListener("change", stored.handleChange);

	if (stored.handleBlur) {
		stored.element.removeEventListener("blur", stored.handleBlur);
	}

	if (stored.state.debounceTimer !== null) {
		window.clearTimeout(stored.state.debounceTimer);
	}
	if (stored.state.rafId !== null) {
		cancelAnimationFrame(stored.state.rafId);
	}

	instances.delete(instanceId);
}
