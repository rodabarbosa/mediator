---
name: documentation
description: >
  Especialista unificado em documentação de software — requisitos, documentação técnica e governança.
  Cobre investigação profunda de código, rastreabilidade entre artefatos, estrutura obrigatória de
  pastas, TOC na raiz, matriz de risco global, APF por requisito e validação cruzada entre artefatos.
  Quando não existir documentação prévia, aplica obrigatoriamente o roteiro de investigação.
keywords:
  [
    documentação,
    documentação de software,
    requisitos,
    documentação técnica,
    governança,
    rastreabilidade,
    matriz de risco,
    APF,
    TOC,
    investigação de código,
    estrutura de pastas,
    critérios de aceitação,
    Gherkin,
    NFRs mensuráveis,
    diagramas,
    histórico de alterações,
    esclarecimentos,
    revisão cruzada,
    mapeamento,
    design system,
    wireframe,
    dicionário de dados,
    API,
    diagrama de classes,
    DER,
    banco de dados,
    procedure,
    database-mcp,
  ]
---

# Skill: documentation

Esta skill consolida as práticas de documentação de software para produzir artefatos **claros, rastreáveis, testáveis e sem ambiguidades**.

## Quando usar

- Criar ou atualizar documentação de requisitos e documentação técnica.
- Ao documentar requisito, produzir obrigatoriamente o par `req-XXXX` (negócio/stackholders) e `tec-req-XXXX` (implementação técnica para desenvolvedores).
- Se disponíveis no workspace, usar as skills `clarification` e `brainstorming` para melhorar entendimento do requisito, elaborar cenários Gherkin mais completos, antecipar riscos e criar cenários exploratórios para áreas ainda não descritas.
- Padronizar estrutura de pastas e nomenclatura de artefatos.
- Garantir critérios de aceitação em Gherkin e NFRs mensuráveis.
- Investigar profundamente o ecossistema técnico relacionado ao requisito.
- Registrar referências de artefatos impactados e impactantes.
- Ao criar novo requisito ou alterar requisito existente, avaliar obrigatoriamente impacto em artefatos correlatos e atualizar os necessários (arquitetura, DER, diagrama de classes, integrações, dicionário de dados, APF, matriz de risco e demais documentos relacionados).
- Manter histórico de alterações e seção de esclarecimentos nos documentos.
- Quando não existir documentação prévia do sistema, aplicar o roteiro de `investigacao.md`.

---

## Sub-arquivos desta skill

| Arquivo                                | Responsabilidade                                                                            |
| -------------------------------------- | ------------------------------------------------------------------------------------------- |
| [`SKILL.md`](./SKILL.md)               | Entrada principal — visão geral, modos de operação e checklist                              |
| [`regras.md`](./regras.md)             | Regras mandatórias completas do agente                                                      |
| [`estrutura.md`](./estrutura.md)       | Estrutura de pastas, prefixos e TOC obrigatório                                             |
| [`lotes.md`](./lotes.md)               | **Modo em lotes — fases, comportamentos e checklist para múltiplos requisitos ou sistemas** |
| [`requisitos.md`](./requisitos.md)     | Especificação de requisitos, Gherkin, APF, wireframe e matriz de risco                      |
| [`api.md`](./api.md)                   | Padrão de documentação de APIs                                                              |
| [`diagramas.md`](./diagramas.md)       | Tipos de diagrama e orientações de uso                                                      |
| [`nfr.md`](./nfr.md)                   | Requisitos não funcionais — padrão de escrita                                               |
| [`escrita.md`](./escrita.md)           | Princípios de escrita técnica e Docs-as-Code                                                |
| [`template/`](./template/)             | Pasta com templates obrigatórios de histórico, esclarecimentos e artefatos relacionados     |
| [`investigacao.md`](./investigacao.md) | Roteiro completo de investigação de sistemas sem documentação prévia                        |

---

## Modos de operação

### Modo padrão — requisito único

Usado quando o usuário solicita a documentação de **um único requisito**. Seguir as etapas abaixo na íntegra.

### Modo em lotes — múltiplos requisitos ou sistema completo

Usado quando o usuário solicita a documentação de **dois ou mais requisitos** ou de um **módulo/sistema inteiro**. Neste caso, **ler e seguir obrigatoriamente o arquivo `lotes.md`** antes de qualquer outra ação. O modo em lotes substitui o fluxo de etapas abaixo e define suas próprias fases sequenciais.

> 🔀 **Regra de decisão:** se a demanda envolver mais de um requisito ou o usuário mencionar "sistema", "módulo", "todos os requisitos" ou similar → ativar modo em lotes (`lotes.md`).

---

## Etapas obrigatórias de trabalho (modo padrão)

1. Ler o `README.md`, `AGENTS.md`, `copilot-instructions.md` e demais instruções do projeto antes de documentar.
2. Quando não houver documentação prévia, aplicar o roteiro de `investigacao.md`.
3. Planejar a investigação com `copilot-planning`, estruturando ondas e lotes dinâmicos em `planning/`.
4. Se disponíveis, aplicar as skills `clarification` e `brainstorming` para refinar entendimento do requisito, levantar ambiguidades e gerar cenários Gherkin robustos.
5. Realizar brainstorming: atores, cenários, exceções, integrações, ambiguidades e cenários exploratórios para áreas não pensadas/descreitas inicialmente.
6. Definir escopo (Dentro do escopo e Fora do escopo).
7. Executar investigação profunda: código, frontend, backend, banco, integrações, UX e design.
8. Identificar artefatos impactados e impactantes.
9. Avaliar impacto em documentos correlatos (arquitetura, DER, diagrama de classes, integrações, dicionário de dados, APF, matriz de risco e demais artefatos existentes) e definir plano de atualização.
10. Produzir ou atualizar artefatos na estrutura de pastas oficial (ver `estrutura.md`).
11. Validar rastreabilidade: requisito ↔ cenário ↔ teste ↔ artefatos técnicos.
12. Revisar documentações relacionadas para adequações.
13. Incrementar versão dos documentos alterados.
14. Atualizar histórico e esclarecimentos.
15. Atualizar TOC raiz da documentação (`/docs/README.md`).
16. Em sistemas sem documentação prévia, criar/atualizar `system-mapping.md` e `architecture-tech-stack.md`.

> Consulte `regras.md` para o detalhamento completo de cada regra mandatória.

---

## Checklist de entrega (obrigatório)

### Modo em lotes (quando aplicável)

- [ ] Demanda avaliada: se mais de um requisito ou sistema → `lotes.md` lido e aplicado.
- [ ] Fase 1 concluída: lista de requisitos apresentada e aprovada pelo usuário.
- [ ] Fase 2 concluída: todos os requisitos documentados com pendências marcadas.
- [ ] Fase 3A concluída: design system e wireframes complementados.
- [ ] Fase 3B concluída: endpoints de API ou dependências de dados complementados.
- [ ] Resumo de conclusão de cada fase apresentado ao usuário.

### Estrutura e governança

- [ ] Estrutura de pastas oficial preservada (ver `estrutura.md`).
- [ ] TOC raiz em `/docs/README.md` criado/atualizado.
- [ ] Prefixo e diretório corretos para cada artefato.
- [ ] Campo de versão presente e incrementado no documento.
- [ ] `README.md` do projeto lido antes de qualquer documentação.

### Investigação

- [ ] Se sistema sem documentação prévia: roteiro de `investigacao.md` aplicado.
- [ ] Investigação profunda executada: código, JS, validações, banco e integrações.
- [ ] Artefatos impactantes e impactados identificados e referenciados.
- [ ] `system-mapping.md` criado/atualizado.
- [ ] `architecture-tech-stack.md` criado/atualizado.

### Requisitos e artefatos

- [ ] Par de requisitos criado/atualizado: `req-XXXX-...` (texto claro para stackholders) e `tec-req-XXXX-...` (detalhamento técnico para desenvolvimento).
- [ ] Documento APF correspondente criado/atualizado no diretório dedicado (`apf/apf-req-XXXX.md`).
- [ ] Matriz de risco global atualizada (`system-risk-matrix.md`).
- [ ] Avaliação de impacto executada para documentos correlatos e correções aplicadas quando necessário (arquitetura, DER, diagrama de classes, integrações, dicionário de dados, APF, matriz de risco etc.).
- [ ] Wireframe incluído (imagem preferencial + fallback ASCII) para requisitos de interface.
- [ ] Critérios de aceitação testáveis em Gherkin.
- [ ] Se disponíveis, `clarification` e `brainstorming` aplicadas para fortalecer cenários Gherkin e cobertura exploratória.
- [ ] NFRs mensuráveis.
- [ ] Regras funcionais e regras de negócio numeradas de forma incremental, única e sem repetição.

### Banco de dados

- [ ] Para cada API: endpoint e payload documentados. Para procedure: corpo SQL completo documentado quando aplicável.
- [ ] Nenhuma procedure está com status "não disponível" sem marcação `⏳ PENDENTE [Fase 3 — Dados]`.

### APIs

- [ ] APIs documentadas com request/response, validações, erros e diagrama de sequência.
- [ ] Máscaras e formatos de campos descritos.

### Finalização

- [ ] Histórico de alterações atualizado.
- [ ] Seção de esclarecimentos adicionada ao final.
- [ ] Revisão cruzada com documentos relacionados realizada.

---

## Geração de Imagem de Wireframe

Para requisitos que possuem componente visual, é recomendada a geração de um mockup em imagem para facilitar a compreensão, mantendo o fallback em ASCII no documento. Para gerar a imagem, siga o processo abaixo:

1.  Crie temporariamente um arquivo HTML estático (ex: `scripts/mockup-req-0002.html`) estruturado com HTML e CSS que simule o layout da página, aplicando cores e identidades visuais do design system da aplicação (LexNova).
2.  Use o script genérico fornecido junto à skill (`.github/skills/documentation/capture-mockup.cjs`) enviando o arquivo HTML como entrada e determinando como saída um PNG dentro da pasta `docs/requirement/wireframe/`.
    _Exemplo de execução:_
    `node .github/skills/documentation/capture-mockup.cjs scripts/mockup-req-0002.html docs/requirement/wireframe/wireframe-req-0002.png`
3.  Após garantir que a imagem gerada foi concluída com sucesso, **exclua o arquivo HTML temporário**.
4.  No arquivo de requisito `.md`, preencha os dois formatos no tópico do Wireframe: aponte a imagem real `![Wireframe](wireframe/wireframe-req-0002.png)` e logo abaixo, mantenha uma representação simples ou fallback em ASCII.
