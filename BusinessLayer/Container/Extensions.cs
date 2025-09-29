using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.AnnouncementAddDTOs;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Container
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICommentService, CommentManager>(); //54
            services.AddScoped<ICommentDal, EfCommentDal>();  //54

            services.AddScoped<IDestinationService, DestinationManager>(); //55
            services.AddScoped<IDestinationDal, EfDestinationDal>();  //55

            services.AddScoped<IAppUserService, AppUserManager>(); //55
            services.AddScoped<IAppUserDal, EfAppUserDal>();  //55

            services.AddScoped<IReservationService, ReservationManager>(); //56
            services.AddScoped<IReservationDal, EfReservationDal>();  //56

            services.AddScoped<IGuideService, GuideManager>(); //57
            services.AddScoped<IGuideDal, EfGuideDal>();  //57

            services.AddScoped<IExcelService, ExcelManager>();  //61
            services.AddScoped<IPdfService, PdfManager>();   //61

            services.AddScoped<IContactUsService, ContactUsManager>();  //66
            services.AddScoped<IContactUsDal, EfContactUsDal>();

            services.AddScoped<IAnnouncementService, AnnouncementManager>();
            services.AddScoped<IAnnouncementDal, EfAnnouncementDal>();

        }

        public static void CustomerValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AnnouncementAddDTO>, AnnouncementValidator>();
        }
    }
}
