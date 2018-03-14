using BasicCrud.Application.AppServices.Interfaces;
using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Services;
using BasicCrud.Dto.Product;
using BasicCrud.Infra.ReadInterfaces;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace BasicCrud.Application.AppServices
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IProductDomainService service;
        private readonly IProductReadRepository readRepository;

        public ProductAppService(IProductDomainService service, IProductReadRepository readRepository, INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            this.service = service;
            this.readRepository = readRepository;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto dto)
        {
            if (!ValidateDto<ProductDto, Guid>(dto))
                return ProductDto.NullInstance;                       

            var builder = Product.Create(Notification)
                .WithDescription(dto.Description)
                .WithValue(dto.Value);

            var entity = await service.InsertProductAsync(builder);
            dto.Id = entity.Id;

            return dto;
        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, ProductDto dto)
        {
            if (!ValidateDtoAndId(dto, id))
                return ProductDto.NullInstance;

            var builder = Product.Create(Notification)
                .WithId(id)
                .WithDescription(dto.Description)
                .WithValue(dto.Value);

            await service.UpdateProductAsync(builder);

            dto.Id = id;
            return dto;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            if (!ValidateId(id))
                return;

            await service.DeleteProductAsync(id);
        }

        public async Task<ProductDto> GetProductAsync(IRequestDto<Guid> id)
        {
            if (!ValidateRequestDto<IRequestDto<Guid>, Guid>(id))
                return ProductDto.NullInstance;

            var entity = await readRepository.GetProductAsync(id);

            return entity.MapTo<ProductDto>();
        }

        public async Task<IListDto<ProductDto, Guid>> GetAllProductAsync(ProductRequestAllDto request)
            => await readRepository.GetAllProductsAsync(request);
    }
}
