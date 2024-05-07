import Root from "./root.svelte";
export function configure() {
  // AddOpenAuth();
  // AddQuery();
}

export function startup(): Root {
  const root = new Root({
    target: document.getElementById("app"),
  });
  return root;
}
