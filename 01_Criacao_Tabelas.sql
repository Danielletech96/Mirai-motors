
-- PROJETO: MIRAI MOTORS
-- ARQUIVO: 01_Criacao_Tabelas.sql
-- DESCRIÇÃO: Criação do banco de dados e das cinco tabelas.

CREATE DATABASE IF NOT EXISTS mirai_motors;

USE mirai_motors;

-- Tabela de clientes
CREATE TABLE Cliente (
    idCliente INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    telefone VARCHAR(20), UNIQUE
    email VARCHAR(100) UNIQUE
) ENGINE=InnoDB;

-- Tabela de vendedores
CREATE TABLE Vendedor (
    idVendedor INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    telefone VARCHAR(20) UNIQUE
) ENGINE=InnoDB;

-- Tabela de veículos
CREATE TABLE Veiculo (
    idVeiculo INT AUTO_INCREMENT PRIMARY KEY,
    marca VARCHAR(50) NOT NULL,
    modelo VARCHAR(50) NOT NULL,
    ano INT NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    situacao VARCHAR(20) NOT NULL
) ENGINE=InnoDB;

-- Tabela de vendas
CREATE TABLE Venda (
    idVenda INT AUTO_INCREMENT PRIMARY KEY,
    dataVenda DATE NOT NULL,
    valor DECIMAL(10,2) NOT NULL,
    formaPagamento VARCHAR(40) NOT NULL,
    statusVenda VARCHAR(30) NOT NULL,
    idCliente INT NOT NULL,
    idVendedor INT NOT NULL,
    idVeiculo INT NOT NULL,

    CONSTRAINT fk_venda_cliente
        FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente),

    CONSTRAINT fk_venda_vendedor
        FOREIGN KEY (idVendedor) REFERENCES Vendedor(idVendedor),

    CONSTRAINT fk_venda_veiculo
        FOREIGN KEY (idVeiculo) REFERENCES Veiculo(idVeiculo),

    UNIQUE (idVeiculo)
) ENGINE=InnoDB;

-- Tabela de agendamentos de test drive
CREATE TABLE Agendamento_Test_Drive (
    idAgendamento INT AUTO_INCREMENT PRIMARY KEY,
    dataHora DATETIME NOT NULL,
    status VARCHAR(30) NOT NULL,
    idCliente INT NOT NULL,
    idVendedor INT NOT NULL,
    idVeiculo INT NOT NULL,

    CONSTRAINT fk_agendamento_cliente
        FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente),

    CONSTRAINT fk_agendamento_vendedor
        FOREIGN KEY (idVendedor) REFERENCES Vendedor(idVendedor),

    CONSTRAINT fk_agendamento_veiculo
        FOREIGN KEY (idVeiculo) REFERENCES Veiculo(idVeiculo)
) ENGINE=InnoDB;

