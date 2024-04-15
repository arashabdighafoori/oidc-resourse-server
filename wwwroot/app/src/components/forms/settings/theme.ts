import { form, field } from "svelte-forms";
import { required } from "svelte-forms/validators";
import { store } from "../../../data/settings/theme";

export default function get_form() {
  const theme_field = field("Theme", "", [required()]);
  const _form = form(theme_field);

  store.subscribe((theme) => {
    if (theme) {
      theme_field.set(theme);
    }
  });

  return {
    theme: theme_field,
    form: _form,
  };
}
