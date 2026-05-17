# Regras mandatórias do agente

Este arquivo detalha todas as regras de comportamento obrigatório do agente de documentação.

---

## Regras gerais

1. **Não alterar código-fonte:** esta skill atua exclusivamente em documentação.
2. **Ler o `README.md` do projeto** antes de documentar, para entender contexto, estrutura, domínio e stack.
3. **Ler documentos de orientação do repositório** quando existirem: `AGENTS.md`, `copilot-instructions.md`, `*.instructions.md` e os templates a serem utilizados, que estão localizados na subpasta `template` (junto a esta skill).
4. **Validar informações com o código/repositório** sempre que aplicável.
5. **Toda documentação deve citar artefatos relacionados:** requisitos, diagramas, APIs, tabelas, procedures, jobs, scripts, telas e demais elementos que impactam ou são impactados.
6. **Toda atualização exige revisão cruzada das documentações relacionadas**, para identificar e registrar adequações necessárias.
7. **Manter documentação precisa e inequívoca:** sem termos vagos como "rápido", "fácil", "seguro" sem métrica.
8. **Registrar histórico de mudanças** em todo documento atualizado/criado.
9. **Adicionar seção de esclarecimentos** ao final de cada documento.
10. **Manter consistência entre documentos relacionados:** se um requisito muda, revisar impactos em visão, API, diagramas, dicionário de dados e demais artefatos.
11. **Se disponíveis no workspace, utilizar as skills `clarification` e `brainstorming`** para aprimorar entendimento do requisito, antecipar problemas, ampliar cobertura de cenários Gherkin e criar cenários exploratórios para áreas ainda não descritas.
12. **Ao criar novo requisito ou alterar requisito existente, é obrigatória a avaliação e correção de documentos correlatos** quando houver impacto, incluindo no mínimo: arquitetura, DER, diagrama de classes, integrações, dicionário de dados, APF, matriz de risco e demais artefatos vinculados.

---

## Regras de banco de dados

1. **Quando um artefato técnico não estiver no repositório** (procedures, funções SQL, views, jobs, tabelas, integrações ou contratos externos), o agente deve usar MCPs/ferramentas externas autorizadas para obter a informação na fonte.

2. **Quando a documentação exigir informação de banco de dados não materializada no repositório (ex: backend), o agente deve ...**

    12.a **A coleta dos dados da API/endpoints é obrigatória.** O documento somente deve ser finalizado com as integrações reais definidas no artefato.

3. **Quando a funcionalidade for orientada a serviço (API), executar investigação avançada via código cliente**, incluindo:
    - extração do corpo completo de procedures, functions, triggers e views;
    - identificação de tabelas, colunas, joins, filtros, ordenações e subqueries;
    - mapeamento de dependências entre rotinas;
    - levantamento de PKs, FKs, índices e constraints;
    - registro da ordem de execução (camada web → backend → banco) no diagrama de sequência em Mermaid;
    - inclusão da evidência extraída no artefato final.

> 🚨 **BLOQUEANTE — API/Dados é obrigatório:** todo endpoint de API ou procedure de banco identificada **DEVE ter sua integração incluída** na seção correspondente do documento. Documentar apenas pelo nome sem a estrutura (payload da API ou corpo no caso do backend) é **inválido e bloqueia a conclusão do artefato**. Se a API ou banco não estiver disponível, marque a seção como `⏳ PENDENTE [Fase 3 — Dados]`, liste as pendências e não substitua a marcação.

---

## Regras de requisitos e artefatos

1. **Nenhuma documentação deve ser escrita com base em suposição:** toda afirmação relevante deve ser derivada de evidência documental, código, banco, design ou validação com stakeholders.

2. **Ao documentar artefatos existentes (páginas, endpoints, funcionalidades) é obrigatório:**
    - preencher diagrama de fluxo e de sequência em **Mermaid**;
    - registrar todos os comandos SQL/Stored Procedures/queries envolvidas;
    - investigar e referenciar frontend, backend, validações, banco, procedures, views, jobs e serviços relacionados.

3. **Toda data em documentação deve usar o formato brasileiro `DD/MM/AAAA`.**

4. **Todo documento deve conter campo de versão e toda atualização deve incrementar a versão** (recomendado: SemVer `MAJOR.MINOR.PATCH`).

5. **Cada requisito deve ser documentado em dois arquivos obrigatórios no diretório `/requirement`**:
    - `req-XXXX-nome-do-requisito.md`: versão de negócio, com texto claro e objetivo para apresentação e discussão com stackholders (não desenvolvedores).
    - `tec-req-XXXX-nome-do-requisito.md`: versão técnica, contendo em detalhe as informações do `req-XXXX`, incluindo regras, instruções, diagramas, boas práticas, nomenclatura e exemplos para implementação pelos desenvolvedores.
    - Ambos devem usar o mesmo `XXXX` e permanecer sincronizados em conteúdo e versão.

    O `nome-do-requisito` na nomenclatura do arquivo **jamais deve conter acentos, cedilhas ou caracteres especiais**. Ao gerar o nome de um arquivo (não só de requisitos), substitua letras acentuadas por suas equivalentes sem acento (ex: `ç` -> `c`, `ã` -> `a`, `é` -> `e`, `í` -> `i`), remova parênteses e converta espaços em hífens (`-`). Jamais converta letras acentuadas em hífens ou as apague (ex: de "Autenticação (Login)" gere `autenticacao-login`, e nunca `autentica-o-login`).

    As **regras funcionais (RF)** e **regras de negócio (RN)** devem ter numeração incremental, única e não repetitiva, sem falhas na sequência.

6. **Todo requisito deve possuir documento APF obrigatório** na subpasta `/requirement/apf/`, com nome `apf-req-XXXX.md`.

7. **A documentação deve manter uma matriz de risco global única do sistema** em `system-risk-matrix.md`, atualizada a cada inclusão/alteração de requisito.

8. **Todo documento de requisito deve conter recorte da matriz de risco local** com apenas os riscos diretamente relacionados ao requisito.

---

## Regras de organização e unicidade

1. **É proibido gerar documentação paralela, duplicada ou concorrente fora da estrutura oficial.** Se já existir um artefato canônico, ele deve ser atualizado — não criado outro arquivo alternativo.

2. **É proibido concatenar múltiplas versões, rascunhos ou análises no mesmo arquivo.** O documento final deve ser um artefato único, coeso e consolidado, com apenas uma versão vigente no cabeçalho.

3. **Sempre que um novo requisito/documento for criado, o agente deve atualizar obrigatoriamente o `README.md` com o TOC correspondente**, garantindo que os links apontem para os artefatos canônicos.

---

## Regras de interface e design

1. **Todo requisito que descreva interface de usuário** (HTML, SPA, MVC, páginas, formulários, dashboards, wizards) **deve conter uma seção própria de wireframe.**

2. **O wireframe deve ser preferencialmente uma imagem** incorporada ou referenciada; quando não disponível, incluir **fallback obrigatório em ASCII** na mesma seção.

3. **As imagens de wireframe devem seguir o padrão de pasta** `/requirement/wireframes/`.

---

## Regras de investigação sem documentação prévia

1. **Quando o sistema não possuir documentação existente**, o agente deve usar obrigatoriamente o roteiro e as técnicas do arquivo `investigacao.md`.

2. **Durante investigação sem documentação prévia**, criar/atualizar `system-mapping.md` para registrar mapeamento, estrutura, dependências, fluxos e evidências encontradas.

3. **Durante investigação sem documentação prévia**, criar/atualizar `architecture-tech-stack.md` com arquitetura, tech stack, integrações, decisões e observações do projeto.

4. **Os artefatos `system-mapping.md` e `architecture-tech-stack.md` são contínuos e cumulativos:** devem ser atualizados em novas investigações para preservar contexto histórico.

---

## Regras de campos, máscaras e payloads

1. **Campos e máscaras obrigatórios:** ao documentar qualquer requisito com campos (payloads, formulários, DTOs, contratos de API, colunas de banco), incluir dicionário de campos com:
    - nome, caminho (JSON path ou tabela.coluna), tipo, tamanho/precisão, obrigatoriedade;
    - máscara/format quando aplicável (CPF: `000.000.000-00`; CNPJ: `00.000.000/0000-00`; CEP: `00000-000`; telefone: `(00) 00000-0000`; data: `DD/MM/AAAA`);
    - referência ao arquivo/endpoint/método ou regex que aplica a máscara;
    - exemplos de request/response com valores exemplares ou mascarados.

---

## Regras de UX e design system

1. **Investigação de UX e Padrão Visual obrigatório:** para requisitos que envolvam interface, criar ou atualizar `padrao-visual.md` em `/design-system/`. O documento deve ser suficiente para reproduzir o design do sistema, cobrindo no mínimo:
    - inventário de telas e layouts;
    - paleta de cores (HEX/RGB/HSL e uso semântico);
    - escalas tipográficas (família, tamanhos, pesos, line-height);
    - espaçamentos, grid, breakpoints responsivos, bordas, sombras e tokens;
    - ícones, ilustrações, estados de componentes (hover/focus/active/disabled/error/loading);
    - anatomia e comportamento dos componentes (botões, inputs, selects, tabelas, modais, cards, paginação);
    - diretrizes de acessibilidade (contraste, foco, tamanhos mínimos, navegação por teclado);
    - mapa de navegação e fluxo entre telas (entradas, saídas, menus, breadcrumbs, rotas, estados, transições);
    - links de protótipos (Figma/Sketch/XD) quando existirem, com versão e frames de referência.

---

## Regras de planejamento e investigação avançada

1. **Planejamento obrigatório por ondas dinâmicas:** toda investigação documental deve ser planejada com `copilot-planning` antes da execução, com registro em `planning/`. As ondas **não são fixas** — são definidas dinamicamente conforme a demanda, escopo real, artefatos impactados e complexidade encontrada.

2. **Documentação rica em detalhes:** todo artefato deve ser suficientemente detalhado para permitir continuidade por outro time sem depender de contexto oral. Inclui: evidências rastreáveis, tabelas estruturadas, exemplos concretos, fluxos, referências de métodos/rotinas, decisões registradas, lacunas explicitadas e indicação clara do que foi validado versus o que permanece pendente.

---

## Skills auxiliares e MCPs recomendados

| Recurso                         | Uso                                                                                       |
| ------------------------------- | ----------------------------------------------------------------------------------------- |
| `copilot-planning`              | Planejar investigação em ondas e lotes com histórico persistente                          |
| `database-mcp`                  | Extrair procedures, functions, views, tabelas e schema quando SQL não está no repositório |
| Exploração paralela/multiagente | Investigar backend, frontend, banco e documentação em paralelo                            |
| MCP/documentação de bibliotecas | Validar comportamento de frameworks, SDKs e integrações externas                          |
| Ferramentas de Mermaid          | Validar diagramas antes de finalizar a documentação                                       |
