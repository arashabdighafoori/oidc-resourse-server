import { ClientJS } from "clientjs";
import { load } from "@fingerprintjs/botd";

export async function get_fingerprint() {
  const detector = await load();
  const isBot = detector.detect().bot;

  const client = new ClientJS();
  const fingerprint = client.getFingerprint();

  console.log(`${isBot ? "@" : "#"}${fingerprint}`);
  return `${isBot ? "@" : "#"}${fingerprint}`;
}
