import { form, field } from "svelte-forms";
import { required, between } from "svelte-forms/validators";
import { get_fingerprint } from "../../../services/finger";

export default async function get_form(data) {
  const redirectUri = field(
    "RedirectUri",
    data.redirectUri?.trim() ?? undefined,
    []
  );
  const code = field("Code", data.code?.trim() ?? undefined, []);
  const nonce = field("Nonce", data.nonce?.trim() ?? undefined, []);
  const scopes = field(
    "RequestedScopes",
    data.requestedScopes?.map((e) => e.trim()) ?? undefined,
    []
  );
  const email = field("UserName", data.userName, [required(), between(5, 256)]);
  const password = field("Password", "", [required()]);
  const fingerprint = field("Fingerprint", await get_fingerprint(), [
    required(),
  ]);
  const _form = form(
    redirectUri,
    code,
    nonce,
    email,
    password,
    fingerprint,
    scopes
  );
  return {
    redirectUri,
    code,
    nonce,
    email,
    password,
    fingerprint,
    form: _form,
    scopes,
  };
}
