<script>
  export let size;
  export let state;
</script>

<svg
  xmlns="http://www.w3.org/2000/svg"
  width={size}
  height={size}
  viewBox="0 0 24 24"
>
  <g class="spinner-group{state ? 'noanimation' : ''}">
    {#if state}
      <path
        fill="none"
        stroke="currentColor"
        stroke-linecap="round"
        stroke-width="2"
        d="M8.124 5a8 8 0 1 0 7.752 0M12 4h.05"
      />
    {:else}
      <path
        fill="none"
        stroke="currentColor"
        stroke-linecap="round"
        stroke-linejoin="round"
        stroke-width="1.5"
        d="m2.75 8.75l3.5 3.5l7-7.5"
      />
    {/if}
  </g>
</svg>
