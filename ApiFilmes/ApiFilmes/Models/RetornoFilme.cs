using System.Collections.Generic;

namespace ApiFilmes.Models
{
    public class RetornoFilme
    {
        public string release_date { get; set; }
        public string original_title { get; set; }
        public List<object> genre_ids { get; set; }
    }
}
