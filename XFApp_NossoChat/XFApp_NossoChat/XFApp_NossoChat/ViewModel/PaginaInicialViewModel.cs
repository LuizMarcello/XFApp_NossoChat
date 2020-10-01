using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using XFApp_NossoChat.Model;
using XFApp_NossoChat.Service;

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
                PropertyChanged(this, new PropertyChangedEventArgs("Nome"));
            }
        }


        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set
            {
                _senha = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Senha"));
            }
        }

        private string _mensagem;
        public string Mensagem
        {
            get { return _mensagem; }
            set
            {
                _mensagem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Mensagem"));
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

            if (usuarioLogado == null)
            {
                Mensagem = "usuario ou senha incorretos!";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}





