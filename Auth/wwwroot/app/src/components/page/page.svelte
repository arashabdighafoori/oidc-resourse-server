<script>
  import { superFly } from "../../helpers/superFly";
  let speed = 700;
</script>

<div class="page" style="width:100%;height:100%">
  <div
    in:superFly={{ x: -400, duration: speed, delay: 100 }}
    out:superFly={{ delay: speed, duration: 0, none: true }}
  >
    <slot />
  </div>
</div>
