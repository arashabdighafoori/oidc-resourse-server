<script>
  export let field;
</script>

<div class="form_group checkbox">
  <input
    {...$$props}
    class="form_input"
    type="checkbox"
    bind:checked={$field.value}
  />
  <slot {field} />
</div>
