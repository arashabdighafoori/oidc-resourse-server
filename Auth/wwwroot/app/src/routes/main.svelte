<script>
  import { Router, Route } from "svelte-routing";
  import MessagesContainer from "../components/messages/messages_container.svelte";
  import MainTemplate from "../templates/main_template.svelte";
  import FormPageTemplate from "../templates/form_page_template.svelte";
  import Consented from "../guards/consented.svelte";
  $: url = location.pathname;
</script>

<Router {url}>
  <!-- app -->
  <Route path="/app">
    <MainTemplate>
      {#await import("../views/app/app.svelte") then c}
        <svelte:component this={c.default} />
      {/await}
    </MainTemplate>
  </Route>

  <!-- app -->
  <Route path="/home">
    <MainTemplate>
      {#await import("../views/app/app.svelte") then c}
        <svelte:component this={c.default} />
      {/await}
    </MainTemplate>
  </Route>

  <Route path="/auth">
    <FormPageTemplate>
      {#await import("../views/auth/auth.svelte") then c}
        <svelte:component this={c.default} />
      {/await}
    </FormPageTemplate>
  </Route>

  <Route path="/settings/*settings">
    {#await import("./settings.svelte") then c}
      <svelte:component this={c.default} />
    {/await}
  </Route>

  <MessagesContainer />
</Router>
