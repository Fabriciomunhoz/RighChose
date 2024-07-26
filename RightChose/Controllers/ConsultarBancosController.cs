using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using RightChose.Models;

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

            List<Banco>? bancos = System.Text.Json.JsonSerializer.Deserialize<List<Banco>>(jsonString);
            var bancosViewModel = new List<Banco>();

            foreach (var banco in bancos)
            {
                bancosViewModel.Add(new Banco()
                {
                    Posicao = banco.Posicao,
                    InstituicaoFinanceira = banco.InstituicaoFinanceira,
                    TaxaJurosAoMes = banco.TaxaJurosAoMes + "%"

                });


                var instituicaoFinanceira = banco.InstituicaoFinanceira;
                var jurosaoMes = Convert.ToDouble(banco.TaxaJurosAoMes) / 100;



                Console.WriteLine(Math.Round(jurosaoMes, 2));



            }

            return Ok(bancosViewModel);



        }

    }
}
