namespace folhaPagamento.Models
{
    public class CalculoMensal
    {
        public int Mes { get; set; }
        public SalarioMensal SalarioMensal { get; set; }
        public Rescisao Rescisao { get; set; }
        public Ferias Ferias { get; set; }
        public DecimoTerceiro DecimoTerceiro { get; set; }

        public CalculoMensal()
        {
        }

    }

}
