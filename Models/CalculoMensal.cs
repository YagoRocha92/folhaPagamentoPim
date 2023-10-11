namespace folhaPagamento.Models
{
    public class CalculoMensal
    {
        public int Mes { get; set; }
        public SalarioMensal Resultado { get; set; }
        public Rescisao Rescisao { get; set; }
        public Ferias Ferias { get; set; }


        public CalculoMensal()
        {
        }

        public CalculoMensal(int mes, SalarioMensal resultado)
        {
            Mes = mes;
            Resultado = resultado;
        }
    }

}
