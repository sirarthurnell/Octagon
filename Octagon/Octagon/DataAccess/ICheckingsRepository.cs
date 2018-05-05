using Octagon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Octagon.DataAccess
{
    public interface ICheckingsRepository
    {
        IEnumerable<Checking> GetCheckings(DateTime from, DateTime to);
        int SaveChecking(Checking checking);
        void DeleteChecking(Checking checking);
    }
}
