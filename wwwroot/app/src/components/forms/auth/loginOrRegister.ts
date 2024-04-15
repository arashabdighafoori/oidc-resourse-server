import { form, field } from "svelte-forms";
import { required, between } from "svelte-forms/validators";
import { get_fingerprint } from "../../../services/finger";

export default function get_form() {
  const email = field("username", "", [required(), between(5, 256)]);
  const fingerprint = field("fingerprint", get_fingerprint(), [required()]);
  const _form = form(email, fingerprint);
  return {
    email,
    fingerprint,
    form: _form,
  };
}
