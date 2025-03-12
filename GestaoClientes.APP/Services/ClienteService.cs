using GestaoClientes.APP.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using GestaoClientes.Domain.Entidades;

namespace GestaoClientes.APP.Services;

public class ClienteService
{
    private readonly HttpClient _httpClient;
    private readonly ConfigService _configService;

    public ClienteService(ConfigService configService)
    {
        _configService = configService;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_configService.GetApiBaseUrl());
    }

    public async Task<List<Cliente>> ListarTodosClientesAsync()
    {
        var urlEndpoint = _httpClient.BaseAddress + "/ListarClientes";
        return await _httpClient.GetFromJsonAsync<List<Cliente>>(urlEndpoint);
    }

    public async Task<bool> AdicionarClienteAsync(ClienteDTO cliente)
    {
        var urlEndpoint = _httpClient.BaseAddress + "/AdicionarCliente";
        var response = await _httpClient.PostAsJsonAsync(urlEndpoint, cliente);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AtualizarClienteAsync(ClienteUpdateDTO cliente, string cpf)
    {
        var urlEndpoint = $"{_httpClient.BaseAddress}/AtualizarCliente?cpf={cpf}";
        var response = await _httpClient.PutAsJsonAsync(urlEndpoint, cliente);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ExcluirClienteAsync(string cpf)
    {
        var UrlEndpoint = $"{_httpClient.BaseAddress}/ExcluirCliente?cpf={cpf}";
        var response = await _httpClient.DeleteAsync(UrlEndpoint);
        return response.IsSuccessStatusCode;
    }
}
