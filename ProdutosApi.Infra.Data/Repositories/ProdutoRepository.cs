using Dapper;
using Microsoft.Data.SqlClient;
using ProdutosApi.Infra.Data.Entities;
using ProdutosApi.Infra.Data.Interfaces;
using static Dapper.SqlMapper;

namespace ProdutosApi.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private string _connectionString = "";

        public ProdutoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Alterar(Produto entity)
        {
            string query = @"update produto set nome=@Nome, preco=@Preco, quantidade=@Quantidade, datacadastro=@DataCadastro where idProduto = @IdProduto";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Produto> Consultar()
        {
            Console.WriteLine(_connectionString);

            string query = @"select * from produto order by nome";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Produto>(query).ToList();
            }
        }

        public void Excluir(Produto entity)
        {
            string query = @"delete from produto where idProduto = @IdProduto";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Inserir(Produto entity)
        {
            string query = @"insert into produto(idproduto, nome, preco, quantidade, datacadastro) values(@IdProduto, @Nome, @Preco, @Quantidade, @DataCadastro)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public Produto? ObterPorId(Guid id)
        {
            string query = @"select * from produto where idProduto = @id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Produto>(query, new { id }).FirstOrDefault();
            }
        }
    }
}
