using folhaPagamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace folhaPagamento.Service
{
    public class VencimentoService
    {
        private const int MesesPorAno = 12;
       
        public SalarioMensal CalcularSalarioMensal(double salarioBruto, int numeroDependentes, bool optanteValeTransporte, double percentualHoraExtra, double faltasEmHoras = 0, double horaExtra = 0)
        {
            DescontoService desconto = new DescontoService();
            SalarioMensal resultado = new SalarioMensal();
            VencimentoService vencimento = new VencimentoService();

            resultado.CalculoHoraExtra = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
            resultado.DescontoFaltasEmHoras = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);
            resultado.SalarioBase = salarioBruto + resultado.CalculoHoraExtra - resultado.DescontoFaltasEmHoras;
            resultado.DescontoINSS = desconto.CalcularINSS(resultado.SalarioBase);
            resultado.DeducaoDependente = vencimento.DeducaoDependentes(numeroDependentes);
            resultado.SalarioBaseIR = resultado.SalarioBase - resultado.DescontoINSS - resultado.DeducaoDependente;
            resultado.DescontoIR = desconto.CalcularIRRF(resultado.SalarioBaseIR);
            resultado.ValeTransporte = 0.0;
            resultado.Fgts = CalcularFgts(salarioBruto);

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

            double horaExtraCalculada = salarioBruto / 220 * percentualHoraExtra;
            double totalHoraExtra = horaExtraCalculada * horaExtra;
            return totalHoraExtra;
        }
        public double CalcularSaldoSalarioRescisao(DateTime dataAdmissao, DateTime dataDemissao, double salarioBruto)
        {
            int diasTrabalhados = dataDemissao.Day + 1 - dataAdmissao.Day;
            double valorProporcional = salarioBruto / 30 * diasTrabalhados + 1;
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
        public double CalcularDecimoTerceiroRescisao(DateTime dataAdmissao, DateTime dataDemissao, double ultimoSalario)
        {
            int mesesTrabalhados = dataDemissao.Month - dataAdmissao.Month;

            return ultimoSalario / MesesPorAno * mesesTrabalhados;
        }
        public (double ValorFeriasProporcionais, int MesesProporcionais) CalcularFeriasProporcionais(DateTime dataAdmissao, DateTime dataCalculo, double salarioBruto)
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

            // Verifica se já se passaram mais de 14 dias desde a data de admissão
            if (dataCalculo.Day - dataAdmissao.Day >= 14)
            {
                mesesProporcionais++; // Adiciona 1 mês completo
            }

            // Inicializar o valor
            double valorFeriasProporcionais = 0.0;

            if (mesesProporcionais > 0)
            {
                valorFeriasProporcionais = salarioBruto / 12 * mesesProporcionais;
            }

            return (Math.Round(valorFeriasProporcionais, 2), mesesProporcionais);
        }
        public double CalcularFgts(double salarioBruto)
        {
            double fgtsMensal = salarioBruto * 0.08;
            return fgtsMensal;
        }
    }

}

