using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Repository
{
    public interface IUnitOfWork
    {
        public IProdutoRepository ProdutoRepository { get;  }

        public ICategoriaRepository CategoriaRepository { get;  }

        void Commit();

    }
}
