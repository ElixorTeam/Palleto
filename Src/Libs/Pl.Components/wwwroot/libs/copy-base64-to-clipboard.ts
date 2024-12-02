/**
 * Copies the base64 image data to the clipboard.
 *
 * @param {string} base64 - The base64 image data to copy.
 * @return {Promise<void>} A promise that resolves when the data is copied to the clipboard.
 */
window.copyBase64ToClipboard = async (base64: string): Promise<void> => {
  const fetchResponse = await fetch(base64)
  const blob = await fetchResponse.blob()
  const data = [new ClipboardItem({ 'image/png': blob })]
  await navigator.clipboard.write(data)
}
