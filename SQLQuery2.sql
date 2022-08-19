CREATE TABLE Veiculo
(
	Id int IDENTITY (1,1) NOT NULL,
	Fabricante varchar(255) NOT NULL,
	Modelo varchar(255) NOT NULL,
	Placa varchar(255) NOT NULL,
	Km varchar(255) NOT NULL,
	IdCliente int NOT NULL,
	CONSTRAINT Pk_Veiculo PRIMARY KEY (Id),
	
);

CREATE TABLE Cliente
(
	Id int IDENTITY (1,1) NOT NULL,
	Nome varchar(255) NOT NULL,
	Endereço varchar(255) NOT NULL,
	Telefone varchar(255) NOT NULL,
	CNH varchar(255) NOT NULL,
	IdVeiculo int NOT NULL,
	CONSTRAINT Pk_Cliente PRIMARY KEY (Id),
	CONSTRAINT Fk_Id_Veiculo FOREIGN KEY (IdVeiculo) REFERENCES Veiculo(Id)
);

CREATE TABLE Aluguel
(
	Id int IDENTITY (1,1) NOT NULL,
	IdVeiculo int NOT NULL,
	IdCliente int NOT NULL,
	Periodo varchar(255) NOT NULL,
	Valor varchar(255) NOT NULL,


	CONSTRAINT Pk_Aluguel PRIMARY KEY (Id),
	CONSTRAINT Fk_Id_Veiculo FOREIGN KEY (IdVeiculo) REFERENCES Veiculo(Id),
	CONSTRAINT Fk_Id_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(Id)
);

CREATE TABLE Funcionario
(
	Id int IDENTITY (1,1) NOT NULL,
	Nome varchar(255) NOT NULL,
	Funcao varchar(255) NOT NULL,
	IdAluguel int NOT NULL,
	
	CONSTRAINT Pk_Funcionario PRIMARY KEY (Id),
	CONSTRAINT Fk_Id_Aluguel FOREIGN KEY (IdAluguel) REFERENCES Aluguel(Id)
);
