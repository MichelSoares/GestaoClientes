namespace GestaoClientes.Domain.Validators;

public static class ClienteValidator
{
    public static string ValidarCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("CPF não pode ser vazio ou nulo.");

        var retCpf = cpf.Trim().Replace("-", "").Replace(".", "");

        if (retCpf.Length != 11 || !retCpf.All(char.IsDigit))
            throw new ArgumentException("CPF deve ter exatamente 11 dígitos numéricos.");

        return retCpf;
    }
}
