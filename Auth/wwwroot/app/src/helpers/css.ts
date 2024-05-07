var queueStyles = {};
export function cookCss(style: string, callback: any) {
  // callback = callback || (() => {});
  if (
    !queueStyles.hasOwnProperty(style) &&
    !document.querySelector(`[href="/app/public/css/${style}"]`)
  ) {
    queueStyles[style] = [callback];
    const url = `/app/public/css/${style}`;
    // document.head.innerHTML += `<link rel="stylesheet" href="/app/public/css/${style}" />`;
    const link = document.createElement("link");
    link.type = "text/css";
    link.rel = "stylesheet";
    link.href = url;
    link.addEventListener(
      "load",
      () => {
        queueStyles[style].forEach((c: any) => {
          c();
        });
        delete queueStyles[style];
      },
      false
    );
    document.head.appendChild(link);
  } else if (queueStyles.hasOwnProperty(style)) {
    queueStyles[style].push(callback);
  } else {
    callback();
  }
}
