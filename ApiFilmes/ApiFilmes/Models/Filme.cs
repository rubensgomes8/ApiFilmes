using System.Collections.Generic;

namespace ApiFilmes.Models
{
    public class Filme
    {
        public string NomeFilme { get; set; }
        public string DataLancamentoFilme { get; set; }
        public List<RespostaConsultaGenero> GenerosFilme { get; set; }
    }
}
