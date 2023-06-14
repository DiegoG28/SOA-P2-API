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

        public Assets GetAssetById(int assetId)
        {
            return _context.Assets.FirstOrDefault(a => a.Id == assetId);
        }

        public Assets CreateAsset(Assets asset)
        {
            _context.Assets.Add(asset);
            _context.SaveChanges();

            return asset;
        }

        public bool DeleteAsset(int assetId)
        {
            var asset = GetAssetById(assetId);
            if (asset == null)
            {
                return false; // El activo no existe
            }

            var assetEmployees = _context.EmployeesHasAssets.Where(eha => eha.AssetId == assetId).ToList();
            foreach (var assetEmployee in assetEmployees)
            {
                _context.EmployeesHasAssets.Remove(assetEmployee);
            }

            _context.Assets.Remove(asset);
            _context.SaveChanges();

            return true;
        }

        public bool ReleaseAsset(int assetId)
        {
            var asset = GetAssetById(assetId);
            if (asset == null)
            {
                return false; // El activo no existe
            }

            if (asset.Status)
            {
                return false; // El asset no está asignado a ningún empleado
            }

            asset.Status = true;

            var employeeAssets = _context.EmployeesHasAssets.Where(eha => eha.AssetId == assetId).ToList();
            foreach (var employeeAsset in employeeAssets)
            {
                employeeAsset.ReleaseDate = DateTime.Now;
            }

            _context.SaveChanges();

            return true;
        }

    }
}

