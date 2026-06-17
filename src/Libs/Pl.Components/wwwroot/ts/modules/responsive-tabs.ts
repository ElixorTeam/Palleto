// Responsive tabs overflow detection
// Uses ResizeObserver to detect when tab buttons overflow their container

import type { DotNetObjectType } from "../types/dotnet-object-type";

interface ResponsiveTabsState {
	observer: ResizeObserver;
	dotNetRef: DotNetObjectType;
	tablist: HTMLElement;
	remeasure: () => void;
}

const states = new Map<string, ResponsiveTabsState>();

/**
 * Initializes overflow detection for a responsive tabs container.
 */
export function initialize(
	dotNetRef: DotNetObjectType,
	componentId: string,
	containerElement: HTMLElement,
): void {
	if (!dotNetRef || !containerElement) {
		return;
	}

	const tablist = containerElement.querySelector<HTMLElement>('[role="tablist"]');
	if (!tablist) {
		return;
	}

	// Measure the natural width of all tabs once, before any hiding occurs.
	// This avoids the feedback loop where hiding tabs changes scrollWidth.
	let tabsNaturalWidth = tablist.scrollWidth;
	let lastOverflowing: boolean | null = null;

	const check = (): void => {
		const availableWidth = containerElement.clientWidth;
		const isOverflowing = tabsNaturalWidth > availableWidth;
		if (isOverflowing !== lastOverflowing) {
			lastOverflowing = isOverflowing;
			void dotNetRef
				.invokeMethodAsync("OnOverflowChange", isOverflowing)
				.catch(() => {});
		}
	};

	const remeasure = (): void => {
		tabsNaturalWidth = tablist.scrollWidth;
		lastOverflowing = null;
		check();
	};

	const observer = new ResizeObserver(() => check());
	observer.observe(containerElement);

	states.set(componentId, { observer, dotNetRef, tablist, remeasure });

	check();
}

/**
 * Triggers a re-measurement of the natural tab width.
 */
export function remeasure(componentId: string): void {
	states.get(componentId)?.remeasure();
}

/**
 * Disposes overflow detection for a responsive tabs instance.
 */
export function dispose(componentId: string): void {
	const state = states.get(componentId);
	if (state) {
		state.observer.disconnect();
		states.delete(componentId);
	}
}
