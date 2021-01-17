using System;
using System.Collections.Generic;
using System.Text;
using Abouca.Database.Context;
using Abouca.Domain.Interfaces;
using Abouca.Domain.User;

namespace Abouca.Database.Data.Repositories
{
    public class UserRepository:EfRepositoryBase<User,MainContext>,IUserRepository
    {
    }
}
