using folhaPagamento.Models;

namespace folhaPagamento.Service
{
    public class SalarioMensalService
    {
        public SalarioMensal CalcularSalarioMensal(DateTime dataAdmissao, double salarioBruto, int numeroDependentes, bool optanteValeTransporte, double percentualHoraExtra, double faltasEmHoras = 0, double horaExtra = 0)
        {
            DescontoService desconto = new DescontoService();
            SalarioMensal resultado = new SalarioMensal();
            VencimentoService vencimento = new VencimentoService();

            resultado.CalculoHoraExtra = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
            resultado.DescontoFaltasEmHoras = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);
            resultado.SalarioBase = vencimento.CalcularSaldoSalario(dataAdmissao, DateTime.Now, salarioBruto);
            resultado.SalarioBaseInss = resultado.SalarioBase + resultado.CalculoHoraExtra - resultado.DescontoFaltasEmHoras;
            resultado.DescontoINSS = desconto.CalcularINSS(resultado.SalarioBase);
            resultado.DeducaoDependente = vencimento.DeducaoDependentes(numeroDependentes);
            resultado.SalarioBaseIR = resultado.SalarioBase - resultado.DescontoINSS - resultado.DeducaoDependente;
            resultado.DescontoIR = desconto.CalcularIRRF(resultado.SalarioBaseIR);
            resultado.ValeTransporte = 0.0;
            resultado.Fgts = vencimento.CalcularFgts(resultado.SalarioBase);

            if (optanteValeTransporte)
            {
                resultado.ValeTransporte = desconto.CalcularValeTransporte(salarioBruto);
            }

            resultado.SalarioLiquido = resultado.SalarioBase - resultado.DescontoINSS - resultado.DescontoIR - resultado.ValeTransporte;

            return resultado;
        }
    }
}
