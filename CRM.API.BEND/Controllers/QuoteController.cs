﻿using CRM.Application.DTOs;
using CRM.Application.Interfaces;
using CRM.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.API.BEND.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly ILogger<QuoteController> _logger;

        public QuoteController(IQuoteService quoteService, ILogger<QuoteController> logger)
        {
            _quoteService = quoteService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(QuoteDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<QuoteDTO>> GetQuoteById(Guid id)
        {
            try
            {
                var quote = await _quoteService.GetByIdAsync(id);
                if (quote == null)
                {
                    _logger.LogWarning("Cotação com ID {QuoteId} não encontrada.", id);
                    return NotFound();
                }
                return Ok(quote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter cotação por ID.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QuoteDTO>), 200)]
        public async Task<ActionResult<IEnumerable<QuoteDTO>>> GetAllQuotes()
        {
            try
            {
                var quotes = await _quoteService.GetAllAsync();
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todas as cotações.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddQuote([FromBody] QuoteDTO quote)
        {
            if (quote == null)
            {
                return BadRequest("Dados da cotação são obrigatórios.");
            }

            try
            {
                await _quoteService.AddAsync(quote);
                return CreatedAtAction(nameof(GetQuoteById), new { id = quote.QuoteID }, quote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar cotação.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateQuote(Guid id, [FromBody] QuoteDTO quote)
        {
            if (quote == null || quote.QuoteID != id)
            {
                return BadRequest("Dados da cotação são inválidos.");
            }

            try
            {
                var existingQuote = await _quoteService.GetByIdAsync(id);
                if (existingQuote == null)
                {
                    return NotFound();
                }

                await _quoteService.UpdateAsync(quote);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar cotação.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteQuote(Guid id)
        {
            try
            {
                var existingQuote = await _quoteService.GetByIdAsync(id);
                if (existingQuote == null)
                {
                    return NotFound();
                }

                await _quoteService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar cotação.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<QuoteDTO>>> SearchAsync([FromQuery] string query = null)
        {
            var quote = await _quoteService.SearchAsync(query);
            return Ok(quote);
        }
    }
}