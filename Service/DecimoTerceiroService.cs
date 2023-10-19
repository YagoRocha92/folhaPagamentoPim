using folhaPagamento.Models;

namespace folhaPagamento.Service
{
    public class DecimoTerceiroService
    {
        public DecimoTerceiro CalcularDecimoTerceiro(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto, int dependente)
        {
            DescontoService descontoService = new DescontoService();
            VencimentoService vencimentoService = new VencimentoService();
            DecimoTerceiro decimoTerceiro = new DecimoTerceiro();

            int anoAdmissao = dataAdmissao.Year;
            int anoCalculo = dataCalculo.Year;

            decimoTerceiro.MesesTrabalhados = 0;

            for (int ano = anoAdmissao; ano <= anoCalculo; ano++)
            {
                int mesInicio = (ano == anoAdmissao) ? dataAdmissao.Month : 1;
                int mesFim = (ano == anoCalculo) ? dataCalculo.Month : 12;

                int mesesNoAnoAtual = mesFim - mesInicio + 1;
                decimoTerceiro.MesesTrabalhados += mesesNoAnoAtual;

                if (ano < anoCalculo)
                {
                    decimoTerceiro.MesesTrabalhados = 0;
                }
            }

            decimoTerceiro.ValorDecimoTerceiro = (salarioBruto / 12) * decimoTerceiro.MesesTrabalhados;
            decimoTerceiro.SalarioBaseInssDecimoTerceiro = decimoTerceiro.ValorDecimoTerceiro;
            decimoTerceiro.DescontoInssDecimoTerceiro = descontoService.CalcularINSS(decimoTerceiro.SalarioBaseInssDecimoTerceiro);

            decimoTerceiro.SalarioBaseIrrfDecimoTerceiro = decimoTerceiro.ValorDecimoTerceiro - decimoTerceiro.DescontoInssDecimoTerceiro - vencimentoService.DeducaoDependentes(dependente);
            decimoTerceiro.DescontoIrrfDecimoTerceiro = descontoService.CalcularIRRF(decimoTerceiro.SalarioBaseIrrfDecimoTerceiro);
            decimoTerceiro.SaldoDecimoTerceiroLiquido = decimoTerceiro.ValorDecimoTerceiro - decimoTerceiro.DescontoInssDecimoTerceiro - decimoTerceiro.DescontoIrrfDecimoTerceiro;
            decimoTerceiro.Fgts = vencimentoService.CalcularFgts(decimoTerceiro.ValorDecimoTerceiro);
            return decimoTerceiro;
        }
    }
}

