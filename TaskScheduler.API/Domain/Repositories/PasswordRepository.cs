using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskScheduler.API.Domain.Repositories
{
    public interface IPasswordRepository
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class PasswordRepository : IPasswordRepository
    {
        public string HashPassword(string password)
        {
            // Gera o hash usando salt interno
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            // Verifica se a senha bate com o hash
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}