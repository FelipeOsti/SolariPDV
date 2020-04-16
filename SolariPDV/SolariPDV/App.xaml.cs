using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SolariPDV.Services;
using SolariPDV.Views;
using SolariPDV.Models;

namespace SolariPDV
{
    public partial class App : Application
    {
        public static App current;

        public string sdsServidorApp = "ec2-18-229-119-232.sa-east-1.compute.amazonaws.com";
        public int nnrPorta = 212;
        public string sdsUsuario;
        public string sdsSenha;
        public EstabelecimentoModel EstabSelected;

        public App()
        {
            InitializeComponent();
            current = this;
            MainPage = new LoginPage();
        }

        public void AfterLogin()
        {
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
