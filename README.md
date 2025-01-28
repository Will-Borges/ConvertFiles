# Conversor de Logs - Minha CDN

O objetivo do projeto é converter arquivos de log de uma empresa para o formato específico de outra, permitindo a integração e o uso dos dados de forma compatível entre os sistemas.  

A aplicação processa arquivos de log no formato original da empresa fornecedora e os converte para um formato padronizado, utilizado pela **Minha CDN**, facilitando a integração e a utilização dos dados pela empresa receptora.  

A proposta do projeto é manter toda a lógica encapsulada em uma library simples, que possa ser integrada a diferentes linguagens de programação sem depender de inicialização no `Program` ou vínculo direto com a camada de **Ports**. Além disso, caso a aplicação seja desenvolvida em C#, ela poderá ser distribuída e utilizada por meio do **NuGet**. Por esse motivo, o design da arquitetura foi pensado para ser simples e flexível.

---

## Funcionalidades

- Recebe arquivos de log no formato original de uma empresa.
- Converte o conteúdo para o formato padronizado esperado pela **Minha CDN**.
- Cria um novo arquivo no diretório especificado, com o nome e extensão definidos pelo usuário.
- Adiciona cabeçalhos informativos, incluindo versão, data e campos relevantes no formato convertido.

---

## Estrutura do Projeto

O projeto utiliza uma arquitetura simples e funcional, organizada nas seguintes camadas:

- **Abstractions**: Contém as interfaces para definir contratos de uso.
- **Application**: Responsável por implementar as regras de negócio da conversão.
- **Models**: Contém os domínios principais da aplicação.
- **Ports**: Exemplifica a integração com o sistema por meio de uma API, sendo utilizado apenas para testes.

---

## Tecnologias Utilizadas

- **C#**
- **.NET Core**
- Arquitetura simples e modular, com possibilidade de integração em diferentes projetos.

---

## Endpoint de Conversão

Para realizar a conversão de logs, envie uma requisição POST para o endpoint configurado na API, informando os seguintes dados no **Body**:

- **fileNameInput**: Nome do arquivo que deseja converter.
- **path**: Caminho onde está localizado o arquivo (web ou local).
- **fileNameOutput**: Nome do arquivo convertido que será criado.
- **extensionOutPut**: Extensão a ser utilizada no arquivo final.

### Exemplo de Requisição

**Body (JSON)**:
```json
{
  "fileNameInput": "input-01.txt",
  "path": "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs",
  "fileNameOutput": "saida1-Formatted",
  "pathOutput": "C:\\Unecont",
  "extensionOutPut": ".log"
}
