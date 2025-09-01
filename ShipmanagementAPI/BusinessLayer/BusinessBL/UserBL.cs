using BusinessLayer.Abstraction;
using DTOs.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Repositories;
using System.Linq.Expressions;

namespace BusinessLayer.BusinessBL
{
    public class UserBL : IUserBL
    {
        private readonly IUserRepository _UserRepository;

        public UserBL(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public UserDTO GetUser(LoginDTO loginUser)
        {
            var user = _UserRepository.GetByEmail(loginUser.Username);
            var dto = new UserDTO()
            {
                Password = user.Password,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Middlename = user.Middlename,
                Phone = user.Phone,
                Address = user.Address,
                UserId =user.UserId
            };

            return dto;
        }
    }
}
