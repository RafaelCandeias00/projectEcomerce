using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Context;
using EGeladinho.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace EGeladinho.Src.Repository.Implements
{
    public class ProductRepository : ICrud<Product>
    {
        private readonly GeladinhoDBC _context;

        public ProductRepository(GeladinhoDBC contexto)
        {
            _context = contexto;
        }
        public async Task Create(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Read(object param)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == (int) param);
        }

        public async Task Update(Product entity)
        {
            bool Exist = await _context.Products.AnyAsync(p => p.Id == entity.Id);

            if (Exist)
            {
                _context.Products.Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task Delete(object param)
        {
            bool Exist = await _context.Products.AnyAsync(p => p.Id == (int) param);

            if (Exist)
            {
                _context.Products.Remove(await _context.Products.FirstOrDefaultAsync(p => p.Id == (int) param));
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}