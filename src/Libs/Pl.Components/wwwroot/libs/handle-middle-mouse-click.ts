import { type DotNetObjectType } from './types/dotnet-object-type'

let middleMouseEventHandler: ((event: MouseEvent) => void) | undefined

/**
 * Handles the middle mouse click event by invoking a specified function on a DotNetObjectType.
 *
 * @param {MouseEvent} event - The middle mouse click event.
 * @param {DotNetObjectType} dotNetObjectReference - The reference to the DotNetObjectType.
 * @param {string} functionName - The name of the function to be invoked.
 * @return {Promise<void>} A Promise that resolves when the function is invoked.
 */
const handleMiddleMouseEvent = async (
  event: MouseEvent,
  dotNetObjectReference: DotNetObjectType,
  functionName: string
): Promise<void> => {
  if (event.button !== 1) return
  await dotNetObjectReference.invokeMethodAsync(functionName)
}

/**
 * Subscribes to the middle mouse click event and assigns a handler function to handle the event.
 *
 * @param {DotNetObjectType} dotNetObjectReference - The reference to the DotNetObjectType.
 * @param {string} functionName - The name of the function to be invoked when the middle mouse click event occurs.
 * @return {void} This function does not return a value.
 */
window.subscribeMiddleMouseClickEvent = (dotNetObjectReference: DotNetObjectType, functionName: string): void => {
  middleMouseEventHandler = (event: MouseEvent) => {
    handleMiddleMouseEvent(event, dotNetObjectReference, functionName).catch((error: unknown) => {
      console.error('Error handling middle mouse click:', error)
    })
  }
  document.addEventListener('mousedown', middleMouseEventHandler)
}

/**
 * Unsubscribes from the middle mouse click event and removes the handler function.
 *
 * @return {void} This function does not return a value.
 */
window.unsubscribeMiddleMouseClickEvent = (): void => {
  if (!middleMouseEventHandler) return
  document.removeEventListener('mousedown', middleMouseEventHandler)
}
