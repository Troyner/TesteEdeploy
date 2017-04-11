using Presentation.VO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class PontuacaoCidadeModel
    {
        [Required]
        public string NomeCidade { get; set; }
        [Required]
        public string Estado { get; set; }

        public IList<CidadeVO> Cidades { get; set; }
    }
}