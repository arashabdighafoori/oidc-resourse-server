<script>
  import Prep from "../../global/prep.svelte";
  import IgnorantForm from "../IgnorantForm.svelte";

  import { _ } from "svelte-i18n";
  import get_form from "./language";
  import SelectField from "../SelectField.svelte";
  import Page from "../../page/page.svelte";
  import configuration from "../../../configuration";
  import {
    store as culture_store,
    direction,
  } from "../../../data/settings/culture";

  let form = get_form();
  $: myform = form?.form;

  const on_submit = ({ detail }) => {
    const { Culture } = detail;
    console.log(detail);
    culture_store.set([direction(Culture), Culture]);
    setTimeout(() => {
      location.reload();
    }, 1500);
  };
</script>

{#if myform}
  <Prep pack="app.settings">
    <Page>
      <div class="form_container">
        <div class="form_header">
          <h1>{$_("app_settings.language.title")}</h1>
          <h5>{$_("app_settings.language.subtitle")}</h5>
        </div>
        <IgnorantForm on:submit={on_submit} form={myform}>
          <SelectField
            field={form.language}
            loadOptions={undefined}
            items={configuration.languages}
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
