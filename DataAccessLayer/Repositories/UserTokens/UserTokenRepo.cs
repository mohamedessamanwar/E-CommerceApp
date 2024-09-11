using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.UserTokens
{
    public class UserTokenRepo : GenericRepo<UserToken> , IUserTokenRepo
    {
        public UserTokenRepo(ECommerceContext context) : base(context)
        {
        }
        public async Task<UserToken?> GetUserToken (string Token)
        {
            return context.userTokens.Where(e => e.Token == Token).FirstOrDefault();
        }


    }
}
