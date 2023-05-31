using System;
using Domain.Entities;
using Domain.Entities.Requests;
using Domain.Entities.ViewModels;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.DAO;
using Service.IServices;

namespace Service.Services
{
	public class AssetsService : IAssetService
	{
		private readonly ILogger<AssetsService> _logger;
		public readonly AssetsRepository assetsRepository;

		public AssetsService(ILogger<AssetsService> logger, ApplicationDbContext context)
		{
			_logger = logger;
			assetsRepository = new AssetsRepository(context);
		}

        public List<AssetViewModel> GetAssets(bool? statusFilter)
        {
            List<AssetViewModel> assets = new List<AssetViewModel>();

            try
            {
                var assetEntities = assetsRepository.GetAssets(statusFilter);

                assets = assetEntities.Select(asset => new AssetViewModel
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    Description = asset.Description,
                    Status = asset.Status,
                }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return assets;
        }


        public Assets CreateAsset(CreateAssetRequest newAsset)
        {
            try
            {
                var asset = new Assets
                {
                    Name = newAsset.Name,
                    Description = newAsset.Description,
                    Status = true,
                };

                var createdAsset = assetsRepository.CreateAsset(asset);
                return createdAsset;
 
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public bool DeleteAsset(int assetId)
        {
            try
            {
                return assetsRepository.DeleteAsset(assetId);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public bool ReleaseAsset(int assetId)
        {
            try
            {
                return assetsRepository.ReleaseAsset(assetId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }


    }
}

