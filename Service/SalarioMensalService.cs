using folhaPagamento.Models;

namespace folhaPagamento.Service
{
    public class SalarioMensalService
    {
        public SalarioMensal CalcularSalarioMensal(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto, int numeroDependentes, bool optanteValeTransporte, double percentualHoraExtra, double valorValeTransporte, double faltasEmHoras = 0, double horaExtra = 0)
        {
            DescontoService descontoService = new DescontoService();
            VencimentoService vencimentoService = new VencimentoService();
            SalarioMensal resultado = new SalarioMensal();

            resultado.CalculoHoraExtra = vencimentoService.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
            resultado.DescontoFaltasEmHoras = descontoService.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);
            resultado.SalarioBase = vencimentoService.CalcularSalarioMes(dataAdmissao, dataCalculo, salarioBruto);
            resultado.SalarioBaseInss = resultado.SalarioBase + resultado.CalculoHoraExtra - resultado.DescontoFaltasEmHoras;
            resultado.DescontoINSS = descontoService.CalcularINSS(resultado.SalarioBase);
            resultado.DeducaoDependente = vencimentoService.DeducaoDependentes(numeroDependentes);
            resultado.SalarioBaseIR = resultado.SalarioBase - resultado.DescontoINSS - resultado.DeducaoDependente;
            resultado.DescontoIR = descontoService.CalcularIRRF(resultado.SalarioBaseIR);
            resultado.ValeTransporte = 0.0;
            resultado.Fgts = vencimentoService.CalcularFgts(resultado.SalarioBase);

            if (optanteValeTransporte)
            {
                resultado.ValeTransporte = descontoService.CalcularValeTransporte(valorValeTransporte, salarioBruto);
            }

            resultado.SalarioLiquido = resultado.SalarioBase - resultado.DescontoINSS - resultado.DescontoIR - resultado.ValeTransporte;

            return resultado;
        }
    }
}
