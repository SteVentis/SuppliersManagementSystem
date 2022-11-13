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
            CreateMap<SupplierCreateDto, Supplier>();
            CreateMap<SupplierUpdateDto, Supplier>();
        }
    }
}
