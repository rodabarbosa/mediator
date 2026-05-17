# Diagramas e visualização

Use diagramas sempre que o texto não for suficiente para eliminar dúvidas de interpretação.

Todos os diagramas são criados em **Mermaid.js** e armazenados no diretório `/diagrams` com o prefixo correspondente ao tipo.

---

## Tipos de diagrama e quando usar

### Diagrama de sequência
**Prefixo:** nenhum (embutido no documento de requisito ou API)
**Uso:** exibir interações entre componentes ao longo do tempo — ideal para fluxos de API, autenticação, processos assíncronos.

```mermaid
sequenceDiagram
    participant U as Usuário
    participant FE as Frontend
    participant BE as Backend
    participant DB as Banco de Dados

    U->>FE: Preenche formulário e clica em Salvar
    FE->>BE: POST /api/v1/recurso { payload }
    BE->>DB: EXEC sp_SalvarRegistro @param1, @param2
    DB-->>BE: ID gerado
    BE-->>FE: 200 OK { id, status }
    FE-->>U: Exibe confirmação
```

---

### Diagrama de casos de uso
**Uso:** mostrar fronteiras do sistema, atores e suas interações de alto nível.

```mermaid
graph LR
    A((Usuário)) --> B[Fazer login]
    A --> C[Consultar pedidos]
    A --> D[Emitir relatório]
    E((Administrador)) --> D
    E --> F[Gerenciar usuários]
```

---

### Diagrama de classes (`dcl-`)
**Arquivo:** `/diagrams/dcl-nome_do_diagrama.md`
**Uso:** modelar estrutura de código, atributos, métodos e relacionamentos entre classes.

```mermaid
classDiagram
    class Usuario {
        +int id
        +string nome
        +string email
        +autenticar()
    }
    class Pedido {
        +int id
        +date dataCriacao
        +calcularTotal()
    }
    Usuario "1" --> "0..*" Pedido : realiza
```

---

### Diagrama de integração de sistemas (`min-`)
**Arquivo:** `/diagrams/min-nome_do_diagrama.md`
**Uso:** mapear fluxo de dados entre sistemas, serviços e integrações externas.

```mermaid
graph TD
    A[Aplicação Web] -->|REST| B[API Gateway]
    B -->|REST| C[Serviço de Pedidos]
    B -->|REST| D[Serviço de Usuários]
    C -->|SQL| E[(Banco de Dados)]
    C -->|Evento| F[Fila de Mensagens]
    F -->|Consome| G[Serviço de Notificações]
```

---

### Diagrama de entidades e relacionamentos (`der-`)
**Arquivo:** `/diagrams/der-nome_do_diagrama.md`
**Uso:** documentar modelo de dados, entidades, atributos e relacionamentos.

```mermaid
erDiagram
    USUARIO {
        int id PK
        string nome
        string email
        date criado_em
    }
    PEDIDO {
        int id PK
        int usuario_id FK
        date data_criacao
        decimal total
    }
    USUARIO ||--o{ PEDIDO : "realiza"
```

---

### Diagrama de fluxo / estado
**Uso:** representar lógica de navegação, estados de uma entidade ou decisões de fluxo.

```mermaid
stateDiagram-v2
    [*] --> Rascunho
    Rascunho --> EmRevisao : Enviar para revisão
    EmRevisao --> Aprovado : Aprovar
    EmRevisao --> Rascunho : Reprovar
    Aprovado --> [*]
```

---

## Diretrizes de uso

- Prefira Mermaid.js para todos os diagramas — mantém a documentação versionável junto ao código.
- Use ASCII Mermaid como fallback apenas quando a ferramenta não suportar renderização.
- **Todo diagrama deve ter título e breve descrição** explicando o que representa.
- Valide a sintaxe Mermaid antes de finalizar o documento.
- Diagramas de sequência são **obrigatórios** em documentos de API e em requisitos que descrevam fluxos multi-camada.
- DER e DCL são **obrigatórios** quando a funcionalidade envolve modelo de dados relevante.
