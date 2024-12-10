using System.ComponentModel.DataAnnotations;

namespace ProdutosApi.Services.Requests
{
    public class ProdutoPostRequest
    {

        [Required(ErrorMessage = "Informe o nome do produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe a quantidade do produto")]
        public int Quantidade { get; set; }

    }
}
