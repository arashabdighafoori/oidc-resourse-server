import { form, field } from "svelte-forms";
import { required, between } from "svelte-forms/validators";
import { get_fingerprint } from "../../../services/finger";

export default function get_form(data) {
  const redirectUri = field("RedirectUri", data.redirectUri.trim(), []);
  const code = field("Code", data.code.trim(), []);
  const nonce = field("Nonce", data.nonce.trim(), []);
  const scopes = data.requestedScopes.map((e, i) => {
    return field(`RequestedScopes[${i}]`, e.trim(), []);
  });
  const email = field("username", data.userName, [required(), between(5, 256)]);
  const password = field("password", "", [required()]);
  const fingerprint = field("fingerprint", get_fingerprint(), [required()]);
  const _form = form(
    redirectUri,
    code,
    nonce,
    email,
    password,
    fingerprint,
    ...scopes
  );
  return {
    redirectUri,
    code,
    nonce,
    email,
    password,
    fingerprint,
    form: _form,
    ...scopes,
  };
}
