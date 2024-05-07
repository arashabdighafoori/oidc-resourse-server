import { writable } from "svelte/store";
import { getCookie, setCookie } from "../../helpers/cookie";

let current_cookie = getCookie(".CONSENT") == "true";

const store = writable(current_cookie);
store.subscribe((consent) => {
  if (consent) setCookie(".CONSENT", consent, 99);
});
export { store };
