using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;

        public ProductsController(IMapper mapper, IService<Product> service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();

            var productsDtos = _mapper.Map<List<ProductDTO>>(products.ToList());

            return Ok(CustomResponseDTO<List<ProductDTO>>.Success(200, productsDtos));
        }
    }
}
