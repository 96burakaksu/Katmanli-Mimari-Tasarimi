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
    
    
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IService<Product> service, IProductService productService)
        {
            _mapper = mapper;
            _service = productService;
        }

        [HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _service.GetProductsWithCategory());
        }




        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();

            var productsDtos = _mapper.Map<List<ProductDTO>>(products.ToList());

            //return Ok(CustomResponseDTO<List<ProductDTO>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            var productsDto = _mapper.Map<ProductDTO>(product);

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {
             var product = await _service.AddAsync(_mapper.Map<Product>(productDTO));

            var productsDto = _mapper.Map<ProductDTO>(product);

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(201, productsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO producUpdatetDTO)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(producUpdatetDTO));

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
             await _service.RemoveAsync(product);

            if (product == null)
            {
                return CreateActionResult(CustomResponseDTO<NoContentDTO>.Fail(404,"Ürün bulunamadı"));
            }

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(204));
        }
    }
}
