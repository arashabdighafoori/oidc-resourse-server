<script>
  import { onMount } from "svelte";
  import { createEditor, Editor, EditorContent } from "svelte-tiptap";
  import StarterKit from "@tiptap/starter-kit";
  import { cookCss } from "../util";

  export let field;
  let editor = undefined;

  onMount(() => {
    editor = createEditor({
      extensions: [StarterKit],
      content:
        $field.value ?? "پرنده کوچک تا صدای درب را شنید، بال گشود و رفت.",
    });

    editor.subscribe((e) => {
      field.set(e.view.dom.innerHTML);
    });
  });

  cookCss("editor.min.css");
</script>

<div class="form_group">
  {#if editor}
    <EditorContent editor={$editor} />
    <slot {field} />

    <div class="tiptap-buttons">
      <div class="pack">
        <!-- bold -->
        <button
          type="button"
          on:click={() =>
            console.log && $editor.chain().focus().toggleBold().run()}
          disabled={!$editor.can().chain().focus().toggleBold().run()}
          class="center {$editor.isActive('bold') ? 'is-active' : ''}"
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path
                fill="currentColor"
                d="M6.8 19V5h5.525q1.625 0 3 1T16.7 8.775q0 1.275-.575 1.963t-1.075.987q.625.275 1.388 1.025T17.2 15q0 2.225-1.625 3.113t-3.05.887zm3.025-2.8h2.6q1.2 0 1.463-.612t.262-.888q0-.275-.262-.887T12.35 13.2H9.825zm0-5.7h2.325q.825 0 1.2-.425t.375-.95q0-.6-.425-.975t-1.1-.375H9.825z"
              /></svg
            >
          </i>
        </button>
        <!-- italic -->
        <button
          type="button"
          on:click={() => $editor.chain().focus().toggleItalic().run()}
          disabled={!$editor.can().chain().focus().toggleItalic().run()}
          class={$editor.isActive("italic") ? "is-active" : ""}
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path
                fill="currentColor"
                d="M5 19v-2.5h4l3-9H8V5h10v2.5h-3.5l-3 9H15V19z"
              /></svg
            >
          </i>
        </button>
      </div>

      <div class="pack">
        <!-- p -->
        <button
          type="button"
          on:click={() => $editor.chain().focus().setParagraph().run()}
          class="center {$editor.isActive('paragraph') ? 'is-active' : ''}"
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
            >
              <path
                fill="currentColor"
                d="M9 20v-6q-2.075 0-3.537-1.463T4 9q0-2.075 1.463-3.537T9 4h9v2h-2v14h-2V6h-3v14z"
              /></svg
            >
          </i>
        </button>
        <!-- h1 -->
        <button
          type="button"
          on:click={() =>
            $editor.chain().focus().toggleHeading({ level: 1 }).run()}
          class="center {$editor.isActive('heading', { level: 1 })
            ? 'is-active'
            : ''}"
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
            >
              <path
                fill="currentColor"
                d="M5 17V7h2v4h4V7h2v10h-2v-4H7v4zm12 0V9h-2V7h4v10z"
              />
            </svg>
          </i>
        </button>
        <!-- h2 -->
        <button
          type="button"
          on:click={() =>
            $editor.chain().focus().toggleHeading({ level: 2 }).run()}
          class="center {$editor.isActive('heading', { level: 2 })
            ? 'is-active'
            : ''}"
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
            >
              <path
                fill="currentColor"
                d="M3 17V7h2v4h4V7h2v10H9v-4H5v4zm10 0v-4q0-.825.588-1.412T15 11h4V9h-6V7h6q.825 0 1.413.588T21 9v2q0 .825-.587 1.413T19 13h-4v2h6v2z"
              />
            </svg>
          </i>
        </button>
        <!-- h3 -->
        <button
          type="button"
          on:click={() =>
            $editor.chain().focus().toggleHeading({ level: 3 }).run()}
          class={$editor.isActive("heading", { level: 3 }) ? "is-active" : ""}
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
            >
              <path
                fill="currentColor"
                d="M3 17V7h2v4h4V7h2v10H9v-4H5v4zm10 0v-2h6v-2h-4v-2h4V9h-6V7h6q.825 0 1.413.588T21 9v6q0 .825-.587 1.413T19 17z"
              />
            </svg>
          </i>
        </button>
        <!-- h4 -->
        <button
          type="button"
          on:click={() =>
            $editor.chain().focus().toggleHeading({ level: 4 }).run()}
          class="center {$editor.isActive('heading', { level: 4 })
            ? 'is-active'
            : ''}"
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path
                fill="currentColor"
                d="M3 17V7h2v4h4V7h2v10H9v-4H5v4zm15 0v-3h-5V7h2v5h3V7h2v5h2v2h-2v3z"
              />
            </svg>
          </i>
        </button>

        <!-- <button type="button"
          on:click={() =>
            $editor.chain().focus().toggleHeading({ level: 5 }).run()}
          class={$editor.isActive("heading", { level: 5 }) ? "is-active" : ""}
        >
          h5
        </button>
        <button type="button"
          on:click={() =>
            $editor.chain().focus().toggleHeading({ level: 6 }).run()}
          class={$editor.isActive("heading", { level: 6 }) ? "is-active" : ""}
        >
          h6
        </button> -->
      </div>

      <div class="pack">
        <!-- horizontal rule -->
        <button
          type="button"
          on:click={() => $editor.chain().focus().setHorizontalRule().run()}
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path fill="currentColor" d="M4 13v-2h16v2z" /></svg
            >
          </i>
        </button>

        <!-- hard break -->
        <button
          type="button"
          on:click={() => $editor.chain().focus().setHardBreak().run()}
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path
                fill="currentColor"
                d="M14.7 20.7L11 17l3.7-3.7l1.4 1.45L14.85 16h2.4q.725 0 1.238-.513T19 14.25q0-.725-.513-1.237T17.25 12.5H4v-2h13.25q1.575 0 2.663 1.088T21 14.25q0 1.575-1.088 2.663T17.25 18h-2.4l1.25 1.25zM4 18v-2h5v2zM4 7V5h16v2z"
              /></svg
            >
          </i>
        </button>
        <!-- ul -->
        <button
          type="button"
          on:click={() => $editor.chain().focus().toggleBulletList().run()}
          class={$editor.isActive("bulletList") ? "is-active" : ""}
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path
                fill="currentColor"
                d="M9 19v-2h12v2zm0-6v-2h12v2zm0-6V5h12v2zM5 20q-.825 0-1.412-.587T3 18q0-.825.588-1.412T5 16q.825 0 1.413.588T7 18q0 .825-.587 1.413T5 20m0-6q-.825 0-1.412-.587T3 12q0-.825.588-1.412T5 10q.825 0 1.413.588T7 12q0 .825-.587 1.413T5 14m0-6q-.825 0-1.412-.587T3 6q0-.825.588-1.412T5 4q.825 0 1.413.588T7 6q0 .825-.587 1.413T5 8"
              /></svg
            >
          </i>
        </button>
        <!-- ol -->
        <button
          type="button"
          on:click={() => $editor.chain().focus().toggleOrderedList().run()}
          class={$editor.isActive("orderedList") ? "is-active" : ""}
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path
                fill="currentColor"
                d="M3 22v-1.5h2.5v-.75H4v-1.5h1.5v-.75H3V16h3q.425 0 .713.288T7 17v1q0 .425-.288.713T6 19q.425 0 .713.288T7 20v1q0 .425-.288.713T6 22zm0-7v-2.75q0-.425.288-.712T4 11.25h1.5v-.75H3V9h3q.425 0 .713.288T7 10v1.75q0 .425-.288.713T6 12.75H4.5v.75H7V15zm1.5-7V3.5H3V2h3v6zM9 19v-2h12v2zm0-6v-2h12v2zm0-6V5h12v2z"
              /></svg
            >
          </i>
        </button>
      </div>

      <div class="pack">
        <!-- undo -->
        <button
          type="button"
          class="center"
          on:click={() => $editor.chain().focus().undo().run()}
          disabled={!$editor.can().chain().focus().undo().run()}
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path
                fill="currentColor"
                d="M7 19v-2h7.1q1.575 0 2.738-1T18 13.5q0-1.5-1.162-2.5T14.1 10H7.8l2.6 2.6L9 14L4 9l5-5l1.4 1.4L7.8 8h6.3q2.425 0 4.163 1.575T20 13.5q0 2.35-1.737 3.925T14.1 19z"
              /></svg
            >
          </i>
        </button>
        <!-- redo -->
        <button
          type="button"
          class="center"
          on:click={() => $editor.chain().focus().redo().run()}
          disabled={!$editor.can().chain().focus().redo().run()}
        >
          <i class="center">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              ><path
                fill="currentColor"
                d="M9.9 19q-2.425 0-4.163-1.575T4 13.5q0-2.35 1.738-3.925T9.9 8h6.3l-2.6-2.6L15 4l5 5l-5 5l-1.4-1.4l2.6-2.6H9.9q-1.575 0-2.738 1T6 13.5Q6 15 7.163 16T9.9 17H17v2z"
              /></svg
            >
          </i>
        </button>
      </div>

      <!-- <button type="button"
        on:click={() => $editor.chain().focus().toggleStrike().run()}
        disabled={!$editor.can().chain().focus().toggleStrike().run()}
        class={$editor.isActive("strike") ? "is-active" : ""}
      >
        strike
      </button>
      <button type="button"
        on:click={() => $editor.chain().focus().toggleCode().run()}
        disabled={!$editor.can().chain().focus().toggleCode().run()}
        class={$editor.isActive("code") ? "is-active" : ""}
      >
        code
      </button>
      <button type="button" on:click={() => $editor.chain().focus().unsetAllMarks().run()}>
        clear marks
      </button>
      <button type="button" on:click={() => $editor.chain().focus().clearNodes().run()}>
        clear nodes
      </button> -->

      <!-- <button type="button"
        on:click={() => $editor.chain().focus().toggleCodeBlock().run()}
        class={$editor.isActive("codeBlock") ? "is-active" : ""}
      >
        code block
      </button>
      <button type="button"
        on:click={() => $editor.chain().focus().toggleBlockquote().run()}
        class={$editor.isActive("blockquote") ? "is-active" : ""}
      >
        blockquote
      </button> -->
    </div>
  {/if}
</div>
