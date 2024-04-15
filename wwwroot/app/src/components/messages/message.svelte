<script>
  import { listen } from "../../helpers/event";
  import { writable } from "svelte/store";
  import CloseIcon from "../icons/close_icon.svelte";
  import TickIcon from "../icons/tick_icon.svelte";
  import { superFly } from "../../helpers/superFly";

  export let message, type, close;
</script>

<div class="message {type}" in:superFly={{ y: 100, duration: 600 }}>
  {#if type == "success"}
    <span class="sign">
      <TickIcon />
    </span>
  {/if}
  <span class="box">
    {message}
  </span>
  {#if close}
    <button on:click={() => close(message)} class="btn i">
      <CloseIcon />
    </button>
  {/if}
</div>
