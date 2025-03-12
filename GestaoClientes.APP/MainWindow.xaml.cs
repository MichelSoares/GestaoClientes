using GestaoClientes.APP.DTOs;
using GestaoClientes.APP.Services;
using GestaoClientes.APP.ViewModels;
using GestaoClientes.Domain.Entidades;
using GestaoClientes.Domain.Enums;
using System.Windows;
using System.Windows.Controls;

namespace GestaoClientes.APP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var config = new ConfigService();
            var clienteService = new ClienteService(config);
            this.DataContext = new ClienteViewModel(clienteService);
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var cpf = textBox.Text;
                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                if (cpf.Length == 11)
                {
                    textBox.Text = string.Format("{0:000\\.000\\.000-00}", double.Parse(cpf));
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = textBox.Text.Replace(".", "").Replace("-", "");
            }
        }
    }
}