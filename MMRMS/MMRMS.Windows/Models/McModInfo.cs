using Newtonsoft.Json;
using System.Collections.Generic;

namespace MMRMS.Windows.Models
{
    public class McModInfo
    {
        [JsonProperty("modid")]
        public string Modid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("mcversion")]
        public string Mcversion { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("updateUrl")]
        public string UpdateUrl { get; set; }

        [JsonProperty("authorList")]
        public IList<string> AuthorList { get; set; }

        [JsonProperty("credits")]
        public string Credits { get; set; }

        [JsonProperty("logoFile")]
        public string LogoFile { get; set; }

        [JsonProperty("screenshots")]
        public IList<string> Screenshots { get; set; }

        [JsonProperty("dependencies")]
        public IList<string> Dependencies { get; set; }
    }
}
