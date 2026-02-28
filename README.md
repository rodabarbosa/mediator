# Mediator

Uma implementação leve e eficiente do padrão Mediator para .NET 10, projetada para desacoplar a comunicação entre componentes da aplicação através de um intermediário central.

## 📋 Índice

- [🎯 Objetivos](#-objetivos)
- [🎯 Expectativas](#-expectativas)
- [🛠️ Tech Stack](#️-tech-stack)
- [✨ Características](#-características)
- [📦 Instalação](#-instalação)
- [🚀 Uso](#-uso)
- [📚 Exemplos](#-exemplos)
- [🔧 Build e Testes](#-build-e-testes)
- [🤝 Contribuindo](#-contribuindo)
- [📄 Licença](#-licença)

## 🎯 Objetivos

Este projeto visa fornecer uma implementação educacional e funcional do padrão Mediator com os seguintes objetivos:

1. **Desacoplamento**: Reduzir dependências diretas entre componentes da aplicação através de um mediador central
2. **Simplicidade**: Fornecer uma API clara e intuitiva, fácil de entender e manter
3. **Performance**: Utilizar caching de reflection e padrões eficientes para minimizar overhead
4. **Extensibilidade**: Suportar tanto requisições (request/response) quanto notificações (pub/sub)
5. **Educacional**: Servir como referência de estudo para implementação do padrão Mediator em .NET

## 🎯 Expectativas

### Funcionalidades Principais

- **Request/Response Pattern**: Envio de requisições com retorno de resposta através de handlers únicos
- **Notification Pattern**: Broadcast de notificações para múltiplos handlers (pub/sub)
- **Injeção de Dependência**: Integração nativa com `Microsoft.Extensions.DependencyInjection`
- **Auto-discovery**: Registro automático de handlers através de assembly scanning
- **Type Safety**: Fortemente tipado com generics para garantir segurança em tempo de compilação
- **Async/Await**: Suporte completo para operações assíncronas
- **Cancellation Support**: Integração com `CancellationToken` para operações canceláveis

### Comportamento Esperado

- **Request Handlers**: Exatamente um handler por tipo de requisição (throw exception se 0 ou >1)
- **Notification Handlers**: Zero ou mais handlers por tipo de notificação
- **Thread Safety**: Cache de reflection thread-safe usando `ConcurrentDictionary`
- **Performance**: Reflexão executada apenas uma vez por tipo de handler (cached)

## 🛠️ Tech Stack

### Framework e Runtime

- **.NET 10.0** - Framework target
- **C# 13** - Linguagem de programação

### Dependências

- **Microsoft.Extensions.DependencyInjection.Abstractions 9.0.0** - Injeção de dependência

### Ferramentas de Desenvolvimento

- **xUnit** - Framework de testes unitários
- **dotCover** - Análise de cobertura de código
- **.slnx** - Formato moderno de solução do Visual Studio

### Recursos da Linguagem Utilizados

- Records
- Nullable reference types
- File-scoped namespaces
- Primary constructors (implícito)
- Pattern matching
- Async/await
- Generics com constraints

## ✨ Características

- 🚀 **Alta Performance**: Cache de reflection para evitar overhead
- 📦 **Zero Dependências Externas**: Apenas abstrações do .NET
- 🔒 **Type-Safe**: Fortemente tipado com validação em compile-time
- 🧪 **Testado**: Cobertura de testes unitários completa
- 📚 **Documentado**: Documentação XML completa em todos os tipos públicos
- ⚡ **Assíncrono**: API totalmente async/await
- 🎯 **Simples**: API minimalista e fácil de usar

## 📦 Instalação

### Opção 1: Adicionar Projeto à Solução

```bash
# Clone o repositório
git clone <repository-url>

# Adicione o projeto à sua solução
dotnet sln add src/Mediator/Mediator.csproj
```

### Opção 2: Referência Direta

```bash
# Adicione referência ao projeto
dotnet add reference path/to/Mediator/Mediator.csproj
```

## 🚀 Uso

### 1. Registrar o Mediator

```csharp
using Mediator.Extensions;

var services = new ServiceCollection();

// Registra o mediator e escaneia assemblies para handlers
services.AddMediator(typeof(Program).Assembly);

var serviceProvider = services.BuildServiceProvider();
var mediator = serviceProvider.GetRequiredService<IMediator>();
```

### 2. Definir Request e Handler

```csharp
// Define a requisição
public record GetUserQuery(int UserId) : IRequest<User>;

// Implementa o handler
public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
    private readonly IUserRepository _repository;

    public GetUserQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.UserId, cancellationToken);
    }
}
```

### 3. Enviar Request

```csharp
var user = await mediator.Send(new GetUserQuery(123), cancellationToken);
```

### 4. Definir Notification e Handlers

```csharp
// Define a notificação
public class UserCreatedEvent : INotification
{
    public int UserId { get; init; }
    public string Email { get; init; } = string.Empty;
}

// Implementa múltiplos handlers
public class SendWelcomeEmailHandler : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Envia email de boas-vindas
        await SendEmailAsync(notification.Email);
    }
}

public class LogUserCreationHandler : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Registra log
        await LogAsync($"User {notification.UserId} created");
    }
}
```

### 5. Enviar Notification

```csharp
// Todos os handlers registrados serão executados
await mediator.Send(new UserCreatedEvent
{
    UserId = 123,
    Email = "user@example.com"
}, cancellationToken);
```

## 📚 Exemplos

### Request/Response Pattern

```csharp
// Comando para criar usuário
public record CreateUserCommand(string Name, string Email) : IRequest<int>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _repository;

    public CreateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { Name = request.Name, Email = request.Email };
        await _repository.AddAsync(user, cancellationToken);
        return user.Id;
    }
}

// Uso
var userId = await mediator.Send(new CreateUserCommand("John Doe", "john@example.com"));
```

### Pub/Sub Pattern

```csharp
// Evento de domínio
public class OrderPlacedEvent : INotification
{
    public int OrderId { get; init; }
    public decimal Total { get; init; }
}

// Múltiplos handlers para o mesmo evento
public class UpdateInventoryHandler : INotificationHandler<OrderPlacedEvent>
{
    public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        // Atualiza inventário
    }
}

public class SendOrderConfirmationHandler : INotificationHandler<OrderPlacedEvent>
{
    public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        // Envia confirmação por email
    }
}

public class NotifyShippingHandler : INotificationHandler<OrderPlacedEvent>
{
    public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        // Notifica setor de envio
    }
}

// Uso - todos os handlers serão executados
await mediator.Send(new OrderPlacedEvent { OrderId = 1, Total = 99.99m });
```

## 🔧 Build e Testes

### Build

```bash
# Build da solução
dotnet build Mediator.slnx

# Build do projeto específico
dotnet build src/Mediator/Mediator.csproj
```

### Executar Testes

```bash
# Executar todos os testes
dotnet test tests/Mediator.Tests/Mediator.Tests.csproj

# Executar com cobertura (dotCover)
dotnet dotcover test tests/Mediator.Tests/Mediator.Tests.csproj --dcReportType=HTML
```

### Análise de Cobertura

```bash
# Gerar relatório de cobertura
dotnet dotcover test --dcReportType=HTML --dcOutput=coverage.html
```

## 🤝 Contribuindo

Contribuições são bem-vindas! Este é um projeto educacional e melhorias são sempre apreciadas.

### Como Contribuir

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

### Diretrizes

- Mantenha a simplicidade - este é um projeto educacional
- Adicione testes para novas funcionalidades
- Mantenha a cobertura de testes alta (>90%)
- Documente código público com XML comments
- Siga as convenções de código do C# e .NET

## 📄 Licença

Este projeto é um estudo de caso para implementação do padrão Mediator em .NET.

---

**Nota**: Este é um projeto educacional criado para demonstrar a implementação do padrão Mediator em .NET 10. Para uso em produção, considere bibliotecas estabelecidas como [MediatR](https://github.com/jbogard/MediatR).
