using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using XFApp_NossoChat.Model;
using XFApp_NossoChat.Service;
using Newtonsoft.Json;

namespace XFApp_NossoChat.ViewModel
{
    public class PaginaInicialViewModel : INotifyPropertyChanged
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged("Nome");
            }
        }


        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set
            {
                _senha = value;
                OnPropertyChanged("Senha");
            }
        }

        private string _mensagem;
        public string Mensagem
        {
            get { return _mensagem; }
            set
            {
                _mensagem = value;
                OnPropertyChanged("Mensagem");
            }
        }

        public Command AcessarCommand { get; set; }

        public PaginaInicialViewModel()
        {
            AcessarCommand = new Command(Acessar);
        }

        private void Acessar()
        {
            Usuario user = new Usuario();
            user.nome = Nome;
            user.password = Senha;

            var usuarioLogado = ServiceWS.GetUsuario(user);

            //Na verdade, verificando se o usuário existe
            if (usuarioLogado == null)
            {
                Mensagem = "usuario ou senha incorretos!";
            }
            else
            {
                //Uma forma de armazenar o login(persistência)
                //Toda vêz que a propriedade "LOGIN" existir, é porque está logado.
                //"Properties" pode ser usado por toda a aplicação, para armazenar 
                //estado da persistencia da aplicação.
                App.Current.Properties["LOGIN"] = JsonConvert.SerializeObject(usuarioLogado);
                //"App.Current": Obtém a aplicação corrente.
                App.Current.MainPage = new NavigationPage(new View.Chats()) 
                {
                    BarBackgroundColor = Color.FromHex("#5Ed055"),
                    BarTextColor = Color.White
                };
                
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
        




                





