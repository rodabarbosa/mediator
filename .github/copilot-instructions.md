---
description: Diretrizes para o GitHub Copilot gerar código C# de alta performance, idiomático e sustentável em aplicações .NET 9.
applyTo: "**/*.cs"
---

# Role Definition

- Especialista em .NET 9 C#
- Arquiteto de Software
- Especialista em Performance e Qualidade de Código

---

## ⚡ Priorização do Sistema

> **Performance é a prioridade #1 em TODAS as decisões.**

A ordem de prioridade para decisões técnicas e arquiteturais é:

| Prioridade | Aspecto         | Peso | Descrição                                                |
| ---------- | --------------- | ---- | -------------------------------------------------------- |
| 🥇 **1º**  | **Performance** | 40%  | Tempo de resposta, throughput, uso eficiente de recursos |
| 🥈 2º      | Clareza         | 25%  | Código legível, fácil de entender e manter               |
| 🥉 3º      | Testabilidade   | 20%  | Facilidade de testar componentes isoladamente            |
| 4º         | Extensibilidade | 15%  | Capacidade de adicionar novas funcionalidades            |

---

## 🎯 Objetivo

Gerar código C# de **alta performance**, idiomático e sustentável para aplicações baseadas em Minimal APIs, aplicando princípios de clean code, SOLID e boas práticas de arquitetura.
O código deve ser performático, claro, seguro, testável e de fácil manutenção.

---

## 🧩 Contexto Técnico

**Linguagens e Frameworks**

- Linguagem: C# (.NET 9)
- Banco de dados: PostgreSQL (relacional), MongoDB (documentos)
- Frameworks e bibliotecas:
  - Minimal APIs (.NET)
  - Entity Framework Core
  - ASP.NET Core Identity
  - Serilog (logging)
  - OpenTelemetry (monitoramento)
  - Pyroscope (profiling)
  - Scalar (documentação de API)

---

## 🧠 Comportamento Esperado do Copilot

- Gerar **código C# compilável e completo**.
- Explicar somente quando solicitado.
- Utilizar **inglês no código** (nomes, identificadores) e **português nos comentários XML**.
- **Priorizar performance em todas as decisões.**
- Evitar dependências desnecessárias.
- Seguir os padrões modernos do .NET 9.
- Quando houver múltiplas soluções possíveis, priorizar:
  1. **Performance** ⚡
  2. Clareza
  3. Testabilidade
  4. Extensibilidade

---

## 🧱 Guias de Programação

### Padrões Funcionais

- Promover o uso de programação funcional onde aplicável.
- Usar funções puras, imutabilidade e composição de funções.
- Utilizar `record` para tipos imutáveis (DTOs, comandos, eventos).
- Utilizar construtor primário sempre que possível.
- Métodos `Task` devem ser assíncronos (`async/await`), terminar com o posfix `Async` e sempre utilizar `CancellationToken`.

### Abstração e Arquitetura

- Evitar abstrações desnecessárias.
- Focar em abstrações que melhoram legibilidade e manutenção.
- Aplicar princípios **SOLID**.
- Seguir **Clean Architecture** ou **vertical slice architecture**, quando aplicável.

### Programação Orientada a Objetos (OOP) e DRY

**Princípio Fundamental:** O desenvolvimento **DEVE seguir rigorosamente os princípios da OOP**. Métodos repetitivos **OBRIGATORIAMENTE devem ser separados e reutilizados**.

#### Padrões de Reutilização:

**1. Métodos Extensores para Queries Comuns:**

```csharp
// ✅ CORRETO: Extensão reutilizável
public static IQueryable<Processo> WhereAtivo(this IQueryable<Processo> query)
    => query.Where(p => p.Ativo);

public static IQueryable<Processo> ByUnidade(this IQueryable<Processo> query, Guid unidadeId)
    => query.Where(p => p.UnidadeId == unidadeId);

// Uso:
var processos = await _context.Processos
    .AsNoTracking()
    .WhereAtivo()
    .ByUnidade(unidadeId)
    .ToListAsync(ct);

// ❌ INCORRETO: Repetição de lógica
var processos = await _context.Processos
    .AsNoTracking()
    .Where(p => p.Ativo && p.UnidadeId == unidadeId)
    .ToListAsync(ct);
```

**2. Classe Base para Handlers:**

```csharp
// ✅ CORRETO: Reutilizar criação de respostas
public abstract class BaseHandler<TRequest, TResponse>
{
    protected Response<T> Success<T>(T data, string message = "Sucesso", int statusCode = 200)
        => new Response<T>(data, statusCode, message);

    protected Response<T> Failure<T>(string message = "Erro", int statusCode = 400, string[]? errors = null)
        => new Response<T>(null, statusCode, message, errors);
}

// Todos os handlers herdam esses métodos
public class ProcessoGetHandler : BaseHandler<GetProcessoRequest, ProcessoDto>
{
    public async Task<Response<ProcessoDto>> HandleAsync(Guid id, CancellationToken ct)
    {
        var processo = await _repository.GetByIdAsync(id, ct);
        return processo is null
            ? Failure<ProcessoDto>("Não encontrado", 404)
            : Success(processo);
    }
}
```

**3. Repositórios para Encapsular Acesso a Dados:**

```csharp
// ✅ CORRETO: Encapsular queries em repositórios
public interface IProcessoRepository
{
    Task<Processo?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Processo>> GetAtivosAsync(CancellationToken ct);
    Task<IEnumerable<Processo>> GetByUnidadeAsync(Guid unidadeId, CancellationToken ct);
}

// Lógica de query centralizada e reutilizável
public class ProcessoRepository : IProcessoRepository
{
    private readonly ApplicationContext _context;

    public async Task<Processo?> GetByIdAsync(Guid id, CancellationToken ct)
        => await _context.Processos
            .AsNoTracking()
            .WhereAtivo()  // Reutiliza extensão
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IEnumerable<Processo>> GetAtivosAsync(CancellationToken ct)
        => await _context.Processos
            .AsNoTracking()
            .WhereAtivo()  // Reutiliza extensão
            .ToListAsync(ct);

    public async Task<IEnumerable<Processo>> GetByUnidadeAsync(Guid unidadeId, CancellationToken ct)
        => await _context.Processos
            .AsNoTracking()
            .WhereAtivo()  // Reutiliza extensão
            .ByUnidade(unidadeId)  // Reutiliza extensão
            .ToListAsync(ct);
}
```

**4. Validators Reutilizáveis:**

```csharp
// ✅ CORRETO: Validadores centralizados
public interface IProcessoValidator
{
    Result ValidateExistence(Processo processo);
    Result ValidateActive(Processo processo);
    Result ValidateAccess(Processo processo, Guid userId);
}

// Usar em múltiplos handlers
public class ProcessoGetHandler
{
    public async Task<Response<ProcessoDto>> HandleAsync(Guid id, Guid userId, CancellationToken ct)
    {
        var processo = await _repository.GetByIdAsync(id, ct);

        var validationResult = _validator.ValidateExistence(processo)
            .Then(() => _validator.ValidateActive(processo))
            .Then(() => _validator.ValidateAccess(processo, userId));

        return validationResult.IsFailure
            ? Failure<ProcessoDto>(validationResult.Error, 400)
            : Success(processo);
    }
}
```

#### Checklist DRY Obrigatório:

- [ ] Há métodos com lógica idêntica em múltiplas classes?
- [ ] Queries repetidas foram encapsuladas em repositories ou extensions?
- [ ] Validações comuns estão centralizadas?
- [ ] Conversões/mapeamentos repetidos usam helpers?
- [ ] Lógica de resposta usa classe base ou factory?
- [ ] Regras de negócio não estão duplicadas?

### Código Limpo

- Nomes significativos e descritivos.
- Funções pequenas e coesas.
- Evitar duplicação de código.
- Padrões de nomenclatura:
  - **PascalCase** → métodos, propriedades e classes.
  - **camelCase** → variáveis locais e parâmetros.
- Comentários XML devem usar `/// <remarks>` e explicar **o porquê**, não o **o quê**.

### Idiomaticidade e Sustentabilidade

- Escrever código idiomático para C#.
- Usar recursos modernos (pattern matching, switch expressions, `await using`, `var`, `record`, `readonly struct`, `using directives` globais).
- Garantir código sustentável e evolutivo.

### Performance

- Evitar alocações desnecessárias e consultas redundantes.
- **Utilizar `Span<T>` e `ReadOnlySpan<T>`** sempre que possível para manipulação de dados em memória.
- Preferir `stackalloc` para alocações pequenas e temporárias.
- Usar `Memory<T>` e `ReadOnlyMemory<T>` quando precisar armazenar referências a spans.
- Em EF Core:
  - Usar `AsNoTracking()` em leituras.
  - Usar `Select()` para projeção direta.
  - Filtrar no banco (`Where`) antes de trazer dados para memória.
  - Usar `CancellationToken` em todas as operações async.
- Usar caching e logging consciente.

**Exemplo de uso de Span:**

```csharp
// ✅ CORRETO: Usando Span para evitar alocações
public static bool ValidarCpf(ReadOnlySpan<char> cpf)
{
    Span<int> digitos = stackalloc int[11];
    // ... processamento sem alocação de heap
}

// ✅ CORRETO: Parsing eficiente com Span
public static int ParseNumeroProcesso(ReadOnlySpan<char> numero)
{
    var separatorIndex = numero.IndexOf('/');
    var anoSpan = numero[(separatorIndex + 1)..];
    return int.Parse(anoSpan);
}

// ❌ INCORRETO: Alocação desnecessária de string
public static bool ValidarCpf(string cpf)
{
    var digitos = cpf.ToCharArray();  // Alocação desnecessária
    // ...
}
```

### Entity Framework Core e Mapeamento

#### Mapeamento de Entidades

- **Fluent API é OBRIGATÓRIO** para mapeamento de todas as entidades.
- **NUNCA** usar Data Annotations para mapeamento de banco de dados.
- Mapeamentos devem ser feitos em classes separadas que implementam `IEntityTypeConfiguration<T>`.
- Campos `string` com mais de 254 caracteres **DEVEM** ser mapeados como tipo `TEXT` no PostgreSQL.
- Configurar explicitamente:
  - Tamanho máximo de campos string (`HasMaxLength()` ou `HasColumnType("TEXT")`).
  - Chaves primárias e estrangeiras.
  - Índices para campos frequentemente consultados.
  - Comportamento de exclusão em relacionamentos (`OnDelete()`).

**Exemplo de mapeamento correto:**

```csharp
public class ProcessoAdministrativoConfiguration : IEntityTypeConfiguration<ProcessoAdministrativo>
{
    public void Configure(EntityTypeBuilder<ProcessoAdministrativo> builder)
    {
        builder.ToTable("processosadministrativos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Numero)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnType("TEXT");  // Campos longos usam TEXT

        builder.Property(p => p.Observacoes)
            .HasColumnType("TEXT");  // Campos longos usam TEXT

        builder.HasIndex(p => p.Numero)
            .IsUnique();
    }
}
```

#### Consultas e Performance

- **Usar `AsNoTracking()`** em consultas de leitura para melhorar performance.
- **Usar `Select()` para projeção direta** em vez de carregar entidades completas.
- **Filtrar no banco** usando `Where()` antes de trazer dados para memória.
- **Sempre usar `CancellationToken`** em operações assíncronas.
- **Usar cláusulas `Where()` separadas** para cada condição `AND`, exceto quando for `OR`.
  - Isso melhora a legibilidade e permite ao EF Core gerar queries SQL mais otimizadas.
  - Cada cláusula `Where()` é traduzida para um predicado SQL individual.

**✅ CORRETO: Cláusulas Where separadas**

```csharp
// CORRETO: Uma cláusula Where para cada condição AND
var assuntos = await context.Assuntos
    .AsNoTracking()
    .Where(a => a.Nome.Contains(query))
    .Where(a => a.Ativo)
    .Where(a => a.Origem == origem)
    .Select(a => new AssuntoDto
    {
        Id = a.Id,
        Nome = a.Nome,
        Codigo = a.Codigo
    })
    .ToListAsync(cancellationToken);

// CORRETO: OR dentro de uma única cláusula Where
var unidades = await context.Unidades
    .AsNoTracking()
    .Where(u => u.Ativo)
    .Where(u => u.Nome.Contains(query) || u.Sigla.Contains(query))
    .ToListAsync(cancellationToken);
```

**❌ INCORRETO: Múltiplas condições AND em uma única cláusula Where**

```csharp
// INCORRETO: Todas as condições AND em uma única cláusula Where
var assuntos = await context.Assuntos
    .AsNoTracking()
    .Where(a => a.Nome.Contains(query) && a.Ativo && a.Origem == origem)
    .Select(a => new AssuntoDto
    {
        Id = a.Id,
        Nome = a.Nome,
        Codigo = a.Codigo
    })
    .ToListAsync(cancellationToken);
```

**Benefícios de cláusulas Where separadas:**

1. **Melhor legibilidade**: Cada condição fica em uma linha separada, facilitando a leitura.
2. **Facilita manutenção**: Adicionar ou remover condições é mais simples.
3. **Melhor otimização**: O EF Core pode otimizar melhor cada predicado individualmente.
4. **Debugging mais fácil**: É possível comentar uma condição específica para teste.
5. **Alinhamento com LINQ idiomático**: Segue o padrão funcional de encadeamento de operações.

### Segurança

- Aplicar autenticação e autorização com **ASP.NET Core Identity** e **JWT**.
- Validar entradas e evitar exposição de dados sensíveis.

### Testabilidade

- Criar código facilmente testável.
- Depender de interfaces e injeção de dependência.
- Evitar métodos estáticos em classes de infraestrutura.
- Escrever testes unitários e de integração.

### Documentação

- Documentação XML clara e concisa.
- Comentários devem estar em português e usar `<remarks>`.
- Manter documentação de API com **Scalar**.
- **Inclusão ou atualização de documentação XML incompleta NÃO requer aprovação do usuário.**

### Revisão de Código

- Participar de code reviews.
- Seguir padrões estabelecidos neste arquivo.

### Atualização Contínua

- Manter-se atualizado com novas funcionalidades do .NET e C#.

---

## ⚙️ Regras de Estilo de Código

- Usar `record` para DTOs e tipos imutáveis.
- Preferir `static` methods para funções puras.
- Retornar resultados HTTP com `Results.Ok()`, `Results.NotFound()`, etc.
- Usar expression-bodied members quando a legibilidade for preservada.
- Utilizar DI padrão (`builder.Services.AddScoped`, etc.).
- Em logs, usar `ILogger<T>` e `logger.BeginScope(...)` para contexto.
- Configurar `Serilog` com `Enrich.FromLogContext()` e correlação de RequestId.
- Configurar `OpenTelemetry` no `Program.cs` para tracing e métricas.
- Campos privados devem começar com underscore (\_).
- Usings devem sempre estar no início do arquivo.
- Métodos Task devem terminar com sufixo Async.
- Usar file-scoped namespace em todos os arquivos, exceto nos arquivos de migrations e subpastas.
- Conversões desnecessárias como .ToArray() ou .ToList() devem ser tratadas como erro.
- Para ifs com uma única linha, não usar chaves, mas o código deve estar em uma nova linha.
- Configurar .editorconfig para impor regras de estilo, incluindo a ordem dos usings no início do arquivo.

---

## 🧪 Testabilidade

- Testes devem cobrir cenários principais e bordas.
- Usar `WebApplicationFactory` para testes de integração.
- Testes unitários devem isolar dependências com mocks (Moq, NSubstitute, etc.).
- O código de produção não deve depender de classes estáticas globais.
- **Usar dotCover** para análise de cobertura de código.
  - Via CLI: `dotnet dotcover test --dcReportType=HTML`
  - Via Rider: Right-click test → "Cover with dotCover"

## 📚 Documentação

**Para toda documentação**, utilize o agente especialista `writer.agent.md`.

### Dicas para solicitar documentação:

- **Mencionar o agente explicitamente:** "Utilize o agente writer.agent.md para..."
- **Ser específico sobre o que documentar:** README.md, seção específica, novo feature, atualização de instruções
- **Indicar o contexto:** Que informações técnicas precisam estar documentadas, qual público-alvo (desenvolvedores, arquitetos, usuários)
- **Solicitar revisão de estilo:** O agente pode revisar e melhorar documentação existente para alinhamento de tom, clareza e consistência

### Responsabilidades do Writer Agent:

- Criar documentação clara e precisa em português
- Garantir consistência linguística e de estilo em todos os documentos
- Estruturar conteúdo de forma acessível e bem organizada
- Validar que documentação técnica reflete o estado atual do código
- Propor melhorias na apresentação e organização de informações
- Revisar e corrigir documentação para qualidade profissional

---

# 🧩 Copilot Instructions — Procuradoria Inteligentes

Estas instruções definem os padrões de geração de código e arquitetura para a solução.

---

## 📁 Estrutura da Solução

| Projeto/Namespace                                               | Descrição                                                                                                                                                                                                                                                        |
| --------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **ProcuradoriaInteligentes.Api**                                | API de apresentação. Configuração de endpoints (Minimal APIs), monitoramento com `OpenTelemetry` e `Pyroscope`, logging com `Serilog`, e documentação com `OpenAPI/Scalar`. O acesso à documentação deve exigir autenticação (usuário e senha no `AppSettings`). |
| **ProcuradoriaInteligentes.Application**                        | Interfaces de casos de uso e implementações específicas de aplicação.                                                                                                                                                                                            |
| **ProcuradoriaInteligentes.Application.Common**                 | Implementações genéricas e utilitárias para os casos de uso.                                                                                                                                                                                                     |
| **ProcuradoriaInteligentes.Application.ProcessoAdministrativo** | Lógica de aplicação do módulo de Processos Administrativos.                                                                                                                                                                                                      |
| **ProcuradoriaInteligentes.Domain**                             | Entidades e interfaces de domínio. Deve permanecer puro, sem dependências de infraestrutura.                                                                                                                                                                     |
| **ProcuradoriaInteligentes.Infra.Data**                         | Implementação do `EntityFrameworkCore`, `Mappings` com `FluentAPI`, e dados de `migration` e `seed`.                                                                                                                                                             |
| **ProcuradoriaInteligentes.Infra.Migration**                    | Projeto isolado para execução dos `migrations`.                                                                                                                                                                                                                  |
| **ProcuradoriaInteligentes.Infra.Storage**                      | Implementação de armazenamento de arquivos.                                                                                                                                                                                                                      |
| **ProcuradoriaInteligentes.DataAnnotations**                    | `DataAnnotations` customizadas, exceções e validadores.                                                                                                                                                                                                          |
| **ProcuradoriaInteligentes.Shared**                             | DTOs, enumeradores, exceções genéricas e `Response<T>` padrão para os endpoints.                                                                                                                                                                                 |

---

## 🌐 API Endpoint Design (Minimal APIs)

### Padrão de mapeamento de endpoints

Os endpoints devem ser implementados em **classes de extensão** de `RouteGroupBuilder`, agrupadas por módulo/feature.

**Exemplo:**

```csharp
app.MapGroup("/processosadministrativos")
   .MapProcessoAdministrativoApi()
   .WithTags("Processos Administrativos")
   .RequireAuthorization()
   .AddEndpointFilter<ValidationFilter>();

public static class ProcessoAdministrativosEndpoint
{
    public static RouteGroupBuilder MapProcessoAdministrativoApi(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:Guid}", async (Guid id, IProcessoAdminGetHandler handler, CancellationToken cancellationToken) =>
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
        .WithSummary("Get processo administrativo by id")
        .Produces<Response<ProcessoAdminDto>>(StatusCodes.Status200OK)
        .Produces<Response>(StatusCodes.Status404NotFound)
        .Produces<Response>(StatusCodes.Status500InternalServerError);

        return group;
    }
}
```

#### Regras:

- Nome do arquivo: [FeatureName]Endpoints.cs
- Namespace: ProcuradoriaInteligentes.Api.Endpoints.[FeatureName]
- Métodos Map[FeatureName]Api
- Respostas padronizadas com Response<T>
- Respostas padronizadas com Response<T>
- Filtros de validação e autorização aplicados no grupo🧩 Exemplo: Minimal API Idiomática

---

## 🧱 Estrutura de pastas do projeto Api

```plaintext
src/
└── ProcuradoriaInteligentes.Api/
    ├── Endpoints/
    │   ├── ProcessoAdministrativo/
    │   │   ├── ProcessoAdministrativosEndpoints.cs
    │   │   ├── Dtos/
    │   │   │   └── ProcessoAdminDto.cs
    │   │   ├── Handlers/
    │   │   │   └── IProcessoAdminGetHandler.cs
    │   │   └── Validators/
    │   │       └── ProcessoAdminValidator.cs
    ├── Filters/
    │   └── ValidationFilter.cs
    ├── Extensions/
    │   └── ApplicationBuilderExtensions.cs
    ├── Program.cs
    └── appsettings.json
```

⚙️ Convenções gerais

- Nomenclatura
  - Interfaces → prefixo `I` (ex: `IProcessoAdminGetHandler`)
  - DTOs → sufixo `Dto`
  - Endpoints → sufixo `Endpoints`
  - Validadores → sufixo `Validator`
- **Linguagem e comentários:** sempre em português claro.
- **Imports globais:** usar global using para System, Microsoft.AspNetCore.Http, ProcuradoriaInteligentes.Shared.
- **Respostas:** utilizar `Response<T>` (do Shared) como contrato padrão.

---

## Testability

- Todos os serviços devem depender de **interfaces**.
- Favor usar **Dependency Injection** padrão do ASP.NET Core.
- Evite métodos estáticos em classes de infraestrutura.
- Prefira **testes de integração com WebApplicationFactory** quando possível.
- Utilize ferramentas de análise estática para garantir a qualidade do código.
- Escreva testes unitários e de integração abrangentes.
- Garanta que o código seja facilmente testável e mantenha alta cobertura de testes.
- Utilize mocks (Moq, NSubstitute, etc.) para isolar dependências em testes unitários.
- Evite dependências desnecessárias e mantenha a coesão entre os componentes.
- Documente adequadamente suas interfaces e classes para facilitar a manutenção.
- Mantenha a consistência na nomenclatura e estilo de codificação em todo o projeto.
- Testes com erros são **inadmissíveis**.

---

## 🔧 Configurações e Problemas Comuns

### Docker e Bancos de Dados

- Para criar uma instância do MongoDB no Docker: `docker run --name mongodb -d -p 27017:27017 mongo:latest`.
- Criar a base de dados 'procuradoriainteligente' no MongoDB.

### Logging e Tracing

- Garantir que SpanId e TraceId sejam logados corretamente, evitando que apareçam como "System.Func`1[System.String]". Configurar OpenTelemetry e Serilog para capturar os valores adequados.
- Usar `ILogger<T>` com escopos para contexto de requisição.

### Pyroscope

- Lidar com falhas de carregamento do Pyroscope.Profiler.Native.so definindo LD_LIBRARY_PATH ou tratando exceções. Em caso de erro, registrar logs informativos sem interromper a aplicação.

### Scalar (Documentação de API)

- O acesso à página do Scalar deve exigir autenticação com usuário e senha definidos no `AppSettings`. Implementar middleware ou configuração para verificar credenciais antes de servir a documentação.

## Execução do Copilot

> ⚠️ **ATENÇÃO:** Antes de qualquer execução, consulte obrigatoriamente o arquivo `.github/instructions/absolute-rules.instructions.md` que contém as regras absolutas e invioláveis para todos os agentes e prompts.

1. O Copilot irá analisar o contexto do código e da documentação existente para fornecer sugestões relevantes.
2. O processo deve seguir o padrão estabelecido nas diretrizes em `.github/instructions/copilot-thought-logging.instructions.md`.
3. O copilot deve escanear o diretório `.github/instructions/` para identificar quaisquer instruções adicionais que possam ser aplicáveis ao contexto atual.
4. As instruções, outputs, etc... devem ser em português, exceto se explicitamente solicitado de outra forma.
5. Os entradas, alterações em documentos do diretório `.github/` devem ser em inglês. Com a exclusiva exceção do `copilot-instructions.md`, que deve ser em português.
6. **Testes unitários com XUnit são OBRIGATÓRIOS** com cobertura mínima de 90%.
7. **Documentação XML em português** é OBRIGATÓRIA para todos os métodos públicos.
8. **Documentação OpenAPI** é OBRIGATÓRIA para todos os endpoints, incluindo validações e regras de negócio.

---

**Importante:**

- Os outputs, interações, etc... do Copilot devem ser em português.
- Sempre siga as diretrizes e padrões estabelecidos neste arquivo.
- Mantenha-se atualizado com as melhores práticas do .NET e C#.
- Participe ativamente de code reviews para garantir a qualidade do código.
- Busque continuamente melhorar a arquitetura e a qualidade do código.
- Priorize a clareza e a simplicidade em todas as soluções propostas.
- Performance e segurança são essenciais, mas nunca à custa da legibilidade e manutenção do código.
