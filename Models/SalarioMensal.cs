using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace folhaPagamento.Models
{
    public class SalarioMensal
    {
        public double SalarioBase { get; set; }
        public double SalarioBaseInss { get; set; }
        public double DescontoINSS { get; set; }
        public double SalarioBaseIR { get; set; }
        public double DescontoIR { get; set; }
        public double ValeTransporte { get; set; }
        public double CalculoHoraExtra { get; set; }
        public double DescontoFaltasEmHoras { get; set; }
        public double SalarioLiquido { get; set; }
        public double DeducaoDependente { get; set; }
        public double Fgts { get; set; }
    }
}
