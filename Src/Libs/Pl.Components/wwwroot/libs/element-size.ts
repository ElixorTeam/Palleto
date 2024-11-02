import { type DotNetObjectType } from './types/dotnet-object-type'
import { type ElementWithHandler } from './types/element-with-handler-type'

/**
 * Resizes the element by updating the width of the dropdown based on the width of the button.
 *
 * @param {Element} element - The element to be resized.
 * @return {void} This function does not return anything.
 */
const resizeElement = (element: Element): void => {
  const button = element.querySelector('.width-sub-element') as HTMLElement | undefined
  const dropdown = element.querySelector('.width-ref-element') as HTMLElement | undefined
  if (dropdown && button) dropdown.style.width = `${button.offsetWidth.toString()}px`
}

/**
 * Subscribes to element resize events and updates the element's dropdown width based on the button width.
 *
 * @param {ElementWithHandler | undefined} element - The element to subscribe to resize events for.
 * @return {void} This function does not return anything.
 */
window.subscribeElementResize = (element: ElementWithHandler | undefined): void => {
  if (!element) return
  const resizeHandler = () => {
    resizeElement(element)
  }
  resizeHandler()
  window.addEventListener('resize', resizeHandler)
  element.handler = resizeHandler
}

/**
 * Updates the size of the element by resizing it using the `resizeElement` function.
 *
 * @param {Element} element - The element to update the size for.
 * @return {void} This function does not return anything.
 */
window.updateElementSize = (element: Element): void => {
  resizeElement(element)
}

/**
 * Unsubscribes from element resize events and clears the resize handler for the given element.
 *
 * @param {ElementWithHandler} element - The element to unsubscribe from resize events for.
 * @return {void} This function does not return anything.
 */
window.unsubscribeElementResize = (element: ElementWithHandler): void => {
  const resizeHandler = element.handler
  if (!resizeHandler) return
  window.removeEventListener('resize', resizeHandler)
  delete element.handler
}

window.getElementWidthById = (id: string): number => {
  const element = document.querySelector(`#${id}`)
  return element ? (element as HTMLElement).offsetWidth : 0
}

type EventHandler = {
  dotNetObjectId: number
  functionName: string
  eventName: string
  handler: EventListener
}

const eventHandlers: EventHandler[] = []

window.addDotNetEventListener = (
  eventName: string,
  dotNetObjectReferencer: DotNetObjectType,
  functionName: string,
  ...arguments_: unknown[]
): void => {
  const handler: EventListener = () => {
    dotNetObjectReferencer.invokeMethodAsync(functionName, ...arguments_).catch((error: unknown) => {
      console.log(error)
    })
  }
  window.addEventListener(eventName, handler)
  eventHandlers.push({ dotNetObjectId: dotNetObjectReferencer._id, functionName, eventName, handler })
}

window.removeDotNetEventListener = (
  eventName: string,
  dotNetObjectReferencer: DotNetObjectType,
  functionName: string
): void => {
  const index = eventHandlers.findIndex(
    (h) =>
      h.dotNetObjectId === dotNetObjectReferencer._id && h.functionName === functionName && h.eventName === eventName
  )
  if (index == -1) return
  window.removeEventListener(eventName, eventHandlers[index].handler)
  eventHandlers.splice(index, 1)
}
