using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using ApiFilmes.Models;
using System.Linq;

namespace ApiFilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        [HttpGet]
        public List<Filme> ListarFilmes()
        {
            string chaveApi = "c5f3d60c9ab8d082b1ce2d30aff8ae40";
            string dataAtual = DateTime.Parse(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd");            
            string respostaApi = ConsultarApiTheMovieDb("https://api.themoviedb.org/3/discover/movie?primary_release_date.gte=" + dataAtual + "&api_key=" + chaveApi + "&language=pt-BR");
            RespostaConsultaFilme respostaConsultaFilmes = JsonConvert.DeserializeObject<RespostaConsultaFilme>(respostaApi);
            List<Filme> filmes = new List<Filme>();

            for (int i = 1; i <= 2; i++)
            //for (int i = 1; i <= respostaConsultaFilmes.total_pages; i++)
            {
                respostaApi = ConsultarApiTheMovieDb("https://api.themoviedb.org/3/discover/movie?primary_release_date.gte=" + dataAtual + "&api_key=" + chaveApi + "&language=pt-BR&page=" + i);
                respostaConsultaFilmes = JsonConvert.DeserializeObject<RespostaConsultaFilme>(respostaApi);

                foreach (RetornoFilme retornoFilme in respostaConsultaFilmes.results)
                {
                    Filme filme = new Filme
                    {
                        NomeFilme = retornoFilme.original_title,
                        DataLancamentoFilme = DateTime.Parse(retornoFilme.release_date).ToString("dd-MM-yyyy")
                    };
                    List<RespostaConsultaGenero> respostaConsultaGeneros = new List<RespostaConsultaGenero>();

                    foreach (var idGenero in retornoFilme.genre_ids)
                    {
                        respostaApi = ConsultarApiTheMovieDb("https://api.themoviedb.org/3/genre/" + idGenero + "?api_key=" + chaveApi + "&language=pt-BR");
                        RespostaConsultaGenero respostaConsultaGenero = JsonConvert.DeserializeObject<RespostaConsultaGenero>(respostaApi);
                        respostaConsultaGeneros.Add(respostaConsultaGenero);
                    }

                    filme.GenerosFilme = respostaConsultaGeneros;
                    filmes.Add(filme);
                }
            }

            return filmes.OrderBy(f => DateTime.Parse(f.DataLancamentoFilme)).ToList();
        }

        public string ConsultarApiTheMovieDb(string url)
        {
            HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            string respostaApi = "";

            using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                respostaApi = reader.ReadToEnd();
            }

            return respostaApi;
        }
    }
}