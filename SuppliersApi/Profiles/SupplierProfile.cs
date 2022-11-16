using AutoMapper;
using Models.Dtos;
using Models.Entities;

namespace SuppliersApi.Profiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierReadDto>();
            CreateMap<SupplierCreateOrUpdateDto, Supplier>();
            CreateMap<Supplier, SupplierCreateOrUpdateDto>();
        }
    }
}
