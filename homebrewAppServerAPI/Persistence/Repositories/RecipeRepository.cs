﻿using homebrewAppServerAPI.Domain.Models;
using homebrewAppServerAPI.Domain.Repositories;
using homebrewAppServerAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homebrewAppServerAPI.Persistence.Repositories
{
    public class RecipeRepository : BaseRepository, IRecipeRepository
    {
#if USE_SQLITE
        public RecipeRepository(SqliteDbContext context) : base(context) { }
#else
        public RecipeRepository(AppDbContext context) : base(context) { }
#endif

        public async Task<IEnumerable<Recipe>> ListAsync()
        {
            return await _context.Recipes
                                    .Include(p => p.WaterProfile).ThenInclude(w => w.Additions)
                                    .Include(p => p.Ingredients)
                                    .ToListAsync();
        }

        public async Task<Recipe> AddAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            await _context.Entry(recipe).GetDatabaseValuesAsync();

            return recipe;
        }

        public async Task<Recipe> FindByIdAsync(int id)
        {
            return await _context.Recipes
                                    .Include(p => p.WaterProfile).ThenInclude(w => w.Additions)
                                    .Include(p => p.Ingredients)
                                    .Include(p => p.Steps)
                                    .FirstOrDefaultAsync(recipe => recipe.ID == id);
        }

        public async Task<Recipe> Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return await _context.Recipes
                                    .Include(p => p.WaterProfile).ThenInclude(w => w.Additions)
                                    .Include(p => p.Ingredients)
                                    .FirstOrDefaultAsync(recipeToFind => recipeToFind.ID == recipe.ID);
        }

        public void Remove(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
        }
    }
}
