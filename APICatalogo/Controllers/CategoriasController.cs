﻿using APICatalogo.Context;
using APICatalogo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                
                return  _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,"Ocorreu um erro ao tratar a solitaçõs");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            return _context.Categorias.AsNoTracking().ToList();
        }
       
        [HttpGet("{id:int}",Name ="ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria= _context.Categorias.FirstOrDefault(x=>x.CategoriaId==id);
            if (categoria == null)
            {
                return NotFound();
            }
            return categoria;
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if(categoria is null) 
            {
                return NotFound();
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria",new {id=categoria.CategoriaId},categoria);
        }
        
        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Categoria categoria) 
        {
            if(id!=categoria.CategoriaId)
            {
                return BadRequest();
            }
            _context.Entry(categoria).State=EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }
       
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria=_context.Categorias.FirstOrDefault(x=>x.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound();
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
}
