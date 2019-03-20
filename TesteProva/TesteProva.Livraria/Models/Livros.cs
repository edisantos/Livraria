using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteProva.Livraria.Models
{
    [Table("Livros")]
    public class Livros
    {
        [Key]
        public int LivrosId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime Data { get; set; }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }


        [Display(Name = "Ano")]
        public int Ano { get; set; }

        [Display(Name = "Numero Exemplar")]
        public int ExemplarNumero { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? Preco { get; set; }
        public int CategoriaId { get; set; }

        public virtual Categoria Categorias { get; set; }
    }
}