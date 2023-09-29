namespace folhaPagamento
{
    internal class Funcionario
    {

        public int NumeroDependentes;
        public double SalarioBruto;
        public bool OptanteValeTransporte;
        public DateTime Admissao;
        public string Nome { get; set; }
        public List<CalculoMensal> CalculosMensais { get; set; }

        public Funcionario(int numeroDependentes, double salarioBruto, bool optanteValeTransporte, DateTime admissao, string nome)
        {
            NumeroDependentes = numeroDependentes;
            SalarioBruto = salarioBruto;
            OptanteValeTransporte = optanteValeTransporte;
            Admissao = admissao;
            Nome = nome;
            CalculosMensais = new List<CalculoMensal>();
        }

        public void AdicionarCalculoMensal(int mes, Vencimento resultado)
        {
            CalculosMensais.Add(new CalculoMensal(mes, resultado));
        }
    }

}

