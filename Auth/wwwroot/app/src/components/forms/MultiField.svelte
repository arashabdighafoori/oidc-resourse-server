<script>
  import { writable, get } from "svelte/store";

  export let field;

  const vs = get(field).value;
  let values = writable(vs == "" ? [] : vs.split(",").map((e, i) => [i, e]));

  let select_value = "";
  let input_value = "";

  const save = () => {
    const vs = get(values);
    const final_value = vs.map(([index, value]) => value).join(",");
    console.log(final_value);
    field.set(final_value);
  };

  const on_delete = (i) => {
    values.update((v) => {
      v.splice(i, 1);
      return v;
    });
  };

  const on_add = () => {
    values.update((v) => {
      v.push([v.length, `${select_value}:${input_value}`]);
      return v;
    });
    console.log($values);
    save();
  };
</script>

<div class="form_group">
  <div class="multi">
    <div class="wrapper">
      <select class="multi-select" id="multi-select" bind:value={select_value}>
        <slot />
      </select>
    </div>
    <input
      {...$$props}
      class="multi-input"
      type="text"
      bind:value={input_value}
    />
    <button type="button" class="multi-button" on:click={on_add}>Add</button>
    <slot name="label" {field} />
  </div>
  {#if $values.length}
    <div class="values">
      {#each $values as [i, value]}
        <span>
          <span class="text">{value}</span>
          <button
            class="close multi-button"
            on:click={() => {
              on_delete(i);
            }}
          >
            <svg
              width="100%"
              height="100%"
              viewBox="-2 -2 50 50"
              focusable="false"
              aria-hidden="true"
              role="presentation"
            >
              <path
                fill="currentColor"
                d="M34.923,37.251L24,26.328L13.077,37.251L9.436,33.61l10.923-10.923L9.436,11.765l3.641-3.641L24,19.047L34.923,8.124l3.641,3.641L27.641,22.688L38.564,33.61L34.923,37.251z"
              />
            </svg>
          </button>
        </span>
      {/each}
    </div>
  {/if}
</div>
