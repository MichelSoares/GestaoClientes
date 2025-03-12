using GestaoClientes.Domain.Entidades;
using GestaoClientes.Infrastructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

public class ClienteRepository : IClienteRepository
{
    private readonly string _connectionString;

    public ClienteRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Cliente> ListarClientes()
    {
        try
        {
            var clientes = new List<Cliente>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var querySql = @"SELECT c.CPF, c.Nome, c.SexoId AS Sexo,
                            JSON_QUERY((SELECT tc.Id, tc.Descricao FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)) AS TipoCliente,
                            JSON_QUERY((SELECT sc.Id, sc.Descricao FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)) AS SituacaoCliente
                                FROM Cliente c
                                JOIN TipoCliente tc ON c.TipoClienteId = tc.Id
                                JOIN SituacaoCliente sc ON c.SituacaoClienteId = sc.Id
                                FOR JSON PATH;";

            using var cmd = new SqlCommand(querySql, conn);
            var retJson = cmd.ExecuteScalar()?.ToString();

            if (retJson is not null)
            {
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(retJson) ?? new List<Cliente>();
            }

            return clientes;
        }
        catch (SqlException ex)
        {
            throw new Exception("Erro ao acessar o banco de dados para obter todos o clientes.", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception("Erro ao processar o JSON retornado do banco.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error inesperado ao obter todos os clientes.", ex);
        }
    }

    public bool AdicionarCliente(Cliente cliente)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var query = "INSERT INTO Cliente (CPF, Nome, SexoId, TipoClienteId, SituacaoClienteId) VALUES (@CPF, @Nome, @Sexo, @TipoCliente, @SituacaoCliente);";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Sexo", (int)cliente.Sexo);
            cmd.Parameters.AddWithValue("@TipoCliente", (int)cliente.TipoCliente.Id);
            cmd.Parameters.AddWithValue("@SituacaoCliente", (int)cliente.SituacaoCliente.Id);

            var retLinha = cmd.ExecuteNonQuery();

            return retLinha > 0;
        }
        catch (SqlException ex)
        {
            throw new Exception("Erro ao acessar o banco de dados para adicionar o cliente.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar o cliente.", ex);
        }
    }
    public bool ExcluirCliente(string cpf)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var query = "DELETE FROM Cliente WHERE CPF = @CPF";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CPF", cpf);

            int retLinhas = cmd.ExecuteNonQuery();

            return retLinhas > 0;
        }
        catch (SqlException ex)
        {
            throw new Exception("Erro ao acessar o banco de dados para excluir o cliente.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao excluir o cliente.", ex);
        }
    }


    public Cliente? AtualizarCliente(Cliente cliente)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var query = @"UPDATE Cliente SET Nome = @Nome, SexoId = @Sexo, TipoClienteId = @TipoCliente, SituacaoClienteId = @SituacaoCliente WHERE CPF = @CPF";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Sexo", (int)cliente.Sexo);
            cmd.Parameters.AddWithValue("@TipoCliente", (int)cliente.TipoCliente.Id);
            cmd.Parameters.AddWithValue("@SituacaoCliente", (int)cliente.SituacaoCliente.Id);

            int linhasAfetadas = cmd.ExecuteNonQuery();

            return linhasAfetadas > 0 ? cliente : null;
        }
        catch (SqlException ex)
        {
            throw new Exception("Erro ao acessar o banco de dados para atualizar o cliente.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error inesperado ao atualizar o cliente.", ex);
        }
    }

    public Cliente? ObterClientePorCpf(string cpf)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var query = @"SELECT CPF, Nome, SexoId, TipoClienteId, SituacaoClienteId FROM Cliente WHERE CPF = @CPF FOR JSON PATH, WITHOUT_ARRAY_WRAPPER";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CPF", cpf);

            var retJson = cmd.ExecuteScalar()?.ToString();

            return retJson is not null ? JsonConvert.DeserializeObject<Cliente>(retJson) : null;
        }
        catch (SqlException ex)
        {
            throw new Exception("Erro ao acessar o banco para buscar o cliente.", ex);
        }
    }
}
