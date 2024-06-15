using AutoMapper;
using DtoLayer.BrandDtos;
using EntitityLayer.Entities;

namespace API.Mapping
{
    public class BrandMapping:Profile
    {
        public BrandMapping()
        {
            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            CreateMap<Brand, ResultBrandDto>().ReverseMap();

        }
    }
}
