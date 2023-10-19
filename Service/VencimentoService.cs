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
        public double CalcularSalarioMes(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto)
        {
            int diasTrabalhados;

            if (dataAdmissao.Month == dataCalculo.Month && dataAdmissao.Year == dataCalculo.Year)
            {
                diasTrabalhados = dataCalculo.Day - dataAdmissao.Day;
            }
            else
            {
                diasTrabalhados = 30;
            }
            double valorProporcional = (salarioBruto / 30) * diasTrabalhados;
            return valorProporcional;
        }

        public double CalcularFgts(double salarioBruto)
        {
            double fgtsMensal = salarioBruto * 0.08;
            return fgtsMensal;
        }
    }
}

