using folhaPagamento.Models;

namespace folhaPagamento.Service
{
    public class RecisaoService
    {
        private const int DiasPorMes = 30;
        public Rescisao CalcularRescisao(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto, double horaExtra, double percentualHoraExtra, MotivoRescisao motivoRescisao, int dependentes, bool cumprirAviso, double faltasEmHoras)
        {
            DescontoService desconto = new DescontoService();
            VencimentoService vencimento = new VencimentoService();
            FeriasService feriasService = new FeriasService();
            Rescisao resultado = new Rescisao();

            switch (motivoRescisao)
            {
                case MotivoRescisao.DespedidaSemJustaCausa:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalario(dataAdmissao, dataCalculo, salarioBruto);
                    resultado.DecimoTerceiroRescisao = vencimento.CalcularDecimoTerceiroRescisao(dataAdmissao, dataCalculo, salarioBruto);
                    resultado.Ferias = feriasService.CalcularFeriasProporcionais(dataAdmissao, dataCalculo, salarioBruto, dependentes);

                    if (cumprirAviso)
                    {
                        resultado.AvisoPrevioRescisao = CalcularAvisoPrevio(dataAdmissao, dataCalculo, salarioBruto);
                    }
                    else
                    {
                        resultado.AvisoPrevioRescisao = -salarioBruto;
                    }

                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);

                    resultado.SalarioBaseInssRescisão = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao +
                        resultado.FeriasRescisao + resultado.AvisoPrevioRescisao + resultado.HoraExtraRescisao - resultado.DescontoFaltas;

                    resultado.DescontoInssRescisao = desconto.CalcularINSS(resultado.SalarioBaseInssRescisão);

                    resultado.SalarioBaseIrrfRescisao = resultado.SalarioBaseInssRescisão - resultado.DescontoInssRescisao - vencimento.DeducaoDependentes(dependentes);

                    resultado.DescontoIrrfRescisao = desconto.CalcularIRRF(resultado.SalarioBaseIrrfRescisao);

                    resultado.Fgts = vencimento.CalcularFgts(resultado.SalarioBaseInssRescisão);

                    resultado.SaldoRescisaoLiquido = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao + resultado.FeriasRescisao + resultado.AvisoPrevioRescisao +
                        resultado.HoraExtraRescisao - resultado.DescontoFaltas - resultado.DescontoInssRescisao - resultado.DescontoIrrfRescisao;
                    break;

                case MotivoRescisao.DespedidaPorJustaCausa:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalario(dataAdmissao, dataCalculo, salarioBruto);
                    resultado.Ferias = feriasService.CalcularFeriasProporcionais(dataAdmissao, dataCalculo, salarioBruto, dependentes);
                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);

                    resultado.SalarioBaseInssRescisão = resultado.SaldoSalarioRescisao + resultado.FeriasRescisao +
                        resultado.HoraExtraRescisao - resultado.DescontoFaltas;

                    resultado.DescontoInssRescisao = desconto.CalcularINSS(resultado.SalarioBaseInssRescisão);

                    resultado.SalarioBaseIrrfRescisao = resultado.SalarioBaseInssRescisão - resultado.DescontoInssRescisao - vencimento.DeducaoDependentes(dependentes);

                    resultado.DescontoIrrfRescisao = desconto.CalcularIRRF(resultado.SalarioBaseIrrfRescisao);

                    resultado.Fgts = vencimento.CalcularFgts(resultado.SalarioBaseInssRescisão);

                    resultado.SaldoRescisaoLiquido = resultado.SaldoSalarioRescisao + resultado.FeriasRescisao + resultado.HoraExtraRescisao - resultado.DescontoFaltas - 
                        resultado.DescontoInssRescisao - resultado.DescontoIrrfRescisao;
                    break;

                case MotivoRescisao.PedidoDeDemissao:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalario(dataAdmissao, dataCalculo, salarioBruto);
                    resultado.DecimoTerceiroRescisao = vencimento.CalcularDecimoTerceiroRescisao(dataAdmissao, dataCalculo, salarioBruto);
                    resultado.Ferias = feriasService.CalcularFeriasProporcionais(dataAdmissao, dataCalculo, salarioBruto, dependentes);
                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);


                    if (cumprirAviso)
                    {
                        resultado.AvisoPrevioRescisao = CalcularAvisoPrevio(dataAdmissao, dataCalculo, salarioBruto);
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
                    
                    resultado.Fgts = vencimento.CalcularFgts(resultado.SalarioBaseInssRescisão);

                    resultado.SaldoRescisaoLiquido = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao + resultado.FeriasRescisao + resultado.AvisoPrevioRescisao +
                        resultado.HoraExtraRescisao - resultado.DescontoFaltas - resultado.DescontoInssRescisao - resultado.DescontoIrrfRescisao;
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
