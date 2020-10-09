using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using XFApp_NossoChat.Model;
using XFApp_NossoChat.Service;
using Xamarin.Forms;

namespace XFApp_NossoChat.ViewModel
{
    public class MensagensViewModel : INotifyPropertyChanged
    {
        
        public MensagensViewModel(Chat chat)
        {
            //TODO - Pesquisa e apresentação na tela
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
