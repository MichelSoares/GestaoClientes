--Prova remota - CAPTA Tecnologia (12/03/2025)

CREATE TABLE TipoCliente (
    Id INT PRIMARY KEY,
    Descricao VARCHAR(100) NOT NULL
);

INSERT INTO TipoCliente (Id, Descricao) 
VALUES (1, 'Consumidor'), (2, 'Fornecedor'), (3, 'Credenciado'), (4, 'Parceiro'), (5, 'Revendedor');
--

CREATE TABLE SituacaoCliente (
    Id INT PRIMARY KEY,
    Descricao VARCHAR(100) NOT NULL
);

INSERT INTO SituacaoCliente (Id, Descricao)
VALUES (1, 'Ativo'), (2, 'Inativo'), (3, 'Bloqueado');
--

CREATE TABLE Sexo (
    Id INT PRIMARY KEY,
    Descricao NVARCHAR(50) NOT NULL
);

INSERT INTO Sexo (Id, Descricao)
VALUES (1, 'Masculino'), (2, 'Feminino'), (3, 'Outro');
--

CREATE TABLE Cliente (
    CPF CHAR(11) PRIMARY KEY,
    Nome VARCHAR(150) NOT NULL,
    SexoId INT NOT NULL,
    TipoClienteId INT NOT NULL,
    SituacaoClienteId INT NOT NULL,
    FOREIGN KEY (SexoId) REFERENCES Sexo(Id),
    FOREIGN KEY (TipoClienteId) REFERENCES TipoCliente(Id),
    FOREIGN KEY (SituacaoClienteId) REFERENCES SituacaoCliente(Id)
);

-----------------------------------------------

---endpoint /ListarClientes (já retornando JSON), 
SELECT c.CPF, c.Nome, c.SexoId AS Sexo,
    JSON_QUERY((SELECT tc.Id, tc.Descricao FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)) AS TipoCliente,
    JSON_QUERY((SELECT sc.Id, sc.Descricao FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)) AS SituacaoCliente FROM Cliente c
JOIN TipoCliente tc ON c.TipoClienteId = tc.Id
JOIN SituacaoCliente sc ON c.SituacaoClienteId = sc.Id
FOR JSON PATH;
