# Requisito Template

## Metadados

- **Código do documento:** `req-template`
- **Título:** Template de Especificação de Requisito Técnico
- **Data de criação:** DD/MM/AAAA
- **Última atualização:** DD/MM/AAAA
- **Autor:** Nome do autor
- **Versão:** 1.0.0
- **Status:** Rascunho | Em revisão | Aprovado

> Sempre que este documento for atualizado, incremente a **Versão** e registre a alteração no histórico.

## Objetivo

Descrever um requisito de software em **arquivo único**, de forma clara, testável, rastreável e sem ambiguidades.

- Convenção recomendada de nome: `req-XXXX-nome_do_requisito.md`.
- Onde `XXXX` é o identificador do requisito (ex.: `0001`, `0123`).

## Escopo

- O que este requisito cobre
- O que este requisito não cobre

## Artefatos relacionados

### Documentos/requisitos que impactam este artefato

- `vis-...`
- `req-...`

### Documentos/requisitos impactados por este artefato

- `api-...`
- `dcl-...`
- `der-...`
- `did-...`

### Componentes técnicos relacionados

- Tela/componente:
- Service/use case:
- Endpoint/API:
- Tabela/view/procedure:
- Script/job/fila:
- Protótipo/design:

## Descrição geral

- Contexto do requisito:
- Problema que resolve:
- Usuário/ator principal:
- Gatilho:
- Resultado esperado:

## Wireframe da página/interface

> **Obrigatório para requisitos que descrevam interfaces** como HTML, WebForms, SPA, MVC, páginas web, telas, formulários, dashboards, wizards e similares.

### Imagem do wireframe (preferencial)

> Inserir imagem incorporada ou referenciada sempre que disponível.
> Caminho padrão: `documentacao/processo_unificado/artefatos_aprovados/detalhamento_requisitos/wireframes/`.

![Wireframe da página](./wireframes/req-XXXX-wireframe.png)

### Fallback ASCII (obrigatório quando não houver imagem)

```text
+---------------------------------------------------------------+
| Título da Página                                              |
+---------------------------------------------------------------+
| Filtro 1: [__________]  Filtro 2: [__________]  [Buscar]      |
+---------------------------------------------------------------+
| Menu/Resumo                                                   |
|                                                               |
| +--------------------- Área principal ----------------------+ |
| | Campo A: [________]                                      | |
| | Campo B: [________]                                      | |
| | Tabela/Lista/Conteúdo                                    | |
| | [Ação Primária] [Ação Secundária]                        | |
| +----------------------------------------------------------+ |
|                                                               |
+---------------------------------------------------------------+
```

### Observações do wireframe

- Componentes principais da interface:
- Estados esperados (carregando, vazio, erro, sucesso):
- Responsividade/comportamento por dispositivo:
- Observações de navegação/UX:

## Requisitos funcionais

| ID | Descrição | Prioridade | Regra de negócio associada |
| -- | --------- | ---------- | -------------------------- |
| RF-01 | Descrever requisito funcional | Alta/Média/Baixa | RN-01 |

## Regras de negócio

| ID | Regra | Origem | Impacto |
| -- | ----- | ------ | ------- |
| RN-01 | Descrever regra de negócio | Origem do requisito | Impacto esperado |

## Critérios de aceitação

```gherkin
Funcionalidade: Nome da funcionalidade
  Como um perfil de usuário
  Quero realizar uma ação
  Para obter um benefício

  Cenário: Fluxo principal
    Dado que ...
    Quando ...
    Então ...
```

## Cenários alternativos e de exceção

- Cenário alternativo 1:
- Cenário de erro 1:
- Cenário de permissão:
- Cenário de limite:

## Requisitos não funcionais

| Categoria | Requisito | Métrica/critério |
| --------- | --------- | ---------------- |
| Desempenho | Descrever requisito | Ex.: até 500ms em 95% das requisições |
| Segurança | Descrever requisito | Ex.: autenticação obrigatória |
| Usabilidade | Descrever requisito | Ex.: mensagem clara em caso de erro |

## Matriz de risco do requisito (recorte)

> Incluir **apenas riscos e requisitos diretamente relacionados** ao requisito deste documento.

| ID do requisito relacionado | Risco | Impacto (1-5) | Probabilidade (1-5) | Score (I×P) | Nível (Baixo/Médio/Alto/Crítico) | Mitigação | Referência na matriz global |
| --------------------------- | ----- | ------------- | ------------------- | ----------- | --------------------------------- | --------- | --------------------------- |
| REQ-XXXX | Descrever risco direto | - | - | - | - | - | `matriz-risco-sistema.md#item-...` |

## Análise de Pontos de Função (APF) vinculada

- **Documento APF obrigatório:** `apf-req-XXXX.md`
- **Localização:** mesmo diretório deste requisito (`detalhamento_requisitos`)
- **Regra:** toda criação/atualização de requisito deve criar/atualizar o respectivo documento APF.

## Impactos técnicos

### Backend

-

### Frontend / JavaScript / TypeScript

-

### Banco de dados

- Tabelas:
- Views:
- Procedures/functions:
- Triggers/jobs:

### Integrações externas

-

### Design / UX

-

## Rastreabilidade

| Item | Referência |
| ---- | ---------- |
| User story / demanda | |
| Código relacionado | |
| Teste relacionado | |
| Documento relacionado | |

## Validações realizadas para esta documentação

- [ ] README do projeto analisado
- [ ] Código principal e subfunções analisados
- [ ] JavaScript/frontend relacionado analisado
- [ ] Banco/procedure/view/triggers verificados
- [ ] Design/protótipos verificados
- [ ] Documentações relacionadas revisadas
- [ ] Matriz global de risco do sistema atualizada (`documentacao/matriz-risco-sistema.md`)
- [ ] Documento APF do requisito criado/atualizado (`apf-req-XXXX.md`)
- [ ] Wireframe incluído em seção própria para requisito de interface (imagem preferencial, ASCII como fallback)
- [ ] Imagem do wireframe armazenada em `documentacao/processo_unificado/artefatos_aprovados/detalhamento_requisitos/wireframes/`

## Histórico de alterações

| Data | Autor | Versão | Alteração |
| ---- | ----- | ------ | --------- |
| DD/MM/AAAA | Nome | 1.0.0 | Criação do documento |

## Esclarecimentos

- Premissas consideradas:
- Dúvidas pendentes:
- Decisões tomadas:
