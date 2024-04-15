<script>
  import Select from "svelte-select/no-styles/Select.svelte";
  export let placeholder;
  export let field;
  export let loadOptions;
  export let items = [];
  export let multiple = false;
  console.log($field);
  if (loadOptions) {
    (async () => {
      items = await loadOptions();
    })();
  }
  const set_field = (e) => {
    if (e.detail == null) field.set("");
    else if (multiple) field.set(e.detail.map((e) => e.id));
    else if (Object.hasOwnProperty.call(e.detail, "id")) field.set(e.detail.id);
    else field.set(e.detail.value);
  };
</script>

<div class="form_group">
  <Select
    class="form_select{$field.value && $field.value != '' ? ' filled' : ''}"
    {loadOptions}
    value={items.length > 0 ? items.find((e) => e.value == $field.value) : null}
    itemId="id"
    {items}
    {placeholder}
    {multiple}
    on:input={set_field}
  >
    <div class="select-item" slot="item" let:item let:index>
      <slot name="item" {item} />
    </div>

    <div class="select-item" slot="selection" let:selection>
      <slot name="selection" {selection} />
    </div>
  </Select>
  <slot {field} />
</div>
