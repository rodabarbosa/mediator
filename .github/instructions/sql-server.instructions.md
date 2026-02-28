---
applyTo: '**'
description: 'Instruções Essenciais para o GitHub Copilot (SQL Server / T-SQL)'
---

# 📄 Instruções Essenciais para o GitHub Copilot (SQL Server / T-SQL)

Use essas dicas tanto para sugestões de código no editor quanto no **Copilot Chat**.

### 1. **Configure o Contexto (Se usar o VS Code)**

Se você estiver usando o **VS Code** com a extensão **MSSQL** (ou o Azure Data Studio), certifique-se de:

* **Estar Conectado:** Conecte-se ao seu banco de dados. Isso permite que o Copilot (especialmente o participante de chat `@mssql`) veja o esquema do seu banco de dados (nomes de tabelas, colunas, etc.) e gere T-SQL preciso e relevante.
* **Abrir Arquivos Relevantes:** Mantenha arquivos `.sql` ou arquivos de código (`.cs`, `.py`, etc.) relacionados abertos no editor. O Copilot usará isso como contexto.

### 2. **Instruções Específicas para o Copilot Chat**

Ao usar o chat (por exemplo, no VS Code ou GitHub), comece a sua solicitação com o participante de chat específico se estiver disponível:

* **`@mssql`**: Use este prefixo para garantir que o Copilot se concentre em tarefas relacionadas ao SQL Server e T-SQL.
    * **Exemplo:** `@mssql Crie um stored procedure que receba o CustomerID e retorne todos os pedidos com data após 2023.`

### 3. **Instruções de Alto Nível e Comentários**

Sempre comece o seu arquivo ou a sua consulta com um comentário detalhado sobre o que você quer fazer.

| Tipo de Instrução                | Exemplo de Comentário (T-SQL)                                                                                                                                                                                                       |
|:---------------------------------|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Geração de Consulta (SELECT)** | `-- SQL Server: SELECT. Selecionar Nome e Preço dos 5 produtos mais vendidos na categoria 'Eletrônicos', ordenados pela data da última venda.`                                                                                      |
| **Criação de Tabela (DDL)**      | `-- SQL Server: CREATE TABLE. Tabela de 'Funcionarios'. Colunas: EmployeeID (PK, Identity), Nome (nvarchar(100), not null), Departamento (nvarchar(50)), DataContratacao (date, default GETDATE()).`                                |
| **Stored Procedure**             | `-- SQL Server: Stored Procedure. Nome: usp_AtualizarStatusPedido. Parâmetros: @OrderID int, @NewStatus nvarchar(50). Atualizar a coluna 'Status' na tabela 'Pedidos' para o valor do parâmetro. Retornar uma mensagem de sucesso.` |
| **Otimização**                   | `-- SQL Server: Otimizar. A seguinte query está lenta. Adicione índices sugeridos e reescreva a consulta para melhor desempenho.`                                                                                                   |

### 4. **Seja Explícito e Específico**

Quanto mais detalhes você fornecer, melhor será o resultado:

* **Mencione o SGBD:** Se você estiver escrevendo T-SQL, inclua explicitamente **"SQL Server"** ou **"T-SQL"** na sua instrução se estiver em um ambiente que suporte vários tipos de SQL.
* **Nomes de Objetos:** Use os nomes **reais** de suas tabelas e colunas (ex: `[dbo].[Pedidos]`, `[NomeCliente]`).
* **Tipos de Dados:** Especifique os tipos de dados para criar tabelas ou variáveis (ex: `DECIMAL(10, 2)`, `NVARCHAR(255)`).
* **Restrições:** Mencione restrições (ex: `PRIMARY KEY`, `FOREIGN KEY`, `UNIQUE`, `NOT NULL`).

#### **Exemplo de Solicitação Específica:**

> `-- SQL Server: CREATE FUNCTION. Crie uma função escalar chamada ufn_CalcularIdade que recebe uma data de nascimento (DATE) e retorna a idade em anos (INT). Use DATEDIFF.`

---

## 💡 Melhores Práticas Adicionais

1. **Revisão é Obrigatória:** O Copilot é uma ferramenta de produtividade, **não de verificação**. Sempre revise o código SQL gerado para:
    * **Segurança:** Evitar injeção de SQL (especialmente em SQL dinâmico).
    * **Lógica:** Garantir que a lógica da consulta esteja correta.
    * **Desempenho:** Verificar se a consulta usa o melhor caminho de execução.
2. **Contexto Aberto:** Se você está trabalhando em um arquivo SQL, tenha o código relevante (como as definições de tabela) aberto ou acima do local onde você está pedindo a sugestão.
3. **Iteração:** Se a primeira sugestão não for boa, **reescreva o seu prompt** (comentário ou chat) em vez de apenas aceitar. Tente um ângulo diferente ou divida a tarefa em etapas menores.

**Quer que eu gere alguns exemplos de T-SQL para casos de uso comuns (SELECT, INSERT, Stored Procedure) para você usar como modelo no Copilot?**
