using System.Threading.Tasks;
using ExpenseManagerBackEnd.Models.DbModels;

namespace ExpenseManagerBackEnd.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUser(string email);
        Task<User> CreateUser(User user);

    }
}