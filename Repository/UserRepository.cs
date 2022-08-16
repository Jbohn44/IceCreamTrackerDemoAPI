using Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    //TODO: Add Encryption/Decryption for user data stored - note login and passwords handled by google auth
    public class UserRepository : IUserRepository
    {
        private Data.DataModels.IceCreamDataContext _context;

        public UserRepository(Data.DataModels.IceCreamDataContext context)
        {
            _context = context;
        }


        public async Task<DomainModels.User> CreateUser(DomainModels.User user)
        {
            using (_context)
            {
                var createdUser = new User
                {
                    UserName = user.UserName,
                };

                _context.Users.Add(createdUser);
                await _context.SaveChangesAsync();

                return Map(createdUser);
            }
        }

        public async Task<DomainModels.User> UpdateUser(DomainModels.User user)
        {
            // will need to do authorization/verification in this method also
            using (_context)
            {
                var userToUpdate = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
                _context.Update(userToUpdate);
                await _context.SaveChangesAsync();
                return Map(userToUpdate);
            }
        }

        //method not used
        public async Task<DomainModels.User> GetUser(DomainModels.User user)
        {
            //THIS NEEDS TO BE RE WRITTEN
            using (_context)
            {
                // this is for testing... needs to be changes to auth google idtokens!!!!!
                var signedInUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
                if(signedInUser != null)
                {
                    return Map(signedInUser);

                }
                else
                {
                    throw new InvalidCredentialException();
                }
            }
        }

        public async Task<DomainModels.User> GetUserLogin(string subject)
        {
            using (_context)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.ExternalId == subject);
                return Map(user);
            }
               
        }

        public async Task<List<string>> GetUserNames()
        {
            using (_context)
            {
                var userNameList = await _context.Users.Select(u => u.UserName).ToListAsync();
                return userNameList;
            }
        }

        private DomainModels.User Map(Data.DataModels.User user)
        {
            return new DomainModels.User
            {
                UserId = user.UserId,
                UserName = user.UserName,
            };
        }
    }
}
