<script>
  import { listen } from "../../helpers/event";
  import { writable } from "svelte/store";
  import CloseIcon from "../icons/close_icon.svelte";
  import TickIcon from "../icons/tick_icon.svelte";
  import ErrorIcon from "../icons/error_icon.svelte";
  import WarnIcon from "../icons/warn_icon.svelte";
  import { superFly } from "../../helpers/superFly";
  import { _, dictionary } from "svelte-i18n";
  import Prep from "../global/prep.svelte";

  export let message, type, close;
  console.log($dictionary);
</script>

<Prep pack={message.pack}>
  <div class="message {type}" in:superFly={{ y: 100, duration: 600 }}>
    {#if type == "success"}
      <span class="sign">
        <TickIcon />
      </span>
    {:else if type == "error"}
      <span class="sign">
        <ErrorIcon />
      </span>
    {:else if type == "warn"}
      <span class="sign">
        <WarnIcon />
      </span>
    {/if}
    <span class="box">
      {$_(message.message, { values: message.values })}
    </span>
    {#if close}
      <button on:click={() => close(message.message)} class="btn i">
        <CloseIcon />
      </button>
    {/if}
  </div>
</Prep>
