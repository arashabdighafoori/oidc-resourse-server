<script>
  export let field;
  let selected_files;
  const set_field = (e) => {
    if (selected_files && selected_files.length && field)
      field.set(selected_files[0]);
  };
</script>

<div class="form_group">
  <input
    {...$$props}
    class="form_input"
    type="file"
    bind:files={selected_files}
    on:change={set_field}
  />
  <slot {field} />
</div>
