using System;
using System.Collections.Generic;
using System.Text;

namespace Abouca.Domain.Interfaces
{
    public interface IUserRepository:IAsyncRepository<User.User>
    {
    }
}
