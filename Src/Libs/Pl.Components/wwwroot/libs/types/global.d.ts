import { type DotNetObjectType } from './dotnet-object-type'
import { type ElementWithHandler } from './element-with-handler-type'

declare global {
  interface Window {
    animateDialogOpening: (dialogId: string) => Promise<void>
    animateDialogClosing: (dialogId: string) => Promise<void>
    isElementContainsFocusedItem: (element: Element) => boolean
    subscribeElementResize: (element: ElementWithHandler) => void
    unsubscribeElementResize: (element: ElementWithHandler) => void
    updateElementSize: (element: Element) => void
    switchTheme: (theme: string) => void
    initializeTheme: () => void
    subscribeBarcodeEnterEvent: (dotNetObjectReference: DotNetObjectType, functionName: string) => void
    unsubscribeBarcodeEnterEvent: () => void
    subscribeMiddleMouseClickEvent: (dotNetObjectReference: DotNetObjectType, functionName: string) => void
    unsubscribeMiddleMouseClickEvent: () => void
    copyBase64ToClipboard: (base64: string) => Promise<void>
    getElementWidthById: (id: string) => number
    addDotNetEventListener: (
      eventName: string,
      dotNetObjectReferencer: DotNetObjectType,
      functionName: string,
      ...arguments_: unknown[]
    ) => void
    removeDotNetEventListener: (
      eventName: string,
      dotNetObjectReferencer: DotNetObjectType,
      functionName: string
    ) => void
  }
}

export {}
