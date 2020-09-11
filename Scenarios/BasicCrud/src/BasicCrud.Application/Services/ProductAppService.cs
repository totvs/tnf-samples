using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Repositories;
using BasicCrud.Domain.Interfaces.Services;
using BasicCrud.Dto;
using BasicCrud.Dto.Product;
using BasicCrud.Infra.ReadInterfaces;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Notifications;
using Tnf.Repositories.Uow;

namespace BasicCrud.Application.Services
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IProductDomainService _domainService;
        private readonly IProductReadRepository _readRepository;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IProductDomainService domainService, 
            IProductReadRepository readRepository,
            IProductRepository repository,
            INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _domainService = domainService;
            _readRepository = readRepository;
            _repository = repository;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto dto)
        {
            if (!ValidateDto<ProductDto>(dto))
                return null;

            var builder = Product.Create(Notification)
                .WithDescription(dto.Description)
                .WithValue(dto.Value);

            Product entity = null;

            using (var uow = _unitOfWorkManager.Begin())
            {
                entity = await _domainService.InsertProductAsync(builder);

                await uow.CompleteAsync().ForAwait();
            }

            if (Notification.HasNotification())
                return null;

            return entity.MapTo<ProductDto>();
        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, ProductDto dto)
        {
            if (!ValidateDtoAndId(dto, id))
                return null;

            var builder = Product.Create(Notification)
                .WithId(id)
                .WithDescription(dto.Description)
                .WithValue(dto.Value);

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _domainService.UpdateProductAsync(builder);

                await uow.CompleteAsync().ForAwait();
            }

            dto.Id = id;
            return dto;
        }

        public async Task<ProductDto> PatchProductAsync(Guid id, JsonPatchDocument productPatch)
        {
            var product = await _readRepository.GetProductAsync(id);

            productPatch.ApplyTo(product);

            // Valida modelo com mudanças
            if (!ValidateDtoAndId(product.MapTo<ProductDto>(), id))
                return null;

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _repository.UpdateProductAsync(product);

                await uow.CompleteAsync().ForAwait();
            }

            var dto = product.MapTo<ProductDto>();
            dto.Id = id;

            return dto;
        }

        // Exemplo de operação de update parcial explícito (valor resetado para mínimo)
        public async Task<IListDto<ProductDto>> ResetAllProductAsync(ProductRequestAllDto request)
        {
            var products = await _readRepository.GetAllProductsAsync(request);

            foreach (var productDto in products.Items)
            {
                productDto.Value = 1;

                var product = Product.Create(Notification)
                    .WithId(productDto.Id)
                    .WithDescription(productDto.Description)
                    .WithValue(1)
                    .Build();

               await _repository.UpdateProductAsync(product, p => p.Value);
            }

            return products;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            if (!ValidateId(id))
                return;

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _domainService.DeleteProductAsync(id);

                await uow.CompleteAsync().ForAwait();
            }
        }

        public async Task<ProductDto> GetProductAsync(DefaultRequestDto id)
        {
            if (!ValidateRequestDto(id) || !ValidateId<Guid>(id.Id))
                return null;

            var entity = await _readRepository.GetProductAsync(id);

            return entity.MapTo<ProductDto>();
        }

        public async Task<IListDto<ProductDto>> GetAllProductAsync(ProductRequestAllDto request)
            => await _readRepository.GetAllProductsAsync(request);
    }
}
