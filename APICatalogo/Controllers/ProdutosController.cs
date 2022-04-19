using APICatalogo.Context;
using APICatalogo.Domain;
using APICatalogo.Services;
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
        private readonly AppDbContext _context;
  
        public ProdutosController(AppDbContext contexto)
        {
            _context = contexto;
        }


        //[HttpGet ("saudacao/{nome}")]
        //public ActionResult<string> GetSaudacao([FromServices] IMeuServico  meuServico, string nome)
        //{
        //    return meuServico.Saudacao(nome);
        //}

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() 
        { 

            try 
	           {	        
	        	return _context.Produtos.AsNoTracking().ToList();
             }
	        catch (Exception)
	        {

	        	return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar acessar o banco de dados");
            }
        }


        [HttpGet("primeiro")]
        public ActionResult<Produto> Get2()
        {

            try
            {
                return _context.Produtos.FirstOrDefault();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Erro ao tentar acessar o banco de dados");
            }
        }

        [HttpGet("{id:int:min(2)}/{param2?}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id, string param2)
        {
            try
            {
                var parametro2 = param2;

                var produto =  _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
              
                if (produto == null)
                {
                    return NotFound($"O Produto id={id} não foi encontrado");
                }
                return produto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar acessar o banco de dados");
            }
            
            
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id); //Assim sempre busca no banco

            //var produto = _context.Produtos.Find(id); //Dessa forma primeiro verifica em memória, observação
            //o parametro precisa ser a chave primária

            if (produto == null) {
                return BadRequest();
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return produto;


        }



    }
}
