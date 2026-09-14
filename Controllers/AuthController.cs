using FleetFlow.Api.Data;
using FleetFlow.Api.DTOs;
using FleetFlow.Api.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FleetFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _cfg;

    public AuthController(AppDbContext db, IConfiguration cfg)
    {
        _db = db; _cfg = cfg;
    }

    [HttpPost("google")]
    public async Task<IActionResult> LoginGoogle(GoogleLoginRequest req)
    {
        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(
                req.IdToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _cfg["Google:ClientId"] }
                });
        }
        catch
        {
            return Unauthorized(new { erro = "Token do Google inválido" });
        }

        // Busca por sub (imutável) ou por email (primeiro login)
        var usuario = await _db.Usuarios
            .Include(u => u.Perfil)
            .FirstOrDefaultAsync(u =>
                u.GoogleSubjectId == payload.Subject || u.Email == payload.Email);

        if (usuario is null)
        {
            // Primeiro login → cria como Motorista aguardando aprovação
            var perfilMotorista = await _db.Perfis.FirstAsync(p => p.Nome == "Motorista");
            usuario = new Usuario
            {
                Nome = payload.Name,
                Email = payload.Email,
                GoogleSubjectId = payload.Subject,
                FotoUrl = payload.Picture,
                PerfilId = perfilMotorista.Id,
                StatusAprovacao = "AGUARDANDO_APROVACAO"
            };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                aguardandoAprovacao = true,
                mensagem = "Cadastro criado. Aguarde aprovação de um administrador."
            });
        }

        // Vincula o sub se ainda não estava vinculado (usuário criado antes via seed, por ex)
        if (usuario.GoogleSubjectId is null)
        {
            usuario.GoogleSubjectId = payload.Subject;
        }

        if (usuario.StatusAprovacao != "APROVADO")
            return Ok(new { aguardandoAprovacao = true, mensagem = "Aguardando aprovação." });

        usuario.UltimoLoginEm = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var jwt = GerarJwt(usuario);
        return Ok(new
        {
            token = jwt,
            usuario = new
            {
                id = usuario.Id,
                nome = usuario.Nome,
                email = usuario.Email,
                fotoUrl = usuario.FotoUrl,
                perfil = usuario.Perfil?.Nome
            }
        });
    }

    private string GerarJwt(Usuario u)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, u.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, u.Email),
            new Claim("perfil", u.Perfil?.Nome ?? "Motorista"),
        };

        var token = new JwtSecurityToken(
            issuer: _cfg["Jwt:Issuer"],
            audience: _cfg["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(int.Parse(_cfg["Jwt:ExpiresHours"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}