using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace folhaPagamento.Service
{
    public class DescontoService
    {
        public double CalcularDescontoFaltasEmHoras(double faltasEmHoras, double salarioBruto)
        {
            const int horasMensaisPadrao = 220;
            double descontoFaltas = salarioBruto / horasMensaisPadrao * faltasEmHoras;
            return descontoFaltas;
        }
        public double CalcularIRRF(double salarioBaseIR)
        {
            double irrf = 0.0;

            if (salarioBaseIR <= 2112.00)
            {
                irrf = 0.0;
            }
            else if (salarioBaseIR <= 2826.65)
            {
                irrf = salarioBaseIR * 0.075 - 158.40;
            }
            else if (salarioBaseIR <= 3751.05)
            {
                irrf = salarioBaseIR * 0.15 - 370.40;
            }
            else if (salarioBaseIR <= 4664.68)
            {
                irrf = salarioBaseIR * 0.225 - 651.73;
            }
            else
            {
                irrf = salarioBaseIR * 0.275 - 884.96;
            }

            return irrf;
        }
        public double CalcularINSS(double salarioBase)
        {
            double tetoFaixa4 = 7507.49;
            double aliquotaFaixa4 = 14.0;

            if (salarioBase > tetoFaixa4)
            {
                return 876.98;
            }

            double inssTotal = 0.0;

            // Faixa 1
            double tetoFaixa1 = 1320.00;
            double aliquotaFaixa1 = 7.5;
            if (salarioBase <= tetoFaixa1)
            {
                inssTotal += salarioBase * (aliquotaFaixa1 / 100);
                return inssTotal;
            }
            else
            {
                inssTotal += tetoFaixa1 * (aliquotaFaixa1 / 100);
            }
            // Faixa 2
            double tetoFaixa2 = 2571.29;
            double aliquotaFaixa2 = 9.0;
            if (salarioBase <= tetoFaixa2)
            {
                inssTotal += (salarioBase - tetoFaixa1) * (aliquotaFaixa2 / 100);
                return inssTotal;
            }
            else
            {
                inssTotal += (tetoFaixa2 - tetoFaixa1) * (aliquotaFaixa2 / 100);
            }
            // Faixa 3
            double tetoFaixa3 = 3856.94;
            double aliquotaFaixa3 = 12.0;
            if (salarioBase <= tetoFaixa3)
            {
                inssTotal += (salarioBase - tetoFaixa2) * (aliquotaFaixa3 / 100);
            }
            else
            {
                inssTotal += (tetoFaixa3 - tetoFaixa2) * (aliquotaFaixa3 / 100);
            }
            // Faixa 4
            if (salarioBase > tetoFaixa3)
            {
                double descontoFaixa4 = (salarioBase - tetoFaixa3) * (aliquotaFaixa4 / 100);
                inssTotal += descontoFaixa4;
                return inssTotal;

            }
            return inssTotal;
        }
        public double CalcularValeTransporte(double salarioBruto)
        {
            return salarioBruto * 0.06;
        }
    }
}
