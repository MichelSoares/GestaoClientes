using GestaoClientes.APP.Commads;
using GestaoClientes.APP.DTOs;
using GestaoClientes.APP.Enums;
using GestaoClientes.APP.Models;
using GestaoClientes.APP.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GestaoClientes.APP.ViewModels;

public class ClienteViewModel : BaseViewModel
{

    private readonly ClienteService _clienteService;

    private string _cpf;
    private string _nome;
    private SexoEnum? _sexo;
    private TipoClienteEnum? _tipoCliente;
    private SituacaoClienteEnum? _situacaoCliente;

    private ClienteModel _clienteSelecionado;

    public ObservableCollection<ClienteModel> Clientes { get; set; }
    public ICommand IncluirClienteCommand { get; set; }
    public ICommand AtualizarClienteFormCommand { get; set; }
    public ICommand AtualizarClienteCommand { get; set; }
    public ICommand ExcluirClienteCommand { get; set; }
    public ICommand ConsultarClientesCommand { get; set; }

    public ClienteViewModel(ClienteService clienteService)
    {
        _clienteService = clienteService;
        Clientes = new ObservableCollection<ClienteModel>();
        IncluirClienteCommand = new RelayCommand(IncluirCliente, CanIncluirCliente);

        AtualizarClienteFormCommand = new RelayCommand(AtualizarClienteForm, CanAtualizarCliente);
        AtualizarClienteCommand = new RelayCommand(AtualizarCliente);

        ExcluirClienteCommand = new RelayCommand(ExcluirCliente);
        ConsultarClientesCommand = new RelayCommand(ListarTodosClientes);
    }

    private bool _cpfDesabilitadoUpdate;
    public bool CPFDesabilitadoUpdate
    {
        get => _cpfDesabilitadoUpdate;
        set { _cpfDesabilitadoUpdate = value; OnPropertyChanged(); }
    }

    public string CPF
    {
        get { return _cpf; }
        set
        {
            _cpf = value;
            OnPropertyChanged();
        }
    }

    public string Nome
    {
        get { return _nome; }
        set
        {
            _nome = value;
            OnPropertyChanged();
        }
    }

    public SexoEnum? Sexo
    {
        get { return _sexo; }
        set
        {
            _sexo = value;
            OnPropertyChanged();
        }
    }

    public TipoClienteEnum? TipoCliente
    {
        get { return _tipoCliente; }
        set
        {
            _tipoCliente = value;
            OnPropertyChanged();
        }
    }

    public SituacaoClienteEnum? SituacaoCliente
    {
        get { return _situacaoCliente; }
        set
        {
            _situacaoCliente = value;
            OnPropertyChanged();
        }
    }

    public ClienteModel ClienteSelecionado
    {
        get => _clienteSelecionado;
        set
        {
            _clienteSelecionado = value;
            OnPropertyChanged();

            IsAtualizarClienteEnabled = _clienteSelecionado != null;
            IsIncluirClienteEnabled = _clienteSelecionado == null;

            if (AtualizarClienteFormCommand is RelayCommand command)
            {
                command.RaiseCanExecuteChanged();
            }
        }
    }

    public async void ListarTodosClientes()
    {
        var clientesDomain = await _clienteService.ListarTodosClientesAsync();

        if (clientesDomain.Count.Equals(0))
        {
            MessageBox.Show("Nenhum cliente a ser listado!");
            return;
        }

        if (clientesDomain != null)
        {
            Clientes.Clear();

            foreach (var cli in clientesDomain)
            {
                if (cli != null) 
                {
                    Clientes.Add(new ClienteModel
                    {
                        CPF = cli.CPF,
                        Nome = cli.Nome,
                        Sexo = cli.Sexo.ToString(),
                        TipoCliente = cli.TipoCliente.Descricao,
                        SituacaoCliente = cli.SituacaoCliente.Descricao
                    });
                }
            }
        }
    }

    private bool _isIncluirClienteEnabled = true;
    public bool IsIncluirClienteEnabled
    {
        get { return _isIncluirClienteEnabled; }
        set
        {
            _isIncluirClienteEnabled = value;
            OnPropertyChanged();
        }
    }
    private async void IncluirCliente()
    {
        if (string.IsNullOrEmpty(CPF) || string.IsNullOrEmpty(Nome) || Sexo == null || TipoCliente == null || SituacaoCliente == null)
            {
                MessageBox.Show("Favor preencher todos os campos!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        var novoCliente = new ClienteDTO
        {
            CPF = CPF,
            Nome = Nome,
            Sexo = Sexo,
            TipoClienteId = TipoCliente,
            SituacaoClienteId = SituacaoCliente
        };

        try
        { 
            var clienteCadastrado = await _clienteService.AdicionarClienteAsync(novoCliente);

            if (clienteCadastrado)
            {
                MessageBox.Show($"Cliente: {novoCliente.Nome} cadastrado com sucesso!");
                ZerarForm();
                ListarTodosClientes();
            }
            else
            {
                MessageBox.Show("Erro ao incluir cliente.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro: {ex.Message}");
        }
    }

    private bool _isAtualizarClienteEnabled;

    public bool IsAtualizarClienteEnabled
    {
        get { return _isAtualizarClienteEnabled; }
        set
        {
            _isAtualizarClienteEnabled = value;
            OnPropertyChanged();
        }
    }

    private bool CanIncluirCliente()
    {
        return ClienteSelecionado == null; 
    }
    private bool CanAtualizarCliente()
    {
        return _clienteSelecionado != null;
    }

    private void AtualizarClienteForm()
    {
        if (_clienteSelecionado != null)
        {
            var cliDTO = new ClienteDTO
            {
                CPF = _clienteSelecionado.CPF,
                Nome = _clienteSelecionado.Nome,
                Sexo = Enum.TryParse(_clienteSelecionado.Sexo.ToString(), out SexoEnum sexoEnum) ? sexoEnum : (SexoEnum?)null,
                TipoClienteId = Enum.TryParse(_clienteSelecionado.TipoCliente.ToString(), out TipoClienteEnum tipoClienteEnum) ? tipoClienteEnum : (TipoClienteEnum?)null,
                SituacaoClienteId = Enum.TryParse(_clienteSelecionado.SituacaoCliente.ToString(), out SituacaoClienteEnum situacaoClienteEnum) ? situacaoClienteEnum : (SituacaoClienteEnum?)null
            };

            PreencherFormulario(cliDTO);
            CPFDesabilitadoUpdate = true;
            MessageBox.Show("Favor atualize os dados no formulário!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async void AtualizarCliente()
    {
        if (string.IsNullOrEmpty(CPF) || string.IsNullOrEmpty(Nome) || Sexo == null || TipoCliente == null || SituacaoCliente == null)
        {
            MessageBox.Show("Favor preencher todos os campos");
            return;
        }

        if (CPF.Replace(".", "").Replace("-", "").Trim().Equals(_clienteSelecionado.CPF) && Nome.Equals(_clienteSelecionado.Nome) && Sexo.ToString().Equals(_clienteSelecionado.Sexo) && 
            TipoCliente.ToString().Equals(_clienteSelecionado.TipoCliente) && SituacaoCliente.ToString().Equals(_clienteSelecionado.SituacaoCliente))
        {
            MessageBox.Show("Nenhum dado alterado!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var atualizaCliente = new ClienteUpdateDTO
        {
            Nome = Nome,
            Sexo = (int) Sexo,
            TipoClienteId = (int) TipoCliente,
            SituacaoClienteId = (int) SituacaoCliente
        };

        try
        {
            
            var clienteCadastrado = await _clienteService.AtualizarClienteAsync(atualizaCliente, _clienteSelecionado.CPF);

            if (clienteCadastrado)
            {
                MessageBox.Show($"Cliente: {atualizaCliente.Nome} atualizado com sucesso!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                ZerarForm();
                ListarTodosClientes();
                CPFDesabilitadoUpdate = false;
            }
            else
            {
                MessageBox.Show("Erro ao incluir cliente.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro: {ex.Message}");
        }
    }

    private async void ExcluirCliente()
    {
        if (_clienteSelecionado is ClienteModel cliente)
        {
            try
            {
                var resultado = await _clienteService.ExcluirClienteAsync(cliente.CPF);

                if (resultado)
                {
                    Clientes.Remove(cliente);
                    MessageBox.Show("Cliente excluído com sucesso.");
                    ListarTodosClientes();
                    ZerarForm();
                    CPFDesabilitadoUpdate = false;
                }
                else
                {
                    MessageBox.Show("Erro ao excluir cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir cliente: {ex.Message}");
            }
          
        }
        _clienteSelecionado = null;
    }

    private void ZerarForm()
    {
        CPF = string.Empty;
        Nome = string.Empty;
        Sexo = null; 
        TipoCliente = null;
        SituacaoCliente = null;
    }

    private void PreencherFormulario(ClienteDTO cliente)
    {
        CPF = cliente.CPF;
        Nome = cliente.Nome;
        Sexo = cliente.Sexo;
        TipoCliente = cliente.TipoClienteId;
        SituacaoCliente = cliente.SituacaoClienteId;
    }

}