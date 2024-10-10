# Gerenciamento de Pacientes e Planos de Saúde

Este projeto é uma aplicação ASP.NET MVC com Entity Framework, desenvolvida para gerenciar o cadastro de pacientes, planos de saúde e a associação entre ambos.

## Funcionalidades Principais

### 1. Cadastro de Pacientes
- **Cadastro**: Permite cadastrar novos pacientes através de uma interface simples.
- **Listagem**: Exibe uma lista de todos os pacientes cadastrados, com opções de edição e exclusão.
- **Detalhes**: Tela dedicada para visualizar informações detalhadas de um paciente específico.

### 2. Cadastro de Planos de Saúde
- **Cadastro**: Permite cadastrar novos planos de saúde.
- **Listagem**: Exibe uma lista de todos os planos de saúde cadastrados, com opções de edição e exclusão.
- **Detalhes**: Tela para visualizar as informações detalhadas de um plano de saúde específico.

### 3. Relacionamento entre Pacientes e Planos de Saúde
- **Associar Pacientes a Planos**: Funcionalidade que permite associar um ou mais planos de saúde a um paciente.
- **Remover Associações**: Permite remover a associação entre um paciente e um plano de saúde.
- **Listagem de Associações**: Exibe a lista de planos de saúde associados a um paciente e de pacientes associados a um plano de saúde.

## Como Executar a Aplicação

### Requisitos
- .NET 6.0 SDK ou superior
- Banco de dados SQL Server

### Passos para Rodar a Aplicação
1. Clone este repositório:
   ```bash
   git clone https://github.com/Miguel-Fr3/CPK5
   
### Execute as migrações para criar as tabelas no banco de dados
  ```bash
  dotnet ef database update
