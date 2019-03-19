using System;

namespace Bot.Marcacao.Model
{
    public class DestinatariosEmailModel
    {
        public Guid GuidId { get; set; }
        public string Destinatario { get; set; }
        public bool Principal { get; set; }
        public bool Oculto { get; set; }
    }
}
