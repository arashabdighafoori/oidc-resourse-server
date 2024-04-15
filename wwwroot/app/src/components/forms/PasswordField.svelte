<script>
  import EyeIcon from "../icons/eye_icon.svelte";

  export let field;
  let isvisible = false;
</script>

<div class="form_group">
  {#if !isvisible}
    <input
      {...$$props}
      type="password"
      class="form_input{$field.value && $field.value != '' ? ' filled' : ''}"
      bind:value={$field.value}
    />
  {:else}
    <input
      {...$$props}
      type="text"
      class="form_input{$field.value && $field.value != '' ? ' filled' : ''}"
      bind:value={$field.value}
    />
  {/if}
  <slot {field} />
  <button
    type="button"
    class="form_icon"
    on:click={() => (isvisible = !isvisible)}
  >
    <EyeIcon />
  </button>
</div>
