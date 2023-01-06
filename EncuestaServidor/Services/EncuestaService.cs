using EncuestaServidor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EncuestaServidor.Services
{
    public class EncuestaService
    {
        public event Action<Voto>? VotoRecibido; 
        HttpListener listener = new();
        public string pregunta = "";
        public EncuestaService()
        {
            listener.Prefixes.Add("http://*:8500/votacion/");
        }
        public void Iniciar()
        {
            if (!listener.IsListening)
            {
                listener.Start();
                listener.BeginGetContext(GetContxt, null);
            }
        }
        public void EstablecerPregunta(Pregunta p)
        {
            pregunta=JsonConvert.SerializeObject(p);    
        }
        
        private void GetContxt(IAsyncResult c)
        {
            var context= listener.EndGetContext(c);
            listener.BeginGetContext(GetContxt, null);
            if (context.Request.Url != null)
            {
              if (context.Request.Url.LocalPath == "/votacion/preguntas")
              {
                byte[] buffer=Encoding.UTF8.GetBytes(pregunta);
                context.Response.ContentType="application/json"; 
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.StatusCode = 200;
                context.Response.Close();
              }
              else if (context.Request.HttpMethod == "POST" && context.Request.Url.LocalPath == "/votacion/responder")
              {
                var stream = new StreamReader(context.Request.InputStream);
                var json = stream.ReadToEnd();
                Voto? voto= JsonConvert.DeserializeObject<Voto>(json);
                context.Response.StatusCode = 200;

                 if (voto != null) 
                 VotoRecibido?.Invoke(voto);

                 context.Response.Close();
                  
              }
                else
                {
                    context.Response.StatusCode = 404;
                    context.Response.Close();
                }
            }
           

        }

    }
}
