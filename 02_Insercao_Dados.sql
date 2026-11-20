
-- PROJETO: MIRAI MOTORS
-- ARQUIVO: 02_Insercao_Dados.sql
-- DESCRIÇÃO: Inserção de dados para testes do sistema.

USE mirai_motors;

-- Inserindo clientes
INSERT INTO Cliente (nome, telefone, email) VALUES
('Mariana Souza', '11987654321', 'mariana@email.com'),
('Pedro Santos', '11976543210', 'pedro@email.com'),
('Ana Oliveira', '11965432109', 'ana@email.com');

-- Inserindo vendedores
INSERT INTO Vendedor (nome, telefone) VALUES
('Carlos Mendes', '11988887777'),
('Juliana Costa', '11977776666');

-- Inserindo veículos
INSERT INTO Veiculo (marca, modelo, ano, preco, situacao) VALUES
('Toyota', 'Corolla', 2025, 145000.00, 'Vendido'),
('Honda', 'Civic', 2024, 138000.00, 'Disponível'),
('Hyundai', 'HB20', 2025, 85000.00, 'Disponível'),
('Volkswagen', 'T-Cross', 2025, 132000.00, 'Disponível');

-- Inserindo uma venda
INSERT INTO Venda (
    dataVenda, valor, formaPagamento, statusVenda,
    idCliente, idVendedor, idVeiculo
) VALUES (
    '2026-11-14', 145000.00, 'Financiamento', 'Concluída',
    1, 1, 1
);

-- Inserindo agendamentos de test drive
INSERT INTO Agendamento_Test_Drive (
    dataHora, status, idCliente, idVendedor, idVeiculo
) VALUES
('2026-11-16 10:00:00', 'Agendado', 2, 1, 2),
('2026-11-17 14:30:00', 'Agendado', 3, 2, 3);

