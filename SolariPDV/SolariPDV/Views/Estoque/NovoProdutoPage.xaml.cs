﻿using SolariPDV.Models;
using SolariPDV.Models.Material;
using SolariPDV.ViewModels;
using SolariPDV.ViewModels.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views.Estoque
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoProdutoPage : ContentPage
    {
        NovoProdutoViewModel produtoViewModel;
        public NovoProdutoPage()
        {
            BindingContext = produtoViewModel = new NovoProdutoViewModel();
            InitializeComponent();
        }

        public NovoProdutoPage(MaterialModel _material)
        {
            BindingContext = produtoViewModel = new NovoProdutoViewModel();
            InitializeComponent();

            produtoViewModel.SetMaterial(_material);
        }

        private void btConfirmar_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(produtoViewModel.scdCodigo))
            {
                DisplayAlert("Código", "Informe o Código do Material", "OK");
                return;
            }
            if (string.IsNullOrEmpty(produtoViewModel.sdsMaterial))
            {
                DisplayAlert("Código", "Informe a Descriçao do Material", "OK");
                return;
            }
            if (produtoViewModel.familiaSelecionada == null)
            {
                DisplayAlert("Familia", "Selecione a Familia", "Ok");
                return;
            }
            produtoViewModel.SalvarProdutoCommand.Execute(null);

            DisplayAlert("Confirmação", "Produto Salvo com sucesso", "Ok");
            Navigation.PopAsync();
        }
    }
}