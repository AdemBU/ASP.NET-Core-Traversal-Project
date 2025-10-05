using AutoMapper;
using DTOLayer.DTOs.AnnouncementAddDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.ContactDTOs;
using EntityLayer.Concrete;

namespace Traversal.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AnnouncementAddDTO, Announcement>();
            CreateMap<Announcement, AnnouncementAddDTO>();

            CreateMap<AppUserRegisterDTO, AppUser>();
            CreateMap<AppUser, AppUserRegisterDTO>();

            CreateMap<AppUserLoginDTO, AppUser>();
            CreateMap<AppUser, AppUserLoginDTO>();

            CreateMap<AnnouncementListDto, Announcement>();
            CreateMap<Announcement, AnnouncementListDto>();

            CreateMap<AnnouncementUpdateDto, Announcement>();
            CreateMap<Announcement, AnnouncementUpdateDto>();

            CreateMap<SendMessageDto, ContactUS>().ReverseMap();  // ReverseMap() => yukarıdakiler gibi tersini yazmış gibi olur.
        }
    }
}
