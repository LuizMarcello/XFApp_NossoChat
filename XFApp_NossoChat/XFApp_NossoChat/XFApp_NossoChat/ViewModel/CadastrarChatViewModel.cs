using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using XFApp_NossoChat.Model;
using XFApp_NossoChat.Service;

namespace XFApp_NossoChat.ViewModel
{
     public class CadastrarChatViewModel : INotifyPropertyChanged
    {
        private string _mensagem;

        public string mensagem
        {
            get { return _mensagem; }
            set { _mensagem = value;
                OnPropertyChanged("mensagem");
            }
        }

        public string noome { get; set; }
        public Command CadastrarCommand { get; set; }

        public CadastrarChatViewModel()
        {
            CadastrarCommand = new Command(Cadastrar);
        }

        private void Cadastrar()
        {
            var chat = new Chat() { nome = noome};

            bool ok = ServiceWS.InsertChat(chat);

            if (ok == true)
            {
                //"PopAsync()" Para voltar a página anterior:
                //Opcões para testar
                //App.Current.MainPage.Navigation.PopAsync();
                //(App.Current.MainPage).Navigation.PopAsync();
                ((NavigationPage)App.Current.MainPage).Navigation.PopAsync();

                //Varios casts para atualizar a pagina dos chats, após o novo chat ser incluido
                var Nav = (NavigationPage)App.Current.MainPage;
                var Chats = (View.Chats)Nav.CurrentPage;
                var ViewModel = (ChatsViewModel)Chats.BindingContext;
                
                if (ViewModel.AtualizarCommand.CanExecute(null))
                {
                    ViewModel.AtualizarCommand.Execute(null);
                }

                //"PushAsync()" Assim vai para a instancia da página no parâmetro
                //App.Current.MainPage.Navigation.PushAsync(new View.CadastrarChat());
            }
            else
            {
                mensagem = "Ocorreu um êrro no cadastro. Tente outra vêz.";
            }
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
               
                


    

        


            