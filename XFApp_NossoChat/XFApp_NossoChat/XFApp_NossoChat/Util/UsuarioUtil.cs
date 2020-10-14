using System;
using System.Collections.Generic;
using System.Text;
using XFApp_NossoChat.Model;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace XFApp_NossoChat.Util
{
    public class UsuarioUtil
    {
        public static void SetUsuarioLogado(Usuario usuario)
        {
            //Uma forma de armazenar o login(persistência)
            //Toda vêz que a propriedade "LOGIN" existir, é porque está logado.
            //"Properties" pode ser usado por toda a aplicação, para armazenar 
            //estado da persistencia da aplicação.
            App.Current.Properties["LOGIN"] = JsonConvert.SerializeObject(usuario);
        }

        public static Usuario GetUsuarioLogado()
        {
            if (App.Current.Properties.ContainsKey("LOGIN"))
            {
                return JsonConvert.DeserializeObject<Usuario>((string)App.Current.Properties["LOGIN"]);
            }
            else
            {
                return null;
            }
        }
    }
}

            

