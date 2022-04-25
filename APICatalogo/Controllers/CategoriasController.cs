using APICatalogo.Context;
using APICatalogo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using APICatalogo.Repository;
using APICatalogo.DTOs;
using AutoMapper;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;

        }


        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
        {

            try 
            {
                var categorias = _uof.CategoriaRepository.GetCategotiaPorProduto().ToList();
                var categoriaDto = _mapper.Map<List<CategoriaDTO>>(categorias);
                return categoriaDto;

            } catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atender sua solicitação.");            
            }
            
        }


        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {

            try {
                var categorias = _uof.CategoriaRepository.Get().ToList();
                var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);

                return categoriasDto;

            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar acessar o banco de dados");
            }

            
        }

        [HttpGet("{id}", Name="ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id) 
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

                var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

                if (categoria == null)
                {
                    return NotFound($"A Categoria id= {id} não foi encontrada");
                }

                return categoriaDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar acessar o banco de dados");
            }
            
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post([FromBody] CategoriaDTO categoriaDto) 
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            _uof.CategoriaRepository.Add(categoria);
            _uof.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDTO.CategoriaId }, categoriaDTO);
    
        }


        [HttpPut("{id}")]
        public ActionResult<Categoria> Put(int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            if (id != categoriaDTO.CategoriaId) 
            {
                return BadRequest();
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            return Ok(categoriaDTO);
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id) 
        {
            var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

            if (categoria==null) 
            {
                return BadRequest();
            }

            _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDTO;
        }

    }
}
