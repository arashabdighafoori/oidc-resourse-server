import { form, field } from "svelte-forms";
import {
  required,
  between,
  pattern,
  email as emailValidator,
} from "svelte-forms/validators";
import { get_fingerprint } from "../../../services/finger";

export default function get_form(passedData) {
  const email = field("email", passedData.trim(), [
    required(),
    between(3, 256),
    emailValidator(),
  ]);
  const friendlyname = field("friendlyname", "", [required(), between(3, 56)]);
  const password = field("password", "", [
    required(),
    pattern(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,64}$/
    ),
  ]);
  const fingerprint = field("fingerprint", get_fingerprint(), [required()]);
  const _form = form(email, friendlyname, password, fingerprint);
  return {
    email,
    friendlyname,
    password,
    fingerprint,
    form: _form,
  };
}
