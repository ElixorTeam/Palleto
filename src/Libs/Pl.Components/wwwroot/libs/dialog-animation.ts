import { type ElementWithDialogType } from './types/element-with-dialog-type'

const ANIMATION_DURATION = 150

/**
 * Animates the opening of a dialog.
 *
 * @param {string} dialogId - The ID of the dialog element.
 * @return {Promise<void>} A promise that resolves when the animation is finished.
 */
window.animateDialogOpening = async (dialogId: string): Promise<void> => {
  const dialog = (document.querySelector(`#${dialogId}`) as ElementWithDialogType | undefined)?.dialog
  if (!dialog) return

  dialog.classList.add('ease-in-out')
  const animation = dialog.animate(
    [
      { opacity: '0', transform: 'scale(.95)' },
      { opacity: '1', transform: '' },
    ],
    {
      duration: ANIMATION_DURATION,
    }
  )
  await animation.finished
}

/**
 * Animates the closing of a dialog.
 *
 * @param {string} dialogId - The ID of the dialog element.
 * @return {Promise<void>} A promise that resolves when the animation is finished.
 */
window.animateDialogClosing = async (dialogId: string): Promise<void> => {
  const dialog = (document.querySelector(`#${dialogId}`) as ElementWithDialogType | undefined)?.dialog
  if (!dialog) return

  dialog.classList.add('ease-in-out')
  const animation = dialog.animate(
    [
      { opacity: '1', transform: '' },
      { opacity: '0', transform: 'scale(.8)' },
    ],
    {
      duration: ANIMATION_DURATION,
    }
  )
  dialog.style.opacity = '0.0'
  await animation.finished
}
