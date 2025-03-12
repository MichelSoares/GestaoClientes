
# Gestão de Clientes

Aplicação CRUD simples desenvolvida em WPF utilizando o padrão MVVM, com integração a uma API REST .NET para gerenciamento de dados, sendo a camada de acesso a dados implementada via ADO.

## Documentação da API


```http
  [GET] /api/Cliente/ListarClientes
```
```http
  [POST] /api/Cliente/AdicionarCliente
```
```http
  [PUT] /api/Cliente/AtualizarCliente
```
```http
  [DELETE] /api/Cliente/ExcluirCliente
```

*Documentação no Swagger UI

https://127.0.0.1:7174/swagger/index.html


## Autores

- [@MichelSoares](https://github.com/MichelSoares)


## Documentação

Arquivo INI da aplicação WPF -> bin/Debug/Config/config.INI

````
[API]
BaseUrlApi = http://127.0.0.1:5231/api/Cliente



