using GestaoClientes.APP.Enums;

namespace GestaoClientes.APP.DTOs;

public class ClienteDTO
{
    public string CPF { get; set; }
    public string Nome { get; set; }
    public SexoEnum? Sexo { get; set; }
    public TipoClienteEnum? TipoClienteId { get; set; }
    public SituacaoClienteEnum? SituacaoClienteId { get; set; }
}

