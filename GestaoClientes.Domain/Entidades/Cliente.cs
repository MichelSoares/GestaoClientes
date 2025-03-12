using GestaoClientes.Domain.Enums;
namespace GestaoClientes.Domain.Entidades;

public class Cliente
{
    public string CPF { get; set; }
    public string Nome { get; set; }
    public Sexo Sexo { get; set; }
    public TipoCliente TipoCliente { get; set; }
    public SituacaoCliente SituacaoCliente { get; set; }

    public Cliente(string cpf, string nome, Sexo sexo, TipoCliente tipoCliente, SituacaoCliente situacaoCliente)
    {
        CPF = cpf;
        Nome = nome;
        Sexo = sexo;
        TipoCliente = tipoCliente;
        SituacaoCliente = situacaoCliente;
    }

    public Cliente()
    {
        
    }
}
