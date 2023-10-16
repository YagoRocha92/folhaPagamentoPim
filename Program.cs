using folhaPagamento.Models;
using folhaPagamento.Service;

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
            DateTime dataAdmissao = new DateTime(2023, 10, 8);
            DateTime dataDemissao = new DateTime(2023, 12, 21);
            DateTime dataCalculoFerias = new DateTime(2025, 10, 25);
            DateTime dataCalculoDecimoTerceiro = new DateTime(2023, 10, 8);

            MotivoRescisao motivoRescisao = MotivoRescisao.DespedidaSemJustaCausa;
            bool cumprirAviso = true;

            Funcionario funcionario = new Funcionario(numeroDependentes, salarioBruto, optanteValeTransporte, dataAdmissao, nome);

            VencimentoService vencimento = new VencimentoService();

            // Calcular 13º 
            double decimoTerceiro = vencimento.CalcularDecimoTerceiroRescisao(dataAdmissao, dataCalculoDecimoTerceiro, salarioBruto);
            Console.WriteLine("-----------CALCULO 13º SALARIO-----------");
            Console.WriteLine($"Data de calculo do 13º é {dataCalculoDecimoTerceiro.ToString("dd/MM/yyyy")} e sua admissão foi em {dataAdmissao.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Funcionário: {funcionario.Nome}");
            Console.WriteLine($"Valor 13º: {decimoTerceiro:F2}");
            Console.WriteLine("-----------FIM CALCULO 13º SALARIO-----------");
            Console.WriteLine();


           // Calcular Férias
            FeriasService calcularFerias = new FeriasService();
            Ferias ferias = new Ferias();
            ferias = calcularFerias.CalcularFeriasProporcionais(dataAdmissao, dataCalculoFerias, salarioBruto, numeroDependentes);

            Console.WriteLine("-----------CALCULO DE FÉRIAS-----------");
            Console.WriteLine($"Está saindo de férias em {dataCalculoFerias.ToString("dd/MM/yyyy")} e sua admissão foi em {dataAdmissao.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Funcionário: {funcionario.Nome}");
            Console.WriteLine($"Valor Férias: {ferias.ValorFeriasProporcionais:F2}");
            Console.WriteLine($"1/3 de férias é {ferias.UmTercoFerias:F2}");
            Console.WriteLine($"Meses Proporcionais: {ferias.MesesProporcionais}");
            Console.WriteLine("DESCONTOS:");
            Console.WriteLine();
            Console.WriteLine($"INSS: {ferias.DescontoInssFerias:F2}");
            Console.WriteLine($"IRRF: {ferias.DescontoIrrfFerias:F2}");
            Console.WriteLine();
            Console.WriteLine($"Valor Liquido: {ferias.SaldoFeriasLiquido:F2}");
            Console.WriteLine("-----------FIM CALCULO DE FÉRIAS-----------");
            Console.WriteLine();

            // Calcular Salario Mensal
            SalarioMensalService salario = new SalarioMensalService();
            SalarioMensal resultado = salario.CalcularSalarioMensal(dataAdmissao, salarioBruto, numeroDependentes, optanteValeTransporte, percentualHoraExtra, faltas, horaExtra);

            funcionario.AdicionarCalculoMensal(1, resultado);

            foreach (var calculoMensal in funcionario.CalculosMensais)
            {
                Console.WriteLine("-----------CALCULO SALARIO MENSAL-----------");
                Console.WriteLine($"Funcionário: {funcionario.Nome}");
                Console.WriteLine($"Admissão: {dataAdmissao.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Mês: {calculoMensal.Mes}");
                Console.WriteLine($"O Salario Bruto é R${calculoMensal.Resultado.SalarioBase:F2}");

                Console.WriteLine($"HORA EXTRA R${calculoMensal.Resultado.CalculoHoraExtra:F2}");
                Console.WriteLine("DESCONTO: ");
                Console.WriteLine($"IR R${calculoMensal.Resultado.DescontoIR:F2}");
                Console.WriteLine($"INSS R${calculoMensal.Resultado.DescontoINSS:F2}");
                Console.WriteLine($"VALE TRANSPORTE R${calculoMensal.Resultado.ValeTransporte:F2}");
                Console.WriteLine($"FALTAS R${calculoMensal.Resultado.DescontoFaltasEmHoras:F2}");
                Console.WriteLine("**************************************************************");
                Console.WriteLine($"LIQUIDO A RECEBER: R${calculoMensal.Resultado.SalarioLiquido:F2}");
                Console.WriteLine("-----------FIM CALCULO SALARIO MENSAL-----------");
                Console.WriteLine();
            }

            // Calcular Rescisão
            RecisaoService rescisao = new RecisaoService();

            Rescisao rescisaoCalculo = rescisao.CalcularRescisao(
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
            funcionario.AdicionarRescisao(1, rescisaoCalculo);

            Console.WriteLine("-----------CALCULO RESCISÃO-----------");
            Console.WriteLine($"Funcionário: {nome}");
            Console.WriteLine($"Admissao: {dataAdmissao.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Demissão: {dataDemissao.ToString("dd/MM/yyyy")}");
            Console.WriteLine("Saldo de Salário na Rescisão: " + rescisaoCalculo.SaldoSalarioRescisao.ToString("N2"));
            Console.WriteLine("Décimo Terceiro na Rescisão: " + rescisaoCalculo.DecimoTerceiroRescisao.ToString("N2"));
            Console.WriteLine("Férias na Rescisão: " + rescisaoCalculo.Ferias.ValorFeriasProporcionais.ToString("N2"));
            Console.WriteLine("1/3 de férias na Rescisão: " + rescisaoCalculo.Ferias.UmTercoFerias.ToString("N2"));
            Console.WriteLine("Mês Proporcional  de Férias: " + rescisaoCalculo.Ferias.MesesProporcionais);
            Console.WriteLine("Aviso Prévio na Rescisão: " + rescisaoCalculo.AvisoPrevioRescisao.ToString("N2"));
            Console.WriteLine("Hora Extra na Rescisão: " + rescisaoCalculo.HoraExtraRescisao.ToString("N2"));
            Console.WriteLine("Desconto de Faltas: " + rescisaoCalculo.DescontoFaltas.ToString("N2"));
            Console.WriteLine("FGTS na Rescisão: " + rescisaoCalculo.Fgts.ToString("N2"));
            Console.WriteLine("Desconto de INSS na Rescisão: " + rescisaoCalculo.DescontoInssRescisao.ToString("N2"));
            Console.WriteLine("Salário Base INSS na Rescisão: " + rescisaoCalculo.SalarioBaseInssRescisão.ToString("N2"));
            Console.WriteLine("Salário Base IRRF na Rescisão: " + rescisaoCalculo.SalarioBaseIrrfRescisao.ToString("N2"));
            Console.WriteLine("Desconto de IRRF na Rescisão: " + rescisaoCalculo.DescontoIrrfRescisao.ToString("N2"));
            Console.WriteLine("-----------FIM CALCULO RESCISÃO-----------");
            Console.WriteLine();
        }
    }
}

