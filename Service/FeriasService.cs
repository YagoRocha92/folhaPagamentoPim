using folhaPagamento.Models;

namespace folhaPagamento.Service
{
    public class FeriasService
    {
        public Ferias CalcularFeriasProporcionais(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto, int dependente)
        {
            Ferias ferias = new Ferias();
            DescontoService descontoService = new DescontoService();
            VencimentoService vencimentoService = new VencimentoService();

            if (dataCalculo < dataAdmissao)
            {
                throw new ArgumentException("A data de cálculo não pode ser anterior à data de admissão.");
            }

            int anosTrabalhados = dataCalculo.Year - dataAdmissao.Year;
            int mesesTrabalhados = (dataCalculo.Year - dataAdmissao.Year) * 12 + dataCalculo.Month - dataAdmissao.Month;
            int mesesProporcionais = mesesTrabalhados;

            // Se houver mais de um ano completo, subtraia os meses de anos completos.
            if (anosTrabalhados > 0)
            {
                mesesProporcionais -= anosTrabalhados * 12;
            }

            // Verifica se já se passaram mais de 14 dias desde a data de admissão
            if (dataCalculo.Day - dataAdmissao.Day >= 14)
            {
                mesesProporcionais++; // Adiciona 1 mês completo
            }


            double valorFeriasProporcionais = 0.0;

            if (mesesProporcionais > 0)
            {
                valorFeriasProporcionais = salarioBruto / 12 * mesesProporcionais;
            }

            ferias.ValorFeriasProporcionais = Math.Round(valorFeriasProporcionais, 2);
            ferias.MesesProporcionais = mesesProporcionais;
            ferias.UmTercoFerias = ferias.ValorFeriasProporcionais / 3;
            ferias.SalarioBaseInssFerias = ferias.ValorFeriasProporcionais + ferias.UmTercoFerias;
            ferias.DescontoInssFerias = descontoService.CalcularINSS(ferias.SalarioBaseInssFerias);
            ferias.SalarioBaseIrrfFerias = ferias.SalarioBaseInssFerias - ferias.DescontoInssFerias - vencimentoService.DeducaoDependentes(dependente);
            ferias.DescontoIrrfFerias = descontoService.CalcularIRRF(ferias.SalarioBaseIrrfFerias);
            ferias.Fgts = vencimentoService.CalcularFgts(ferias.SalarioBaseInssFerias);

            ferias.SaldoFeriasLiquido = ferias.SalarioBaseInssFerias - ferias.DescontoInssFerias - ferias.DescontoIrrfFerias;


            return ferias;
       
        }
    }
}
