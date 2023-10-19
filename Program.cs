using folhaPagamento.Models;
using folhaPagamento.Service;

namespace folhaPagamento
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dados para estanciar objeto Funcionario  
            string nome = "Joao";
            int numeroDependentes = 0;
            double salarioBruto = 3500.00;
            bool optanteValeTransporte = true;
            DateTime dataAdmissao = new DateTime(2023, 10, 8);

            // Dados para calculos
            int faltas = 2;
            double horaExtra = 6;
            double percentualHoraExtra = 1.5;
            double valorValeTransporte = 250.00;
            bool cumprirAviso = true;
            DateTime dataDemissao = new DateTime(2023, 12, 22);
            DateTime dataCalculoFerias = new DateTime(2025, 10, 25);
            DateTime dataCalculoDecimoTerceiro = new DateTime(2024, 10, 8);
            DateTime dataCalculoSalarioMensal = new DateTime(2023, 10, 15);

            MotivoRescisao motivoRescisao = MotivoRescisao.DespedidaSemJustaCausa;

            Funcionario funcionario = new Funcionario(numeroDependentes, salarioBruto, optanteValeTransporte, dataAdmissao, nome);

            // Calcular 13º 
            DecimoTerceiroService decimoTerceiroService = new DecimoTerceiroService();
            DecimoTerceiro decimoTerceiro = new DecimoTerceiro();

            decimoTerceiro = decimoTerceiroService.CalcularDecimoTerceiro(dataAdmissao, dataCalculoDecimoTerceiro, salarioBruto, numeroDependentes);

            Console.WriteLine("-----------CALCULO 13º SALARIO-----------");
            Console.WriteLine($"Data de calculo do 13º é {dataCalculoDecimoTerceiro.ToString("dd/MM/yyyy")} e sua admissão foi em {dataAdmissao.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Funcionário: {funcionario.Nome}");
            Console.WriteLine($"Valor 13º: {decimoTerceiro.ValorDecimoTerceiro:F2}");
            Console.WriteLine("-----------DESCONTOS-----------");
            Console.WriteLine($"INSS: {decimoTerceiro.DescontoInssDecimoTerceiro:F2}");
            Console.WriteLine($"IRRF: {decimoTerceiro.DescontoIrrfDecimoTerceiro:F2}");
            Console.WriteLine($"QUANTIDADE DE MESES TRABALHADOS: {decimoTerceiro.MesesTrabalhados:F2}");
            Console.WriteLine($"VALOR LIQUIDO: {decimoTerceiro.SaldoDecimoTerceiroLiquido:F2}");
            Console.WriteLine("-----------FIM CALCULO 13º SALARIO-----------");
            Console.WriteLine();

            funcionario.AdicionarDecimoTerceiro(1, decimoTerceiro);



            // Calcular Férias
            FeriasService feriasService = new FeriasService();
            Ferias ferias = new Ferias();

            ferias = feriasService.CalcularFeriasProporcionais(dataAdmissao, dataCalculoFerias, salarioBruto, numeroDependentes);

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

            funcionario.AdicionarFerias(1, ferias);



            // Calcular Salario Mensal
            SalarioMensalService salarioMensalService = new SalarioMensalService();
            SalarioMensal salarioMensal = salarioMensalService.CalcularSalarioMensal(dataAdmissao, dataCalculoSalarioMensal, salarioBruto, numeroDependentes, optanteValeTransporte, percentualHoraExtra, valorValeTransporte, faltas, horaExtra);


                Console.WriteLine("-----------CALCULO SALARIO MENSAL-----------");
                Console.WriteLine($"Funcionário: {funcionario.Nome}");
                Console.WriteLine($"Admissão: {dataAdmissao.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Mês: {dataCalculoSalarioMensal.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"O Salario Bruto é R${salarioMensal.SalarioBase:F2}");
                Console.WriteLine($"HORA EXTRA R${salarioMensal.CalculoHoraExtra:F2}");
                Console.WriteLine("DESCONTO: ");
                Console.WriteLine($"IR R${salarioMensal.DescontoIR:F2}");
                Console.WriteLine($"INSS R${salarioMensal.DescontoINSS:F2}");
                Console.WriteLine($"VALE TRANSPORTE R${salarioMensal.ValeTransporte:F2}");
                Console.WriteLine($"FALTAS R${salarioMensal.DescontoFaltasEmHoras:F2}");
                Console.WriteLine("**************************************************************");
                Console.WriteLine($"LIQUIDO A RECEBER: R${salarioMensal.SalarioLiquido:F2}");
                Console.WriteLine("-----------FIM CALCULO SALARIO MENSAL-----------");
                Console.WriteLine();

                funcionario.AdicionarCalculoMensal(1, salarioMensal);

            // Calcular Rescisão
            RecisaoService recisaoService = new RecisaoService();
            Rescisao rescisao = recisaoService.CalcularRescisao(dataAdmissao, dataDemissao, salarioBruto, horaExtra, percentualHoraExtra, motivoRescisao, numeroDependentes, cumprirAviso, faltas);

            Console.WriteLine("-----------CALCULO RESCISÃO-----------");
            Console.WriteLine($"Funcionário: {nome}");
            Console.WriteLine($"Admissao: {dataAdmissao.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Demissão: {dataDemissao.ToString("dd/MM/yyyy")}");
            Console.WriteLine("Saldo de Salário na Rescisão: " + rescisao.SaldoSalarioRescisao.ToString("N2"));
            Console.WriteLine($"QUANTIDADE DE MESES TRABALHADOS: {rescisao.DecimoTerceiroRescisao.MesesTrabalhados:F2}");
            Console.WriteLine("Décimo Terceiro na Rescisão: " + rescisao.DecimoTerceiroRescisao.ValorDecimoTerceiro.ToString("N2"));
            Console.WriteLine("Férias na Rescisão: " + rescisao.Ferias.ValorFeriasProporcionais.ToString("N2"));
            Console.WriteLine("1/3 de férias na Rescisão: " + rescisao.Ferias.UmTercoFerias.ToString("N2"));
            Console.WriteLine("Mês Proporcional  de Férias: " + rescisao.Ferias.MesesProporcionais);
            Console.WriteLine("Aviso Prévio na Rescisão: " + rescisao.AvisoPrevioRescisao.ToString("N2"));
            Console.WriteLine("Hora Extra na Rescisão: " + rescisao.HoraExtraRescisao.ToString("N2"));
            Console.WriteLine("Desconto de Faltas: " + rescisao.DescontoFaltas.ToString("N2"));
            Console.WriteLine("FGTS na Rescisão: " + rescisao.Fgts.ToString("N2"));
            Console.WriteLine("Desconto de INSS na Rescisão: " + rescisao.DescontoInssRescisao.ToString("N2"));
            Console.WriteLine("Salário Base INSS na Rescisão: " + rescisao.SalarioBaseInssRescisão.ToString("N2"));
            Console.WriteLine("Salário Base IRRF na Rescisão: " + rescisao.SalarioBaseIrrfRescisao.ToString("N2"));
            Console.WriteLine("Desconto de IRRF na Rescisão: " + rescisao.DescontoIrrfRescisao.ToString("N2"));
            Console.WriteLine("-----------FIM CALCULO RESCISÃO-----------");
            Console.WriteLine();

            funcionario.AdicionarRescisao(1, rescisao);
        }


    }
}

