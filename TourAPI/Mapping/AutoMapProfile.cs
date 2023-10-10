using AutoMapper;
using TourAPI.Models.Domains;
using TourAPI.Models.Request;
using TourAPI.Models.Response;

namespace TourAPI.Mapping
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            MapLocation();
        }

        private void MapLocation()
        {
            CreateMap<Location, LocationResponse>().ReverseMap();
            CreateMap<Location, LocationRequest>().ReverseMap();
        }
    }
}
