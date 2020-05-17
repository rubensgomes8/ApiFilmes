using System.Collections.Generic;

namespace ApiFilmes.Models
{
    public class RespostaConsultaFilme
    {
        public int page { get; set; }
        public List<RetornoFilme> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }
}
