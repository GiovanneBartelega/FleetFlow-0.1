using System.Security.Cryptography;

namespace FleetFlow.Api.Services;

public class PasswordService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public string GerarHash(string senha)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            senha,
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool Verificar(string senha, string senhaHash)
    {
        var partes = senhaHash.Split('.');
        if (partes.Length != 2)
            return false;

        var salt = Convert.FromBase64String(partes[0]);
        var hashArmazenado = Convert.FromBase64String(partes[1]);

        var hashInformado = Rfc2898DeriveBytes.Pbkdf2(
            senha,
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize);

        return CryptographicOperations.FixedTimeEquals(hashArmazenado, hashInformado);
    }
}
