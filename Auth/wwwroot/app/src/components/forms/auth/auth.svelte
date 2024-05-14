<script>
  import LoginOrRegister from "./loginOrRegister.svelte";
  import Login from "./login.svelte";
  import Register from "./register.svelte";
  import { updateQueryParam, getQueryParams } from "../../../helpers/uri";
  import PageContainer from "../../page/page_container.svelte";
  import LangThemeChain from "../../chains/lang_theme_chain.svelte";

  let state = "initial";
  updateQueryParam("view", "checkup");
  var params = getQueryParams();
  let data = {};
  var obj = {
    response_type: params.get("response_type"),
    client_id: params.get("client_id"),
    redirect_uri: params.get("redirect_uri"),
    scope: params.get("scope"),
    state: params.get("state"),
  };
  (async () => {
    const rawResponse = await fetch("/api/v1/auth/authorize-client", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(obj),
    });
    data = await rawResponse.json();
  })();

  const handle_initial = ({ detail }) => {
    const { ok, is_user } = detail.response;
    const { passed } = detail.data;
    data.userName = passed;
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
    <Login {data} />
  {:else if state == "register"}
    <Register {data} />
  {:else if state == "initial"}
    <LoginOrRegister handle_response={handle_initial} />
  {/if}
</PageContainer>
<LangThemeChain />
