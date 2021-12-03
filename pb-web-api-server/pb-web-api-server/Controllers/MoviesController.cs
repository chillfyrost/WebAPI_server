using AutoMapper;
using Contracts;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pb_web_api_server.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public MoviesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _repository.Movie.GetAllMoviesAsync();
                _logger.LogInfo("$Returned all movies from the database");

                var moviesResult = _mapper.Map<IEnumerable<MovieDto>>(movies);

                return Ok(moviesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll Movies action: {ex.Message}");
                return StatusCode(500, "Internal Server Error!");
            }
        }
        [HttpGet("{id}", Name = "MovieById")]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            try
            {
                var movie = await _repository.Movie.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned movie with id: {id}");

                    var movieResult = _mapper.Map<MovieDto>(movie);
                    return Ok(movieResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMovieById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateDto movie)
        {
            try
            {
                if (movie == null)
                {
                    _logger.LogError("Movie object sent from client is null");
                    return BadRequest("Movie object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie object sent to client");
                    return BadRequest("Invalid model object");
                }

                var movieEntity = _mapper.Map<Movie>(movie);
               
                _repository.Movie.CreateMovie(movieEntity);
                await _repository.SaveAsync();

                var createdMovie = _mapper.Map<MovieDto>(movieEntity);

                return CreatedAtRoute("MovieById", new { id = createdMovie.Id }, createdMovie);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMovie action: {ex.Message}");
                return StatusCode(500, "Internal Server Error!");
            }
        }
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetMovieWithDetails(Guid id)
        {
            try
            {
                var movie = await _repository.Movie.GetMovieWithDetailsAsync(id);
                if (movie == null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned movie with details for id: {id}");

                    var movieResult = _mapper.Map<MovieDto>(movie);
                    return Ok(movieResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMovieWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] MovieUpdateDto movie)
        {
            try
            {
                if (movie == null)
                {
                    _logger.LogError("Movie object sent from client is null.");
                    return BadRequest("Movie object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var movieEntity = await _repository.Movie.GetMovieByIdAsync(id);
                if (movieEntity == null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(movie, movieEntity);

                _repository.Movie.UpdateMovie(movieEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateMovie action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            try
            {
                var movie = await _repository.Movie.GetMovieByIdAsync(id);
                if (movie == null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }



                _repository.Movie.DeleteMovie(movie);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteMovie action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
