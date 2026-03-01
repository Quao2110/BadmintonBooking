using Application.DTOs.RequestDTOs.ProductImage;
using Application.DTOs.ResponseDTOs.ProductImage;
using Application.Interfaces.IServices;
using Application.Interfaces.IUnitOfWork;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class ProductImageService : IProductImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductImageService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductImageResponse>> GetAllAsync()
    {
        var images = await _unitOfWork.ProductImageRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductImageResponse>>(images);
    }

    public async Task<IEnumerable<ProductImageResponse>> GetByProductIdAsync(Guid productId)
    {
        var images = await _unitOfWork.ProductImageRepository.GetByProductIdAsync(productId);
        return _mapper.Map<IEnumerable<ProductImageResponse>>(images);
    }

    public async Task<ProductImageResponse> GetByIdAsync(Guid id)
    {
        var image = await _unitOfWork.ProductImageRepository.GetByIdAsync(id)
            ?? throw new Exception("Product image not found.");

        return _mapper.Map<ProductImageResponse>(image);
    }

    public async Task<ProductImageResponse> CreateAsync(ProductImageCreateRequest request)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        if (product == null)
            throw new Exception("Product not found.");

        var image = new ProductImage
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            ImageUrl = request.ImageUrl,
            IsThumbnail = request.IsThumbnail ?? false
        };

        await _unitOfWork.ProductImageRepository.CreateAsync(image);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductImageResponse>(image);
    }

    public async Task<ProductImageResponse> UpdateAsync(Guid id, ProductImageUpdateRequest request)
    {
        var image = await _unitOfWork.ProductImageRepository.GetByIdAsync(id)
            ?? throw new Exception("Product image not found.");

        image.ImageUrl = request.ImageUrl;
        image.IsThumbnail = request.IsThumbnail ?? image.IsThumbnail;

        _unitOfWork.ProductImageRepository.Update(image);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductImageResponse>(image);
    }

    public async Task DeleteAsync(Guid id)
    {
        var image = await _unitOfWork.ProductImageRepository.GetByIdAsync(id)
            ?? throw new Exception("Product image not found.");

        _unitOfWork.ProductImageRepository.Delete(image);
        await _unitOfWork.SaveChangesAsync();
    }
}
