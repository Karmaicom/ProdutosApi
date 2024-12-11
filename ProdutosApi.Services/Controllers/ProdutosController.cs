using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Infra.Data.Entities;
using ProdutosApi.Infra.Data.Interfaces;
using ProdutosApi.Services.Requests;

namespace ProdutosApi.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Produto> produtos = _produtoRepository.Consultar();
                if (produtos.Count() > 0)
                {
                    return StatusCode(200, new { mensagem = $"{produtos.Count()} produto(s) encontrado(s)!", produtos });
                }
                else
                {
                    return StatusCode(200, new { mensagem = "Nenhum produto encontrado!", produtos });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPorId([FromRoute] Guid id)
        {
            try
            {

                var produto = _produtoRepository.ObterPorId(id);

                if (produto != null)
                {
                    return StatusCode(200, new { mensagem = "Produto encontrado!", produto });
                }
                else 
                {
                    return StatusCode(200, new { mensagem = "Produto não foi encontrado!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProdutoPostRequest request)
        {
            try
            {
                var produto = new Produto();
                produto.IdProduto = Guid.NewGuid();
                produto.Nome = request.Nome;
                produto.Preco = request.Preco;
                produto.Quantidade = request.Quantidade;
                produto.DataCadastro = DateTime.Now;

                _produtoRepository.Inserir(produto);

                return StatusCode(201, new { mensagem = "Cadastro realizado com sucesso!", produto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProdutoPutRequest request)
        {
            try
            {

                var produto = _produtoRepository.ObterPorId(request.IdProduto);

                if (produto != null)
                {
                    produto.Nome = request.Nome;
                    produto.Preco = request.Preco;
                    produto.Quantidade= request.Quantidade;

                    _produtoRepository.Alterar(produto);

                    return StatusCode(200, new { mensagem = "Dados atualizados com sucesso!", produto });
                }
                else
                {
                    return StatusCode(400, new { mensagem = "Produto não encontrado!", request });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var produto = _produtoRepository.ObterPorId(id);

                if (produto != null)
                {
                    _produtoRepository.Excluir(produto);

                    return StatusCode(200, new { mensagem = $"Produto {produto.Nome} foi excluído com sucesso!", produto });
                } 
                else
                {
                    return StatusCode(404, new { mensagem = "Produto não foi encontrado!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }        
    }
}
