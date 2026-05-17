# Estrutura de pastas e nomenclatura

> **IMPORTANTE:** a estrutura abaixo deve ser preservada integralmente. Qualquer novo artefato deve respeitar diretório e prefixo definidos aqui.

---

## Estrutura oficial de diretórios

```text
/docs
|-- README.md                    ← TOC raiz obrigatório
|-- system-risk-matrix.md        ← Matriz de risco global obrigatória
|-- system-mapping.md            ← Registro contínuo de mapeamento, estrutura, dependências e evidências
|-- architecture-tech-stack.md   ← Registro de arquitetura, stack tecnológica, integrações e observações
|-- /design-system
|   |-- padrao-visual.md
|-- /api_documentation
|   |-- api-nome_da_api.md
|-- /vision
|   |-- vis-nome_da_visao.md
|-- /requirement
|   |-- req-XXXX-nome_do_requisito.md
|   |-- tec-req-XXXX-nome_do_requisito.md
|   |-- apr_req-nome_do_requisito_de_aprovacao.md
|   |-- /apf
|       |-- apf-req-XXXX.md
|   |-- /wireframes
|       |-- req-XXXX-wireframe.png
|-- /diagrams
|   |-- dcl-nome_do_diagrama_de_classes.md
|   |-- min-nome_do_diagrama_de_integracao_de_sistemas.md
|   |-- der-nome_do_diagrama_de_entidades_e_relacionamentos.md
|-- /database
    |-- did-nome_do_dicionario_de_dados.md
    |-- script.sql
```

---

## Tipos de documento e prefixos

| Tipo de documento                       | Prefixo                      | Diretório            | Finalidade                                                 |
| --------------------------------------- | ---------------------------- | -------------------- | ---------------------------------------------------------- |
| Documentação de API                     | `api-`                       | `/api_documentation` | Descrever endpoints, payloads e respostas                  |
| Documento de visão                      | `vis-`                       | `/vision`            | Objetivos, escopo, stakeholders e benefícios               |
| Requisito (arquivo único)               | `req-`                       | `/requirement`       | Fluxos, regras, impactos técnicos e de negócio             |
| Requisito tecnico (detalhado)           | `tec-req-`                   | `/requirement`       | Detalhamento tecnico para implementacao do requisito       |
| APF por requisito                       | `apf-req-`                   | `/requirement/apf`   | Cálculo de pontos de função vinculado ao requisito         |
| Aprovação de requisito                  | `apr_req-`                   | `/requirement`       | Confirmação formal de que o requisito está aprovado        |
| Diagrama de classes                     | `dcl-`                       | `/diagrams`          | Estrutura de código e relacionamentos entre classes        |
| Diagrama de integração de sistemas      | `min-`                       | `/diagrams`          | Mapeamento de integrações e fluxos entre sistemas          |
| Diagrama de entidades e relacionamentos | `der-`                       | `/diagrams`          | Modelo de dados e relacionamentos                          |
| Dicionário de dados                     | `did-`                       | `/database`          | Metadados de campos e tabelas                              |
| Padrão visual                           | `padrao-visual.md`           | `/design-system`     | Reproduzir design, navegação, componentes e tokens visuais |
| Mapeamento de investigação              | `system-mapping.md`          | `/docs`              | Registro contínuo de mapeamento e evidências do sistema    |
| Arquitetura e tech stack                | `architecture-tech-stack.md` | `/docs`              | Registro de arquitetura, stack e integrações               |
| Matriz de risco global                  | `system-risk-matrix.md`      | `/docs`              | Riscos do sistema, atualizada a cada requisito             |

---

## TOC raiz obrigatório (`/docs/README.md`)

O `README.md` na raiz de `/docs` é **obrigatório** e deve conter:

1. Contexto e objetivo da documentação do sistema.
2. Índice navegável para todas as áreas da estrutura (links relativos).
3. Convenção de prefixos utilizada.
4. Status dos artefatos: `rascunho`, `em revisão`, `aprovado`.
5. Data da última atualização global da documentação.
6. Índice de vínculos entre requisitos e artefatos relacionados (quando aplicável).

> Sem esse TOC raiz, a documentação é considerada **incompleta**.

### Exemplo de seção de índice no README.md

```markdown
## Índice

### Requisitos
- [req-0001 — Login de usuário](./requirement/req-0001-login-usuario.md) · `aprovado`
- [req-0002 — Recuperação de senha](./requirement/req-0002-recuperacao-senha.md) · `em revisão`

### APIs
- [api-autenticacao](./api_documentation/api-autenticacao.md) · `aprovado`

### Diagramas
- [der-entidades-principais](./diagrams/der-entidades-principais.md) · `aprovado`

### Banco de dados
- [did-usuarios](./database/did-usuarios.md) · `aprovado`
```

---

## Regras de criação de artefatos

- **Proibido** criar arquivos fora da estrutura oficial sem justificativa registrada.
- **Proibido** criar artefatos duplicados ou concorrentes para o mesmo requisito/funcionalidade.
- **Obrigatório** atualizar o `README.md` (TOC raiz) sempre que um novo artefato for criado.
- **Obrigatório** usar o prefixo correto para cada tipo de documento.
- Wireframes de requisitos devem ser salvos exclusivamente em `/requirement/wireframes/`.
