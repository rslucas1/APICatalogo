using APICatalogo.Context;
using APICatalogo.Domain;
using APICatalogo.DTOs;
using APICatalogo.Paginations;
using APICatalogo.Repository;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }


        //[HttpGet ("saudacao/{nome}")]
        //public ActionResult<string> GetSaudacao([FromServices] IMeuServico  meuServico, string nome)
        //{
        //    return meuServico.Saudacao(nome);
        //}

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            try
            {
                var produtos = _uof.ProdutoRepository.Get().ToList();
                var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

                return produtosDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar acessar o banco de dados");
            }
        }


        [HttpGet("paginados")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetPaginados([FromQuery] ProdutosParameters produtosParameters)
        {
            try
            {
                var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters).ToList();
                var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

                return produtosDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar acessar o banco de dados");
            }
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutoPrecos()
        {

            var produtos = _uof.ProdutoRepository.GetProdutoPorPreco().ToList();
            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDTO;
        }

    

      //  [HttpGet("primeiro")]
      //  public ActionResult<Produto> Get2()
      //  {

      //            try
      //   {
      //       return _uof.Produtos.FirstOrDefault();
      //   }
      // catch (Exception)
      // {

      //          return StatusCode(StatusCodes.Status500InternalServerError,
      //                "Erro ao tentar acessar o banco de dados");
      //      }
      //  }

        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id, string param2)
        {
            try
            {
                var parametro2 = param2;

                var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

                if (produto == null)
                {
                    return NotFound($"O Produto id={id} não foi encontrado");
                }

                var produtoDto = _mapper.Map<ProdutoDTO>(produto);
                return produtoDto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar acessar o banco de dados");
            }


        }

        [HttpPost]
        public ActionResult Post([FromBody] ProdutoDTO produtoDTO)
        {

            var produto = _mapper.Map<Produto>(produtoDTO);

            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDTO);

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id); //Assim sempre busca no banco

            //var produto = _uof.Produtos.Find(id); //Dessa forma primeiro verifica em memória, observação
            //o parametro precisa ser a chave primária

            if (produto == null)
            {
                return BadRequest();
            }


            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return produtoDto;


        }



    }
}
