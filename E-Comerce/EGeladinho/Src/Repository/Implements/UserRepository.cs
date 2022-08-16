using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Context;
using EGeladinho.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace EGeladinho.Src.Repository.Implements
{
    public class UserRepository : ICrud<User>
    {
        private readonly GeladinhoDBC _context;

        public UserRepository(GeladinhoDBC contexto)
        {
            _context = contexto;
        }
        public async Task Create(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Read(object param)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == (int) param);
        }

        public async Task Update(User entity)
        {
            bool Exist = await _context.Users.AnyAsync(u => u.Id == entity.Id);

            if (Exist)
            {
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();
            }else{
                throw new Exception("User not found");
            }

        }

        public async Task Delete(object param)
        {
            bool Exist = await _context.Users.AnyAsync(u => u.Id == (int) param);

            if (Exist)
            {
                _context.Users.Remove(await _context.Users.FirstOrDefaultAsync(u => u.Id == (int) param));
                await _context.SaveChangesAsync();
            }else{
                throw new Exception("User not found");
            }
        }

    }
}