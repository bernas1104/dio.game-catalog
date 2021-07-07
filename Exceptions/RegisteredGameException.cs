using System;

namespace DIO.GameCatalog.Exceptions
{
    public class RegisteredGameException : Exception
    {
        public RegisteredGameException() :
            base("Já existe um jogo com este nome para esta produtora.")
        {}
    }
}
