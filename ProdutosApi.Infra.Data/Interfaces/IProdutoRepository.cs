using ProdutosApi.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosApi.Infra.Data.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
    }
}
