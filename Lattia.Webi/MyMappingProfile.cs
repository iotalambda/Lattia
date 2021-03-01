using AutoMapper;
using Lattia.AutoMapper;

namespace Lattia.Webi
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {
            CreateMap<MyModel, MyEntity>();
            CreateMap<MyNestedModel, MyNestedEntity>();
            CreateMap<MyEntity, MyModel>()
                .AfterMap<ToLattiaModelAfterMapsAction<MyEntity, MyModel>>();
            CreateMap<MyNestedEntity, MyNestedModel>()
                .AfterMap<ToLattiaModelAfterMapsAction<MyNestedEntity, MyNestedModel>>(); ;
        }
    }
}