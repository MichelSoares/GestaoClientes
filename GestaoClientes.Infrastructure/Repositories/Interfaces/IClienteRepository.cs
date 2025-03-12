using GestaoClientes.Domain.Entidades;

namespace GestaoClientes.Infrastructure.Repositories.Interfaces;

public interface IClienteRepository
{
    List<Cliente> ListarClientes();
    bool AdicionarCliente(Cliente cliente);
    bool ExcluirCliente(string cpf);
    Cliente? AtualizarCliente(Cliente cliente);
    Cliente? ObterClientePorCpf(string cpf);
}
