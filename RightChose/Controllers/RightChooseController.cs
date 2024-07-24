using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json;


namespace RightChose.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class RightChooseController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Consulta(double valor)
        {
            List<double> valoresComJuros = new List<double>();

            var porcentagemNubank = 0.0526;

            for (int parcelas = 1; parcelas <= 12; parcelas++)
            {
                valor = (valor * porcentagemNubank) + valor;
                valoresComJuros.Add(Math.Round(valor, 2));
            }
            return Ok(valoresComJuros);
        }
    }
}
