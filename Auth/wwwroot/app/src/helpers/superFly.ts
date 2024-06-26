import { quintOut } from "svelte/easing";

export function superFly(
  node,
  { delay, duration, x = 0, y = 0, motion = quintOut, none = false }
) {
  const style = getComputedStyle(node);
  const transform = style.transform === "none" ? "" : style.transform;

  return {
    delay,
    duration,
    css: (t) => {
      const easeMotion = motion(t);
      if (none) return ``;
      return `
        transform: ${transform} translate(${(1 - easeMotion) * x}px, ${
        (1 - easeMotion) * y
      }px)`;
    },
  };
}
