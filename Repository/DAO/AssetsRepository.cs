using System;
using Domain.Entities;
using Domain.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.DAO
{
	public class AssetsRepository
	{
        private readonly ApplicationDbContext _context;

        public AssetsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

        public List<Assets> GetAssets(bool? statusFilter)
        {
            var query = _context.Assets.AsQueryable();

            if (statusFilter.HasValue)
            {
                query = query.Where(asset => asset.Status == statusFilter.Value);
            }

            return query.ToList();
        }

        public Assets CreateAsset(Assets asset)
        {

            _context.Assets.Add(asset);
            _context.SaveChanges();

            return asset;
        }

    }
}

