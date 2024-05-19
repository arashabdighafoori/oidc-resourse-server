import { form, field } from "svelte-forms";
import {
  required,
  between,
  pattern,
  email as emailValidator,
} from "svelte-forms/validators";
import { get_fingerprint } from "../../../services/finger";

export default async function get_form(data) {
  const redirectUri = field("RedirectUri", data.redirectUri?.trim(), []);
  const code = field("Code", data.code?.trim(), []);
  const nonce = field("Nonce", data.nonce?.trim(), []);
  const scopes = field(
    "RequestedScopes",
    data.requestedScopes?.map((e) => e.trim()),
    []
  );

  const email = field("Email", data.userName.trim(), [
    required(),
    between(3, 256),
    emailValidator(),
  ]);
  const friendlyname = field("Friendlyname", "", [required(), between(3, 56)]);
  const password = field("Password", "", [
    required(),
    pattern(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,64}$/
    ),
  ]);
  const fingerprint = field("Fingerprint", await get_fingerprint(), [
    required(),
  ]);
  const _form = form(
    redirectUri,
    code,
    nonce,
    email,
    friendlyname,
    password,
    fingerprint,
    scopes
  );
  return {
    redirectUri,
    code,
    nonce,
    email,
    friendlyname,
    password,
    fingerprint,
    form: _form,
    scopes,
  };
}
