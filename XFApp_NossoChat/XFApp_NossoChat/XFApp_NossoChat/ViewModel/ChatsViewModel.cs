using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using XFApp_NossoChat.Model;
using XFApp_NossoChat.Service;
using System.Linq;

namespace XFApp_NossoChat.ViewModel
{
    public class ChatsViewModel : INotifyPropertyChanged
    {
        private Chat _selectedItemChat;
        public Chat SelectedItemChat
        {
            get { return _selectedItemChat; }
            set
            {
                _selectedItemChat = value;
                OnPropertyChanged("SelectedItemChat");
                GoPaginaMensagem(value);
            }
        }

        private void GoPaginaMensagem(Chat chat)
        {
            if (chat != null)
            {
                ((NavigationPage)App.Current.MainPage).Navigation.PushAsync(new View.Mensagens(chat));
            }
            
        }

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

            AdicionarCommand = new Command(Adicionar);
            OrdenarCommand = new Command(Ordenar);
            AtualizarCommand = new Command(Atualizar);
        }

        private void Adicionar()
        {
            //Com esse não volta página
            //App.Current.MainPage = new NavigationPage(new View.CadastrarChat());

            //Com esse volta página(Tudo OK)
            //App.Current.MainPage.Navigation.PushAsync(new View.CadastrarChat());

            //Com esse tbém volta página(Tudo OK)
            //(App.Current.MainPage).Navigation.PushAsync(new View.CadastrarChat());

            //Com esse tbém volta página(Tudo OK)
            ((NavigationPage)App.Current.MainPage).Navigation.PushAsync(new View.CadastrarChat());
        }

        private void Ordenar()
        {
            Chats = Chats.OrderBy(a => a.nome).ToList();
        }

        private void Atualizar()
        {
            Chats = ServiceWS.GetChats();
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























