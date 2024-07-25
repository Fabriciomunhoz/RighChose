using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.IO;

namespace RightChose.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultarBancosController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListarBancos()
        {
            string pathFile = "./historicotaxajurosdiario_TodosCampos.json";

            string jsonString = System.IO.File.ReadAllText(pathFile);

            List<Banco> bancos = System.Text.Json.JsonSerializer.Deserialize<List<Banco>>(jsonString);

            foreach (var banco in bancos)
            {
                
            }



        }

        public class Banco
        {
            public int Posicao { get; set; }
            public string InstituicaoFinanceira { get; set; }
            public string TaxaJurosAoMes { get; set; }
            //public string InstituicaoFinanceira { get; set; }
            //public string commands { get; set; }
            //public string battery { get; set; }
        }
    }
}
