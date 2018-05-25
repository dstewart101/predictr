using Predictr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Interfaces
{
    public interface IFixtureRepository
    {
        Task<List<Fixture>> GetAll();
        Task<Fixture> GetSingleFixture(int id);
        void Add(Fixture fixture);
        void Delete(Fixture fixture);
        Boolean FixtureExists(int id);
        Task SaveChanges();
    }
}
