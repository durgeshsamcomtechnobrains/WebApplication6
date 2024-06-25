using AutoMapper;
using WebApplication1.Model;
using WebApplication1.Model.DTO_s;
namespace WebApplication1.mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //user Controller        
            CreateMap<User, UserDTO>().ReverseMap();
        }        
    }
}
