using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.DTOs.Comment;
using web_api.interfaces;
using web_api.Mappers;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public readonly ICommentRepository _commentRepository;
        public readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(comment => comment.ToCommentDto());

            return Ok(commentDto);
        } 

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

           if(comment == null){
                return NotFound();
           }

           return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateAsync([FromRoute] int stockId, [FromBody] CreateCommentDto comment)
        {
            if( !await _stockRepository.StockExist(stockId)) 
            {
                return NotFound("Stock not found");
            }

            var newComment = comment.ToCommentFromCreate(stockId);
            await _commentRepository.CreateAsync(newComment);

            return CreatedAtAction(nameof(GetById), new { id = newComment.Id }, newComment.ToCommentDto());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateCommentDto commentUpdate)
        {
            
            var comment =await _commentRepository.UpdateAsync(id,commentUpdate.ToCommentFromUpdate());

            if(comment == null){
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var commentModel = await _commentRepository.DeleteAsync(id);

            if(commentModel == null){
                return NotFound("Coment does not exists");
            }
            return Ok(commentModel);
        }   
    }
}