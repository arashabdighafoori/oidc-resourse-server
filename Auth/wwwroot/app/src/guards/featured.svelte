<script>
  import { FeatureFlag } from "svelte-feature-flag";
  import { store as features } from "../data/settings/features";

  export let feature;
</script>

<FeatureFlag on={feature} flags={$features}>
  <slot />
</FeatureFlag>
