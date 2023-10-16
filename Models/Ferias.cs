namespace folhaPagamento.Models
{
    public class Ferias
    {
        public double ValorFeriasProporcionais { get; set; }
        public int MesesProporcionais { get; set; }
        public double Fgts { get; set; }
        public double DescontoInssFerias { get; set; }
        public double SalarioBaseInssFerias { get; set; }
        public double SalarioBaseIrrfFerias { get; set; }
        public double DescontoIrrfFerias { get; set; }
        public double SaldoFeriasLiquido { get; set; }
        public double UmTercoFerias { get; set; }

        public Ferias()
        {
        }
    }
}
