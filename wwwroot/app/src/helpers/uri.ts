import { navigate } from "svelte-routing";

export function updateQueryParam(key, value) {
  if ("URLSearchParams" in window) {
    var searchParams = new URLSearchParams(window.location.search);
    searchParams.set(key, value);
    var newRelativePathQuery =
      window.location.pathname + "?" + searchParams.toString();
    navigate(newRelativePathQuery);
  }
}
