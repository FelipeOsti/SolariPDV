using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Title = "Entrar";
        }

        private void btConfirma_Clicked(object sender, EventArgs e)
        {
            RealizaLogin();
        }

        private async void RealizaLogin()
        {
            try
            {
                indicator.IsVisible = true;
                btConfirma.IsEnabled = false;

                App.current.sdsSenha = entrySenha.Text;
                App.current.sdsUsuario = entryUsuario.Text;

                var loginLogic = new LoginLogic();
                var bboOk = await loginLogic.VerificaLogin(entryUsuario.Text);
                if (bboOk)
                {
                    App.current.AfterLogin();
                }
                else
                    throw new Exception();
            }
            catch
            {
                await DisplayAlert("Ops", "Usuário ou senha inválido", "Ok");
            }
            finally
            {
                indicator.IsVisible = false;
                btConfirma.IsEnabled = true;
            }

        }

        private void btSemAcesso_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SemCadastroPage());
        }
    }
}