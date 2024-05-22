using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.CommentMapper;
using api.Dtos.Comment;
using api.Interface;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            return Ok(comments.Select(c => c.ToCommentDTO()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDTO createcommentDTO)
        {
            if(!await _stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var comment = createcommentDTO.ToCommentFromCreateCommentDTO(stockId);

            await _commentRepository.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDTO());
        }
    }
}