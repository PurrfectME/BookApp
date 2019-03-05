﻿using BLL.DataAccess;
using DAL.Context;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;


        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }


        public async void Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
