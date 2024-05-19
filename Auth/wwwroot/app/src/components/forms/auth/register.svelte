<script>
  import { _ } from "svelte-i18n";
  import { Link, navigate } from "svelte-routing";
  import BodyForm from "../BodyForm.svelte";
  import TextField from "../TextField.svelte";
  import get_form from "./register";
  import { setCookie } from "../../../helpers/cookie";
  import { cookCss } from "../../../helpers/css";
  import { dispatch } from "../../../helpers/event";
  import PasswordField from "../PasswordField.svelte";
  import Page from "../../page/page.svelte";
  import DataField from "../DataField.svelte";
  import Prep from "../../global/prep.svelte";
  import UserIcon from "../../icons/user_icon.svelte";

  export let data;

  let myform;
  let isFormLoaded = false;
  let form;
  get_form(data).then((e) => {
    form = e;
    myform = form.form;
    isFormLoaded = true;
  });

  const on_success = ({ detail }) => {
    const { ok, name, url } = detail.response;
    console.log($myform.summary);
    setCookie(".NOM20.COOKIECONSENT", "true", 9999);
    dispatch("message", {
      message: `Welcome aboard ${name}!`,
      type: "success",
      uncloseable: true,
    });
    setTimeout(() => {
      window.location = url;
    }, 3000);
  };
</script>

{#if isFormLoaded}
  <Prep pack="auth" css_file="form.min.css">
    <Page>
      <div class="form_container">
        <BodyForm
          on:success={on_success}
          form={myform}
          action="/api/v1/auth/register"
        >
          <div class="form_header">
            <h1 class="text-display-1">{$_("auth.register.title")}</h1>
            <h5 class="text-display-3">
              {$_("auth.register.subtitle")}
            </h5>
          </div>

          <DataField field={form.email} name="email" id="email">
            <label for="email" class="form_label required">
              <UserIcon />
            </label>
          </DataField>

          <TextField
            field={form.friendlyname}
            name="friendlyname"
            id="friendlyname"
          >
            <label for="friendlyname" class="form_label">
              {$_("form.labels.friendlyname")}
            </label>
            {#if $myform.hasError("Name.required")}
              <div class="form_error">
                {$_("form.errors.required", {
                  values: { label: $_("form.labels.friendlyname") },
                })}
              </div>
            {/if}
            {#if $myform.hasError("Name.between")}
              <div class="form_error">
                {$_("form.errors.min", {
                  values: { label: $_("form.labels.friendlyname"), minimum: 3 },
                })}
              </div>
            {/if}
          </TextField>
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
            <Link to="/privacy-policy">
              {$_("auth.agreement.1")}
            </Link>
            {$_("auth.agreement.2")}
            <Link to="/terms-of-use">
              {$_("auth.agreement.3")}
            </Link>
            {$_("auth.agreement.4")}
          </div>
        </BodyForm>
      </div>
    </Page>
  </Prep>
{/if}
