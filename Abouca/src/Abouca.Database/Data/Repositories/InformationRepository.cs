using System;
using System.Collections.Generic;
using System.Text;
using Abouca.Database.Context;
using Abouca.Domain.Information;
using Abouca.Domain.Interfaces;

namespace Abouca.Database.Data.Repositories
{
    public class InformationRepository:EfRepositoryBase<Information,MainContext>,IInformationRepository
    {
    }
}
