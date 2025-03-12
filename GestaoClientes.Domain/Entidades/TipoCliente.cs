namespace GestaoClientes.Domain.Entidades;

public class TipoCliente
{
    public int Id { get; set; }
    public string Descricao { get; set; }

    public TipoCliente(int id, string nome)
    {
        Id = id;
        Descricao = nome;
    }

    public TipoCliente()
    {
        
    }
}
