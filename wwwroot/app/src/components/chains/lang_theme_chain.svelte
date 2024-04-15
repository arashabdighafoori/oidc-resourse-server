<script>
  import { _ } from "svelte-i18n";
  import Prep from "../global/prep.svelte";
  import { Link } from "svelte-routing";
</script>

<Prep pack="app" css_file="lang_theme_chain.min.css">
  <div id="langthemechain">
    <Link to="/settings/language" class="text-display-sm-shade">
      {$_("app.lang_theme.0")}
    </Link>

    {$_("app.lang_theme.1")}

    <Link to="/settings/theme" class="text-display-sm-shade">
      {$_("app.lang_theme.2")}
    </Link>
  </div>
</Prep>
