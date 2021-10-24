using Microsoft.AspNetCore.Mvc;
using MovieList.Data;
using MovieList.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using MovieList.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MovieList.Controller
{
    [ApiController]
    [Route("v1")]
    public class MovieController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieController(AppDbContext context)
        {
            _context = context;   
        }

        [HttpGet]
        [Route("movies")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(){
            var movies = await _context.Movies
                .AsNoTracking()
                .ToListAsync();
            return Ok(movies);
        }

        [HttpGet]
        [Route("movie/{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id){
            var movie = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return movie == null ? NoContent() : Ok(movie);
        }

        [HttpPost]
        [Route("movie")]
        [Authorize(Roles ="employee, manager")]
        public async Task<IActionResult> PostAsync([FromBody] CreateMoviesModel model){

            if(!ModelState.IsValid)
                return BadRequest();

            var movie = new Movie{
                Title = model.Title,
                Id = Guid.NewGuid()
            };

            try
            {
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();

                return Created($"v1/movie/{movie.Id}", movie);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("movie/{id}")]
        [Authorize(Roles ="employee, manager")]
        public async Task<IActionResult> PutAsync([FromBody] UpdateMoviesModel model, [FromRoute] Guid id){

            if(!ModelState.IsValid)
                return BadRequest();

            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            
            if(movie == null)
                return NoContent();
            
            movie.Title = model.Title;
            movie.Year = model.Year;
            movie.Synopsis = model.Synopsis;
            movie.Genre = model.Genre;


            try
            {
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();

                return Ok(movie);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("movie/{id}")]
        [Authorize(Roles ="manager")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id){

            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if(movie == null)
                return NoContent();

            try
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();

                return Ok();
               
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}