<script>
  import Prep from "../../global/prep.svelte";
  import IgnorantForm from "../IgnorantForm.svelte";

  import { _ } from "svelte-i18n";
  import get_form from "./theme";
  import SelectField from "../SelectField.svelte";
  import Page from "../../page/page.svelte";
  import configuration from "../../../configuration";

  let form;
  $: myform = form?.form;
  (async () => {
    form = await get_form();
  })();

  const on_submit = ({ detail }) => {
    console.log(detail);
  };
</script>

{#if myform}
  <Prep pack="app.settings">
    <Page>
      <div class="form_container">
        <div class="form_header">
          <h1>{$_("app_settings.theme.title")}</h1>
          <h5>{$_("app_settings.theme.subtitle")}</h5>
        </div>
        <IgnorantForm on:submit={on_submit} form={myform}>
          <SelectField
            field={form.theme}
            loadOptions={undefined}
            items={configuration.themes}
            name="theme"
            id="theme"
          >
            <div slot="item" let:item>
              <span>{item.label}</span>
            </div>
            <div slot="selection" let:selection>
              <span>{selection.label}</span>
            </div>
            <label for="theme" class="form_label">
              {$_("form.labels.theme")}
            </label>
            {#if $myform.hasError("Theme.required")}
              <div class="form_error">
                {$_("form.errors.required", {
                  values: { label: $_("form.labels.theme") },
                })}
              </div>
            {/if}
          </SelectField>
        </IgnorantForm>
      </div>
    </Page>
  </Prep>
{/if}
