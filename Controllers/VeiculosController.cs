using FleetFlow.Api.Data;
using FleetFlow.Api.DTOs;
using FleetFlow.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetFlow.Api.Controllers;

[ApiController]
[Route("api/veiculos")]
[Authorize(Roles = "Administrador,Gestor de Frota")]
public class VeiculosController : ControllerBase
{
    private readonly AppDbContext _db;

    private static readonly string[] StatusPermitidos =
    [
        "Disponível",
        "Em viagem",
        "Manutenção",
        "Inativo"
    ];

    public VeiculosController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Veiculo>>> Listar()
    {
        return Ok(await _db.Veiculos
            .AsNoTracking()
            .OrderBy(x => x.Placa)
            .ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Veiculo>> Buscar(int id)
    {
        var veiculo = await _db.Veiculos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return veiculo is null ? NotFound("Veículo não encontrado.") : Ok(veiculo);
    }

    [HttpPost]
    public async Task<ActionResult<Veiculo>> Adicionar(VeiculoRequest request)
    {
        var placa = NormalizarPlaca(request.Placa);

        if (!StatusPermitidos.Contains(request.Status))
            return BadRequest("Status de veículo inválido.");

        if (await _db.Veiculos.AnyAsync(x => x.Placa == placa))
            return Conflict("Já existe um veículo cadastrado com essa placa.");

        var veiculo = new Veiculo
        {
            Placa = placa,
            Modelo = request.Modelo.Trim(),
            Marca = request.Marca.Trim(),
            Status = request.Status,
            AnoFabricacao = request.AnoFabricacao,
            CapacidadeCarga = request.CapacidadeCarga
        };

        _db.Veiculos.Add(veiculo);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Buscar), new { id = veiculo.Id }, veiculo);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, VeiculoRequest request)
    {
        var veiculo = await _db.Veiculos.FirstOrDefaultAsync(x => x.Id == id);
        if (veiculo is null)
            return NotFound("Veículo não encontrado.");

        var placa = NormalizarPlaca(request.Placa);

        if (!StatusPermitidos.Contains(request.Status))
            return BadRequest("Status de veículo inválido.");

        if (await _db.Veiculos.AnyAsync(x => x.Id != id && x.Placa == placa))
            return Conflict("Já existe outro veículo cadastrado com essa placa.");

        veiculo.Placa = placa;
        veiculo.Modelo = request.Modelo.Trim();
        veiculo.Marca = request.Marca.Trim();
        veiculo.Status = request.Status;
        veiculo.AnoFabricacao = request.AnoFabricacao;
        veiculo.CapacidadeCarga = request.CapacidadeCarga;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var veiculo = await _db.Veiculos.FirstOrDefaultAsync(x => x.Id == id);
        if (veiculo is null)
            return NotFound("Veículo não encontrado.");

        _db.Veiculos.Remove(veiculo);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static string NormalizarPlaca(string placa) =>
        placa.Trim().Replace("-", "").ToUpperInvariant();
}
