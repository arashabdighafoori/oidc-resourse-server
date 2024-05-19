<script>
  import Prep from "../global/prep.svelte";
</script>

<Prep css_file="pongee.min.css">
  <div class="pongee">
    <div class="content">
      <slot name="content" />
    </div>

    <div class="bottom">
      <slot name="bottom" />
    </div>
  </div>
</Prep>
