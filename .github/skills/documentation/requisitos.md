# Especificação de requisitos

Este arquivo define o padrão de documentação de requisitos, critérios de aceitação, APF, wireframe e matriz de risco.

---

## Estrutura mínima para o par de documentos de requisito

Para cada requisito, criar e manter obrigatoriamente **2 documentos** com o mesmo número `XXXX`:

- **`req-XXXX-nome-do-requisito.md`**
  Documento orientado a negócio para apresentação com stackholders (não desenvolvedores), com linguagem clara, objetiva e sem detalhamento técnico excessivo.

- **`tec-req-XXXX-nome-do-requisito.md`**
  Documento técnico para implementação por desenvolvedores, com detalhes completos: regras, instruções, diagramas, boas práticas, nomenclatura, fluxos e exemplos aplicáveis.

### Estrutura mínima do documento `req-XXXX-...md`

1. **Cabeçalho:** número do requisito, título, versão, data, autor, status.
2. **Introdução:** objetivos, escopo e glossário específico do requisito.
3. **Descrição geral:** missão do produto/funcionalidade, atores e contexto.
4. **Artefatos relacionados:** documentação e requisitos que impactam ou são impactados.
5. **Requisitos funcionais:** casos de uso ou histórias de usuário.
6. **Critérios de aceitação (Gherkin):** cenários testáveis que definem quando o requisito está pronto.
7. **Requisitos não funcionais (NFRs):** desempenho, segurança, usabilidade, estabilidade etc. (ver `nfr.md`).
8. **Integração de API / Banco de Dados:** Como o frontend obtém dados via API e não conecta ao banco diretamente, para documentos de Frontend, referir APENAS aos métodos e endpoints da API consumidos. A referência direta a Banco de Dados e queries (*procedures*, etc) deve existir APENAS em documentação de requisitos específicos da API/Backend.
9. **Impactos técnicos:** código, APIs, integrações, jobs, scripts, design e dependências.
10. **Wireframe da interface:** obrigatório para requisitos que envolvam páginas/telas/interfaces.
11. **Padrão visual e fluxo de navegação:** referência ao `padrao-visual.md`, com indicação dos tokens, componentes, layouts, cores, estados e fluxos impactados.
12. **Matriz de risco local (recorte):** apenas riscos diretamente relacionados a este requisito.
13. **Histórico de alterações:** quem mudou o quê e quando.
14. **Esclarecimentos:** dúvidas, premissas e decisões registradas.

---

### Estrutura mínima do documento `tec-req-XXXX-...md`

1. **Cabeçalho:** número do requisito, título, versão, data, autor, status.
2. **Resumo do requisito de negócio:** referência ao `req-XXXX` correspondente.
3. **Detalhamento técnico completo:** regras, instruções de implementação, validações, contratos, fluxos e exceções.
4. **Regras funcionais e regras de negócio numeradas:** numeração incremental, única e não repetitiva (sem saltos indevidos).
5. **Diagramas técnicos:** fluxo, sequência e/ou arquitetura em Mermaid quando aplicável.
6. **Exemplos técnicos:** payloads, cenários, bordas, mensagens de erro e exemplos de dados.
7. **Rastreabilidade:** vínculos com código, API, banco, jobs, integrações e artefatos relacionados.
8. **Histórico de alterações** e **esclarecimentos**.

## Artefatos obrigatórios por requisito

Para cada requisito, produzir e manter no diretório `/requirement`:

| Artefato                    | Nome do arquivo                      | Obrigatoriedade                          |
| --------------------------- | ------------------------------------ | ---------------------------------------- |
| Requisito de negócio        | `req-XXXX-nome_do_requisito.md`      | Obrigatório                              |
| Requisito técnico           | `tec-req-XXXX-nome_do_requisito.md`  | Obrigatório                              |
| Análise de Pontos de Função | `/apf/apf-req-XXXX.md`               | Obrigatório                              |
| Aprovação formal            | `apr_req-nome_do_requisito.md`       | Obrigatório antes da implementação       |
| Wireframe (imagem)          | `/wireframes/req-XXXX-wireframe.png` | Obrigatório para requisitos de interface |

Além disso, atualizar obrigatoriamente:

- `system-risk-matrix.md` (matriz global) com os riscos do novo/alterado requisito.
- `README.md` (TOC raiz) com link para o novo artefato.

---

## Critérios de aceitação em Gherkin

### Estrutura padrão

Use **Given / When / Then** para tornar os critérios testáveis e automatizáveis.

- **Given (Dado):** contexto ou pré-condição.
- **When (Quando):** ação do usuário ou evento.
- **Then (Então):** resultado esperado e verificável.

```gherkin
Funcionalidade: Autenticação de usuário
  Como um usuário cadastrado
  Quero autenticar minha conta
  Para acessar recursos restritos

  Cenário: Login com credenciais válidas
    Dado que o usuário preencheu o e-mail e senha válidos
    Quando ele clica em "Entrar"
    Então ele deve ser redirecionado para a página inicial
    E deve ver a mensagem "Bem-vindo, [Nome]"

  Cenário: Login com senha incorreta
    Dado que o usuário preencheu o e-mail correto e senha incorreta
    Quando ele clica em "Entrar"
    Então deve ver a mensagem "Credenciais inválidas"
    E permanecer na tela de login
```

### Qualidade dos cenários de aceitação

- Cada cenário deve ter **uma única interpretação** — sem ambiguidade.
- Use **Scenario Outline** para variações de dados.
- Vincule cada cenário a um requisito rastreável dentro do documento.
- Inclua **cenários negativos:** erro, ausência de dados, permissão, limites.
- Garanta coerência com validações de frontend, backend e banco de dados.

---

## Subseção obrigatória — Endpoints de API ou Procedures

**⚠️ ATENÇÃO:** Devido à arquitetura do projeto, o frontend NÃO se conecta a base de dados, devendo consultar dados via API. Para requisitos de interface/frontend, detalhar os métodos, *endpoints* e integrações ao invés de código SQL.

Para requisitos que envolvam operações diretas do **Backend/API no Banco de Dados**, para **cada procedure, function, trigger ou view** identificada, incluir a subseção abaixo na seção de banco de dados do requisito:

````markdown
### 8.X Procedure `NOME_DA_PROCEDURE`

- **Chamada por:** `NomeClasse.NomeMetodo()`
- **Parâmetros:** `@PARAM1` (tipo), `@PARAM2` (tipo)
- **Fonte:** Endpoint de API documentado ou extraído em DD/MM/AAAA

```sql
-- Endpoint ou Payload completo obtido via documentação
CREATE OR ALTER PROCEDURE [dbo].[NOME_DA_PROCEDURE]
    @PARAM1 TIPO,
    @PARAM2 TIPO
AS
BEGIN
    -- SQL completo aqui
END
```

**Tabelas referenciadas:** `tabela_a`, `tabela_b`
**Dependências:** (outras procedures, functions ou views chamadas internamente)
````

> ⚠️ Se a conexão ao banco estiver indisponível, usar o marcador abaixo — **nunca omitir a subseção:**
>
> ```text
> ⏳ PENDENTE [Fase 3 — Dados] — endpoint de API ou dados ainda não extraídos.
> Procedure a recuperar: NOME_DA_PROCEDURE
> ```

---

## Wireframe

### Obrigatoriedade

Todo requisito que descreva **interface de usuário** (HTML, SPA, MVC, páginas, formulários, dashboards, wizards) **deve conter wireframe em seção própria**.

### Formato preferencial

- **Imagem** (`.png`, `.jpg`, `.svg`) armazenada em `/requirement/wireframes/req-XXXX-wireframe.png`.
- Quando a imagem não estiver disponível: **fallback em ASCII obrigatório** na mesma seção.

### Exemplo de fallback ASCII

```text
┌─────────────────────────────────────────┐
│  [Logo]           [Menu] [Perfil] [Sair]│
├─────────────────────────────────────────┤
│                                         │
│  Título da Página                       │
│                                         │
│  [ Campo 1         ]  [ Campo 2      ]  │
│  [ Campo 3                           ]  │
│                                         │
│              [ Cancelar ] [ Confirmar ] │
└─────────────────────────────────────────┘
```

---

## Matriz de risco

### Matriz global do sistema (`system-risk-matrix.md`)

Deve ser mantida e atualizada a cada inclusão/alteração de requisito. Colunas recomendadas:

| ID do Risco | Requisito relacionado | Descrição do risco                | Probabilidade | Impacto | Nível | Mitigação                               | Status |
| ----------- | --------------------- | --------------------------------- | ------------- | ------- | ----- | --------------------------------------- | ------ |
| RSK-001     | req-0001              | Falha na autenticação por timeout | Médio         | Alto    | Alto  | Implementar retry e feedback ao usuário | Aberto |

### Recorte local por requisito

Cada documento de requisito deve conter uma seção de **recorte da matriz de risco**, listando apenas os riscos diretamente relacionados ao requisito em questão.

---

## Regra de numeração de RF/RN

- Regras Funcionais (RF) e Regras de Negócio (RN) devem usar **numeração incremental, única e não repetitiva** dentro do mesmo documento.
- Não é permitido repetir identificadores (ex.: `RF-01` duplicado) nem quebrar a sequência sem justificativa.
- Padrão recomendado:
  - `RF-001`, `RF-002`, `RF-003`, ...
  - `RN-001`, `RN-002`, `RN-003`, ...
