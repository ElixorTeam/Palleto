window.animateDialogOpening=async e=>{const t=document.querySelector(`#${e}`)?.dialog;if(!t)return;t.classList.add("ease-in-out");const n=t.animate([{opacity:"0",transform:"scale(.95)"},{opacity:"1",transform:""}],{duration:150});await n.finished},window.animateDialogClosing=async e=>{const t=document.querySelector(`#${e}`)?.dialog;if(!t)return;t.classList.add("ease-in-out");const n=t.animate([{opacity:"1",transform:""},{opacity:"0",transform:"scale(.8)"}],{duration:150});t.style.opacity="0.0",await n.finished};const e=e=>{const t=e.querySelector(".width-sub-element"),n=e.querySelector(".width-ref-element");n&&t&&(n.style.width=`${t.offsetWidth}px`)};window.subscribeElementResize=t=>{if(!t)return;const n=()=>e(t);n(),window.addEventListener("resize",n),t.resizeHandler=n},window.updateElementSize=t=>e(t),window.unsubscribeElementResize=e=>{const t=e.resizeHandler;t&&(window.removeEventListener("resize",t),delete e.resizeHandler)};let t=null,n=null,o="";window.subscribeBarcodeEnterEvent=(e,i)=>{n=n=>(async(e,n,i)=>{if(t&&clearInterval(t),"Enter"===e.key)return o&&await n.invokeMethodAsync(i,o),void(o="");"Shift"!==e.key&&(o+=e.key),t=setInterval((()=>o=""),20)})(n,e,i),document.addEventListener("keypress",n)},window.unsubscribeBarcodeEnterEvent=()=>{null!==n&&document.removeEventListener("keypress",n)};let i=null;window.subscribeMiddleMouseClickEvent=(e,t)=>{i=n=>(async(e,t,n)=>{1===e.button&&await t.invokeMethodAsync(n)})(n,e,t),document.addEventListener("mousedown",i)},window.unsubscribeMiddleMouseClickEvent=()=>{null!==i&&document.removeEventListener("mousedown",i)},window.isElementContainsFocusedItem=e=>!!e&&e.contains(document.activeElement),window.switchTheme=e=>{const t=document.documentElement;let n="dark"===e;"system"===e?(localStorage.removeItem("color-theme"),n=window.matchMedia("(prefers-color-scheme: dark)").matches):localStorage.setItem("color-theme",e),t.classList.toggle("dark",n)},window.initializeTheme=()=>{"dark"===localStorage.getItem("color-theme")||!("color-theme"in localStorage)&&window.matchMedia("(prefers-color-scheme: dark)").matches?document.documentElement.classList.add("dark"):document.documentElement.classList.remove("dark")};
