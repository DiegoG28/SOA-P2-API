﻿using System;
using Domain.Entities;
using Domain.Entities.Requests;
using Domain.Entities.ViewModels;

namespace Service.IServices
{
	public interface IAssetService
	{
		List<AssetViewModel> GetAssets(bool? statusFilter);
		public Assets CreateAsset(CreateAssetRequest asset);
		public bool DeleteAsset(int assetId);
		public bool ReleaseAsset(int assetId);
    }
}

