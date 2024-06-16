using AutoMapper;
using DtoLayer.CarDtos;
using EntitityLayer.Entities;

namespace API.Mapping
{
    public class CarMapping: Profile
    {
        public CarMapping()
        {
            CreateMap<Car, CreateCarDto>().ReverseMap();
            CreateMap<Car, UpdateCarDto>().ReverseMap();
            CreateMap<Car, ResultCarWithBrandDto>().ReverseMap();
        }
    }
}
