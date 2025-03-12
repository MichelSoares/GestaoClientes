using GestaoClientes.API.DTOs;
using GestaoClientes.Domain.Entidades;
using GestaoClientes.Domain.Enums;
using GestaoClientes.Domain.Validators;
using GestaoClientes.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoClientes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    [Route("ListarClientes")]
    public ActionResult<IEnumerable<Cliente>> ListarTodosClientes()
    {
        try
        {
            var clientes = _clienteRepository.ListarClientes();

            if (clientes != null && clientes.Count > 0)
            {
                return Ok(clientes);
            }
            else
            {
                return Ok(new List<Cliente>());
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro ao processar a requisição: " + ex.Message);
        }
    }

    [HttpPost]
    [Route("AdicionarCliente")]
    public ActionResult<Cliente> AddCliente([FromBody] ClienteDTO clienteDTO)
    {
        try
        {
            if (clienteDTO is null) return BadRequest("Cliente inválido.");

            var cliente = new Cliente(
                cpf: ClienteValidator.ValidarCPF(clienteDTO.CPF),
                nome: clienteDTO.Nome,
                sexo: (Sexo)clienteDTO.Sexo,
                tipoCliente: new TipoCliente { Id = clienteDTO.TipoClienteId },
                situacaoCliente: new SituacaoCliente { Id = clienteDTO.SituacaoClienteId }
            );

            var retAdd = _clienteRepository.AdicionarCliente(cliente);

            if (retAdd)
            {
                return CreatedAtAction(nameof(AddCliente), new { cpf = cliente.CPF }, cliente);
            }
            else
            {
                return BadRequest("Não foi possível incluir o cliente.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error ao processar a requisiçao: " + ex.Message);
        }
    }

    [HttpPut]
    [Route("AtualizarCliente")]
    public ActionResult AtualizarCliente([FromQuery] string cpf, [FromBody] ClienteUpdateDTO clienteUpdateDTO)
    {
        try
        {
            if (clienteUpdateDTO is null) return BadRequest("Dados inválidos.");

            var retCliente = _clienteRepository.ObterClientePorCpf(ClienteValidator.ValidarCPF(cpf));

            if (retCliente is null) return NotFound("Cliente não encontrado");

            retCliente.Nome = clienteUpdateDTO.Nome;
            retCliente.Sexo = (Sexo)clienteUpdateDTO.Sexo;
            retCliente.TipoCliente = new TipoCliente { Id = clienteUpdateDTO.TipoClienteId };
            retCliente.SituacaoCliente = new SituacaoCliente { Id = clienteUpdateDTO.SituacaoClienteId };

            var clienteUpdate = _clienteRepository.AtualizarCliente(retCliente);

            if (clienteUpdate != null)
            {
                return Ok(clienteUpdate);
            }

            return StatusCode(500, "Error interno ao atualizar o cliente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro ao processar a requisicao: " + ex.Message);
        }
    }

    [HttpDelete]
    [Route("ExcluirCliente")]
    public ActionResult ExcluirCliente([FromQuery] string cpf)
    {
        try
        {
            var retExclusao = _clienteRepository.ExcluirCliente(ClienteValidator.ValidarCPF(cpf));

            if (retExclusao)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"Nao foi possivel encontrar o cliente com CPF: {cpf} para exclusão.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error ao processar a requisição: " + ex.Message);
        }
    }
}
