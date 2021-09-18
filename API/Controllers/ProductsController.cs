using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {

    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductBrand> _productBrand;
    private readonly IGenericRepository<ProductType> _productType;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepo,
    IGenericRepository<ProductBrand> productBrand, IGenericRepository<ProductType> productType, IMapper mapper)
    {
      _mapper = mapper;
      _productType = productType;
      _productBrand = productBrand;
      _productRepo = productRepo;


    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
      var spec = new ProductWithTypesAndBrandSpecif();
      var products = await _productRepo.ListAsync(spec);
      return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      var spec = new ProductWithTypesAndBrandSpecif(id);
      var product = await _productRepo.GetEntityWithSpec(spec);
      var productToReturnDto = _mapper.Map<Product,ProductToReturnDto>(product);
      return Ok(productToReturnDto);
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
    {
      return Ok(await _productBrand.GetAllAsync());
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsType()
    {
      return Ok(await _productType.GetAllAsync());
    }
  }
}