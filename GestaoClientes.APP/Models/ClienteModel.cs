using GestaoClientes.APP.DTOs;
using System.ComponentModel;

namespace GestaoClientes.APP.Models;

public class ClienteModel : INotifyPropertyChanged
{
    private string _cpf;
    private string _nome;
    private string _sexo;
    private string _tipoCliente;
    private string _situacaoCliente;


    public string CPF
    {
        get { return _cpf; }
        set { _cpf = value; OnPropertyChanged(nameof(CPF)); }
    }

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; OnPropertyChanged(nameof(Nome)); }
    }

    public string Sexo
    {
        get { return _sexo; }
        set { _sexo = value; OnPropertyChanged(nameof(Sexo)); }
    }

    public string TipoCliente
    {
        get { return _tipoCliente; }
        set { _tipoCliente = value; OnPropertyChanged(nameof(TipoCliente)); }
    }

    public string SituacaoCliente
    {
        get { return _situacaoCliente; }
        set { _situacaoCliente = value; OnPropertyChanged(nameof(SituacaoCliente)); }
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

