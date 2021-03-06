﻿using SolariPDV.Models;
using SolariPDV.Models.Comercial;
using SolariPDV.ViewModels;
using SolariPDV.ViewModels.PedidoPDV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views.PedidoPDV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerMesasPage : ContentPage
    {

        VerMesasViewModel verMesasViewModel;
        public VerMesasPage()
        {
            BindingContext = verMesasViewModel = new VerMesasViewModel();
            InitializeComponent();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var collection = (CollectionView)sender;
            if (collection.SelectedItem == null) return;

            var mesa = (MesasModel)e.CurrentSelection[0];
            var pedido = await verMesasViewModel.GetPedido(mesa);
            if (pedido != null) await Navigation.PushAsync(new PedidoInicioPage(pedido));           
            collection.SelectedItem = null;
        }
    }
}