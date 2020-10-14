using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using XFApp_NossoChat.Model;
using XFApp_NossoChat.Service;
using Xamarin.Forms;
using XFApp_NossoChat.Util;


namespace XFApp_NossoChat.ViewModel
{
    public class MensagensViewModel : INotifyPropertyChanged
    {
        private StackLayout SL;

        private List<Mensagem> _mensagens;

        public List<Mensagem> Mensagens
        {
            get { return _mensagens; }
            set
            {   //Estes comandos abaixo só são executados quando esta
                //propriedade "Mensagens" sofrer alguma alteração
                _mensagens = value;
                OnPropertyChanged("Mensagens");
                ShowOnScreen();
            }
        }

        public MensagensViewModel(Chat chat, StackLayout SLMensagemContainer)
        {
            //TODO - Pesquisa e apresentação na tela
            SL = SLMensagemContainer;
            Mensagens = ServiceWS.GetMensagemsChat(chat);
        }

        //Método que vai mostrar a estrutura na tela, usando o stacklayout
        private void ShowOnScreen()
        {
            var usuario = UsuarioUtil.GetUsuarioLogado();
            SL.Children.Clear();
            foreach (var msg in Mensagens)
            {
                if (msg.usuario.id == usuario.id)
                {
                    SL.Children.Add(CriarMensagemPropria(msg));
                }
                else
                {
                    SL.Children.Add(CriarMensagenOutrosUsuarios(msg));
                }
                
            }
        }

        private Xamarin.Forms.View CriarMensagemPropria(Mensagem mensagem)
        {
            var layout = new StackLayout()
            {
                Padding = 5,
                BackgroundColor = Color.FromHex("#5Ed055"),
                HorizontalOptions = LayoutOptions.End
            };
            var label = new Label() { TextColor = Color.White, Text = mensagem.mensagem };

            layout.Children.Add(label);
            return layout;
        }

        private Xamarin.Forms.View CriarMensagenOutrosUsuarios(Mensagem mensagem)
        {
            var labelNome = new Label() { Text = mensagem.usuario.nome, FontSize = 10, TextColor = Color.FromHex("#5Ed055") };
            var labelMensagem = new Label() { Text = mensagem.mensagem, TextColor = Color.FromHex("#5Ed055") };
            var SL = new StackLayout();

            SL.Children.Add(labelNome);
            SL.Children.Add(labelMensagem);
            
            var frame = new Frame()
            {
                HorizontalOptions = LayoutOptions.Start,
                //OutLineColor = Color.FromHex("#5Ed055"),
                CornerRadius = 0
            };
            frame.Content = SL;
            return frame;
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
        






