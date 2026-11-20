
-- PROJETO: MIRAI MOTORS
-- ARQUIVO: 03_Consultas.sql
-- DESCRIÇÃO: Consultas para visualizar informações do sistema.

USE mirai_motors;

-- CONSULTA 1: Histórico de vendas
SELECT
    Venda.idVenda,
    Cliente.nome AS Cliente,
    Vendedor.nome AS Vendedor,
    Veiculo.marca,
    Veiculo.modelo,
    Venda.dataVenda,
    Venda.valor,
    Venda.formaPagamento
FROM Venda
INNER JOIN Cliente ON Venda.idCliente = Cliente.idCliente
INNER JOIN Vendedor ON Venda.idVendedor = Vendedor.idVendedor
INNER JOIN Veiculo ON Venda.idVeiculo = Veiculo.idVeiculo;

-- CONSULTA 2: Veículos disponíveis
SELECT
    idVeiculo,
    marca,
    modelo,
    ano,
    preco,
    situacao
FROM Veiculo
WHERE situacao = 'Disponível';

-- CONSULTA 3: Agendamentos de test drive
SELECT
    Agendamento_Test_Drive.idAgendamento,
    Cliente.nome AS Cliente,
    Vendedor.nome AS Vendedor,
    Veiculo.marca,
    Veiculo.modelo,
    Agendamento_Test_Drive.dataHora,
    Agendamento_Test_Drive.status
FROM Agendamento_Test_Drive
INNER JOIN Cliente
    ON Agendamento_Test_Drive.idCliente = Cliente.idCliente
INNER JOIN Vendedor
    ON Agendamento_Test_Drive.idVendedor = Vendedor.idVendedor
INNER JOIN Veiculo
    ON Agendamento_Test_Drive.idVeiculo = Veiculo.idVeiculo;
