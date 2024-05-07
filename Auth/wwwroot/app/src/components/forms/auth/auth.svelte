<script>
  import LoginOrRegister from "./loginOrRegister.svelte";
  import Login from "./login.svelte";
  import Register from "./register.svelte";
  import { updateQueryParam } from "../../../helpers/uri";
  import PageContainer from "../../page/page_container.svelte";
  import LangThemeChain from "../../chains/lang_theme_chain.svelte";

  let state = "initial";
  updateQueryParam("view", "checkup");
  let passedData = "";
  const handle_initial = ({ detail }) => {
    const { ok, is_user } = detail.response;
    const { passed } = detail.data;
    passedData = passed;
    if (is_user) {
      state = "login";
      updateQueryParam("view", "login");
    } else {
      state = "register";
      updateQueryParam("view", "register");
    }
  };
</script>

<PageContainer>
  {#if state == "login"}
    <Login {passedData} />
  {:else if state == "register"}
    <Register {passedData} isPassed={true} />
  {:else if state == "initial"}
    <LoginOrRegister handle_response={handle_initial} />
  {/if}
</PageContainer>
<LangThemeChain />
