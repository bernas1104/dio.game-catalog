using System;

namespace DIO.GameCatalog.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public double Preco { get; set; }
    }
}
