using FleetFlow.Api.Data;
using FleetFlow.Api.DTOs;
using FleetFlow.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetFlow.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly PasswordService _passwordService;
    private readonly TokenService _tokenService;

    public AuthController(
        AppDbContext db,
        PasswordService passwordService,
        TokenService tokenService)
    {
        _db = db;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var usuario = await _db.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email && x.Ativo);

        if (usuario is null || !_passwordService.Verificar(request.Senha, usuario.SenhaHash))
            return Unauthorized("E-mail ou senha inválidos.");

        return Ok(new LoginResponse
        {
            Token = _tokenService.GerarToken(usuario),
            Nome = usuario.Nome,
            Email = usuario.Email,
            Perfil = usuario.Perfil
        });
    }
}
