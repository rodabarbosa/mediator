# Documentação em lotes

Este arquivo define o comportamento obrigatório do agente quando a demanda envolver **mais de um requisito** ou a documentação de um **sistema inteiro**.

---

## Quando aplicar o modo em lotes

O modo em lotes é ativado sempre que o usuário solicitar:

- documentação de dois ou mais requisitos simultaneamente;
- documentação de um módulo, funcionalidade ou sistema completo;
- revisão ou geração de toda a documentação de um projeto.

Para um único requisito isolado, o fluxo normal descrito no `SKILL.md` se aplica.

---

## Visão geral das fases

A documentação em lote é executada em **três fases sequenciais e obrigatórias**, na ordem abaixo. Nenhuma fase deve ser iniciada antes da anterior estar concluída e confirmada.

```
Fase 1 — Levantamento e listagem dos requisitos
        ↓
Fase 2 — Documentação dos requisitos (todos, em lote)
        ↓
Fase 3 — Complementação: design system e banco de dados
```

---

## Fase 1 — Levantamento e listagem dos requisitos

### Objetivo

Identificar, listar e priorizar **todos os requisitos** presentes no escopo antes de produzir qualquer documento.

### Comportamento obrigatório

1. Investigar o repositório, codebase, briefings, telas e demais fontes disponíveis para identificar os requisitos existentes ou necessários.
2. Consolidar os requisitos encontrados em uma **lista numerada** com os seguintes campos por requisito:

   | Campo                   | Descrição                                                |
   | ----------------------- | -------------------------------------------------------- |
   | Código                  | `req-XXXX` (sequencial)                                  |
   | Nome                    | Nome curto e descritivo do requisito                     |
   | Tipo                    | Funcional / Não funcional                                |
   | Módulo/Área             | Área do sistema que pertence                             |
   | Envolve interface?      | Sim / Não (determina se precisará de wireframe e design) |
   | Envolve banco de dados? | Sim / Não (determina se precisará de extração SQL)       |
   | Prioridade sugerida     | Alta / Média / Baixa                                     |
   | Observações             | Dependências, riscos ou notas relevantes                 |

3. Apresentar a lista ao usuário antes de iniciar qualquer documentação.
4. Aguardar confirmação, ajustes ou aprovação da lista pelo usuário.
5. Somente após aprovação, avançar para a Fase 2.

### Saída da Fase 1

Uma tabela de requisitos aprovada, que servirá como backlog de documentação para as fases seguintes. Esta tabela deve ser registrada no `README.md` como seção "Backlog de documentação".

---

## Fase 2 — Documentação dos requisitos em lote

### Objetivo

Produzir o documento principal de cada requisito (`req-XXXX-nome.md`) e seu APF (`/apf/apf-req-XXXX.md`), cobrindo funcionalidade, regras de negócio, critérios de aceitação e impactos técnicos.

### Comportamento obrigatório

1. Documentar **todos os requisitos** da lista aprovada na Fase 1, um a um, respeitando a ordem de prioridade.
2. Cada requisito deve seguir a estrutura mínima definida em `requisitos.md`.
3. **Padrão visual (design system):** para requisitos que envolvam interface, incluir a seção de wireframe com o marcador de pendência abaixo — **não bloquear o requisito por isso**:

   ```markdown
   ## Wireframe e padrão visual

   ⏳ PENDENTE [Fase 3 — Design] — wireframe e referências ao `padrao-visual.md` serão incluídos na Fase 3.
   ```

4. **Banco de dados / Integração de API:**
   - Se o documento for de Frontend: Descrever os métodos da API que retornarão as informações, destacando que o App é provido diretamente pela API.
   - Se o documento for de Backend/API (onde haja acesso direto a DB), incluir a seção de banco com o marcador de pendência abaixo — **não bloquear o requisito por isso**:

   ```markdown
   ## 8. Integração de API / Banco de dados e queries

   ⏳ PENDENTE [Fase 3 — Dados] — Os endpoints/métodos de API correspondentes devem ser detalhados na correspondente stack que acessa ou serve a funcionalidade.

   Rotinas identificadas para extração:
   - `sp_NomeDaProcedure` — chamada por `Classe.Metodo()`
   - `fn_NomeDaFunction` — utilizada em `contexto`
   ```

5. As demais seções do requisito devem estar **completas e aprovadas**: descrição, atores, fluxos, regras de negócio, critérios de aceitação em Gherkin, NFRs, impactos técnicos e artefatos relacionados.
6. Atualizar a matriz de risco global (`system-risk-matrix.md`) ao término de cada requisito.
7. Atualizar o `README.md` (TOC raiz) ao término de cada requisito.

### Ordem de execução

- Priorizar requisitos de maior impacto e menor dependência entre si.
- Quando um requisito depender de outro, documentar o requisito base primeiro.
- Registrar dependências entre requisitos na seção "Artefatos relacionados" de cada documento.

### Saída da Fase 2

- Todos os arquivos `req-XXXX-nome.md` criados.
- Todos os arquivos `/apf/apf-req-XXXX.md` criados.
- Matriz de risco global atualizada.
- TOC raiz atualizado.
- Seções de design e banco marcadas como `⏳ PENDENTE` onde aplicável.

---

## Fase 3 — Complementação: design system e integração de dados

Esta fase é dividida em dois sub-passos executados **nesta ordem**: primeiro o design, depois o banco de dados.

---

### Fase 3A — Design system e padrão visual

#### Objetivo

Complementar todos os requisitos que possuem seções de design marcadas como `⏳ PENDENTE [Fase 3 — Design]`.

#### Comportamento obrigatório

1. Investigar ou consolidar o design system do projeto (ver `investigacao.md` — Onda 003).
2. Criar ou atualizar o artefato `padrao-visual.md` em `/docs/design-system/` com o conteúdo mínimo exigido pela regra 33 de `regras.md`.
3. Para **cada requisito** que envolva interface:
   - Produzir o wireframe (imagem preferencial; fallback ASCII quando a imagem não estiver disponível).
   - Salvar a imagem em `/docs/requirement/wireframes/req-XXXX-wireframe.png`.
   - Substituir o marcador `⏳ PENDENTE [Fase 3 — Design]` pelo wireframe real e pela referência ao `padrao-visual.md`.
   - Registrar tokens, componentes, layouts e fluxos de navegação impactados pelo requisito.
4. Atualizar o `README.md` com o status dos artefatos de design (`aprovado` ou `em revisão`).

#### Saída da Fase 3A

- `padrao-visual.md` criado/atualizado.
- Wireframes produzidos e referenciados em cada requisito.
- Marcadores `⏳ PENDENTE [Fase 3 — Design]` substituídos em todos os requisitos.

---

### Fase 3B — Integração de Dados

#### Objetivo

Complementar todos os requisitos que possuem seções de dados marcadas como `⏳ PENDENTE [Fase 3 — Dados]`.

#### Comportamento obrigatório

1. Para **cada requisito** com pendência de banco:
   - Identificar e documentar os endpoints de API ou requests de dados pendentes.
   - Substituir o marcador `⏳ PENDENTE [Fase 3 — Dados]` pelo conteúdo real extraído, usando o template de subseção SQL definido em `requisitos.md`.
   - Se a conexão ao banco estiver indisponível, **manter o marcador** e adicionar data de tentativa: `⏳ PENDENTE [Fase 3 — Dados] — tentativa em DD/MM/AAAA`.
2. Atualizar o dicionário de dados (`did-nome.md`) em `/docs/database/` com os campos e tabelas mapeados.
3. Criar ou atualizar diagramas DER em `/docs/diagrams/` quando o volume de entidades justificar.
4. Atualizar o `README.md` com o status dos artefatos de banco.

#### Saída da Fase 3B

- Corpo SQL de todas as rotinas incluído nos respectivos requisitos.
- Dicionário de dados atualizado.
- DER criado/atualizado quando necessário.
- Marcadores `⏳ PENDENTE [Fase 3 — Dados]` substituídos ou mantidos com data de tentativa.

---

## Comunicação e rastreabilidade entre fases

- Ao final de cada fase, o agente deve apresentar um **resumo de conclusão** informando: o que foi produzido, o que ficou pendente e o que será feito na próxima fase.
- O `README.md` deve refletir o status atualizado dos artefatos após cada fase.
- Nunca avançar de fase sem registrar o estado atual no TOC raiz.

### Modelo de resumo de fase

```markdown
## Resumo — Fase X concluída

### Produzido
- req-0001-nome.md ✅
- req-0002-nome.md ✅
- /apf/apf-req-0001.md ✅
- /apf/apf-req-0002.md ✅

### Pendências para a próxima fase
- req-0001: wireframe e padrao-visual.md ⏳
- req-0002: integração com API pendentes ⏳

### Próxima fase
Fase 3A — Design system e padrão visual
```

---

## Checklist do modo em lotes

### Fase 1


- [ ] Todos os requisitos do escopo identificados e listados.
- [ ] Tabela de requisitos apresentada ao usuário.
- [ ] Aprovação do usuário registrada antes de avançar.
- [ ] Backlog de documentação incluído no `README.md`.


### Fase 2

- [ ] Todos os requisitos documentados (`req-XXXX-nome.md`).
- [ ] Todos os APFs produzidos (`/apf/apf-req-XXXX.md`).
- [ ] Seções de design marcadas como `⏳ PENDENTE [Fase 3 — Design]` onde aplicável.
- [ ] Seções de banco marcadas como `⏳ PENDENTE [Fase 3 — Dados]` com lista de rotinas identificadas.
- [ ] Matriz de risco global atualizada.
- [ ] TOC raiz atualizado.


### Fase 3A — Design

- [ ] `padrao-visual.md` criado/atualizado.
- [ ] Wireframes produzidos para todos os requisitos com interface.
- [ ] Marcadores `⏳ PENDENTE [Fase 3 — Design]` substituídos em todos os requisitos.

- [ ] README atualizado com status de design.

### Fase 3B — Integração de Dados

- [ ] Integração de endpoints da API ou dados extraídos para todas as rotinas identificadas.
- [ ] Dicionário de dados atualizado.
- [ ] DER criado/atualizado quando necessário.
- [ ] Marcadores `⏳ PENDENTE [Fase 3 — Dados]` substituídos ou mantidos com data de tentativa.
- [ ] README atualizado com status de banco.
