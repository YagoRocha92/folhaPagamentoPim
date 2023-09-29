/*namespace folhaPagamento
{
    public enum MotivoRescisao
    {
        DespedidaSemJustaCausa,
        DespedidaPorJustaCausa,
        PedidoDeDemissao,

    }
    internal class Rescisao
    {

        private const int DiasPorMes = 30;
        private const int MesesPorAno = 12;

        public double SaldoSalarioRescisao { get; set; }
        public double DecimoTerceiroRescisao { get; set; }
        public double FeriasVencidasRescisao { get; set; }
        public double FeriasProporcionaisRescisao { get; set; }
        public double AvisoPrevioRescisao { get; set; }
        public double MultaFgtsRescisao { get; set; }
        public double HoraExtraRescisao { get; set; }
        public double DescontoFaltas { get; set; }


        public static Rescisao CalcularRescisao(DateTime dataAdmissao, DateTime dataDemissao, double salarioBruto, double horaExtra, double percentualHoraExtra, MotivoRescisao motivoRescisao, bool cumprirAviso, double faltasEmHoras)
        {

            
            double valorRescisao = 0;
            double saldoSalarioRescisao = 0;
            double decimoTerceiroRescisao = 0;
            double avisoPrevioRescisao = 0;
            double horaExtraRescisao = 0;
          

            Desconto desconto = new Desconto();
            Vencimento vencimento = new Vencimento();
            Rescisao resultado = new Rescisao();

            switch (motivoRescisao)
            {
                case MotivoRescisao.DespedidaSemJustaCausa:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalarioRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.DecimoTerceiroRescisao = vencimento.CalcularDecimoTerceiroRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.FeriasRescisao = vencimento.CalcularFerias(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.AvisoPrevioRescisao = CalcularAvisoPrevio(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.DescontoFaltas = desconto.CalcularDescontoFaltasEmHoras(faltasEmHoras, salarioBruto);
                    break;

                case MotivoRescisao.DespedidaPorJustaCausa:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalarioRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.FeriasRescisao = vencimento.CalcularFerias(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    break;

                case MotivoRescisao.PedidoDeDemissao:
                    resultado.SaldoSalarioRescisao = vencimento.CalcularSaldoSalarioRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.DecimoTerceiroRescisao = vencimento.CalcularDecimoTerceiroRescisao(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.FeriasRescisao = vencimento.CalcularFerias(dataAdmissao, dataDemissao, salarioBruto);
                    resultado.HoraExtraRescisao = vencimento.CalcularHoraExtra(horaExtra, salarioBruto, percentualHoraExtra);
                    resultado.AvisoPrevioRescisao = CalcularAvisoPrevio(dataAdmissao, dataDemissao, salarioBruto);
                                        
                    break;
            }

            return resultado;
        }


        private static double CalcularAvisoPrevio(DateTime dataAdmissao, DateTime dataDemissao, double ultimoSalario)
        {
            int anosTrabalhados = dataDemissao.Year - dataAdmissao.Year;
            int diasAvisoPrevio = DiasPorMes * 2 + (anosTrabalhados * 3); // Aviso prévio proporcional
            return (ultimoSalario / DiasPorMes) * diasAvisoPrevio;
        }

        private static double CalcularMultaFGTS(double ultimoSalario)
        {
            // Implemente a lógica para calcular a multa do FGTS
            // Por exemplo, 40% do saldo do FGTS
            return 0.4 * saldoFGTS;
        }
    }
}
  */