CREATE TABLE Pessoa -- Criando Tabela Pessoa - Nome de tabela SEMPRE no singular.
(
	Id int IDENTITY (1,1) NOT NULL,
	Nome varchar(250) NOT NULL,
	Cpf varchar(14) NOT NULL,
	Rg varchar(9) NOT NULL,
	Datadenascimento date NOT NULL,
	Naturalidade varchar(50) NOT NULL,
	CONSTRAINT Pk_Pessoa PRIMARY KEY (Id), -- Constraint é uma regra 	
);

SELECT * FROM Pessoa -- mostrar a tabela

INSERT INTO Pessoa (Nome, Cpf, Rg, Datadenascimento, Naturalidade) VALUES('Rafael', '456128458-77', '451287-8', '04/05/1900', 'Salvador')
CREATE TABLE Telefone
(
	Id int IDENTITY (1,1) NOT NULL, -- incremento
	Numero varchar (20) NOT NULL,
	Ddd varchar (5) NOT NULL,
	IdPessoa int NOT NULL,
	CONSTRAINT Pk_Telefone PRIMARY KEY (Id), -- Constraint é uma regra 
	CONSTRAINT Fk_Id_Pessoa FOREIGN KEY (IdPessoa) REFERENCES Pessoa(Id)
);

SELECT * FROM Pessoa
SELECT * FROM Telefone -- mostrar a tabela  * Mostra tudo para Telefone
SELECT p.Id, p.Nome, t.Ddd, t.Numero FROM Pessoa p, Telefone t WHERE p.Id = t.IdPessoa -- Cunsulta para pegar todos os números
SELECT * FROM Pessoa p, Telefone t

SELECT * FROM Pessoa p LEFT JOIN Telefone t ON p.Id = t.IdPessoa

INSERT INTO Telefone (Numero, Ddd, IdPessoa) VALUES('1111-1111', '71', (SELECT Id FROM Pessoa WHERE Pessoa.Nome = 'Rafael'))