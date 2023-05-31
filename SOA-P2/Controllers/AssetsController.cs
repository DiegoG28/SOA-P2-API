using System;
using Domain.Entities.Requests;
using Domain.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;

namespace SOA_P2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetsController : Controller
    {

        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;

        }

        [HttpGet]
        public IActionResult GetAll(bool? status)
        {
            var assets = _assetService.GetAssets(status);
            return Ok(assets);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAssetRequest request)
        {
            if (ModelState.IsValid)
            {
                var createdAsset = _assetService.CreateAsset(request);
                if (createdAsset != null)
                {
                    return Ok(createdAsset);
                }
                return BadRequest("No se pudo crear el activo.");
            }
            return BadRequest("Datos de activo inválidos.");
        }

        [HttpDelete("{assetId}")]
        public IActionResult DeleteAsset(int assetId)
        {
            bool result = _assetService.DeleteAsset(assetId);
            if (result)
            {
                return Ok("Activo eliminado correctamente.");
            }
            else
            {
                return NotFound("El activo no existe.");
            }
        }

        [HttpPatch("{assetId}/release")]
        public IActionResult ReleaseAsset(int assetId)
        {
            bool released = _assetService.ReleaseAsset(assetId);
            if (released)
            {
                return Ok("Activo liberado exitosamente.");
            }
            else
            {
                return NotFound("El activo no está asignado a ningún empleado o no existe.");
            }
        }
    }
}

