using FleetFlow.Api.Data;
using FleetFlow.Api.DTOs;
using FleetFlow.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // exige JWT válido
public class CategoriasController : ControllerBase
{
	private readonly AppDbContext _db;
	public CategoriasController(AppDbContext db) => _db = db;

	[HttpGet]
	public async Task<IActionResult> Listar([FromQuery] bool incluirInativas = false)
	{
		var query = _db.CategoriasFinanceiras.AsQueryable();
		if (!incluirInativas) query = query.Where(c => c.Ativo);
		return Ok(await query.OrderBy(c => c.Nome).ToListAsync());
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> Obter(Guid id)
	{
		var cat = await _db.CategoriasFinanceiras.FindAsync(id);
		return cat is null ? NotFound() : Ok(cat);
	}

	[HttpPost]
	public async Task<IActionResult> Criar(CategoriaRequest req)
	{
		if (req.Tipo != "RECEITA" && req.Tipo != "DESPESA")
			return BadRequest(new { erro = "Tipo deve ser RECEITA ou DESPESA" });

		var cat = new CategoriaFinanceira
		{
			Nome = req.Nome,
			Descricao = req.Descricao,
			Tipo = req.Tipo
		};
		_db.CategoriasFinanceiras.Add(cat);
		await _db.SaveChangesAsync();
		return CreatedAtAction(nameof(Obter), new { id = cat.Id }, cat);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Atualizar(Guid id, CategoriaRequest req)
	{
		var cat = await _db.CategoriasFinanceiras.FindAsync(id);
		if (cat is null) return NotFound();

		cat.Nome = req.Nome;
		cat.Descricao = req.Descricao;
		cat.Tipo = req.Tipo;
		await _db.SaveChangesAsync();
		return Ok(cat);
	}

	// Soft delete — US01 pede desativação, não exclusão física
	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Desativar(Guid id)
	{
		var cat = await _db.CategoriasFinanceiras.FindAsync(id);
		if (cat is null) return NotFound();
		cat.Ativo = false;
		await _db.SaveChangesAsync();
		return NoContent();
	}
}