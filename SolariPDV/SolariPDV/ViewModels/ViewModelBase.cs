using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SolariPDV.ViewModels
{

    public class ViewModelBase : INotifyPropertyChanged
    {
        bool isBusy;
        public bool IsBusy { get { return isBusy; } set { SetValue(ref isBusy, value); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected bool SetValue<T>(ref T BackingField, T Value, [CallerMemberName] string PropertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(BackingField, Value) && PropertyName != "IsBusY")
            {
                return false;
            }
            BackingField = Value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
        
}
