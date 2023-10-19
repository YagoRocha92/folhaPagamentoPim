namespace folhaPagamento.Models
{
    public enum MotivoRescisao
    {
        DespedidaSemJustaCausa,
        DespedidaPorJustaCausa,
        PedidoDeDemissao,

    }
    public class Rescisao
    {
        public double SaldoSalarioRescisao { get; set; }
        public double AvisoPrevioRescisao { get; set; }
        public double HoraExtraRescisao { get; set; }
        public double DescontoFaltas { get; set; }
        public double Fgts { get; set; }
        public double DescontoInssRescisao { get; set; }
        public double SalarioBaseInssRescisão { get; set; }
        public double SalarioBaseIrrfRescisao { get; set; }
        public double DescontoIrrfRescisao { get; set; }
        public double SaldoRescisaoLiquido { get; set; }
        public Ferias Ferias { get; set; }
        public DecimoTerceiro DecimoTerceiroRescisao { get; set; }


        public Rescisao()
        {
        }
    }
}
