using APICatalogo.Context;
using APICatalogo.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contexto) :base(contexto)
        {

        }

        public IEnumerable<Categoria> GetCategotiaPorProduto()
        {
            return Get().Include(x => x.Produtos);
        }
    }
}
