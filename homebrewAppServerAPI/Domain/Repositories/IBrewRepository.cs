﻿using homebrewAppServerAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homebrewAppServerAPI.Domain.Repositories
{
    public interface IBrewRepository
    {
        Task<IEnumerable<Brew>> ListAsync();
        Task AddAsync(Brew brew);
        Task<Brew> FindByIdAsync(int id);
        void Update(Brew brew);
        void Remove(Brew brew);
    }
}
