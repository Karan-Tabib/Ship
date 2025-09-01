using AutoMapper;
using DTOs.DTO;
using DataLayer.Entities;
namespace DTOs.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<BoatInformation, BoatInfoDTO>().ReverseMap();
            CreateMap<CrewInformation, CrewInfoDTO>().ReverseMap();
            CreateMap<FishInformation, FishInfoDTO>().ReverseMap();
            CreateMap<LeaveInformation, LeaveInfoDTO>().ReverseMap();
            CreateMap<LeaveSummary, LeaveSummaryDTO>().ReverseMap();
            CreateMap<SalaryInformation, SalaryInfoDTO>().ReverseMap();
            CreateMap<SalaryInformation, SalaryInfoDTO>().ReverseMap();
            CreateMap<TripExpenditure, TripExpenditureDTO>().ReverseMap();
            CreateMap<TripParticular, TripParticularDTO>().ReverseMap();
            CreateMap<SupplierInformation, SupplierInfoDTO>().ReverseMap();
            CreateMap<TripInformation, TripInfoDTO>().ReverseMap();
        }
    }
}
