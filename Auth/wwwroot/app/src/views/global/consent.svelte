<script>
  import { _, dictionary, locale } from "svelte-i18n";
  import Pongee from "../../components/page/pongee.svelte";
  import Prep from "../../components/global/prep.svelte";
  import { store as consent } from "../../data/settings/consent";

  let culture = $locale;
  let texts = [];

  const on_pack_loaded = () => {
    dictionary.subscribe((dict) => {
      console.log(dict);
      if (!dict.hasOwnProperty(culture)) {
        culture = "en";
      }
      if (dict.hasOwnProperty(culture)) {
        texts = dict[culture]["consent"]["text"];
      }
    });
    return "";
  };

  const agree = () => {
    consent.set("true");
  };
</script>

<Prep css_file="consent.min.css" pack="consent">
  {on_pack_loaded()}
  <div id="consent">
    <Pongee>
      <div slot="content">
        <h1 class="consent_title">
          {$_("consent.title")}
        </h1>
        {#each texts as txt}
          {#if txt.t == "text"}
            <p>{txt.value}</p>
          {:else if (txt.t = "list")}
            <ul>
              {#each txt.value as item}
                <li>
                  {item}
                </li>
              {/each}
            </ul>
          {/if}
        {/each}
      </div>
      <div slot="bottom">
        <button class="btn" on:click={agree}>
          {$_("consent.buttons.agree")}
        </button>
      </div>
    </Pongee>
  </div>
</Prep>
