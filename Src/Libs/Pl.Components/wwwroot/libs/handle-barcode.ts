import { type DotNetObjectType } from './types/dotnet-object-type'

const TIMEOUT_INTERVAL = 20

let timeoutId: ReturnType<typeof setInterval> | undefined
let inputEventHandler: ((event: KeyboardEvent) => void) | undefined
let barcode: string = ''

/**
 * Handles the event when a barcode is entered.
 *
 * @param {KeyboardEvent} event - The keyboard event triggered by the barcode input.
 * @param {DotNetObjectType} dotNetObjectReference - The reference to the DotNetObjectType.
 * @param {string} functionName - The name of the function to be invoked.
 * @return {Promise<void>} A promise that resolves when the barcode is handled.
 */
const handleBarcodeEnter = async (
  event: KeyboardEvent,
  dotNetObjectReference: DotNetObjectType,
  functionName: string
): Promise<void> => {
  if (timeoutId) clearInterval(timeoutId)

  if (event.key === 'Enter') {
    if (barcode) await dotNetObjectReference.invokeMethodAsync(functionName, barcode)
    barcode = ''
    return
  }

  if (event.key !== 'Shift') barcode += event.key

  timeoutId = setInterval(() => (barcode = ''), TIMEOUT_INTERVAL)
}

/**
 * Subscribes to the barcode enter event.
 *
 * @param {DotNetObjectType} dotNetObjectReference - The reference to the DotNetObjectType.
 * @param {string} functionName - The name of the function to be invoked.
 * @return {void} This function does not return anything.
 */
window.subscribeBarcodeEnterEvent = (dotNetObjectReference: DotNetObjectType, functionName: string): void => {
  inputEventHandler = (event) => {
    handleBarcodeEnter(event, dotNetObjectReference, functionName).catch((error: unknown) => {
      console.error('Error handling barcode enter:', error)
    })
  }
  document.addEventListener('keypress', inputEventHandler)
}

/**
 * Unsubscribes from the barcode enter event.
 *
 * @return {void} This function does not return anything.
 */
window.unsubscribeBarcodeEnterEvent = (): void => {
  if (!inputEventHandler) return
  document.removeEventListener('keypress', inputEventHandler)
}
