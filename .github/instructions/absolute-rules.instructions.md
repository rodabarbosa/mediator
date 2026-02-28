---
applyTo: '**/*'
description: 'Regras absolutas e obrigatórias para todos os agentes, prompts e modos do GitHub Copilot neste repositório.'
---

# 🚨 Regras Absolutas para Todos os Agentes e Prompts

Este arquivo define **regras obrigatórias e invioláveis** que todos os agentes, prompts e modos do GitHub Copilot **DEVEM** seguir ao interagir com este repositório.

**ATENÇÃO:** Estas regras têm **precedência máxima** sobre quaisquer outras instruções.

---

## 📋 Fluxo de Execução Obrigatório

Todo agente **DEVE** seguir este fluxo sequencial antes de executar qualquer tarefa:

### Fase 1: Entendimento do Contexto

| Passo | Ação                                                                                                                     | Obrigatório |
|-------|--------------------------------------------------------------------------------------------------------------------------|-------------|
| 1     | Verificar o `README.md` para entender o projeto, sua estrutura e objetivos.                                              | ✅ SIM       |
| 2     | Verificar o `AGENTS.md` e o `copilot-instructions.md` para garantir o comportamento desejado pelo usuário.               | ✅ SIM       |
| 3     | Entender a demanda por completo. **Se houver dúvidas, questione o usuário antes de prosseguir.**                         | ✅ SIM       |
| 4     | Verificar se existem instruções adicionais aplicáveis em `.github/instructions/`, `Doc/` ou via MCP.                     | ✅ SIM       |
| 5     | Escanear a base de código se necessário para entender o contexto técnico e padrões existentes.                           | ✅ SIM       |

### Fase 2: Planejamento

| Passo | Ação                                                                                                                     | Obrigatório |
|-------|--------------------------------------------------------------------------------------------------------------------------|-------------|
| 6     | Planejar como executar a tarefa, considerando impactos e dependências.                                                   | ✅ SIM       |
| 7     | Dividir em pequenas tarefas mensuráveis e rastreáveis.                                                                   | ✅ SIM       |
| 8     | Registrar o plano para que possa ser retomado ou desfeito caso algo dê errado ou assim seja demandado.                   | ✅ SIM       |

### Fase 3: Execução e Validação

| Passo | Ação                                                                                                                     | Obrigatório |
|-------|--------------------------------------------------------------------------------------------------------------------------|-------------|
| 9     | Executar as tarefas conforme planejado, uma de cada vez.                                                                 | ✅ SIM       |
| 10    | Ao término, verificar se **todas** as tarefas foram executadas para garantir a entrega completa.                         | ✅ SIM       |
| 11    | Apresentar um resumo claro do que foi feito, incluindo arquivos criados/modificados e próximos passos (se houver).       | ✅ SIM       |

---

## 🗣️ Regras de Comunicação

| Regra                                                                                                   | Obrigatório |
|---------------------------------------------------------------------------------------------------------|-------------|
| As perguntas ao usuário **DEVEM** ser feitas **uma por vez** e de forma **progressiva**.                | ✅ SIM       |
| Todos os processos (interações, perguntas, saídas, logs, etc.) **DEVEM** ser em **português**.          | ✅ SIM       |
| Código (identificadores, classes, métodos) **DEVE** ser em **inglês**.                                  | ✅ SIM       |
| Comentários XML e documentação **DEVEM** ser em **português**.                                          | ✅ SIM       |

---

## 🧪 Regras de Testes Unitários (XUnit)

| Regra                                                                                                   | Obrigatório |
|---------------------------------------------------------------------------------------------------------|-------------|
| A criação de testes unitários usando **XUnit** é **OBRIGATÓRIA** para qualquer código novo ou alterado. | ✅ SIM       |
| Cobertura de código **MÍNIMA**: **90%**.                                                                | ✅ SIM       |
| Cobertura de código **IDEAL**: **100%**.                                                                | ✅ SIM       |
| Testes **DEVEM** cobrir cenários de sucesso, falha e edge cases.                                        | ✅ SIM       |
| Testes **DEVEM** ser isolados e não depender de estado externo.                                         | ✅ SIM       |
| Usar mocks (Moq, NSubstitute) para isolar dependências.                                                 | ✅ SIM       |
| Testes com erros são **INADMISSÍVEIS** — todos os testes devem passar antes de considerar a tarefa concluída. | ✅ SIM       |
| Usar **dotCover** para análise de cobertura de código.                                                  | ✅ SIM       |

### Padrão de Nomenclatura de Testes

```
[MétodoSobTeste]_[Cenário]_[ResultadoEsperado]
```

**Exemplo:**
```csharp
public async Task HandleAsync_WithValidId_ReturnsProcessoAdministrativo()
public async Task HandleAsync_WithInvalidId_ReturnsNotFound()
public async Task HandleAsync_WithNullId_ThrowsArgumentNullException()
```

---

## 📝 Regras de Documentação XML

| Regra                                                                                                   | Obrigatório |
|---------------------------------------------------------------------------------------------------------|-------------|
| Documentação XML de métodos públicos é **ABSOLUTAMENTE OBRIGATÓRIA**.                                   | ✅ SIM       |
| Documentação **DEVE** estar em **português**.                                                           | ✅ SIM       |
| Usar `<summary>` para descrever **o que** o método faz.                                                 | ✅ SIM       |
| Usar `<remarks>` para explicar **o porquê** ou detalhes adicionais.                                     | ✅ SIM       |
| Usar `<param>` para documentar cada parâmetro.                                                          | ✅ SIM       |
| Usar `<returns>` para documentar o retorno.                                                             | ✅ SIM       |
| Usar `<exception>` para documentar exceções que podem ser lançadas.                                     | ✅ SIM       |

### Dispensa de Aprovação

| Ação                                                                                                    | Requer Aprovação |
|---------------------------------------------------------------------------------------------------------|------------------|
| Inclusão de documentação XML em código sem documentação                                                 | ❌ NÃO            |
| Atualização de documentação XML incompleta ou desatualizada                                             | ❌ NÃO            |
| Correção de erros em documentação XML existente                                                         | ❌ NÃO            |

> **Nota:** O agente pode adicionar ou corrigir documentação XML automaticamente sem solicitar aprovação do usuário, desde que siga os padrões estabelecidos.

### Exemplo de Documentação XML

```csharp
/// <summary>
/// Obtém um processo administrativo pelo seu identificador único.
/// </summary>
/// <remarks>
/// Este método realiza uma busca no banco de dados utilizando o ID fornecido.
/// Retorna null caso o processo não seja encontrado.
/// </remarks>
/// <param name="id">Identificador único do processo administrativo.</param>
/// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
/// <returns>O processo administrativo encontrado ou null se não existir.</returns>
/// <exception cref="ArgumentNullException">Lançada quando o ID é nulo ou vazio.</exception>
public async Task<ProcessoAdministrativo?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
```

---

## 📖 Regras de Documentação OpenAPI

| Regra                                                                                                   | Obrigatório |
|---------------------------------------------------------------------------------------------------------|-------------|
| Documentação OpenAPI é **OBRIGATÓRIA** para todos os endpoints.                                         | ✅ SIM       |
| Usar `.WithName()` para identificar o endpoint.                                                         | ✅ SIM       |
| Usar `.WithSummary()` para descrição curta.                                                             | ✅ SIM       |
| Usar `.WithDescription()` para detalhes e validações pertinentes ao uso do cliente.                     | ✅ SIM       |
| Usar `.Produces<T>()` para documentar **todos** os tipos de resposta possíveis.                         | ✅ SIM       |
| Usar `.WithOpenApi()` para enriquecer a documentação com exemplos e schemas.                            | ✅ SIM       |
| Documentar **validações**, **regras de negócio** e **casos de erro** na descrição.                      | ✅ SIM       |

### Exemplo de Endpoint Documentado

```csharp
group.MapGet("/{id:guid}", async (Guid id, IProcessoAdminGetHandler handler, CancellationToken cancellationToken) =>
{
    var result = await handler.HandleAsync(id, cancellationToken);
    return result.StatusCode switch
    {
        StatusCodes.Status200OK => Results.Ok(result),
        StatusCodes.Status404NotFound => Results.NotFound(result),
        _ => Results.InternalServerError(result)
    };
})
.WithName("GetProcessoAdministrativoById")
.WithSummary("Obtém um processo administrativo pelo ID")
.WithDescription("""
    Retorna os detalhes completos de um processo administrativo.

    **Validações:**
    - O ID deve ser um GUID válido e não vazio.

    **Regras de Negócio:**
    - Apenas processos ativos são retornados.
    - O usuário deve ter permissão de leitura.

    **Códigos de Erro:**
    - 404: Processo não encontrado ou inativo.
    - 500: Erro interno do servidor.
    """)
.Produces<Response<ProcessoAdminDto>>(StatusCodes.Status200OK)
.Produces<Response>(StatusCodes.Status404NotFound)
.Produces<Response>(StatusCodes.Status500InternalServerError)
.RequireAuthorization();
```

---

## ⚠️ Penalidades por Violação

O não cumprimento destas regras resulta em:

1. **Rejeição automática** do código gerado.
2. **Solicitação de correção** antes de prosseguir.
3. **Registro da violação** no log de execução.

---

## 📌 Checklist de Validação Final

Antes de considerar qualquer tarefa concluída, o agente **DEVE** verificar:

- [ ] `README.md` foi consultado para entendimento do projeto.
- [ ] `AGENTS.md` e `copilot-instructions.md` foram seguidos.
- [ ] Demanda foi completamente entendida (sem ambiguidades).
- [ ] Instruções adicionais aplicáveis foram verificadas.
- [ ] Código foi escaneado quando necessário.
- [ ] Plano de execução foi criado e registrado.
- [ ] Tarefas foram divididas e rastreadas.
- [ ] Todas as tarefas foram executadas.
- [ ] Testes unitários foram criados com cobertura >= 90%.
- [ ] Documentação XML foi adicionada a todos os métodos públicos.
- [ ] Documentação OpenAPI foi adicionada a todos os endpoints.
- [ ] Resumo final foi apresentado ao usuário.
- [ ] Todas as interações foram em português.

---

## 🔄 Versionamento

| Versão | Data       | Descrição                                      |
|--------|------------|------------------------------------------------|
| 1.0    | 2025-11-25 | Versão inicial das regras absolutas.           |
