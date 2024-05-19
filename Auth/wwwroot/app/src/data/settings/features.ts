import { writable } from "svelte/store";
import configuration from "../../configuration";
import { CacheService } from "../../services/cache";
import { store as consent } from "./consent";

let current_value = undefined;
const store = writable(current_value);

/**
 Request to get feature flags
 */
async function requestFlags() {
  current_value = await fetch("/api/v1/features")
    .then((res) => res.json())
    .catch((error) => {
      // something went wrong, did not get featureflags
      console.error(error);
    });
  store.set(current_value);
}

consent.subscribe((consent) => {
  (async () => {
    if (consent) {
      const cache = new CacheService();
      current_value = await cache.get(
        "flags",
        configuration.itemKeys["featureFlagKey"],
        async () => {
          await requestFlags();
          return current_value;
        }
      );
    } else {
      await requestFlags();
    }
    console.log(current_value);
  })();
});

export { store };
