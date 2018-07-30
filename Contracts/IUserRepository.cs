using System.Threading.Tasks;
using ExpenseManagerBackEnd.Models.DbModels;
using ExpenseManagerBackEnd.Models.ApiModels;

namespace ExpenseManagerBackEnd.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUser(string email);
        Task<User> CreateUser(User user);

    }
}