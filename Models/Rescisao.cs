using folhaPagamento.Service;
using System.Security.Cryptography;

namespace folhaPagamento.Models
{
    public enum MotivoRescisao
    {
        DespedidaSemJustaCausa,
        DespedidaPorJustaCausa,
        PedidoDeDemissao,

    }
    public class Rescisao
    {
        private const int DiasPorMes = 30;
        public double SaldoSalarioRescisao { get; set; }
        public double DecimoTerceiroRescisao { get; set; }
        public double FeriasRescisao { get; set; }
        public int MesesFerias { get; set; }
        public double AvisoPrevioRescisao { get; set; }
        public double HoraExtraRescisao { get; set; }
        public double DescontoFaltas { get; set; }
        public double Fgts { get; set; }
        public double DescontoInssRescisao { get; set; }
        public double SalarioBaseInssRescisão { get; set; }
        public double SalarioBaseIrrfRescisao { get; set; }
        public double DescontoIrrfRescisao { get; set; }


        public Rescisao CalcularRescisao(DateTime dataAdmissao, DateTime dataDemissao, double salarioBruto, double horaExtra, double percentualHoraExtra, MotivoRescisao motivoRescisao, int dependentes, bool cumprirAviso, double faltasEmHoras)
        {
            DescontoService desconto = new DescontoService();
            VencimentoService vencimento = new VencimentoService();
            Rescisao resultado = new Rescisao();

            switch (motivoRescisao)
            {
                case MotivoRescisao.DespedidaSemJustaCausa:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalarioRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.DecimoTerceiroRescisao = vencimento.CalcularDecimoTerceiroRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    (resultado.FeriasRescisao, resultado.MesesFerias) = vencimento.CalcularFeriasProporcionais(dataAdmissao, dataDemissao, salarioBruto);

                    if (cumprirAviso)
                    {
                        resultado.AvisoPrevioRescisao = CalcularAvisoPrevio(dataAdmissao, dataDemissao, salarioBruto);
                    }
                    else
                    {
                        resultado.AvisoPrevioRescisao = -salarioBruto;
                    }

                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.Fgts = vencimento.CalcularFgts(salarioBruto);
                    resultado.DescontoFaltas = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);

                    resultado.SalarioBaseInssRescisão = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao +
                        resultado.FeriasRescisao + resultado.AvisoPrevioRescisao + resultado.HoraExtraRescisao - resultado.DescontoFaltas;

                    resultado.DescontoInssRescisao = desconto.CalcularINSS(resultado.SalarioBaseInssRescisão);

                    resultado.SalarioBaseIrrfRescisao = resultado.SalarioBaseInssRescisão - resultado.DescontoInssRescisao - vencimento.DeducaoDependentes(dependentes);

                    resultado.DescontoIrrfRescisao = desconto.CalcularIRRF(resultado.SalarioBaseIrrfRescisao);
                    break;

                case MotivoRescisao.DespedidaPorJustaCausa:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalarioRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    (resultado.FeriasRescisao, resultado.MesesFerias) = vencimento.CalcularFeriasProporcionais(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);

                    resultado.SalarioBaseInssRescisão = resultado.SaldoSalarioRescisao + resultado.FeriasRescisao +
                        resultado.HoraExtraRescisao - resultado.DescontoFaltas;

                    resultado.DescontoInssRescisao = desconto.CalcularINSS(resultado.SalarioBaseInssRescisão);

                    resultado.SalarioBaseIrrfRescisao = resultado.SalarioBaseInssRescisão - resultado.DescontoInssRescisao - vencimento.DeducaoDependentes(dependentes);

                    resultado.DescontoIrrfRescisao = desconto.CalcularIRRF(resultado.SalarioBaseIrrfRescisao);

                    resultado.Fgts = vencimento.CalcularFgts(salarioBruto);
                    break;

                case MotivoRescisao.PedidoDeDemissao:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalarioRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.DecimoTerceiroRescisao = vencimento.CalcularDecimoTerceiroRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    (resultado.FeriasRescisao, resultado.MesesFerias) = vencimento.CalcularFeriasProporcionais(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);


                    if (cumprirAviso)
                    {
                        resultado.AvisoPrevioRescisao = CalcularAvisoPrevio(dataAdmissao, dataDemissao, salarioBruto);
                    }
                    else
                    {
                        resultado.AvisoPrevioRescisao = -salarioBruto;
                    }

                    resultado.SalarioBaseInssRescisão = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao +
                        resultado.FeriasRescisao + resultado.AvisoPrevioRescisao + resultado.HoraExtraRescisao - resultado.DescontoFaltas;

                    resultado.DescontoInssRescisao = desconto.CalcularINSS(resultado.SalarioBaseInssRescisão);

                    resultado.SalarioBaseIrrfRescisao = resultado.SalarioBaseInssRescisão - resultado.DescontoInssRescisao - vencimento.DeducaoDependentes(dependentes);

                    resultado.DescontoIrrfRescisao = desconto.CalcularIRRF(resultado.SalarioBaseIrrfRescisao);
                    resultado.Fgts = vencimento.CalcularFgts(salarioBruto);
                    break;
            }

            return resultado;
        }
        public double CalcularAvisoPrevio(DateTime dataAdmissao, DateTime dataDemissao, double ultimoSalario)
        {
            int anosTrabalhados = dataDemissao.Year - dataAdmissao.Year;
            int diasAvisoPrevio = DiasPorMes + anosTrabalhados * 3;
            return ultimoSalario / DiasPorMes * diasAvisoPrevio;
        }

    }
}
