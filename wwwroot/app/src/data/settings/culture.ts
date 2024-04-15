import { writable } from "svelte/store";
import { getLocaleFromNavigator } from "svelte-i18n";
import { getCookie, setCookie } from "../../helpers/cookie";
import { store as consent } from "./consent";

export function direction(locale) {
  if (["ar", "fa"].some((e) => e == locale)) return "rtl";
  return "ltr";
}

let current_cookie = getCookie(".CULTURE")?.split(",")[0];
const store = writable([direction(current_cookie), current_cookie]);

consent.subscribe((consent) => {
  if (consent) {
    store.subscribe((culture) => {
      setCookie(".CULTURE", `${culture[1]},${culture[0]}`, 999);

      if (!current_cookie) {
        const culture = getLocaleFromNavigator();
        const dir = direction(culture);
        setCookie(".CULTURE", `${culture},${dir}`, 999);
        current_cookie = culture;
      }
    });
  }
  else{
    store.set(["ltr", "en"])
  }
});

export { store };
