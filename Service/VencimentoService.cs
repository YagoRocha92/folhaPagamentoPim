namespace folhaPagamento.Service
{
    public class VencimentoService
    {
        public double DeducaoDependentes(int dependentes)
        {
            double deducao = 189.59;
            return dependentes * deducao;
        }
        public double CalcularHoraExtra(double horaExtra, double salarioBruto, double percentualHoraExtra)
        {
            if (horaExtra <= 0)
            {
                throw new ArgumentException("O valor de faltasEmHoras deve ser maior que zero.");
            }

            double horaExtraCalculada = salarioBruto / 220 * percentualHoraExtra;
            double totalHoraExtra = horaExtraCalculada * horaExtra;
            return totalHoraExtra;
        }
        public double CalcularSaldoSalario(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto)
        {
            int diasTrabalhados;

            if (dataAdmissao.Month == dataCalculo.Month && dataAdmissao.Year == dataCalculo.Year)
            {
                diasTrabalhados = dataCalculo.Day - dataAdmissao.Day;
            }
            else
            {
                diasTrabalhados = dataCalculo.Day;
            }
            double valorProporcional = (salarioBruto / 30) * diasTrabalhados;
            return valorProporcional;
        }
        public double CalcularDecimoTerceiro(double salarioBruto, DateTime dataAdmissao)
        {
            DateTime dataAtualDoCalculo = new DateTime(DateTime.Now.Year, 12, 31);

            if (dataAdmissao <= dataAtualDoCalculo)
            {
                int mesesTrabalhados = (dataAtualDoCalculo.Year - dataAdmissao.Year) * 12 + dataAtualDoCalculo.Month - dataAdmissao.Month;

                if (mesesTrabalhados > 0)
                {
                    double salarioDecimoTerceiro = salarioBruto * (mesesTrabalhados / 12.0);
                    return salarioDecimoTerceiro;
                }
                else
                {
                    throw new ArgumentException("O funcionário não trabalhou tempo suficiente para receber o 13º salário.");
                }
            }
            else
            {
                throw new ArgumentException("A data de admissão não pode estar no futuro.");
            }

        }
        public double CalcularDecimoTerceiroRescisao(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto)
        {

            if (dataAdmissao.Year == dataCalculo.Year)
            {
                // Cálculo proporcional ao número de meses trabalhados no mesmo ano
                int mesesTrabalhados = (dataCalculo.Month - dataAdmissao.Month);
                double decimoTerceiroProporcional = (salarioBruto / 12) * mesesTrabalhados;
                return decimoTerceiroProporcional;
            }
            else
            {
                // Cálculo integral, considerando o salário bruto
                return salarioBruto;
            }
        }
        public double CalcularFgts(double salarioBruto)
        {
            double fgtsMensal = salarioBruto * 0.08;
            return fgtsMensal;
        }
    }
}

