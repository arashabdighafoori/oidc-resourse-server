<script>
  import { get } from "svelte/store";
  import { _ } from "svelte-i18n";
  import { cookCss } from "../../helpers/css";
  import { createEventDispatcher } from "svelte";
  import Language from "../global/language.svelte";
  import Spinner from "../global/spinner/spinner.svelte";

  const dispatcher = createEventDispatcher();

  export let action;
  export let form;
  let isSubmitting = false;
  let isSuccess = false;
  let isError = false;
  $: isForm = !isSuccess && !isError;

  const get_form_data = (model, form = null) => {
    return JSON.stringify(model);
  };

  const on_submit = async (ev) => {
    await form.validate();
    const f = get(form);
    console.log("posting: ", f);
    const formData = get_form_data(f.summary);
    if (!f.valid) return;
    isSubmitting = true;
    fetch(action, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      redirect: "follow",
      body: formData,
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Request failed: " + response.statusText);
        }
        return response.json();
      })
      .then((response) => {
        if (response?.ok) dispatcher("success", { response });
        else dispatcher("error", { response });
        isSuccess = response?.ok;
        isError = !response?.ok;
        isSubmitting = false;
      })
      .catch((error) => {
        isError = true;
        isSubmitting = false;
      });
  };

  let isCssLoaded = false;
  cookCss("form.min.css", () => {
    isCssLoaded = true;
  });
</script>

<Language pack="form">
  {#if isCssLoaded}
    {#if isForm}
      <form
        on:submit|preventDefault={on_submit}
        class="form"
        method="post"
        {action}
      >
        <slot />

        <div class="form_group">
          <button class="btn p text center{!$form.valid ? ' invalid' : ''}">
            {#if isSubmitting}
              <Spinner size="24" state={isSubmitting} />
            {:else if !$form.valid}
              <span> {$_("form.submit.errors")} </span>
            {:else if isError}
              <span> {$_("form.submit.repeat")} </span>
            {:else}
              <span> {$_("form.submit.submit")} </span>
            {/if}
          </button>
        </div>
        <div class="form_group">
          <slot name="underbutton" />
        </div>
      </form>
      <!-- {:else if isSuccess}
      <div class="form_message">
        <h3 style="color: #57a957;">{$_("form.submit.submit")}</h3>
      </div>-->
    {/if}
  {/if}
</Language>
