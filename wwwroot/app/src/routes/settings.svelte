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

  <Route path="/theme">
    <Consented>
      <FormPageTemplate>
        {#await import("../views/settings/theme.svelte") then c}
          <svelte:component this={c.default} />
        {/await}
      </FormPageTemplate>
    </Consented>
  </Route>

  <Route path="/language">
    <Consented>
      <FormPageTemplate>
        {#await import("../views/settings/language.svelte") then c}
          <svelte:component this={c.default} />
        {/await}
      </FormPageTemplate>
    </Consented>
  </Route>
</Router>
