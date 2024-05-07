<script>
  export let field;
</script>

<div class="form_group">
  <input 
  {...$$props}
  class="form_input" type="number" bind:value={$field.value} />
  <slot {field} />
</div>
