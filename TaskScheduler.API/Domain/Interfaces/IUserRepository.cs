using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.API.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task Add(User user);
        
    }
}