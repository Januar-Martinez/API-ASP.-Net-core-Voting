# # 🗳️ API-ASP.-Net-core-Voting

API REST desarrollada en ASP.NET Core 8 para la gestión de un sistema de votaciones. Proyecto con buenas prácticas, arquitectura limpia.

---

## 🛠️ Tecnologías

- **ASP.NET Core 8** — Framework principal
- **Entity Framework Core** — ORM para acceso a datos
- **SQL Server** — Base de datos relacional
- **Swagger / Swashbuckle** — Documentación interactiva de endpoints

---

## 🗄️ Entidades

### Voter (Votante)
| Campo | Tipo | Descripción |
|---|---|---|
| Id | int | Clave primaria autogenerada |
| Name | string | Nombre completo (obligatorio) |
| Email | string | Correo único (obligatorio) |
| HasVoted | bool | Si ya emitió su voto (default: false) |

### Candidate (Candidato)
| Campo | Tipo | Descripción |
|---|---|---|
| Id | int | Clave primaria autogenerada |
| Name | string | Nombre completo (obligatorio) |
| Party | string? | Partido político (opcional) |
| Votes | int | Votos recibidos (default: 0) |

### Vote (Voto)
| Campo | Tipo | Descripción |
|---|---|---|
| Id | int | Clave primaria autogenerada |
| VoterId | int | FK hacia Voter |
| CandidateId | int | FK hacia Candidate |
| VotedAt | DateTime | Fecha y hora del voto |

---