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
        private Chat chat;
        public Command BtnEnviarCommand { get; set; }
        public Command AtualizarCommand { get; set; }

        private List<Mensagem> _mensagens;
        public List<Mensagem> Mensagens
        {
            get { return _mensagens; }
            set
            {   //Estes comandos abaixo só são executados quando esta
                //propriedade "Mensagens" sofrer alguma alteração
                _mensagens = value;
                OnPropertyChanged("Mensagens");
                if (value != null)
                {
                    ShowOnScreen();
                }
            }
        }

        private string _txtMensagem;
        public string TxtMensagem
        {
            get { return _txtMensagem; }
            set
            {   //Estes comandos abaixo só são executados quando esta
                //propriedade "TxtMensagem" sofrer alguma alteração
                _txtMensagem = value;
                OnPropertyChanged("TxtMensagem");
            }
        }

        public MensagensViewModel(Chat chat, StackLayout SLMensagemContainer)
        {
            this.chat = chat;
            SL = SLMensagemContainer;
            Atualizar();
            BtnEnviarCommand = new Command(BtnEnviar);
            AtualizarCommand = new Command(Atualizar);
            //Função: a cada 1 segundo chama o método atualizar():
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Atualizar();
                return true;
            });
        }

        public void BtnEnviar()
        {
            var msg = new Mensagem()
            {
                id_usuario = UsuarioUtil.GetUsuarioLogado().id,
                mensagem = TxtMensagem,
                id_chat = chat.id 
            };
            ServiceWS.InsertMensagem(msg);
            Atualizar();
            TxtMensagem = string.Empty;
        }

        public void Atualizar()
        {
            Mensagens = ServiceWS.GetMensagensChat(chat);
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











