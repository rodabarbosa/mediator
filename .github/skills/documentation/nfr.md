# Requisitos não funcionais (NFRs)

NFRs devem ser **mensuráveis e verificáveis**. Nunca use termos vagos sem métrica associada.

---

## Padrão de escrita

| ❌ Ruim                             | ✅ Bom                                                                                               |
| ---------------------------------- | --------------------------------------------------------------------------------------------------- |
| "o sistema deve ser rápido"        | "o endpoint X deve responder em até 500ms para 95% das requisições sob 1.000 usuários concorrentes" |
| "o sistema deve ser seguro"        | "toda comunicação deve usar TLS 1.2+; tokens JWT devem expirar em 15 minutos"                       |
| "o sistema deve ser estável"       | "disponibilidade mínima de 99,5% mensurável via uptime monitor"                                     |
| "o sistema deve ser fácil de usar" | "o fluxo de cadastro deve ser concluído em até 3 etapas, com taxa de abandono inferior a 10%"       |

---

## Categorias recomendadas

### Desempenho
- Latência máxima por endpoint/operação (ex.: p95, p99).
- Throughput esperado (requisições por segundo, transações por minuto).
- Capacidade (volume de dados, número de registros, usuários simultâneos).
- Escalabilidade: comportamento esperado sob carga crescente.

**Exemplo:**
> O endpoint `POST /api/v1/pedidos` deve responder em até 300ms (p95) para 500 requisições simultâneas.

---

### Segurança
- Protocolo e versão mínima de TLS.
- Política de autenticação (tipo, expiração de token, MFA).
- Controle de autorização (RBAC, ABAC, escopos).
- Proteção de dados sensíveis (mascaramento, criptografia em repouso e em trânsito).
- Conformidade regulatória (LGPD, PCI-DSS, SOC 2).

**Exemplo:**
> Dados de cartão de crédito não devem ser armazenados; apenas o token PAN gerado pelo gateway.

---

### Confiabilidade
- Disponibilidade mínima (ex.: 99,5% mensal).
- Tolerância a falhas (degradação graciosa, circuit breaker).
- Recuperação de desastre (RTO e RPO definidos).
- Política de retry e idempotência de operações críticas.

**Exemplo:**
> O serviço deve manter disponibilidade de 99,9% (excluindo janelas de manutenção planejadas).

---

### Usabilidade e acessibilidade
- Conformidade com WCAG (nível AA ou AAA).
- Tempo máximo para conclusão de tarefas críticas.
- Suporte a navegação por teclado e leitores de tela.
- Contraste mínimo (ex.: 4,5:1 para texto normal).

**Exemplo:**
> Todas as telas devem atingir conformidade WCAG 2.1 nível AA.

---

### Observabilidade
- Nível mínimo de logging (requisições, erros, eventos críticos).
- Rastreamento distribuído (trace IDs, correlation IDs).
- Métricas expostas (latência, taxa de erro, saturação).
- Alertas definidos (thresholds e canais de notificação).

**Exemplo:**
> Toda requisição deve gerar log estruturado com correlation-id, timestamp, método, path, status e duração.

---

## Template de seção de NFR em documentos de requisito

```markdown
## Requisitos não funcionais

### Desempenho
- O endpoint `[método] [path]` deve responder em até [X]ms para [Y]% das requisições sob [Z] usuários simultâneos.

### Segurança
- [Descrever exigências mensuráveis de autenticação, autorização, criptografia ou conformidade.]

### Confiabilidade
- Disponibilidade mínima: [X]% (mensal/anual).
- Política de retry: [X] tentativas com backoff exponencial de [Y]ms.

### Usabilidade
- [Descrever métricas de usabilidade e acessibilidade.]

### Observabilidade
- [Descrever requisitos de log, rastreamento e alertas.]
```
