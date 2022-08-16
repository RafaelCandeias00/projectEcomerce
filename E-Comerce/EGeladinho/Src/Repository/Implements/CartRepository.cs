using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Context;
using EGeladinho.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace EGeladinho.Src.Repository.Implements
{
    public class CartRepository : ICrud<Cart>
    {
        private readonly GeladinhoDBC _context;

        public CartRepository(GeladinhoDBC contexto)
        {
            _context = contexto;
        }
        public async Task Create(Cart entity)
        {
            await _context.Carts.AddAsync(new Cart 
            {
                Buyer = _context.Users.FirstOrDefault(u => u.Id == entity.Buyer.Id),
                Product = _context.Products.FirstOrDefault(p => p.Id == entity.Product.Id),
                Date = DateTime.Now,
                StatusPayment = entity.StatusPayment
            });
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> Read(object param)
        {
            return await _context.Carts
                .Include(c => c.Buyer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == (int) param);
        }

        public async Task Update(Cart entity)
        {
            bool Exist = await _context.Carts.AnyAsync(p => p.Id == entity.Id);

            if (Exist)
            {
                _context.Carts.Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task Delete(object param)
        {
            bool Exist = await _context.Carts.AnyAsync(p => p.Id == (int) param);

            if (Exist)
            {
                _context.Carts.Remove(await _context.Carts.FirstOrDefaultAsync(p => p.Id == (int) param));
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}