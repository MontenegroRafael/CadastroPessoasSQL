CREATE TABLE Clientes
(
	Id_Cliente int PRIMARY KEY NOT NULL,
	Nome varchar(200) NOT NULL,
	CNH varchar(50) NOT NULL,
	Data_Cadastro datetime NOT NULL,
	Login_Cadastro varchar(15) NOT NULL,
	
);
--SELECT * FROM Clientes; -- Mostra a tabela Cliente
--DROP TABLE Cliente; -- Apagar a tabelo toda Cliente

CREATE TABLE Veiculos
(
	Id_Veiculo int PRIMARY KEY NOT NULL,
	Modelo varchar(50) NOT NULL,
	Placa varchar(10) NOT NULL,
	Data_Cadastro datetime NOT NULL,
	Login_Cadastro varchar(15) NOT NULL,
	
);
--SELECT * FROM Veiculos; -- Mostra a tabela Veiculos
--DROP TABLE Veiculo; -- Apagar a tabelo toda Veiculos

CREATE TABLE Alugueis
(
	Id_Aluguel int PRIMARY KEY NOT NULL,
	Id_Cliente int NOT NULL,
	Id_Veiculo int NOT NULL,
	Data_Inicio date NOT NULL,
	Data_Fim date NOT NULL,
	Valor_Aluguel numeric(15,2) NOT NULL,
	Data_Cadastro datetime NOT NULL,
	Login_Cadastro varchar(15) NOT NULL,

);
--SELECT * FROM Alugueis; -- Mostra a tabela Alugueis
--DROP TABLE Alugueis; -- Apagar a tabelo toda Alugueis

ALTER TABLE Alugueis -- Altera a tabela Alugueis
	ADD CONSTRAINT FK_Cliente FOREIGN KEY (Id_Cliente) REFERENCES Clientes (Id_Cliente); -- Adiciona a CONSTRAINT
ALTER TABLE Alugueis -- Altera a tabela Alugueis
	ADD CONSTRAINT FK_Veiculos FOREIGN KEY (Id_Veiculo) REFERENCES Veiculos (Id_Veiculo);
ALTER TABLE Alugueis
	DROP CONSTRAINT FK_Cliente;

CREATE TABLE Controle_Frota
(
	Id_CFrota int PRIMARY KEY NOT NULL,
	Id_Aluguel int NOT NULL,
	--Id_Cliente int NOT NULL, 
	Id_Situacao int NOT NULL,
	Data_ControlFfrota date NOT NULL,
	Data_Cadastro datetime NOT NULL,
	Login_Cadastro varchar(15) NOT NULL,

);
--SELECT * FROM Controle_Frota; -- Mostra a tabela Controle_Frota
DROP TABLE Controle_Frota; -- Apagar a tabelo toda Controle_Frota

ALTER TABLE Controle_Frota
	ADD CONSTRAINT FK_AluguelCF FOREIGN KEY (Id_Aluguel) REFERENCES Alugueis (Id_Aluguel);
--ALTER TABLE Controle_Frota
	--ADD CONSTRAINT FK_ClienteCF FOREIGN KEY (Id_Cliente) REFERENCES Clientes (Id_Cliente);
ALTER TABLE Controle_Frota
	ADD CONSTRAINT FK_SituacaoCF FOREIGN KEY (Id_Situacao) REFERENCES Situacao (Id_Situacao);

CREATE TABLE Situacao
(
	Id_Situacao int PRIMARY KEY NOT NULL,
	Nome varchar(30) NOT NULL,
	Data_Cadastro datetime NOT NULL,
	Login_Cadastro varchar(15) NOT NULL,

);
--SELECT * FROM Situacao; -- Mostra a tabela Situacao
--DROP TABLE Situacao; -- Apagar a tabelo toda Situacao