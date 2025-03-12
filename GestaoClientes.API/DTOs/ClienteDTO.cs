namespace GestaoClientes.API.DTOs;

public class ClienteDTO
{
    public string CPF { get; set; }
    public string Nome { get; set; }
    public int Sexo { get; set; }
    public int TipoClienteId { get; set; }
    public int SituacaoClienteId { get; set; }
}
