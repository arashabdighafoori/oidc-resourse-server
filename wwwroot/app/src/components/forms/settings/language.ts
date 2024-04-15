import { form, field } from "svelte-forms";
import { required } from "svelte-forms/validators";
import { store } from "../../../data/settings/culture";

export default function get_form() {
  const language = field("Culture", "", [required()]);
  const _form = form(language);

  store.subscribe((theme) => {
    if (theme) {
      language.set(theme[1]);
    }
  });

  return {
    language,
    form: _form,
  };
}
