namespace folhaPagamento.Models
{
    public class Funcionario
    {
        public string Nome { get; set; }
        public double SalarioBruto { get; set; }
        public int NumeroDependentes { get; set; }
        public bool OptanteValeTransporte { get; set; }
        public DateTime Admissao { get; set; }
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

        public void AdicionarCalculoMensal(int mes, SalarioMensal resultado)
        {
            CalculoMensal calculoMensal = new CalculoMensal();
            calculoMensal.SalarioMensal = resultado;
            CalculosMensais.Add(calculoMensal);
        }

        public void AdicionarRescisao(int mes, Rescisao calculoRescisao)
        {
            CalculoMensal calculoMensal = new CalculoMensal();
            calculoMensal.Rescisao = calculoRescisao;
            calculoMensal.Mes = mes;
            CalculosMensais.Add(calculoMensal);
        }

        public void AdicionarFerias(int mes, Ferias ferias)
        {
            CalculoMensal calculoMensal = new CalculoMensal();
            calculoMensal.Ferias = ferias;
            CalculosMensais.Add(calculoMensal);
        }

        public void AdicionarDecimoTerceiro(int mes, DecimoTerceiro decimoTerceiro)
        {
            CalculoMensal calculoMensal = new CalculoMensal();
            calculoMensal.DecimoTerceiro = decimoTerceiro;
            CalculosMensais.Add(calculoMensal);
        }

    }

}

