using folhaPagamento.Models;

namespace folhaPagamento.Service
{
    public class RecisaoService
    {
        private const int DiasPorMes = 30;
        public Rescisao CalcularRescisao(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto, double horaExtra, double percentualHoraExtra, MotivoRescisao motivoRescisao, int dependentes, bool cumprirAviso, double faltasEmHoras)
        {
            DescontoService descontoService = new DescontoService();
            VencimentoService vencimentoService = new VencimentoService();
            FeriasService feriasService = new FeriasService();
            DecimoTerceiroService decimoTerceiroService = new DecimoTerceiroService();

            Rescisao resultado = new Rescisao();

            switch (motivoRescisao)
            {
                case MotivoRescisao.DespedidaSemJustaCausa:
                    resultado.SaldoSalarioRescisao = vencimentoService.CalcularSaldoSalario(dataAdmissao, dataCalculo, salarioBruto);
                    resultado.DecimoTerceiroRescisao = decimoTerceiroService.CalcularDecimoTerceiro(dataAdmissao, dataCalculo, salarioBruto, dependentes);
                    resultado.Ferias = feriasService.CalcularFeriasProporcionais(dataAdmissao, dataCalculo, salarioBruto, dependentes);

                    if (cumprirAviso)
                    {
                        resultado.AvisoPrevioRescisao = CalcularAvisoPrevio(dataAdmissao, dataCalculo, salarioBruto);
                    }
                    else
                    {
                        resultado.AvisoPrevioRescisao = -salarioBruto;
                    }

                    resultado.HoraExtraRescisao = vencimentoService.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = descontoService.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);

                    resultado.SalarioBaseInssRescisão = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao.ValorDecimoTerceiro +
                        resultado.Ferias.ValorFeriasProporcionais    + resultado.Ferias.UmTercoFerias + resultado.AvisoPrevioRescisao + resultado.HoraExtraRescisao - resultado.DescontoFaltas;

                    resultado.DescontoInssRescisao = descontoService.CalcularINSS(resultado.SalarioBaseInssRescisão);

                    resultado.SalarioBaseIrrfRescisao = resultado.SalarioBaseInssRescisão - resultado.DescontoInssRescisao - vencimentoService.DeducaoDependentes(dependentes);

                    resultado.DescontoIrrfRescisao = descontoService.CalcularIRRF(resultado.SalarioBaseIrrfRescisao);

                    resultado.Fgts = vencimento.CalcularFgts(resultado.SalarioBaseInssRescisão);

                    resultado.SaldoRescisaoLiquido = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao.ValorDecimoTerceiro + resultado.Ferias.ValorFeriasProporcionais + resultado.Ferias.UmTercoFerias 
                        + resultado.AvisoPrevioRescisao + resultado.HoraExtraRescisao - resultado.DescontoFaltas - resultado.DescontoInssRescisao - resultado.DescontoIrrfRescisao;
                    break;

                case MotivoRescisao.DespedidaPorJustaCausa:
                    resultado.SaldoSalarioRescisao = vencimentoService.CalcularSaldoSalario(dataAdmissao, dataCalculo, salarioBruto);
                    resultado.Ferias = feriasService.CalcularFeriasProporcionais(dataAdmissao, dataCalculo, salarioBruto, dependentes);
                    resultado.HoraExtraRescisao = vencimentoService.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = descontoService.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);

                    resultado.SalarioBaseInssRescisão = resultado.SaldoSalarioRescisao + resultado.Ferias.ValorFeriasProporcionais + 
                        resultado.Ferias.UmTercoFerias + resultado.HoraExtraRescisao - resultado.DescontoFaltas;

                    resultado.DescontoInssRescisao = descontoService.CalcularINSS(resultado.SalarioBaseInssRescisão);

                    resultado.SalarioBaseIrrfRescisao = resultado.SalarioBaseInssRescisão - resultado.DescontoInssRescisao - vencimentoService.DeducaoDependentes(dependentes);

                    resultado.DescontoIrrfRescisao = descontoService.CalcularIRRF(resultado.SalarioBaseIrrfRescisao);

                    resultado.Fgts = vencimentoService.CalcularFgts(resultado.SalarioBaseInssRescisão);

                    resultado.SaldoRescisaoLiquido = resultado.SaldoSalarioRescisao + resultado.Ferias.ValorFeriasProporcionais + resultado.Ferias.UmTercoFerias
                        + resultado.HoraExtraRescisao - resultado.DescontoFaltas - resultado.DescontoInssRescisao - resultado.DescontoIrrfRescisao;
                    break;

                case MotivoRescisao.PedidoDeDemissao:
                    resultado.SaldoSalarioRescisao = vencimentoService.CalcularSaldoSalario(dataAdmissao, dataCalculo, salarioBruto);
                    resultado.DecimoTerceiroRescisao = decimoTerceiroService.CalcularDecimoTerceiro(dataAdmissao, dataCalculo, salarioBruto, dependentes);
                    resultado.Ferias = feriasService.CalcularFeriasProporcionais(dataAdmissao, dataCalculo, salarioBruto, dependentes);
                    resultado.HoraExtraRescisao = vencimentoService.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = descontoService.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);


                    if (cumprirAviso)
                    {
                        resultado.AvisoPrevioRescisao = CalcularAvisoPrevio(dataAdmissao, dataCalculo, salarioBruto);
                    }
                    else
                    {
                        resultado.AvisoPrevioRescisao = -salarioBruto;
                    }

                    resultado.SalarioBaseInssRescisão = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao.ValorDecimoTerceiro +
                        resultado.Ferias.ValorFeriasProporcionais + resultado.Ferias.UmTercoFerias + resultado.AvisoPrevioRescisao + resultado.HoraExtraRescisao - resultado.DescontoFaltas;

                    resultado.DescontoInssRescisao = descontoService.CalcularINSS(resultado.SalarioBaseInssRescisão);

                    resultado.SalarioBaseIrrfRescisao = resultado.SalarioBaseInssRescisão - resultado.DescontoInssRescisao - vencimentoService.DeducaoDependentes(dependentes);

                    resultado.DescontoIrrfRescisao = descontoService.CalcularIRRF(resultado.SalarioBaseIrrfRescisao);
                    
                    resultado.Fgts = vencimentoService.CalcularFgts(resultado.SalarioBaseInssRescisão);

                    resultado.SaldoRescisaoLiquido = resultado.SaldoSalarioRescisao + resultado.DecimoTerceiroRescisao.ValorDecimoTerceiro + resultado.Ferias.ValorFeriasProporcionais
                      + resultado.Ferias.UmTercoFerias + resultado.AvisoPrevioRescisao + resultado.HoraExtraRescisao - resultado.DescontoFaltas - resultado.DescontoInssRescisao - resultado.DescontoIrrfRescisao;
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
