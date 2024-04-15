<script>
  import { store as culture } from "./data/settings/culture";
  import { router } from "tinro";
  import { store as theme } from "./data/settings/theme";
  import { cookCss } from "./helpers/css";
  import MainRouter from "./routes/main.svelte";
  import { init } from "svelte-i18n";
  import { dispatch } from "./helpers/event";
  init({
    fallbackLocale: "en",
    initialLocale: $culture[1],
  });
  let css_loaded = 0;
  console.log($culture, $theme);
  document.body.classList.add($culture[0]);
  cookCss("main.min.css", () => css_loaded++);
  cookCss(`theme.${$theme}.min.css`, () => css_loaded++);

  // setTimeout(
  //   () =>
  //     dispatch("message", {
  //       message: `Welcome !`,
  //       type: "success",
  //       uncloseable: true,
  //     }),
  //   1000
  // );
</script>

<svelte:head>
  <!-- <title>نمره بیست</title>  -->
</svelte:head>

{#if css_loaded == 2}
  <MainRouter />
{/if}
