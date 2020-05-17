using System.Text.Json.Serialization;

namespace ApiFilmes.Models
{
    public class RespostaConsultaGenero
    {
        [JsonPropertyName("nomeGenero")]
        public string name { get; set; }
    }
}
