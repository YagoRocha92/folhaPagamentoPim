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
            DateTime dataAdmissao = new DateTime(2022, 5, 7);
            DateTime dataDemissao = new DateTime(2025, 8, 7);
            DateTime dataCalculoFerias = new DateTime(2025, 10, 22);
            DateTime dataCalculoDecimoTerceiro = new DateTime(2025, 11, 20);

            MotivoRescisao motivoRescisao = MotivoRescisao.DespedidaSemJustaCausa;
            bool cumprirAviso = true;

            Funcionario funcionario = new Funcionario(numeroDependentes, salarioBruto, optanteValeTransporte, dataAdmissao, nome);

            Vencimento vencimento = new Vencimento();

            // Calcular 13º 
            double decimoTerceiro = vencimento.CalcularDecimoTerceiroRescisao(dataAdmissao, dataCalculoDecimoTerceiro, salarioBruto);
            Console.WriteLine("13º Salario");
            Console.WriteLine($"Data de calculo do 13º é {dataCalculoDecimoTerceiro} e sua admissão foi em {dataAdmissao}");
            Console.WriteLine($"Funcionário: {funcionario.Nome}");
            Console.WriteLine($"Valor 13º: {decimoTerceiro:F2}");
            Console.WriteLine();


            // Calcular Férias
            (double calculoFerias, int mesesProporcionais) = vencimento.CalcularFeriasProporcionais(dataAdmissao, dataCalculoFerias, salarioBruto);
            Console.WriteLine("Férias");
            Console.WriteLine($"Está saindo de férias em {dataCalculoFerias} e sua admissão foi em {dataAdmissao}");
            Console.WriteLine($"Funcionário: {funcionario.Nome}");
            Console.WriteLine($"Valor Férias: {calculoFerias:F2}");
            Console.WriteLine($"1/3 de férias é {calculoFerias / 3:F2}");
            Console.WriteLine($"Meses Proporcionais: {mesesProporcionais}");
            Console.WriteLine();

            // Calcular Salario Mensal
            Vencimento resultado = vencimento.CalcularSalarioMensal(salarioBruto, numeroDependentes, optanteValeTransporte, percentualHoraExtra, faltas, horaExtra);

            funcionario.AdicionarCalculoMensal(1, resultado);

            foreach (var calculoMensal in funcionario.CalculosMensais)
            {
                Console.WriteLine("Calculo Mensal");
                Console.WriteLine($"Funcionário: {funcionario.Nome}");
                Console.WriteLine($"Admissão: {dataAdmissao}");
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

            // Calcular Rescisão
            Rescisao rescisaoCalculada = new Rescisao();

            Rescisao rescisaoCalculo = rescisaoCalculada.CalcularRescisao(
                dataAdmissao,
                dataDemissao,
                salarioBruto,
                horaExtra,
                percentualHoraExtra,
                motivoRescisao,
                numeroDependentes,
                cumprirAviso,
                faltas
            );

            Console.WriteLine("Rescisão");
            Console.WriteLine($"Funcionário: {nome}");
            Console.WriteLine($"Admissao: {dataAdmissao}");
            Console.WriteLine($"Demissão: {dataDemissao}");
            Console.WriteLine("Saldo de Salário na Rescisão: " + rescisaoCalculo.SaldoSalarioRescisao.ToString("N2"));
            Console.WriteLine("Décimo Terceiro na Rescisão: " + rescisaoCalculo.DecimoTerceiroRescisao.ToString("N2"));
            Console.WriteLine("Férias na Rescisão: " + rescisaoCalculo.FeriasRescisao.ToString("N2"));
            Console.WriteLine("Mês Proporcional  de Férias: " + rescisaoCalculo.MesesFerias);
            Console.WriteLine("Aviso Prévio na Rescisão: " + rescisaoCalculo.AvisoPrevioRescisao.ToString("N2"));
            Console.WriteLine("Hora Extra na Rescisão: " + rescisaoCalculo.HoraExtraRescisao.ToString("N2"));
            Console.WriteLine("Desconto de Faltas: " + rescisaoCalculo.DescontoFaltas.ToString("N2"));
            Console.WriteLine("FGTS na Rescisão: " + rescisaoCalculo.Fgts.ToString("N2"));
            Console.WriteLine("Desconto de INSS na Rescisão: " + rescisaoCalculo.DescontoInssRescisao.ToString("N2"));
            Console.WriteLine("Salário Base INSS na Rescisão: " + rescisaoCalculo.SalarioBaseInssRescisão.ToString("N2"));
            Console.WriteLine("Salário Base IRRF na Rescisão: " + rescisaoCalculo.SalarioBaseIrrfRescisao.ToString("N2"));
            Console.WriteLine("Desconto de IRRF na Rescisão: " + rescisaoCalculo.DescontoIrrfRescisao.ToString("N2"));
        }
    }
}

