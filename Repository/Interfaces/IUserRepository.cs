using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<DomainModels.User> CreateUser(DomainModels.User user);
        Task<DomainModels.User> UpdateUser(DomainModels.User user);
        Task<DomainModels.User> GetUser(DomainModels.User user);
        Task<DomainModels.User> GetUserLogin(string subject);
        Task<List<string>> GetUserNames();
    }
}
