namespace folhaPagamento
{
    internal class CalculoMensal
    {
        public int Mes { get; set; }
        public Vencimento Resultado { get; set; }

        public CalculoMensal(int mes, Vencimento resultado)
        {
            Mes = mes;
            Resultado = resultado;
        }
    }

}
