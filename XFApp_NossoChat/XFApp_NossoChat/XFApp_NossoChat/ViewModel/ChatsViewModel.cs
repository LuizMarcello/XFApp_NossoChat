using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using XFApp_NossoChat.Model;
using XFApp_NossoChat.Service;

namespace XFApp_NossoChat.ViewModel
{
    public class ChatsViewModel : INotifyPropertyChanged
    {
        //private string _nome;
        //public string Nome
        //{
        //    get { return _nome; }
        //    set
        //    {
        //        _nome = value;
        //        OnPropertyChanged("Nome");
        //    }
        //}

        public Command AdicionarCommand { get; set; }
        public Command OrdenarCommand { get; set; }
        public Command AtualizarCommand { get; set; }


        private List<Chat> _chats;
        public List<Chat> Chats
        {
            get { return _chats; }
            set 
            {
                _chats = value;
                OnPropertyChanged("Chats");
            }
        }

        public ChatsViewModel()
        {
            Chats = ServiceWS.GetChats();

            AdicionarCommand = new Command();
            OrdenarCommand = new Command();
            AtualizarCommand = new Command();
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





