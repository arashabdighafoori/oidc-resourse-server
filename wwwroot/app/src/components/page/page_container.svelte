<script>
  import { cookCss } from "../../helpers/css";

  let css = false;
  cookCss("pagecontainer.min.css", () => (css = true));
</script>

{#if css}
  <div class="page-container">
    <slot />
  </div>
{/if}
