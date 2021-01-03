using Newtonsoft.Json;
using SolariPDV.Logic;
using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels
{
    public class CardapioPageViewModel : ViewModelBase
    {
        public static long nqtCarac = 1;

        string _txtItens;
        public string txtItens { get { return _txtItens; } set { SetValue(ref _txtItens, value); } }

        private double? _vlTotal;
        public double? vlTotal { get { return _vlTotal; } set { SetValue(ref _vlTotal, value); } }

        CardapioCateg categoriaSelecionada;
        public CardapioCateg CategoriaSelecionada { get { return categoriaSelecionada; } set { SetValue(ref categoriaSelecionada, value); } }

        ObservableCollection<CardapioModel> lstCardapio;
        public ObservableCollection<CardapioModel> LstCardapio { get { return lstCardapio; } set { SetValue(ref lstCardapio, value); } }

        ObservableCollection<CardapioProd> lstCardapioProd;
        public ObservableCollection<CardapioProd> LstCardapioProd { get { return lstCardapioProd; } set { SetValue(ref lstCardapioProd, value); } }

        public ObservableCollection<FichaModel> LstFicha { get; set; }

        ObservableCollection<CardapioCateg> lstCategoria;

        ICommand _RecuperarCardapioCommand;
        public ICommand RecuperarCardapioCommand
        {
            get { return _RecuperarCardapioCommand; }
            set { SetValue(ref _RecuperarCardapioCommand, value); }
        }

        public ObservableCollection<CardapioCateg> LstCategoria
        {
            get { return lstCategoria; }
            set { SetValue(ref lstCategoria, value); }
        }

        public CardapioPageViewModel()
        {
            nqtCarac = 0;
            buscarCardapioAsync();
            RecuperarCardapioCommand = new Command(buscarCardapioAsync);
        }

        public void SetCategoria(CardapioCateg categ)
        {
            CategoriaSelecionada = categ;
            if (LstCategoria != null) {
                var categProd = LstCategoria.First(i => i.ID_CATEGORIA == CategoriaSelecionada.ID_CATEGORIA).ToList();
                LstCardapioProd = new ObservableCollection<CardapioProd>(categProd);
            }
        }

        public List<CardapioProd> GetAdicional()
        {
            if (LstCategoria == null) return null;
            return LstCategoria.FirstOrDefault(c => c.FL_ADICIONAL);
        }

        public void FiltrarProduto(string sdsFiltro)
        {
            try
            {
                IsBusy = true;
                var lstProd = LstCategoria.Where(i => i.ID_CATEGORIA == CategoriaSelecionada.ID_CATEGORIA).ToList()[0];
                if (String.IsNullOrEmpty(sdsFiltro))
                {
                    LstCardapioProd = new ObservableCollection<CardapioProd>(lstProd);
                }
                else
                {
                    LstCardapioProd = new ObservableCollection<CardapioProd>(lstProd.Where(p => p.DS_MATERIAL.ToLower().Contains(sdsFiltro.ToLower()) || p.DS_FICHA.ToLower().Contains(sdsFiltro.ToLower())));
                }
            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void buscarCardapioAsync()
        {
            try
            {
                IsBusy = true;
                await buscarFichaAsync();

                var logic = new CardapioLogic();
                var cardapio = await logic.GetCardapio("");

                LstCardapio = JsonConvert.DeserializeObject<ObservableCollection<CardapioModel>>(cardapio);
                ConverteCardapioCategorias();

                if (CategoriaSelecionada != null) SetCategoria(CategoriaSelecionada);

                IsBusy = false;
            }
            catch
            {
                //await DisplayAlert("Ops", "Não foi possível buscar o cardapio", "Ok");
            }
        }

        private void ConverteCardapioCategorias()
        {
            try
            {
                LstCategoria = new ObservableCollection<CardapioCateg>();
                int c = -1;
                int p = -1;
                foreach (var it in LstCardapio)
                {
                    if (!LstCategoria.Any(cat => cat.ID_CATEGORIA == it.ID_CATEGORIA))
                    {
                        LstCategoria.Add(new CardapioCateg() { ID_CATEGORIA = it.ID_CATEGORIA, DS_CATEGORIA = it.DS_CATEGORIA, FL_ADICIONAL = it.FL_ADICIONAL == "T",FL_ASSAR = it.FL_ASSAR, FL_PERMITEADICIONAL = it.FL_PERMITEADICIONAL == "T"  });
                        p = -1;
                        c++;
                    }
                    if (!LstCategoria[c].Any(pro => pro.ID_MATERIAL == it.ID_MATERIAL))
                    {
                        var sds_ficha = "";
                        if (LstFicha != null)
                        {
                            foreach (var ficha in LstFicha)
                            {
                                if (ficha.ID_MATERIAL == it.ID_MATERIAL)
                                {
                                    if (sds_ficha == "")
                                        sds_ficha = ficha.DS_MATERIAL;
                                    else if (!sds_ficha.Contains(ficha.DS_MATERIAL))
                                        sds_ficha = sds_ficha + ", " + ficha.DS_MATERIAL;
                                }
                            }
                        }

                        LstCategoria[c].Add(new CardapioProd()
                        {
                            ID_MATERIAL = it.ID_MATERIAL,
                            DS_MATERIAL = it.DS_MATERIAL,
                            DS_FICHA = sds_ficha,
                            lstFicha = LstFicha?.Where(f => f.ID_MATERIAL == it.ID_MATERIAL)
                        });
                        p++;
                    }

                    if (!LstCategoria[c][p].Any(t => t.ID_TAMANHO == it.ID_TAMANHO))
                    {
                        if (String.IsNullOrEmpty(it.DS_TAMANHO))
                        {
                            it.DS_TAMANHO = "Único";
                            LstCategoria[c][p].VL_UNITARIO = it.VL_UNITARIO;
                        }
                        else
                            LstCategoria[c][p].VL_UNITARIO = null;

                        LstCategoria[c][p].Add(new TamanhoProd()
                        {
                            ID_TAMANHO = it.ID_TAMANHO,
                            VL_UNITARIO = it.VL_UNITARIO,
                            DS_TAMANHO = it.DS_TAMANHO
                        });
                    }
                }
            }
            catch
            {
                //await DisplayAlert("Ops", "Não foi possível converter os dados", "Ok");
            }

        }

        private async Task buscarFichaAsync()
        {
            try
            {
                var logic = new FichaLogic();
                var ficha = await logic.GetFicha(0);

                LstFicha = JsonConvert.DeserializeObject<ObservableCollection<FichaModel>>(ficha);
            }
            catch
            {
                //await DisplayAlert("Ops", "Não foi possível buscar a ficha", "Ok");
            }
        }

    }
}
