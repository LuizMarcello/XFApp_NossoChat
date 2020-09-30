using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using XFApp_NossoChat.Model;
using Newtonsoft.Json;

namespace XFApp_NossoChat.Service
{
    public class ServiceWS
    {
        private static string EnderecoBase = "http://ws.spacedu.com.br/xf2018/rest/api";

        //Retorna ou cria o usuario especificado 
        public static Usuario GetUsuario(Usuario usuario)
        {
            var URL = EnderecoBase + "/usuario";

            /*
             * Usando QueryString:
             * StringContent param = new StringContent(string.Format("?nome={0}&password={1}", usuario.nome, usuario.password));
             */
            //ou assim:
            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("nome",usuario.nome),
                new KeyValuePair<string,string>("password",usuario.password)
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                //TODO - Deserializar, retornar método e salvar como login
            }
            return null;
        }

        //Retorna uma lista com todos os chats
        public static List<Chat> GetChats()
        {
            var URL = EnderecoBase + "/chats";

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.GetAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                string conteudo = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (conteudo.Length > 2)
                {
                    List<Chat> lista = JsonConvert.DeserializeObject<List<Chat>>(conteudo);
                    return lista;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        //Retorna "true" ou "false", se o chat foi inserido, ou não
        public static bool InsertChat(Chat chat)
        {
            var URL = EnderecoBase + "/chat";
            
            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("nome", chat.nome),
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        //Renomeia o chat especificado no id
        public static bool RenomearChat(Chat chat)
        {
            var URL = EnderecoBase + "/chat/" + chat.id;

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("nome", chat.nome),
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PutAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        //Deleta o chat especificado no id
        public static bool DeleteChat(Chat chat)
        {
            var URL = EnderecoBase + "/chat/delete/" + chat.id;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.DeleteAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
        
        


               

           
            
           
               

            
