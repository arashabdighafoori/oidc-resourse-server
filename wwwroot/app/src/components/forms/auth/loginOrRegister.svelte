<script>
  import { _ } from "svelte-i18n";
  import BaseForm from "../BaseForm.svelte";
  import TextField from "../TextField.svelte";
  import get_form from "./loginOrRegister";
  import { cookCss } from "../../../helpers/css";
  import Language from "../../global/language.svelte";
  import Page from "../../page/page.svelte";
  import { Link } from "svelte-routing";
  import Prep from "../../global/prep.svelte";

  export let handle_response;
  const form = get_form();
  const email = form.email;
  const myform = form.form;
  const on_success = ({ detail }) => {
    console.log("1", detail);
    handle_response({
      detail: {
        response: detail.response,
        data: { passed: $email.value },
      },
    });
  };
</script>

<Prep pack="auth" css_file="form.min.css">
  <Page>
    <div class="form_container">
      <div class="form_header">
        <h1 class="text-display-1">{$_("auth.login_or_signin.title")}</h1>
        <h5 class="text-display-3">{$_("auth.login_or_signin.subtitle")}</h5>
      </div>
      <BaseForm
        on:success={on_success}
        form={myform}
        action="/api/auth/isuser/"
      >
        <TextField field={form.email} name="email" id="email">
          <label for="email" class="form_label required"
            >{$_("form.labels.email")}</label
          >
          {#if $myform.hasError("Email.required")}
            <div class="form_error">
              {$_("form.errors.required", {
                values: { label: $_("form.labels.email") },
              })}
            </div>
          {/if}
          {#if $myform.hasError("Email.between")}
            <div class="form_error">
              {$_("form.errors.min", {
                values: { label: $_("form.labels.email"), minimum: 5 },
              })}
            </div>
          {/if}
        </TextField>
        <div class="form_text" slot="underbutton">
          {$_("auth.agreement.0")}
          <Link on:click={close} to="/privacy-policy">
            {$_("auth.agreement.1")}
          </Link>
          {$_("auth.agreement.2")}
          <Link on:click={close} to="/terms-of-use">
            {$_("auth.agreement.3")}
          </Link>
          {$_("auth.agreement.4")}
        </div>
      </BaseForm>
    </div>
  </Page>
</Prep>
