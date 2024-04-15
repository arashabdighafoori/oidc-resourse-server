import { writable } from "svelte/store";
import { getCookie, setCookie } from "../../helpers/cookie";
import { store as consent } from "./consent";

let current_cookie = getCookie(".THEME");
const store = writable(current_cookie);
consent.subscribe((consent) => {

  if (consent) {
    if (!current_cookie) {
      setCookie(".THEME", "dark", 99);
      current_cookie = "dark";
    }
    store.subscribe((theme) => {
      setCookie(".THEME", theme, 999);
    });
  } else {
    store.set("dark");
  }
});

export { store };
