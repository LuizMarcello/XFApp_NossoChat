using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFApp_NossoChat.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp_NossoChat.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mensagens : ContentPage
    {
        public Mensagens(Chat chat)
        {
            InitializeComponent();

            BindingContext = new ViewModel.MensagensViewModel(chat);
        }
    }
}