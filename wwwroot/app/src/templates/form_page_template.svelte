<script>
  import { cookCss } from "../helpers/css";

  let css_loaded = false;
  cookCss("formpagetemplate.min.css", () => (css_loaded = true));
</script>

{#if css_loaded}
  <div id="form_page">
    <slot />
  </div>
{/if}
