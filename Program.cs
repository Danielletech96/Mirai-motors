
using System;
using System.Globalization;

// PROJETO: Sistema de Gestão - MIRAI MOTORS
// GRUPO: [Danielle Gonçalves, Gustavo Ribeiro, Ítalo João, Larissa Maria, Mayara Lima]


class Program
{
    // Limites máximos de registros
    const int MAX_CLIENTES = 50;
    const int MAX_VENDEDORES = 20;
    const int MAX_VEICULOS = 50;
    const int MAX_VENDAS = 50;
    const int MAX_AGENDAMENTOS = 50;

    // Dados da tabela CLIENTE
    static int[] idsClientes = new int[MAX_CLIENTES];
    static string[] nomesClientes = new string[MAX_CLIENTES];
    static string[] telefonesClientes = new string[MAX_CLIENTES];
    static string[] emailsClientes = new string[MAX_CLIENTES];

    // Dados da tabela VENDEDOR
    static int[] idsVendedores = new int[MAX_VENDEDORES];
    static string[] nomesVendedores = new string[MAX_VENDEDORES];
    static string[] telefonesVendedores = new string[MAX_VENDEDORES];

    // Dados da tabela VEICULO
    static int[] idsVeiculos = new int[MAX_VEICULOS];
    static string[] marcas = new string[MAX_VEICULOS];
    static string[] modelos = new string[MAX_VEICULOS];
    static int[] anos = new int[MAX_VEICULOS];
    static decimal[] precos = new decimal[MAX_VEICULOS];
    static string[] situacoes = new string[MAX_VEICULOS];

    // Matriz da tabela VENDA
    // Colunas: ID, data, valor, pagamento, status,
    // ID do cliente, ID do vendedor e ID do veículo.
    static string[,] vendas = new string[MAX_VENDAS, 8];

    // Matriz da tabela AGENDAMENTO_TEST_DRIVE
    // Colunas: ID, data e hora, status,
    // ID do cliente, ID do vendedor e ID do veículo.
    static string[,] agendamentos = new string[MAX_AGENDAMENTOS, 6];

    // Contadores de registros
    static int qtdClientes = 0;
    static int qtdVendedores = 0;
    static int qtdVeiculos = 0;
    static int qtdVendas = 0;
    static int qtdAgendamentos = 0;

    // Acumulador do valor total das vendas
    static decimal totalVendas = 0;

    // Configuração para leitura de valores no padrão brasileiro
    static readonly CultureInfo culturaBR =
        CultureInfo.GetCultureInfo("pt-BR");

    static void Main()
    {
        int opcao;

        // Menu principal do sistema
        do
        {
            Console.Clear();

            Console.WriteLine("==========================================");
            Console.WriteLine("               MIRAI MOTORS");
            Console.WriteLine("        Tecnologia que move o futuro");
            Console.WriteLine("==========================================");
            Console.WriteLine("1 - Cadastrar cliente");
            Console.WriteLine("2 - Cadastrar vendedor");
            Console.WriteLine("3 - Cadastrar veículo");
            Console.WriteLine("4 - Consultar veículos disponíveis");
            Console.WriteLine("5 - Agendar test-drive");
            Console.WriteLine("6 - Consultar agendamentos");
            Console.WriteLine("7 - Registrar venda");
            Console.WriteLine("8 - Consultar vendas");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("==========================================");

            opcao = LerInteiro("Escolha uma opção: ");

            switch (opcao)
            {
                case 1:
                    CadastrarCliente();
                    break;

                case 2:
                    CadastrarVendedor();
                    break;

                case 3:
                    CadastrarVeiculo();
                    break;

                case 4:
                    ConsultarVeiculos();
                    break;

                case 5:
                    AgendarTestDrive();
                    break;

                case 6:
                    ConsultarAgendamentos();
                    break;

                case 7:
                    RegistrarVenda();
                    break;

                case 8:
                    ConsultarVendas();
                    break;

                case 0:
                    Console.WriteLine("\nSistema encerrado. Obrigado!");
                    break;

                default:
                    Console.WriteLine("\nOpção inválida!");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    // Lê textos obrigatórios
    static string LerTexto(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string texto = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(texto))
            {
                return texto.Trim();
            }

            Console.WriteLine("Este campo não pode ficar vazio.");
        }
    }

    // Lê textos que podem ficar vazios, como o e-mail
    static string LerTextoOpcional(string mensagem)
    {
        Console.Write(mensagem);
        return (Console.ReadLine() ?? "").Trim();
    }

    // Lê números inteiros
    static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);

            if (int.TryParse(Console.ReadLine(), out int numero))
            {
                return numero;
            }

            Console.WriteLine("Digite um número inteiro válido.");
        }
    }

    // Lê valores monetários no padrão brasileiro
    static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string entrada = Console.ReadLine() ?? "";

            if (decimal.TryParse(
                entrada,
                NumberStyles.Number,
                culturaBR,
                out decimal valor))
            {
                return valor;
            }

            Console.WriteLine("Digite um valor válido. Exemplo: 85000,00");
        }
    }

    // Lê uma data e hora futuras para o test-drive
    static DateTime LerDataHoraFutura()
    {
        while (true)
        {
            Console.Write("Data e hora (dd/MM/aaaa HH:mm): ");
            string entrada = Console.ReadLine() ?? "";

            bool dataValida = DateTime.TryParseExact(
                entrada,
                "dd/MM/yyyy HH:mm",
                culturaBR,
                DateTimeStyles.None,
                out DateTime dataHora);

            if (dataValida && dataHora > DateTime.Now)
            {
                return dataHora;
            }

            Console.WriteLine(
                "Data inválida ou passada. Informe uma data futura.");
        }
    }

    // Localiza um cliente pelo seu identificador
    static int BuscarCliente(int id)
    {
        for (int i = 0; i < qtdClientes; i++)
        {
            if (idsClientes[i] == id)
            {
                return i;
            }
        }

        return -1;
    }

    // Localiza um vendedor pelo seu identificador
    static int BuscarVendedor(int id)
    {
        for (int i = 0; i < qtdVendedores; i++)
        {
            if (idsVendedores[i] == id)
            {
                return i;
            }
        }

        return -1;
    }

    // Localiza um veículo pelo seu identificador
    static int BuscarVeiculo(int id)
    {
        for (int i = 0; i < qtdVeiculos; i++)
        {
            if (idsVeiculos[i] == id)
            {
                return i;
            }
        }

        return -1;
    }

    // Cadastra um cliente
    static void CadastrarCliente()
    {
        Console.WriteLine("\n--- CADASTRO DE CLIENTE ---");

        if (qtdClientes >= MAX_CLIENTES)
        {
            Console.WriteLine("Limite de clientes atingido!");
            return;
        }

        idsClientes[qtdClientes] = qtdClientes + 1;
        nomesClientes[qtdClientes] = LerTexto("Nome: ");
        telefonesClientes[qtdClientes] = LerTexto("Telefone: ");
        emailsClientes[qtdClientes] =
            LerTextoOpcional("E-mail (opcional): ");

        qtdClientes++;

        Console.WriteLine("Cliente cadastrado com sucesso!");
    }

    // Cadastra um vendedor
    static void CadastrarVendedor()
    {
        Console.WriteLine("\n--- CADASTRO DE VENDEDOR ---");

        if (qtdVendedores >= MAX_VENDEDORES)
        {
            Console.WriteLine("Limite de vendedores atingido!");
            return;
        }

        idsVendedores[qtdVendedores] = qtdVendedores + 1;
        nomesVendedores[qtdVendedores] = LerTexto("Nome: ");
        telefonesVendedores[qtdVendedores] = LerTexto("Telefone: ");

        qtdVendedores++;

        Console.WriteLine("Vendedor cadastrado com sucesso!");
    }

    // Cadastra um veículo
    static void CadastrarVeiculo()
    {
        Console.WriteLine("\n--- CADASTRO DE VEÍCULO ---");

        if (qtdVeiculos >= MAX_VEICULOS)
        {
            Console.WriteLine("Limite de veículos atingido!");
            return;
        }

        string marca = LerTexto("Marca: ");
        string modelo = LerTexto("Modelo: ");
        int ano = LerInteiro("Ano: ");
        decimal preco = LerDecimal("Preço: R$ ");

        if (ano < 1886 || ano > DateTime.Now.Year + 1 || preco <= 0)
        {
            Console.WriteLine("Ano ou preço inválido. Cadastro não realizado.");
            return;
        }

        idsVeiculos[qtdVeiculos] = qtdVeiculos + 1;
        marcas[qtdVeiculos] = marca;
        modelos[qtdVeiculos] = modelo;
        anos[qtdVeiculos] = ano;
        precos[qtdVeiculos] = preco;
        situacoes[qtdVeiculos] = "Disponível";

        qtdVeiculos++;

        Console.WriteLine("Veículo cadastrado com sucesso!");
    }

    // Mostra os veículos disponíveis
    static bool ListarVeiculosDisponiveis()
    {
        bool encontrou = false;

        for (int i = 0; i < qtdVeiculos; i++)
        {
            if (situacoes[i] == "Disponível")
            {
                Console.WriteLine(
                    $"ID: {idsVeiculos[i]} | " +
                    $"{marcas[i]} {modelos[i]} | " +
                    $"Ano: {anos[i]} | " +
                    $"Preço: {precos[i].ToString("C", culturaBR)}");

                encontrou = true;
            }
        }

        return encontrou;
    }

    // Consulta o estoque de veículos disponíveis
    static void ConsultarVeiculos()
    {
        Console.WriteLine("\n--- VEÍCULOS DISPONÍVEIS ---");

        bool encontrou = ListarVeiculosDisponiveis();

        if (!encontrou)
        {
            Console.WriteLine("Não existem veículos disponíveis.");
        }
    }

    // Mostra os clientes cadastrados
    static void ListarClientes()
    {
        Console.WriteLine("\nCLIENTES CADASTRADOS:");

        for (int i = 0; i < qtdClientes; i++)
        {
            Console.WriteLine(
                $"ID: {idsClientes[i]} | " +
                $"Nome: {nomesClientes[i]}");
        }
    }

    // Mostra os vendedores cadastrados
    static void ListarVendedores()
    {
        Console.WriteLine("\nVENDEDORES CADASTRADOS:");

        for (int i = 0; i < qtdVendedores; i++)
        {
            Console.WriteLine(
                $"ID: {idsVendedores[i]} | " +
                $"Nome: {nomesVendedores[i]}");
        }
    }

    // Agenda um test-drive
    static void AgendarTestDrive()
    {
        Console.WriteLine("\n--- AGENDAMENTO DE TEST-DRIVE ---");

        if (qtdAgendamentos >= MAX_AGENDAMENTOS)
        {
            Console.WriteLine("Limite de agendamentos atingido!");
            return;
        }

        if (qtdClientes == 0 || qtdVendedores == 0 || qtdVeiculos == 0)
        {
            Console.WriteLine(
                "Cadastre clientes, vendedores e veículos antes de agendar.");
            return;
        }

        ListarClientes();
        int idCliente = LerInteiro("Informe o ID do cliente: ");
        int indiceCliente = BuscarCliente(idCliente);

        if (indiceCliente == -1)
        {
            Console.WriteLine("Cliente não encontrado!");
            return;
        }

        ListarVendedores();
        int idVendedor = LerInteiro("Informe o ID do vendedor: ");
        int indiceVendedor = BuscarVendedor(idVendedor);

        if (indiceVendedor == -1)
        {
            Console.WriteLine("Vendedor não encontrado!");
            return;
        }

        Console.WriteLine("\nVEÍCULOS DISPONÍVEIS:");

        if (!ListarVeiculosDisponiveis())
        {
            Console.WriteLine("Não há veículos disponíveis para test-drive.");
            return;
        }

        int idVeiculo = LerInteiro("Informe o ID do veículo: ");
        int indiceVeiculo = BuscarVeiculo(idVeiculo);

        if (indiceVeiculo == -1 ||
            situacoes[indiceVeiculo] != "Disponível")
        {
            Console.WriteLine("Veículo não encontrado ou indisponível!");
            return;
        }

        DateTime dataHora = LerDataHoraFutura();

        int linha = qtdAgendamentos;

        // Armazena os dados na matriz de agendamentos
        agendamentos[linha, 0] = (qtdAgendamentos + 1).ToString();
        agendamentos[linha, 1] =
            dataHora.ToString("yyyy-MM-dd HH:mm:ss");
        agendamentos[linha, 2] = "Agendado";
        agendamentos[linha, 3] = idCliente.ToString();
        agendamentos[linha, 4] = idVendedor.ToString();
        agendamentos[linha, 5] = idVeiculo.ToString();

        qtdAgendamentos++;

        Console.WriteLine("Test-drive agendado com sucesso!");
    }

    // Consulta os agendamentos registrados
    static void ConsultarAgendamentos()
    {
        Console.WriteLine("\n--- AGENDAMENTOS DE TEST-DRIVE ---");

        if (qtdAgendamentos == 0)
        {
            Console.WriteLine("Nenhum agendamento registrado.");
            return;
        }

        for (int i = 0; i < qtdAgendamentos; i++)
        {
            int idCliente = int.Parse(agendamentos[i, 3]);
            int idVendedor = int.Parse(agendamentos[i, 4]);
            int idVeiculo = int.Parse(agendamentos[i, 5]);

            int indiceCliente = BuscarCliente(idCliente);
            int indiceVendedor = BuscarVendedor(idVendedor);
            int indiceVeiculo = BuscarVeiculo(idVeiculo);

            DateTime dataHora = DateTime.ParseExact(
                agendamentos[i, 1],
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture);

            Console.WriteLine($"\nAgendamento: {agendamentos[i, 0]}");
            Console.WriteLine($"Cliente: {nomesClientes[indiceCliente]}");
            Console.WriteLine($"Vendedor: {nomesVendedores[indiceVendedor]}");
            Console.WriteLine(
                $"Veículo: {marcas[indiceVeiculo]} {modelos[indiceVeiculo]}");
            Console.WriteLine($"Data e hora: {dataHora:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Status: {agendamentos[i, 2]}");
        }
    }

    // Registra uma venda
    static void RegistrarVenda()
    {
        Console.WriteLine("\n--- REGISTRO DE VENDA ---");

        if (qtdVendas >= MAX_VENDAS)
        {
            Console.WriteLine("Limite de vendas atingido!");
            return;
        }

        if (qtdClientes == 0 || qtdVendedores == 0 || qtdVeiculos == 0)
        {
            Console.WriteLine(





                "Cadastre clientes, vendedores e veículos antes de vender.");
            return;
        }

        ListarClientes();
        int idCliente = LerInteiro("Informe o ID do cliente: ");
        int indiceCliente = BuscarCliente(idCliente);

        if (indiceCliente == -1)
        {
            Console.WriteLine("Cliente não encontrado!");
            return;
        }

        ListarVendedores();
        int idVendedor = LerInteiro("Informe o ID do vendedor: ");
        int indiceVendedor = BuscarVendedor(idVendedor);

        if (indiceVendedor == -1)
        {
            Console.WriteLine("Vendedor não encontrado!");
            return;
        }

        Console.WriteLine("\nVEÍCULOS DISPONÍVEIS:");

        if (!ListarVeiculosDisponiveis())
        {
            Console.WriteLine("Não há veículos disponíveis para venda.");
            return;
        }

        int idVeiculo = LerInteiro("Informe o ID do veículo: ");
        int indiceVeiculo = BuscarVeiculo(idVeiculo);

        if (indiceVeiculo == -1 ||
            situacoes[indiceVeiculo] != "Disponível")
        {
            Console.WriteLine("Veículo não encontrado ou indisponível!");
            return;
        }

        string formaPagamento = LerTexto("Forma de pagamento: ");

        Console.WriteLine(
            $"Valor da venda: {precos[indiceVeiculo].ToString("C", culturaBR)}");

        string confirmacao;

        // Confirmação da venda
        do
        {
            Console.Write("Confirma a venda? (S/N): ");
            confirmacao = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (confirmacao != "S" && confirmacao != "N")
            {
                Console.WriteLine("Digite S para confirmar ou N para cancelar.");
            }

        } while (confirmacao != "S" && confirmacao != "N");

        if (confirmacao == "S")
        {
            int linha = qtdVendas;

            // Armazena os dados da venda na matriz
            vendas[linha, 0] = (qtdVendas + 1).ToString();
            vendas[linha, 1] = DateTime.Now.ToString("yyyy-MM-dd");
            vendas[linha, 2] =
                precos[indiceVeiculo].ToString(CultureInfo.InvariantCulture);
            vendas[linha, 3] = formaPagamento;
            vendas[linha, 4] = "Concluída";
            vendas[linha, 5] = idCliente.ToString();
            vendas[linha, 6] = idVendedor.ToString();
            vendas[linha, 7] = idVeiculo.ToString();

            qtdVendas++;

            // Soma o valor da venda ao total
            totalVendas += precos[indiceVeiculo];

            // Atualiza a situação do veículo
            situacoes[indiceVeiculo] = "Vendido";

            Console.WriteLine("Venda registrada com sucesso!");
        }
        else
        {
            Console.WriteLine("Venda cancelada.");
        }
    }

    // Consulta o histórico de vendas
    static void ConsultarVendas()
    {
        Console.WriteLine("\n--- HISTÓRICO DE VENDAS ---");

        if (qtdVendas == 0)
        {
            Console.WriteLine("Nenhuma venda registrada.");
            return;
        }

        for (int i = 0; i < qtdVendas; i++)
        {
            int idCliente = int.Parse(vendas[i, 5]);
            int idVendedor = int.Parse(vendas[i, 6]);
            int idVeiculo = int.Parse(vendas[i, 7]);

            int indiceCliente = BuscarCliente(idCliente);
            int indiceVendedor = BuscarVendedor(idVendedor);
            int indiceVeiculo = BuscarVeiculo(idVeiculo);

            DateTime dataVenda = DateTime.ParseExact(
                vendas[i, 1],
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture);

            decimal valor = decimal.Parse(
                vendas[i, 2],
                CultureInfo.InvariantCulture);

            Console.WriteLine($"\nVenda: {vendas[i, 0]}");
            Console.WriteLine($"Cliente: {nomesClientes[indiceCliente]}");
            Console.WriteLine($"Vendedor: {nomesVendedores[indiceVendedor]}");
            Console.WriteLine(
                $"Veículo: {marcas[indiceVeiculo]} {modelos[indiceVeiculo]}");
            Console.WriteLine($"Data: {dataVenda:dd/MM/yyyy}");
            Console.WriteLine($"Valor: {valor.ToString("C", culturaBR)}");
            Console.WriteLine($"Pagamento: {vendas[i, 3]}");
            Console.WriteLine($"Status: {vendas[i, 4]}");
        }

        Console.WriteLine("\n-----------------------------------");
        Console.WriteLine($"Quantidade de vendas: {qtdVendas}");
        Console.WriteLine(
            $"TOTAL DE VENDAS: {totalVendas.ToString("C", culturaBR)}");
    }
}
