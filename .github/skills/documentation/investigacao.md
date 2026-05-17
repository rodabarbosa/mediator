# Roteiro de Investigação

Este roteiro define como investigar sistemas sem documentação prévia com profundidade suficiente para gerar documentação técnica e funcional rica, rastreável e reutilizável.

---

## 1. Princípios obrigatórios

- Investigar com base em **evidência** — nunca por suposição.
- Ler `README.md`, `AGENTS.md`, `copilot-instructions.md` e documentos correlatos antes de iniciar.
- Usar a skill `copilot-planning` para planejar a investigação em ondas e lotes, com histórico persistente em `planning/`.
- Definir as ondas **dinamicamente** a partir da demanda do usuário, do escopo e dos riscos da investigação.
- Quebrar tópicos complexos em múltiplas ondas sequenciais quando houver alto volume de telas, serviços, schemas, rotinas SQL, componentes, fluxos ou dependências cruzadas.
- Produzir documentação rica em detalhes, com referências de arquivos, métodos, tabelas, queries, telas, componentes, campos, máscaras, fluxos e pendências explícitas.

---

## 2. Artefatos obrigatórios de saída

Ao longo da investigação, produzir ou atualizar os seguintes artefatos canônicos:

| Artefato                                         | Local                                                                                     |
| ------------------------------------------------ | ----------------------------------------------------------------------------------------- |
| TOC raiz                                         | `/docs/README.md`                                                                         |
| Mapeamento do sistema                            | `/docs/system-mapping.md`                                                                 |
| Arquitetura e tech stack                         | `/docs/architecture-tech-stack.md`                                                        |
| Padrão visual (quando houver interface)          | `/docs/design-system/padrao-visual.md`                                                    |
| Requisitos, APIs, diagramas, dicionário de dados | Conforme estrutura oficial em `estrutura.md`                                              |
| Histórico de ondas                               | `planning/onda-XXX/1-plano.md`, `2-checklist.md`, `3-resumo.md`, `4-melhorias-e-erros.md` |

---

## 3. Modelo obrigatório de planejamento em ondas

As ondas devem ser definidas **dinamicamente** conforme a demanda do usuário. Os tópicos abaixo representam um **catálogo de frentes possíveis de investigação** — não uma sequência fixa obrigatória.

Antes de criar as ondas, o agente deve:

1. Identificar o objetivo real pedido pelo usuário.
2. Separar o que está em escopo e fora de escopo.
3. Selecionar apenas os tópicos do roteiro necessários para responder à demanda.
4. Combinar tópicos próximos em uma mesma onda quando isso reduzir fragmentação sem perder rastreabilidade.
5. Dividir tópicos extensos em múltiplas ondas quando houver risco de perda de qualidade ou alto volume de evidências.

> Quando a demanda for pequena, uma única onda pode cobrir múltiplos tópicos. Quando a demanda for ampla, um único tópico pode gerar várias ondas encadeadas.

### Catálogo de frentes de investigação

| Onda base | Tópico                                          | Objetivo                                                                              | Critério de quebra                                                                |
| --------- | ----------------------------------------------- | ------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Onda 001  | Contexto e negócio                              | Entender domínio, atores, regras críticas e fluxos de valor                           | Quebrar se houver múltiplos domínios ou muitos perfis de usuário                  |
| Onda 002  | UX, navegação e acessibilidade                  | Mapear jornadas, telas, navegação, estados e comportamentos de interface              | Quebrar se houver muitas telas, múltiplos módulos de UI ou UX distinto por perfil |
| Onda 003  | Design system e padrão visual                   | Consolidar tokens, componentes, estilos, layouts e diretrizes visuais reproduzíveis   | Quebrar se houver mais de um tema, design system amplo ou múltiplas bibliotecas   |
| Onda 004  | Arquitetura e infraestrutura                    | Identificar estilo arquitetural, integrações, deployment, segurança e observabilidade | Quebrar se houver múltiplos serviços, ambientes ou arquiteturas híbridas          |
| Onda 005  | Engenharia de dados: modelo e integridade       | Mapear schema, tabelas, relacionamentos, índices, constraints e jobs                  | Quebrar se houver múltiplos bancos, schemas ou alto volume de entidades           |
| Onda 006  | Engenharia de dados: rotinas SQL e dependências | Extrair procedures, functions, triggers, views e dependências reais                   | Quebrar se houver grande volume de rotinas ou forte acoplamento entre elas        |
| Onda 007  | Código-fonte, APIs e padrões                    | Mapear pastas, camadas, dependências, DI, erros, contratos e validações               | Quebrar se o sistema tiver múltiplos backends/frontends ou muitos endpoints       |
| Onda 008  | Consolidação, rastreabilidade e revisão cruzada | Fechar lacunas, revisar consistência e atualizar documentos relacionados              | Quebrar se houver muitos requisitos/artefatos afetados simultaneamente            |

> Use a numeração acima como **referência metodológica**. Na execução real, a numeração e a quantidade de ondas devem seguir o histórico incremental em `planning/` e a necessidade concreta da demanda atual.

---

## 4. Estrutura detalhada por onda

### Onda 001 — Contexto e negócio

**Objetivo:** entender o porquê do sistema, seu valor de negócio, atores e fluxos críticos.

**Investigar obrigatoriamente:**
- Elevator pitch do sistema em até 3 frases.
- Objetivos de negócio e métricas implícitas no produto.
- Glossário de termos do domínio.
- Atores, perfis, permissões e restrições.
- Fluxos críticos, caminho feliz e exceções.
- Regras de ouro que jamais podem ser quebradas.
- Integrações externas relevantes ao negócio.

**Entregáveis mínimos:**
- Atualização do `system-mapping.md` com atores, fluxos críticos e riscos.
- Referências aos requisitos, módulos ou áreas impactadas.
- Backlog de lacunas de negócio ainda não confirmadas.

---

### Onda 002 — UX, navegação e acessibilidade

**Objetivo:** documentar como a aplicação é usada, percorrida e percebida pelo usuário.

**Investigar obrigatoriamente:**
- Arquitetura de informação e hierarquia de telas.
- Mapa de navegação, menus, breadcrumbs, estados e transições.
- Jornadas por perfil, inclusive erros e desvios.
- Eventos de interface (`click`, `blur`, `change`, `submit`, atalhos).
- Estados globais da interface (loading, erro, vazio, sucesso).
- Critérios de acessibilidade, foco, teclado, contraste e feedback.

**Entregáveis mínimos:**
- Atualização do `padrao-visual.md` com navegação e fluxos.
- Identificação de telas e wireframes exigidos.
- Relação entre jornada do usuário e componentes/telas envolvidos.

---

### Onda 003 — Design system e padrão visual

**Objetivo:** permitir a reprodução fiel do design do sistema sem depender de memória tácita.

**Investigar obrigatoriamente:**
- Paleta de cores com uso semântico.
- Tipografia, grid, espaçamento, bordas, sombras e breakpoints.
- Biblioteca de componentes e variações.
- Estados visuais de cada componente.
- Ícones, ilustrações, temas e assets gráficos.
- Tokens de design e mapeamento para código.
- Padrões de formulário, máscaras, validações visuais e mensagens.
- Microinterações, animações e regras de composição visual.

**Entregáveis mínimos:**
- Criação ou atualização detalhada do `padrao-visual.md`.
- Referência a arquivos, componentes, folhas de estilo, design tokens ou protótipos.
- Registro do que está implementado, divergente ou pendente.

---

### Onda 004 — Arquitetura e infraestrutura

**Objetivo:** identificar onde o sistema reside e como suas partes se conectam.

**Investigar obrigatoriamente:**
- Estilo arquitetural predominante.
- Linguagens, frameworks e runtimes.
- Serviços, aplicações, jobs, workers e integrações.
- Protocolos de comunicação e contratos entre componentes.
- Hospedagem, ambientes, deploy e configuração.
- Autenticação, autorização, secrets, observabilidade e segurança.
- Dependências externas críticas e pontos de falha.

**Entregáveis mínimos:**
- Atualização do `architecture-tech-stack.md`.
- Lista de componentes e integrações com responsabilidades.
- Riscos arquiteturais e dependências relevantes para documentação posterior.

---

### Onda 005 — Engenharia de dados: modelo e integridade

**Objetivo:** entender o modelo de dados, sua integridade e evolução estrutural.

**Investigar obrigatoriamente:**
- Banco principal, bancos auxiliares e schemas.
- Tabelas, views, coleções e seus propósitos.
- PKs, FKs, constraints, índices e volumetria esperada.
- Entidades centrais do domínio e relacionamentos.
- Migrações, versionamento de schema e jobs de manutenção.
- Processos em segundo plano e persistência derivada.

**Entregáveis mínimos:**
- Atualização do `system-mapping.md` com entidades e riscos de dados.
- Insumos para DER e dicionário de dados.
- Identificação das rotinas SQL que exigirão extração completa na onda seguinte.

---

### Onda 006 — Engenharia de dados: rotinas SQL e dependências

**Objetivo:** extrair a lógica real do banco e suas dependências operacionais.

**Investigar obrigatoriamente:**
- Procedures, functions, triggers e views usadas pela funcionalidade.
- Payload ou endpoint da API completo de cada rotina, via documentação/API, quando aplicável.
- Tabelas, filtros, joins, subqueries e ordenações utilizadas.
- Chamadas encadeadas entre rotinas.
- Origem da chamada nas camadas web/backend.
- Impactos de performance, segurança e consistência.

**Entregáveis mínimos:**
- SQL completo documentado no artefato canônico correspondente.
- Dependências entre rotinas registradas.
- Pendências explicitadas apenas quando a conexão com o banco estiver indisponível (`⏳ PENDENTE [Fase 3 — Dados]`).

---

### Onda 007 — Código-fonte, APIs e padrões

**Objetivo:** mapear a engrenagem do sistema em frontend, backend e integrações.

**Investigar obrigatoriamente:**
- Organização de pastas e convenções arquiteturais.
- Classes, métodos, componentes e subfunções críticas.
- Contratos de API, DTOs, schemas, serializers e validações.
- Middlewares, DI, tratamento de exceção e logging.
- Dependências externas e internas.
- Campos, máscaras, validações e formatos de payload.
- Rastreabilidade entre interface, serviço e persistência.

**Entregáveis mínimos:**
- Insumos para requisitos, documentação de API e diagramas.
- Inventário de endpoints, métodos, componentes e arquivos relevantes.
- Gaps técnicos ou divergências entre código e documentação.

---

### Onda 008 — Consolidação, rastreabilidade e revisão cruzada

**Objetivo:** consolidar descobertas, fechar inconsistências e preparar a documentação final.

**Investigar obrigatoriamente:**
- Coerência entre artefatos produzidos.
- Links cruzados entre requisito, API, diagramas, banco, risco e padrão visual.
- Lacunas pendentes, dúvidas e decisões abertas.
- Necessidade de revisão de documentos já existentes.
- Atualização do TOC raiz e status dos artefatos.

**Entregáveis mínimos:**
- Documentação consolidada e versionada.
- Revisão cruzada registrada.
- Lista explícita do que foi validado e do que permanece pendente.

---

## 5. Lotes recomendados por onda

Cada onda deve ser executada com ao menos estes lotes:

1. **Coleta de evidências:** leitura de arquivos, busca de código, inspeção de banco e fontes externas autorizadas.
2. **Consolidação analítica:** síntese das descobertas, identificação de conflitos e definição do recorte documental.
3. **Atualização de artefatos:** edição dos documentos canônicos, diagramas, tabelas e listas de rastreabilidade.
4. **Validação cruzada:** conferência de consistência entre artefatos, checklist da onda e pendências.

---

## 6. Regras de composição dinâmica das ondas

- Se a demanda for focada em um único problema, criar apenas as ondas estritamente necessárias.
- Se a demanda envolver UX + design + navegação do mesmo fluxo, esses temas podem ficar na mesma onda ou em ondas separadas, conforme o volume de evidências.
- Se a demanda envolver somente documentação de API, não criar ondas de banco ou padrão visual sem justificativa concreta.
- Se a demanda tocar vários subsistemas, separar as ondas por domínio, camada ou risco dominante.
- Se durante a execução surgir nova complexidade, abrir novas ondas sequenciais em vez de inflar artificialmente a onda corrente.
- Sempre registrar no plano da onda por que aquela decomposição foi escolhida para a demanda atual.

---

## 7. Regras para quebrar ondas complexas

Quebre uma onda em duas ou mais quando ocorrer qualquer situação abaixo:

- Mais de 10 telas relevantes no mesmo fluxo.
- Mais de 15 endpoints ou contratos a comparar.
- Mais de 20 entidades/tabelas ou múltiplos schemas envolvidos.
- Mais de 10 procedures/functions/triggers/views relevantes.
- Múltiplos frontends ou backends independentes.
- Múltiplos perfis com navegação muito distinta.
- Dependência de múltiplas integrações externas.
- Alto risco de inconsistência caso a consolidação fique concentrada em uma única onda.

Quando quebrar, manter numeração sequencial (`onda-009`, `onda-010`, etc.) e registrar claramente o motivo da subdivisão.

---

## 8. Perguntas-gatilho por tópico

### Contexto e negócio
- Qual o propósito do sistema e qual problema de negócio ele resolve?
- Quais fluxos, se pararem, impedem a operação da empresa?
- Quais termos do domínio precisam de glossário explícito?
- Quais perfis existem e o que cada um pode fazer?

### UX, navegação e acessibilidade
- Como o usuário entra, navega e sai de cada fluxo?
- Quais telas, menus e rotas compõem a jornada?
- Quais estados de loading, erro, vazio e sucesso existem?
- Como navegação por teclado, foco e contraste são tratados?

### Design system e padrão visual
- Existe padrão visual formal ou ele precisa ser inferido do código?
- Quais cores, fontes, grids e espaçamentos são usados?
- Quais componentes existem e quais estados cada um suporta?
- Quais máscaras, formatos e feedbacks visuais existem em formulários?

### Arquitetura e infraestrutura
- Qual é o estilo arquitetural real do sistema?
- Onde o sistema roda e como é distribuído entre ambientes?
- Como os componentes se comunicam?
- Quais controles de segurança e observabilidade existem?

### Engenharia de dados
- Quais entidades, tabelas e relacionamentos sustentam o domínio?
- Quais índices e constraints garantem integridade e performance?
- Quais rotinas SQL contêm lógica crítica?
- Como as mudanças de schema são gerenciadas?

### Código-fonte, APIs e padrões
- Como o código está organizado e quais convenções predominam?
- Quais classes, métodos, componentes e endpoints são o núcleo do fluxo?
- Como exceções, logs, DI e validações são tratados?
- Quais campos, máscaras e formatos precisam aparecer na documentação final?

---

## 9. Critério de riqueza documental

A documentação resultante deve ser rica em detalhes. No mínimo, cada tópico investigado deve registrar:

- Fontes de evidência utilizadas.
- Arquivos, componentes, tabelas, rotinas ou endpoints relevantes.
- Fluxos e dependências entre elementos.
- Exemplos concretos quando existirem.
- Status de validação: `validado`, `inferido`, `pendente validação`.
- Riscos, dúvidas e próximos passos.

---

## 10. Checklist final do roteiro

- [ ] Planejamento em ondas criado no diretório `planning/`.
- [ ] As ondas foram definidas dinamicamente conforme a demanda atual.
- [ ] Apenas os tópicos necessários foram incluídos no planejamento.
- [ ] Ondas complexas quebradas em ondas adicionais quando necessário.
- [ ] `/docs/README.md` atualizado com TOC e links canônicos.
- [ ] `system-mapping.md` atualizado.
- [ ] `architecture-tech-stack.md` atualizado.
- [ ] `padrao-visual.md` criado/atualizado quando houver interface.
- [ ] Evidências técnicas e funcionais registradas com riqueza de detalhes.
- [ ] Pendências e lacunas claramente explicitadas.
