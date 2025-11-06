# Uma-Boa-Comida

## Descrição
Este repositório contém todo o código-fonte do projeto **Uma Boa Comida**, incluindo backend, frontend.

**Repositório:** [https://github.com/CaioHOF/Uma-Boa-Comida](https://github.com/CaioHOF/Uma-Boa-Comida)

- Estrutura de pastas clara, facilitando manutenção e entendimento.  
- Scripts de criação de banco e seeds estão na pasta `Scripts` ou equivalente.

---

## Stack
- **Back-end:** C#  
- **Front-end:** XAML  
- **Banco de dados:** PostgreSQL  / arquivosLocais

---

## Estrutura do Projeto (MVVM)

O projeto segue o **padrão MVVM (Model-View-ViewModel)**.  

### Componentes:

1. **Model**  
   - Contém as classes que representam os dados do sistema, geralmente mapeadas para as tabelas do banco de dados.  
   - Exemplo: `Funcionario`, `Produto`, `Pedido`, `Cliente`.

2. **View**  
   - Representa a interface do usuário (UI).  
   - Criada com **XAML**, cada página (`.xaml`) tem sua lógica visual e elementos de interface.  
   - Exemplo: `FuncionarioMainPage.xaml`, `AdminMainPage.xaml`.

3. **ViewModel**  
   - Faz a ligação entre **View** e **Model**, expondo propriedades e comandos para a interface.  
   - Responsável por atualizar a UI quando os dados mudam e por processar ações do usuário.  
   - Evita que a View acesse diretamente o Model, mantendo a separação de responsabilidades.

### Fluxo de dados:
View <----> ViewModel <----> Model <----> Banco de Dados
- A **View** consome propriedades e comandos do **ViewModel** via **Data Binding**.  
- O **ViewModel** manipula os dados do **Model** e notifica a View sobre mudanças.  
- O **Model** representa os dados puros do sistema e pode conter regras de negócio.

---

## Como executar localmente

### 1. Instalar dependências
- Visual Studio 2022 (ou mais recente) com suporte a **.NET MAUI**  

### 2. Executar o projeto
- Abra a pasta do programa no Visual Studio
- No Visual Studio, clique em **Run** ou **Start Debugging**  
- O aplicativo será iniciado localmente, conectado ao banco com os dados iniciais  

---

Temos um banco de dados feito e pronto para implementação:
## Scripts do banco de dados

### Criação das tabelas e seeders

```sql
CREATE TABLE Estoque
(
  id_estoque SERIAL PRIMARY KEY,
  dataUltimaVerificacao DATE NOT NULL,
  capacidadeDoEstoque REAL NOT NULL
);

CREATE TABLE HistoricoAdministrativoDiario
(
  id_HistoricoAdministrador SERIAL PRIMARY KEY,
  horaEntrada TIMESTAMP NOT NULL,
  horaSaida TIMESTAMP NOT NULL,
  observacoes TEXT NOT NULL
);

CREATE TABLE Empresa
(
  id_empresa SERIAL PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  cnpj VARCHAR(20) NOT NULL,
  email VARCHAR(100) NOT NULL,
  telefone VARCHAR(20) NOT NULL,
  contratosExtras VARCHAR(100) NOT NULL,
  descricao VARCHAR(600) NOT NULL
);

CREATE TABLE Avaliacao
(
  id_Avaliacao SERIAL PRIMARY KEY,
  nota INT NOT NULL,
  comentario VARCHAR(300) NOT NULL,
  dataAvaliacao DATE NOT NULL
);

CREATE TABLE Receita
(
  id_Receita SERIAL PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  descricao VARCHAR(1000) NOT NULL,
  instrucoes TEXT NOT NULL,
  rendimentoPorcoes REAL NOT NULL,
  tempoPreparoMin INT NOT NULL
);

CREATE TABLE Restaurante
(
  id_Restaurante SERIAL PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  local VARCHAR(100) NOT NULL,
  cnpj VARCHAR(20) NOT NULL,
  precoImposto REAL NOT NULL,
  custoMensalGeral REAL NOT NULL,
  id_estoque INT NOT NULL REFERENCES Estoque(id_estoque)
);

CREATE TABLE Funcionario
(
  id_Funcionario SERIAL PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  idade INT NOT NULL,
  cpf VARCHAR(20) NOT NULL,
  salario REAL NOT NULL,
  bonusAReceber REAL NOT NULL,
  ativoAtualmente BOOLEAN NOT NULL,
  estaTrabalhando BOOLEAN NOT NULL,
  id_Restaurante INT NOT NULL REFERENCES Restaurante(id_Restaurante)
);

CREATE TABLE Administrador
(
  id_Funcionario INT PRIMARY KEY REFERENCES Funcionario(id_Funcionario),
  ultimoLogin TIMESTAMP NOT NULL
);

CREATE TABLE Cozinheiro
(
  id_Funcionario INT PRIMARY KEY REFERENCES Funcionario(id_Funcionario),
  especialidade VARCHAR(100) NOT NULL
);

CREATE TABLE Atendente
(
  id_Funcionario INT PRIMARY KEY REFERENCES Funcionario(id_Funcionario),
  linguasFaladas VARCHAR(100) NOT NULL
);

CREATE TABLE EtiquetaContrato
(
  id_EtiquetaContrato SERIAL PRIMARY KEY,
  id_Funcionario INT NOT NULL REFERENCES Administrador(id_Funcionario),
  id_HistoricoAdministrador INT NOT NULL REFERENCES HistoricoAdministrativoDiario(id_HistoricoAdministrador),
  inicioContrato DATE NOT NULL,
  fimPrevisto DATE NOT NULL,
  dataRenovacao DATE NOT NULL,
  salarioBase REAL NOT NULL,
  beneficios REAL NOT NULL,
  contratoAtivo BOOLEAN NOT NULL,
  jornadaSemanal VARCHAR(100) NOT NULL,
  periodoExperiencia VARCHAR(100) NOT NULL,
  descricao VARCHAR(600) NOT NULL,
  cargo VARCHAR(100) NOT NULL,
  tipoDoContrato VARCHAR(100) NOT NULL
);

CREATE TABLE EtiquetaReciboCompra
(
  id_EtiquetaRecibo SERIAL PRIMARY KEY,
  id_Funcionario INT NOT NULL REFERENCES Administrador(id_Funcionario),
  id_empresa INT NOT NULL REFERENCES Empresa(id_empresa),
  descricao VARCHAR(600) NOT NULL,
  custoTotal REAL NOT NULL,
  quantidade INT NOT NULL,
  tipoQuantidade VARCHAR(100) NOT NULL,
  dataCompra DATE NOT NULL,
  tipo VARCHAR(100) NOT NULL
);

CREATE TABLE Produto
(
  id_produto SERIAL PRIMARY KEY,
  id_empresa INT NOT NULL REFERENCES Empresa(id_empresa),
  id_EtiquetaRecibo INT NOT NULL REFERENCES EtiquetaReciboCompra(id_EtiquetaRecibo),
  id_estoque INT NOT NULL REFERENCES Estoque(id_estoque),
  nome VARCHAR(100) NOT NULL,
  descricao VARCHAR(600) NOT NULL,
  tipo VARCHAR(100) NOT NULL,
  perecivel BOOLEAN NOT NULL,
  dataValidade DATE NOT NULL,
  medidaQuantidade VARCHAR(30) NOT NULL,
  quantidade INT NOT NULL,
  quantidadeEmEstoque INT NOT NULL,
  quantidadeEmEstoqueEstimada INT NOT NULL,
  valorUnitarioMedio REAL NOT NULL,
  custoUnitario REAL NOT NULL
);

-- ALTERAÇÃO: EtiquetaDemissao agora referencia Funcionario
CREATE TABLE EtiquetaDemissao
(
  id_EtiquetaDemissao SERIAL PRIMARY KEY,
  id_Funcionario INT NOT NULL REFERENCES Funcionario(id_Funcionario),
  id_HistoricoAdministrador INT NOT NULL REFERENCES HistoricoAdministrativoDiario(id_HistoricoAdministrador),
  descricao VARCHAR(300) NOT NULL,
  motivo VARCHAR(100) NOT NULL,
  dataDemissao DATE NOT NULL,
  tipoDemissao VARCHAR(100) NOT NULL,
  valorRescisao REAL NOT NULL,
  dataAvisoPrevio DATE NOT NULL
);

CREATE TABLE HistoricoCozinheiroDiario
(
  id_HistoricoCozinheiro SERIAL PRIMARY KEY,
  id_Funcionario INT NOT NULL REFERENCES Cozinheiro(id_Funcionario),
  horaEntrada TIMESTAMP NOT NULL,
  horaSaida TIMESTAMP NOT NULL,
  observacoes TEXT NOT NULL
);

CREATE TABLE HistoricoAtendenteDiario
(
  id_HistoricoAtendente SERIAL PRIMARY KEY,
  id_Funcionario INT NOT NULL REFERENCES Atendente(id_Funcionario),
  horaEntrada TIMESTAMP NOT NULL,
  horaSaida TIMESTAMP NOT NULL,
  observacoes TEXT NOT NULL
);

CREATE TABLE Cliente
(
  id_Cliente SERIAL PRIMARY KEY,
  id_Avaliacao INT NOT NULL REFERENCES Avaliacao(id_Avaliacao),
  nome VARCHAR(100) NOT NULL,
  cpf VARCHAR(20) NOT NULL,
  email VARCHAR(100) NOT NULL,
  telefone VARCHAR(20) NOT NULL
);

CREATE TABLE Pedido
(
  id_Pedido SERIAL PRIMARY KEY,
  id_HistoricoCozinheiro INT NOT NULL REFERENCES HistoricoCozinheiroDiario(id_HistoricoCozinheiro),
  id_HistoricoAtendente INT NOT NULL REFERENCES HistoricoAtendenteDiario(id_HistoricoAtendente),
  id_Avaliacao INT NOT NULL REFERENCES Avaliacao(id_Avaliacao),
  id_Cliente INT NOT NULL REFERENCES Cliente(id_Cliente),
  detalhes VARCHAR(100) NOT NULL,
  status VARCHAR(30) NOT NULL,
  dataPedido DATE NOT NULL
);

CREATE TABLE Demanda
(
  id_produto INT NOT NULL REFERENCES Produto(id_produto),
  id_Pedido INT NOT NULL REFERENCES Pedido(id_Pedido),
  PRIMARY KEY (id_produto, id_Pedido)
);

CREATE TABLE Usa
(
  id_produto INT NOT NULL REFERENCES Produto(id_produto),
  id_Receita INT NOT NULL REFERENCES Receita(id_Receita),
  PRIMARY KEY (id_produto, id_Receita)
);

CREATE TABLE UsaReceitaPedido
(
  id_Pedido INT NOT NULL REFERENCES Pedido(id_Pedido),
  id_Receita INT NOT NULL REFERENCES Receita(id_Receita),
  PRIMARY KEY (id_Pedido, id_Receita)
);




INSERT INTO Estoque (id_estoque, dataUltimaVerificacao, capacidadeDoEstoque) VALUES
(1, '2025-11-01', 1000.0);

INSERT INTO Empresa (id_empresa, nome, cnpj, email, telefone, contratosExtras, descricao) VALUES
(1, 'Uma Boa Comida', '12.345.678/0001-99', 'contato@umaboacomida.com', '11999998888', 'Contrato de limpeza', 'Empresa focada em comida caseira de qualidade.');

INSERT INTO Avaliacao (id_Avaliacao, nota, comentario, dataAvaliacao) VALUES
(1, 5, 'Excelente atendimento', '2025-10-20'),
(2, 3, 'Ok, poderia melhorar', '2025-10-22');

INSERT INTO Receita (id_Receita, nome, descricao, instrucoes, rendimentoPorcoes, tempoPreparoMin) VALUES
(1, 'Lasanha', 'Lasanha de carne e queijo', 'Montar camadas e assar 40 minutos', 6, 50),
(2, 'Sushi', 'Sushi de salmão', 'Preparar arroz, enrolar com salmão e cortar', 8, 30);

INSERT INTO Restaurante (id_Restaurante, nome, local, cnpj, precoImposto, custoMensalGeral, id_estoque) VALUES
(1, 'Uma Boa Comida', 'São Paulo', '12.345.678/0001-99', 1.2, 20000.0, 1);

INSERT INTO Funcionario (id_Funcionario, nome, idade, cpf, salario, bonusAReceber, ativoAtualmente, estaTrabalhando, id_Restaurante) VALUES
(1, 'Carlos Silva', 35, '123.456.789-00', 3000.0, 500.0, TRUE, TRUE, 1),
(2, 'Ana Pereira', 28, '987.654.321-00', 2500.0, 300.0, TRUE, TRUE, 1);

INSERT INTO Administrador (id_Funcionario, ultimoLogin) VALUES
(1, '2025-11-02 09:00:00');

INSERT INTO Cozinheiro (id_Funcionario, especialidade) VALUES
(2, 'Culinária japonesa');

INSERT INTO Atendente (id_Funcionario, linguasFaladas) VALUES
(1, 'Português, Inglês');

INSERT INTO HistoricoAdministrativoDiario (id_HistoricoAdministrador, horaEntrada, horaSaida, observacoes) VALUES
(1, '2025-11-01 08:00:00', '2025-11-01 17:00:00', 'Turno normal'),
(2, '2025-11-02 08:30:00', '2025-11-02 16:30:00', 'Turno parcial');

INSERT INTO EtiquetaContrato (id_EtiquetaContrato, id_Funcionario, id_HistoricoAdministrador, inicioContrato, fimPrevisto, dataRenovacao, salarioBase, beneficios, contratoAtivo, jornadaSemanal, periodoExperiencia, descricao, cargo, tipoDoContrato) VALUES
(1, 1, 1, '2025-01-01', '2025-12-31', '2025-06-01', 3000.0, 500.0, TRUE, '40h', '3 meses', 'Contrato anual de administrador', 'Administrador', 'CLT');

INSERT INTO EtiquetaReciboCompra (id_EtiquetaRecibo, id_Funcionario, id_empresa, descricao, custoTotal, quantidade, tipoQuantidade, dataCompra, tipo) VALUES
(1, 1, 1, 'Compra de massas e queijos', 1200.0, 50, 'kg', '2025-11-01', 'Ingrediente'),
(2, 1, 1, 'Compra de peixes frescos', 2000.0, 30, 'kg', '2025-11-02', 'Ingrediente');

INSERT INTO Produto (id_produto, id_empresa, id_EtiquetaRecibo, id_estoque, nome, descricao, tipo, perecivel, dataValidade, medidaQuantidade, quantidade, quantidadeEmEstoque, quantidadeEmEstoqueEstimada, valorUnitarioMedio, custoUnitario) VALUES
(1, 1, 1, 1, 'Massa Lasanha', 'Massa fresca para lasanha', 'Ingrediente', TRUE, '2025-12-01', 'Pacote', 50, 50, 50, 10.0, 8.0),
(2, 1, 2, 1, 'Salmão', 'Salmão fresco para sushi', 'Ingrediente', TRUE, '2025-11-05', 'kg', 30, 30, 30, 50.0, 45.0);

INSERT INTO EtiquetaDemissao (id_EtiquetaDemissao, id_Funcionario, id_HistoricoAdministrador, descricao, motivo, dataDemissao, tipoDemissao, valorRescisao, dataAvisoPrevio) VALUES
(1, 2, 2, 'Demissão voluntária', 'Mudança de cidade', '2025-11-01', 'Voluntária', 2500.0, '2025-10-25');

INSERT INTO HistoricoCozinheiroDiario (id_HistoricoCozinheiro, id_Funcionario, horaEntrada, horaSaida, observacoes) VALUES
(1, 2, '2025-11-01 09:00:00', '2025-11-01 17:00:00', 'Preparou sushi para almoço');

INSERT INTO HistoricoAtendenteDiario (id_HistoricoAtendente, id_Funcionario, horaEntrada, horaSaida, observacoes) VALUES
(1, 1, '2025-11-01 08:00:00', '2025-11-01 16:00:00', 'Atendeu clientes no salão');

INSERT INTO Cliente (id_Cliente, id_Avaliacao, nome, cpf, email, telefone) VALUES
(1, 1, 'João Santos', '111.222.333-44', 'joao@gmail.com', '11955554444'),
(2, 2, 'Maria Oliveira', '555.666.777-88', 'maria@gmail.com', '11966667777');

INSERT INTO Pedido (id_Pedido, id_HistoricoCozinheiro, id_HistoricoAtendente, id_Avaliacao, id_Cliente, detalhes, status, dataPedido) VALUES
(1, 1, 1, 1, 1, 'Pedido de lasanha', 'Concluído', '2025-11-01'),
(2, 1, 1, 2, 2, 'Pedido de sushi', 'Pendente', '2025-11-02');

INSERT INTO Demanda (id_produto, id_Pedido) VALUES
(1, 1),
(2, 2);

INSERT INTO Usa (id_produto, id_Receita) VALUES
(1, 1),
(2, 2);

INSERT INTO UsaReceitaPedido (id_Pedido, id_Receita) VALUES
(1, 1),
(2, 2);
