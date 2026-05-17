# Princípios de escrita técnica

---

## Clareza e escaneabilidade

- **Use voz ativa** e frases curtas.
- **Seja direto:** se o leitor precisa reler para entender, simplifique.
- **Organize com Markdown:** títulos, subtítulos, listas e blocos de código tornam a informação navegável.
- **Prefira exemplos concretos** a descrições abstratas.
- **Evite jargão desnecessário:** se precisar de termo técnico, adicione ao glossário do documento.

---

## Empatia por público-alvo

Defina quem vai usar o documento e adapte o tom e o nível de detalhe:

| Público                | Foco                                                                                   |
| ---------------------- | -------------------------------------------------------------------------------------- |
| **Desenvolvedores**    | APIs, exemplos de código, detalhes de implementação, referências de métodos e arquivos |
| **QA / Testadores**    | Critérios de aceitação, cenários negativos, dados de entrada e saída esperados         |
| **Stakeholders / PMs** | Objetivos, impacto de negócio, escopo, métricas e riscos                               |
| **Usuários finais**    | Passo a passo (tutorials), FAQs e resolução de problemas                               |

---

## Docs-as-Code

- **Versione a documentação junto com o código:** mantenha os artefatos no repositório.
- **Use Git** para histórico, revisão por pull request e colaboração assíncrona.
- **Prefira Markdown** como formato base para todos os artefatos.
- Considere ferramentas de geração de site a partir de Markdown (**Docusaurus**, **MkDocs**, **Hugo**) para projetos com documentação pública ou interna de grande porte.

---

## Processo padrão de produção de documentação

1. **Brainstorming com stakeholders:** identifique atores, fluxos e casos de borda.
2. **Leitura do projeto:** entenda contexto, estrutura e stack pelo `README.md` e documentos-base.
3. **Investigação profunda:** percorra código, subfunções, frontend, validações, banco, integrações e design.
4. **Mapeamento de impactos:** liste documentação/requisitos impactantes e impactados.
5. **Esboço inicial:** crie um rascunho com estrutura mínima.
6. **Produção do wireframe:** para requisitos de interface, incluir wireframe com imagem preferencial e fallback ASCII.
7. **Revisão técnica:** valide com engenheiros e QA.
8. **Revisão documental cruzada:** ajuste documentos correlatos quando necessário.
9. **Aprovação:** registre aprovação formal (`apr_req-`) antes da implementação.
10. **Manutenção:** atualize quando o requisito mudar e registre histórico de alterações.

---

## Ferramentas recomendadas

| Ferramenta                      | Uso                                                           |
| ------------------------------- | ------------------------------------------------------------- |
| **Markdown**                    | Base de todos os artefatos                                    |
| **Mermaid.js**                  | Diagramas embutidos em Markdown                               |
| **Git / GitHub / GitLab**       | Versionamento e revisão colaborativa                          |
| **Docusaurus / MkDocs**         | Geração de sites de documentação                              |
| **Swagger / OpenAPI / Postman** | Documentação interativa de APIs                               |
| **database-mcp**                | Inspecionar procedures, views, functions e schema de banco    |
| **MCP de bibliotecas**          | Validar integrações e comportamento de frameworks externos    |
| **Figma / Sketch / Adobe XD**   | Protótipos e design system (referência no `padrao-visual.md`) |
