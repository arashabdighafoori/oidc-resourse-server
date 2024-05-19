<script>
  import { store } from "../data/settings/consent";
</script>

{#if $store}
  <slot />
{:else}
  {#await import("../views/global/consent.svelte") then c}
    <svelte:component this={c.default} />
  {/await}
{/if}
