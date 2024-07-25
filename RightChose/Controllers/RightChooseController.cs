using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Cors;


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
        public async Task<IActionResult> Consulta(double valor, string formadepagamento, int? quantidadeParcelas)
        {

            if (quantidadeParcelas == null)
            {
                quantidadeParcelas = 12;
            }

            if (valor <= 0)
            {
                return BadRequest("O valor deve ser maior que zero");
            }

            List<object> parcelasDeJuros = new List<object>();


            double juros = 0;
            switch (formadepagamento.ToLower())
            {
                case "nubank":
                    juros = JurosNubank;
                    break;
                case "pix parcelado":
                    juros = JurosPixParceladoNubank;
                    break;
                case "juros pix picpay":
                    juros = JurosPixPicPay;
                    break;
                default:
                    return BadRequest("Forma de pagamento não reconhecida.");
                    break;
            }


            for (int parcela = 1; parcela <= quantidadeParcelas; parcela++)
            {

                double valorComJuros = valor + (valor * juros * parcela);
                double valorcomParcela = valorComJuros / parcela;



                parcelasDeJuros.Add(new
                {
                    Parcela = parcela,
                    ValorComJuros = Math.Round(valorComJuros, 2),
                    ValorDasParcelas = Math.Round(valorcomParcela, 2)
                });

            }



            return Ok(parcelasDeJuros);
        }





    }
}
