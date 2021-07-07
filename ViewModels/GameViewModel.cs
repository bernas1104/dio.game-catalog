using System;
using System.ComponentModel.DataAnnotations;

namespace DIO.GameCatalog.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres.")]
        public string Produtora { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo R$ 1,00 e no máximo R$ 1.000,00")]
        public double Preco { get; set; }
    }
}
