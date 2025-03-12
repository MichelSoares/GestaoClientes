namespace GestaoClientes.Domain.Entidades;

public class SituacaoCliente
{
    public int Id { get; set; }
    public string Descricao { get; set; }

    public SituacaoCliente(int id, string nome)
    {
        Id = id;
        Descricao = nome;
    }

    public SituacaoCliente()
    {
        
    }
}