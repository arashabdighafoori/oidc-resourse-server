export function dispatch(event: any, detail: any) {
  window.dispatchEvent(new CustomEvent(event, { detail }));
}

export function listen(event: any, callback: any) {
  window.addEventListener(event, callback);
}
