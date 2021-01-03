using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SolariPDV.Services;
using SolariPDV.Views;
using SolariPDV.Models;
using SolariPDV.GradientView;
using Xamarin.Essentials;
using System.Diagnostics;

namespace SolariPDV
{
    public partial class App : Application
    {
        public static App current;

        //public string sdsServidorApp = "ec2-18-229-119-232.sa-east-1.compute.amazonaws.com";
        public string sdsServidorApp = "192.168.0.113";
        public int nnrPorta = 212;
        public string sdsUsuario;
        public string sdsSenha;
        public EstabelecimentoModel EstabSelected;

        static string usuarioKey = "userKey";
        static string senhaKey = "senhaKey";

        public App()
        {
            InitializeComponent();
            current = this;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            if (!Preferences.ContainsKey(usuarioKey))
            {
                MainPage = new NavigationGradient(new LoginPage());
            }
            else
            {
                sdsUsuario = Preferences.Get(usuarioKey, null);
                sdsSenha = Preferences.Get(senhaKey, null);
                AfterLogin();
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine((e.ExceptionObject as Exception).Message);
        }

        public void AfterLogin()
        {
            Preferences.Set(usuarioKey, sdsUsuario);
            Preferences.Set(senhaKey, sdsSenha);

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

        internal void limparLogin()
        {
            Preferences.Remove(usuarioKey);
            Preferences.Remove(senhaKey);
        }
    }
}
