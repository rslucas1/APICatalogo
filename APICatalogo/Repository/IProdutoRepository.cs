﻿using APICatalogo.Domain;
using APICatalogo.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutoPorPreco();

        IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);

    }
}
