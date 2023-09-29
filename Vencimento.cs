using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace folhaPagamento
{
    internal class Vencimento
    {
        private const int DiasPorMes = 30;
        private const int MesesPorAno = 12;
        public double SalarioBase { get; set; }
        public double DescontoINSS { get; set; }
        public double SalarioBaseIR { get; set; }
        public double DescontoIR { get; set; }
        public double ValeTransporte { get; set; }
        public double CalculoHoraExtra { get; set; }
        public double DescontoFaltasEmHoras { get; set; }
        public double SalarioLiquido { get; set; }
        public double DeducaoDependente { get; set; }


        public Vencimento CalcularSalarioMensal(double salarioBruto, int faltasEmHoras, int numeroDependentes, double horaExtra, bool optanteValeTransporte, double percentualHoraExtra)
        {
            Desconto desconto = new Desconto();
            Vencimento resultado = new Vencimento();

            resultado.CalculoHoraExtra = resultado.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
            resultado.DescontoFaltasEmHoras = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);
            resultado.SalarioBase = salarioBruto + resultado.CalculoHoraExtra - resultado.DescontoFaltasEmHoras;
            resultado.DescontoINSS = desconto.CalcularINSS(resultado.SalarioBase);
            resultado.DeducaoDependente = resultado.DeducaoDependentes(numeroDependentes);
            resultado.SalarioBaseIR = resultado.SalarioBase - resultado.DescontoINSS - resultado.DeducaoDependente;
            resultado.DescontoIR = desconto.CalcularIRRF(resultado.SalarioBaseIR);
            resultado.ValeTransporte = 0.0;

            if (optanteValeTransporte)
            {
                resultado.ValeTransporte = desconto.CalcularValeTransporte(salarioBruto);
            }

            resultado.SalarioLiquido = resultado.SalarioBase - resultado.DescontoINSS - resultado.DescontoIR - resultado.ValeTransporte;

            return resultado;
        }

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

            double horaExtraCalculada = (salarioBruto / 220) * percentualHoraExtra;
            double totalHoraExtra = horaExtraCalculada * horaExtra;
            return totalHoraExtra;
        }

        public double CalcularSaldoSalarioRescisao(DateTime dataAdmissao, DateTime dataDemissao, double ultimoSalario)
        {
            int diasTrabalhados = (dataDemissao - dataAdmissao).Days + 1;
            return (ultimoSalario / DiasPorMes) * diasTrabalhados;
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
        public double CalcularDecimoTerceiroRescisao(DateTime dataAdmissao, DateTime dataDemissao, double ultimoSalario)
        {
            int mesesTrabalhados = (dataDemissao.Year - dataAdmissao.Year) * MesesPorAno + dataDemissao.Month - dataAdmissao.Month + 1;
            return (ultimoSalario / MesesPorAno) * mesesTrabalhados;
        }
        public (double ValorFeriasProporcionais, int MesesProporcionais) CalcularFeriasProporcionais(DateTime dataCalculo, DateTime dataAdmissao, double salarioBruto)
        {
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

            // Verifica se já se passaram mais de 15 dias desde a data de admissão
            if (dataCalculo.Subtract(dataAdmissao).Days >= 14)
            {
                mesesProporcionais++; // Adiciona 1 mês completo
            }

            // Inicializar o valor
            double valorFeriasProporcionais = 0.0;

            if (mesesProporcionais > 0)
            {
                valorFeriasProporcionais = (salarioBruto / 12) * mesesProporcionais;
            }

            return (Math.Round(valorFeriasProporcionais, 2), mesesProporcionais);
        }
    }

}

