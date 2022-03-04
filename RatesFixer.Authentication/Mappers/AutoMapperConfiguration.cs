using AutoMapper;
using RatesFixer.Authentication.Entities;
using RatesFixer.Authentication.Model;

namespace RatesFixer.Authentication.Mappers
{
    public class AutoMapperConfiguration
    {
        private static MapperConfiguration EntityToVM
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InternalUserData, UserVM>();
                });
            }
        }

        public static MapperConfiguration InternalUserDataToUserVM
        {
            get => EntityToVM;
        }
    }
}
