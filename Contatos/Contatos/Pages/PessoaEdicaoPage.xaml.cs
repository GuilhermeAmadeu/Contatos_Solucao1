using Contatos.Models;
using Contatos.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contatos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PessoaEdicaoPage : ContentPage
    {
        // Declarar os eventos
        public EventHandler<ItemEventArgs> Salvando { get; set; }
        public EventHandler<ItemEventArgs> Excluindo { get; set; }

        public PessoaEdicaoPage()
        {
            InitializeComponent();

            Inicializar();
        }

        private void Inicializar()
        {
            
        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            // Fazer a operação de conversão
            Pessoa item = (Pessoa)this.BindingContext;

            // Executar o evento de salvar
            Salvando?.Invoke(sender, new ItemEventArgs(item));
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            // Retornar para a página anterior
            await Navigation.PopAsync();
        }

        private async void txtCep_Unfocused(object sender, FocusEventArgs e)
        {
            // Verificar a quantidade de caracteres do cep
            if (txtCep.Text.Length < 8)
            {
                return;
            }

            // Instanciar o serviço de endereco
            var es = new EnderecoService();

            var endereco = await es.Pesquisar(txtCep.Text);

            // Verificar se retornou com sucesso
            if (es.Resultado.Sucesso == true)
            {
                txtEndereco.Text = endereco.Logradouro;
                txtBairro.Text = endereco.Bairro;
                txtCidade.Text = endereco.Localidade;
                txtUf.Text = endereco.Uf;
            }
            else
            {
                // Exibir mensagem de erro
                await DisplayAlert("Erro", es.Resultado.Mensagem, "Fechar");
            }
        }

        private void tbiExcluir_Clicked(object sender, EventArgs e)
        {
            // Chamar o evento de exclusão
            Excluindo?.Invoke(sender, new ItemEventArgs(BindingContext));
        }
    }
}