namespace folhaPagamento.Models
{
    public class DecimoTerceiro
    {
        public int MesesTrabalhados { get; set; }
        public double ValorDecimoTerceiro { get; set; }
        public double DescontoInssDecimoTerceiro { get; set; }
        public double SalarioBaseInssDecimoTerceiro { get; set; }
        public double SalarioBaseIrrfDecimoTerceiro { get; set; }
        public double DescontoIrrfDecimoTerceiro { get; set; }
        public double SaldoDecimoTerceiroLiquido { get; set; }
        public double Fgts { get; set; }
        public DecimoTerceiro()
        {
        }
    }
}
