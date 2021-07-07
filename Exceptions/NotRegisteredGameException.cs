using System;

namespace DIO.GameCatalog.Exceptions
{
    public class NotRegisteredGameException : Exception
    {
        public NotRegisteredGameException() : base("Este jogo não existe.")
        {}
    }
}
