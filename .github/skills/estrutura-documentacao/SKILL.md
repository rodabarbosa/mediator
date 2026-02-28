---
name: Estrutura de documentação
description: Este agente ajuda a criar uma estrutura de documentação clara e organizada para projetos, garantindo que todas as seções essenciais sejam incluídas e bem definidas.
---

# Documentação

Esta seção descreve a estrutura recomendada para a documentação do projeto, incluindo os tipos de documentos que devem ser criados e suas respectivas convenções de nomenclatura.

## Estrutura de Diretórios

```
/documentacao
|-- /processo_unificado
    |-- /artefatos_aprovados
        |-- /documentacao_api
            |-- api-nome_da_api.md
        |-- /visao
            |-- vis-nome_da_visao.md
        |-- /detalhamento_requisitos
            |-- req-nome_do_requisito.md
            |-- apr_req-nome_do_requisito_de_aprovacao.md
        |-- /diagrama_de_classes
            |-- dcl-nome_do_diagrama_de_classes.md
        |-- /diagrama_de_integracao_de_sistemas
            |-- min-nome_do_diagrama_de_integracao_de_sistemas.md
        |-- /diagrama_de_entidades_e_relacionamento
            |-- der-nome_do_diagrama_de_entidades_e_relacionamentos.md
        |-- /dicionario_de_dados
            |-- did-nome_do_dicionario_de_dados.md
```

## Tipos de Documento

> Cada tipo de documentação deve ser adicionada em seu diretório respectivo e obrigatoriamente seguir o prefixo definido para a conveção de nomeclatura.

| Tipo de documento                                                     | Prefixo  | Diretório                                                                                  |
| --------------------------------------------------------------------- | -------- | ------------------------------------------------------------------------------------------ |
| Documentação para API                                                 | api-     | documentacao/processo_unificado/artefatos_aprovados/documentacao_api                       |
| Documento de visão                                                    | vis-     | documentacao/processo_unificado/artefatos_aprovados/visao                                  |
| Requisição / Especificação de Caso de Uso e Configuração de Interface | req-     | documentacao/processo_unificado/artefatos_aprovados/detalhamento_requisitos                |
| Aprovação da Especificação de Caso de Uso e Configuração de Interface | apr_req- | documentacao/processo_unificado/artefatos_aprovados/detalhamento_requisitos                |
| Diagrama de classes                                                   | dcl-     | documentacao/processo_unificado/artefatos_aprovados/diagrama_de_classes                    |
| Diagrama de Integração de Sistema                                     | min-     | documentacao/processo_unificado/artefatos_aprovados/diagrama_de_integracao_de_sistemas     |
| Diagrama de Entidade e Relacionamentos                                | der-     | documentacao/processo_unificado/artefatos_aprovados/diagrama_de_entidades_e_relacionamento |
| Dícionário de Dados                                                   | did-     | documentacao/processo_unificado/artefatos_aprovados/dicionario_de_dados                    |

### Documentação para API

Documento técnico detalhado que descreve a interface de comunicação da aplicação. Deve ser utilizado para guiar desenvolvedores na implementação e consumo de serviços.

- **Diagrama de Sequência**: Deve incluir um diagrama (preferencialmente em Mermaid) que ilustre o fluxo de chamadas entre o cliente, a API, camadas de serviço e dependências externas (banco de dados, outras APIs).
- **Validações**: Lista detalhada de regras de negócio e validações sintáticas aplicadas aos dados de entrada.
- **Exceções**: Registro de exceções esperadas e cenários de erro tratados (ex: `UserNotFoundException`).
- **Exemplos**: Fornecer payloads claros de JSON/XML para entrada (request) e saída (response).
- **Códigos de Retorno**: Tabela com códigos HTTP (200, 201, 400, 401, 404, 500, etc.) e a descrição exata do cenário que dispara cada um.

### Documento de Visão

Estabelece o alinhamento estratégico, descrevendo o problema a ser resolvido, os objetivos do negócio, stakeholders, limites do escopo e os benefícios esperados com a solução.

### Requisição / Especificação de Caso de Uso e Configuração de Interface

Detalhamento minucioso do comportamento funcional do sistema.

- **Fluxo da Informação**: Deve conter um diagrama de fluxo (preferencialmente em Mermaid) para melhor entendimento do caminho percorrido pelos dados e decisões do sistema.
- **Detalhamento**: Inclui fluxos principais, alternativos e de exceção, regras de negócio associadas e protótipos ou definições de interface (UI/UX).

### Aprovação da Especificação de Caso de Uso e Configuração de Interface

Documento de formalização que atesta que os requisitos foram revisados e aprovados pelos responsáveis técnicos e de negócio antes do início do desenvolvimento.

### Diagrama de Classes

Representação da estrutura lógica do código, detalhando classes, atributos, métodos e as relações de herança, associação e dependência entre os componentes do sistema.

### Diagrama de Integração de Sistema

Visão macro das comunicações entre a aplicação e sistemas externos ou microserviços, identificando protocolos (REST, gRPC, Mensageria) e a finalidade da troca de informações.

### Diagrama de Entidade e Relacionamentos (DER)

Modelagem lógica dos dados que serão persistidos, focada na estrutura de tabelas, chaves primárias/estrangeiras e a cardinalidade dos relacionamentos.

### Dicionário de Dados

Catálogo descritivo que detalha o metadado de cada campo do sistema, incluindo tipo físico, tamanho, obrigatoriedade, descrição do propósito e restrições de domínio.
