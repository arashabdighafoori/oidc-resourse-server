<script>
  import { _ } from "svelte-i18n";
  import BaseForm from "../BaseForm.svelte";
  import get_form from "./login";
  import { setCookie } from "../../../helpers/cookie";
  import { cookCss } from "../../../helpers/css";
  import PasswordField from "../PasswordField.svelte";
  import Page from "../../page/page.svelte";
  import { Link, navigate } from "svelte-routing";
  import { dispatch } from "../../../helpers/event";
  import DataField from "../DataField.svelte";
  import Prep from "../../global/prep.svelte";
  import UserIcon from "../../icons/user_icon.svelte";

  export let data;
  const form = get_form(data);
  const myform = form.form;

  const on_success = ({ detail }) => {
    const { ok, name } = detail.response;
    setCookie(".COOKIECONSENT", "true", 9999);
    dispatch("message", {
      message: `Welcome Back ${name}!`,
      type: "success",
      uncloseable: true,
    });
    navigate("/home/");
  };
</script>

<Prep pack="auth" css_file="form.min.css">
  <Page>
    <div class="form_container">
      <div class="form_header">
        <h1 class="text-display-1">{$_("auth.password.title")}</h1>
        <h5 class="text-display-3">{$_("auth.password.subtitle")}</h5>
      </div>
      <BaseForm
        on:success={on_success}
        form={myform}
        action="/api/v1/auth/login"
      >
        <DataField field={form.email} name="email" id="email">
          <label for="email" class="form_label required">
            <UserIcon />
          </label>
        </DataField>
        <PasswordField field={form.password} name="password" id="password">
          <label for="password" class="form_label">
            {$_("form.labels.password")}
          </label>
          {#if $myform.hasError("Password.required")}
            <div class="form_error">
              {$_("form.errors.required", {
                values: { label: $_("form.labels.password") },
              })}
            </div>
          {/if}
          {#if $myform.hasError("Password.pattern")}
            <div class="form_error">
              {$_("form.errors.passpattern", {
                values: { label: $_("form.labels.password") },
              })}
            </div>
          {/if}
        </PasswordField>

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
