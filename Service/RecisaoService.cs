using folhaPagamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace folhaPagamento.Service
{
    public class RecisaoService
    {
        private const int DiasPorMes = 30;
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
