<script>
  import Prep from "../global/prep.svelte";
</script>

<Prep css_file="pongee.min.css">
  <div class="pongee" style="width:100%;height:100%">
    <div class="content">
      <slot name="content" />
    </div>

    <div class="bottom">
      <slot name="bottom" />
    </div>
  </div>
</Prep>
