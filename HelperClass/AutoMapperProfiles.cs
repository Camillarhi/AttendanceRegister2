using AttendanceRegister2.DTOs;
using AttendanceRegister2.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceRegister2.HelperClass
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
          //  CreateMap<RegisterModel, StaffModel>().ReverseMap();
            CreateMap<StaffModelDTO, StaffModel>().ReverseMap()
                .ForMember(x => x.ProfilePicture, options => options.Ignore())
                //.ForMember(x => x.StaffId, options => options.Ignore())
                ;
        }
    }
}
