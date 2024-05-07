<script>
  import { cookCss } from "../../helpers/css";

  let isCssLoaded = false;
  export let css_file;
  cookCss(css_file, () => {
    isCssLoaded = true;
  });
</script>

{#if isCssLoaded}
  <slot />
{/if}
