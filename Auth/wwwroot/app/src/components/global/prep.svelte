<script>
  import { _, addMessages } from "svelte-i18n";
  import { cookCss } from "../../helpers/css";
  export let pack = undefined;

  let culture = "en";
  let is_lang_loaded = !pack;

  !is_lang_loaded
    ? (async () => {
        const t = await fetch(`/app/lang/${culture}/${pack}.json`);
        if (t.ok) {
          const data = await t.json();
          addMessages(culture, data);
          is_lang_loaded = true;
        }
      })()
    : void 0;

  export let css_file = undefined;
  let is_css_loaded = !css_file;
  !is_css_loaded
    ? cookCss(css_file, () => {
        is_css_loaded = true;
      })
    : void 0;
</script>

{#if is_css_loaded && is_lang_loaded}
  <slot />
{/if}
