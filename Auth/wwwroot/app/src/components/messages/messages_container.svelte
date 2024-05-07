<script>
  import { listen } from "../../helpers/event";
  import { writable } from "svelte/store";
  import Message from "./message.svelte";
  import { cookCss } from "../../helpers/css";

  const messages = writable([]);
  const close = (message) => {
    const index = $messages.findIndex((e) => e[0] == message);
    console.log(index, message);
    messages.update((messages) => {
      messages.splice(index, 1);
      return messages;
    });
  };
  listen("message", ({ detail }) => {
    const { message, type } = detail;
    const can_close = !detail.uncloseable;
    messages.update((messages) => {
      messages.push([message, type, can_close]);
      return messages;
    });
    setTimeout(() => {
      if (!can_close) close(message);
    }, 5000);
  });

  let css = 0;
  cookCss("message.min.css", () => css++);
</script>

{#if css}
  <div class="messages">  
    {#each $messages as message}
      <Message
        message={message[0]}
        type={message[1]}
        close={message[2] ? close : undefined}
      />
    {/each}
  </div>
{/if}
