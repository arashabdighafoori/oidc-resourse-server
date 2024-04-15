<script>
  export let field;
</script>

<div class="form_group">
  <input
    {...$$props}
    class="form_input{$field.value && $field.value != '' ? ' filled' : ''}"
    type="text"
    bind:value={$field.value}
  />
  <slot {field} />
</div>
