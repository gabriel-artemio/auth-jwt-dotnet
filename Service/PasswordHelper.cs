namespace AuthJwtWebApi.Service
{
    public static class PasswordHelper
    {
        public static string HashPassword(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool VerifyPassword(string senhaDigitada, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senhaDigitada, hash);
        }
    }
}