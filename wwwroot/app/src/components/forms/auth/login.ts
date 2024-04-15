import { form, field } from "svelte-forms";
import { required, between } from "svelte-forms/validators";
import { get_fingerprint } from "../../../services/finger";

export default function get_form(passedData: string) {
  const email = field("username", passedData, [required(), between(5, 256)]);
  const password = field("password", "", [required()]);
  const fingerprint = field("fingerprint", get_fingerprint(), [required()]);
  const _form = form(email, password, fingerprint);
  return {
    email,
    password,
    fingerprint,
    form: _form,
  };
}
