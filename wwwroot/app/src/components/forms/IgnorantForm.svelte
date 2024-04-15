<script>
  import { _ } from "svelte-i18n";
  import { createEventDispatcher } from "svelte";
  import Spinner from "../global/spinner/spinner.svelte";
  import Prep from "../global/prep.svelte";

  const dispatcher = createEventDispatcher();

  export let form;
  let isSubmitting = false;
  let isSuccess = false;
  let isError = false;
  $: isForm = !isSuccess && !isError;

  const on_submit = async (ev) => {
    await form.validate();
    dispatcher("submit", $form.summary);
  };
</script>

<Prep css_file="form.min.css" pack="form">
  {#if isForm}
    <form on:submit|preventDefault={on_submit} class="form" method="post">
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
  {/if}
</Prep>
