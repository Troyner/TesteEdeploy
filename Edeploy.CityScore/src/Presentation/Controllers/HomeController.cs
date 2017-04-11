using Presentation.CidadeServico;
using Presentation.Models;
using Presentation.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        bool error = false;

        public ActionResult Index(PontuacaoCidadeModel model)
        {
            if (error)
            {
                ViewBag.Error = "Erro ao carregar cidades!";
            }
            else
            {
                ViewBag.Error = string.Empty;
            }

            error = false;

            if (model.Cidades == null)
            {
                model = new PontuacaoCidadeModel()
                {
                    NomeCidade = string.Empty,
                    Estado = string.Empty,
                    Cidades = new List<CidadeVO>()
                };
            }

            return View(model);
        }

        public ActionResult BuscarCidade(PontuacaoCidadeModel model)
        {
            try
            {
                var cidadeServicoClient = new CidadeServicoClient();
                var cidades = cidadeServicoClient.BuscaTodasCidades();

                var cidadesEncontradas = cidades.Where(x => x.Nome.ToUpper().Contains(
                                                         string.IsNullOrWhiteSpace(model.NomeCidade) ? "" : model.NomeCidade.ToUpper())
                                                         && x.Estado.ToUpper().Contains(
                                                         string.IsNullOrWhiteSpace(model.NomeCidade) ? "" : model.Estado.ToUpper()));

                model.Cidades = new List<CidadeVO>();

                foreach (var cidadeEncontrada in cidadesEncontradas)
                {
                    var pontuacao = cidadeServicoClient.BuscaPontos(cidadeEncontrada);

                    var cidadeVO = new CidadeVO()
                    {
                        Nome = cidadeEncontrada.Nome,
                        Estado = cidadeEncontrada.Estado,
                        Pontuacao = pontuacao
                    };

                    model.Cidades.Add(cidadeVO);
                }

                return RedirectToAction("Index", "Home", model);
            }
            catch (Exception exception)
            {
                error = true;
                return RedirectToAction("Index");
            }
        }
    }
}