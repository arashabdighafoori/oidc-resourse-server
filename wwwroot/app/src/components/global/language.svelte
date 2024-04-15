<script>
  import { _, addMessages } from "svelte-i18n";
  export let pack;

  let culture = "en";
  let is_loaded = false;

  (async () => {
    const t = await fetch(`/app/lang/${culture}/${pack}.json`);
    if (t.ok) {
      const data = await t.json();
      addMessages(culture, data);
      is_loaded = true;
    }
  })();
</script>

{#if is_loaded}
  <slot />
{/if}
