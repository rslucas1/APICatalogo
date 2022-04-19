using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Domain
{
    [Table("Produtos")]
    public class Produto  : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O Nome é Obrigatório")]
        [StringLength(80)]
        public string Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string Descricao { get; set;}
        [Required]
        [Range (1,100,ErrorMessage = "O Valor deve ser entre {1} a {50} reais")]
        public decimal Preco { get; set; }
        [Required]
        [MaxLength(500)]
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
     
        
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Estoque<=0) 
            {
                yield return new ValidationResult(
                    "O estoque deve ser maior que zero", new[] {
                        nameof(this.Estoque)}
                    );
            }
        }
    }
}
