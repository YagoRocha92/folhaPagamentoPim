namespace folhaPagamento
{
    class Program
    {
        static void Main(string[] args)
        {
            string nome = "Joao";
            int faltas = 2;
            int numeroDependentes = 3;
            double salarioBruto = 3500.00;
            double horaExtra = 6;
            bool optanteValeTransporte = true;
            double percentualHoraExtra = 1.5;
            DateTime dateTime = new DateTime(2022, 5, 7);

            Funcionario funcionario = new Funcionario(numeroDependentes, salarioBruto, optanteValeTransporte, dateTime, nome);

            Vencimento vencimento = new Vencimento();
            Vencimento resultado = vencimento.CalcularSalarioMensal(salarioBruto, faltas, numeroDependentes, horaExtra, optanteValeTransporte, percentualHoraExtra);
            
            funcionario.AdicionarCalculoMensal(1, resultado);

            foreach (var calculoMensal in funcionario.CalculosMensais)
            {
                Console.WriteLine($"Funcionário: {funcionario.Nome}");
                Console.WriteLine($"Mês: {calculoMensal.Mes}");
                Console.WriteLine($"O Salario Bruto é R${funcionario.SalarioBruto:F2}");
                Console.WriteLine($"HORA EXTRA R${calculoMensal.Resultado.CalculoHoraExtra:F2}");
                Console.WriteLine("DESCONTO: ");
                Console.WriteLine($"IR R${calculoMensal.Resultado.DescontoIR:F2}");
                Console.WriteLine($"INSS R${calculoMensal.Resultado.DescontoINSS:F2}");
                Console.WriteLine($"VALE TRANSPORTE R${calculoMensal.Resultado.ValeTransporte:F2}");
                Console.WriteLine($"FALTAS R${calculoMensal.Resultado.DescontoFaltasEmHoras:F2}");
                Console.WriteLine("**************************************************************");
                Console.WriteLine($"LIQUIDO A RECEBER: R${calculoMensal.Resultado.SalarioLiquido:F2}");
                Console.WriteLine();
            }

            //metodo consulta ferias proporcionais
            DateTime dataCalculo = new DateTime(2022, 5, 20);
            var result = vencimento.CalcularFeriasProporcionais(dataCalculo, funcionario.Admissao, funcionario.SalarioBruto);
            double valorFeriasProporcionais = result.ValorFeriasProporcionais;
            int mesesProporcionais = result.MesesProporcionais;

            Console.WriteLine("Ferias é " + valorFeriasProporcionais + " e o 1/3 das ferias é " + valorFeriasProporcionais / 3 + " e seus meses proporcionais são: " + mesesProporcionais);
            //FIM


        }
    }
}

