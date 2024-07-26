using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Cors;
using RightChose.Models;


namespace RightChose.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class RightChooseController : ControllerBase
    {
        private const double JurosNubank = 0.0526;
        private const double JurosPixParceladoNubank = 0.0199 + 0.0438;
        private const double JurosPixPicPay = 0.0599;

        [HttpGet]
        public async Task<IActionResult> Consulta(double valor, string formadepagamento, int quantidadeParcelas)
        {



            string? pathFile = "./historicotaxajurosdiario_TodosCampos.json";
            string? jsonString = await System.IO.File.ReadAllTextAsync(pathFile);

            List<Banco>? bancos = System.Text.Json.JsonSerializer.Deserialize<List<Banco>>(jsonString);

            Banco? banco = bancos?.FirstOrDefault(b => Convert.ToString(b.InstituicaoFinanceira) == formadepagamento);



            if (quantidadeParcelas == null)
            {
                quantidadeParcelas = 12;
            }

            if (valor <= 0)
            {
                return BadRequest("O valor deve ser maior que zero");
            }

            List<object>? parcelasDeJuros = new List<object>();


            for (int parcela = 1; parcela <= quantidadeParcelas; parcela++)
            {

                

                double valorComJuros = valor + (valor * (Convert.ToDouble(banco?.TaxaJurosAoMes?.Replace(",", "."))/100) * parcela);
                double valorcomParcela = valorComJuros / parcela;

                double valorDoJuros = valorComJuros - valor;

                parcelasDeJuros.Add(new
                {
                    Parcela = parcela,
                    ValorComJuros = Math.Round(valorComJuros, 2),
                    ValorDasParcelas = Math.Round(valorcomParcela, 2),
                    ValorDosJuros = Math.Round(valorDoJuros, 2)
                });

            }



            return Ok(parcelasDeJuros);
        } 
    }
}

