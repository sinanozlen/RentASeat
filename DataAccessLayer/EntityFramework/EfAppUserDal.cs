using Azure.Core;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Enums;
using DataAccessLayer.Repositories;
using DtoLayer.AppUserDtos;
using DtoLayer.RentACarDtos;
using EntitityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAppUserDal : GenericRepository<AppUser>, IAppUserDal
    {
        public EfAppUserDal(RenASeatContext renASeatContext) : base(renASeatContext)
        {
        }

        public async Task<CreateAppUserDto> CreateAppUser(CreateAppUserDto createAppUserDto)
        {
            using var context = new RenASeatContext();

            var newUser = new AppUser
            {
                Password = createAppUserDto.Password,
                Username = createAppUserDto.Username,
                AppRoleId = (int)RolesType.User,
                Email = createAppUserDto.Email,
                Name = createAppUserDto.Name,
                Surname = createAppUserDto.Surname
            };

            context.AppUsers.Add(newUser);
            await context.SaveChangesAsync();
            return createAppUserDto;
        }


        public async Task<GetCheckAppUserDto> GetCheckAppUserAsync(GetCheckAppUserQuery request)
        {
            using var _renASeatContext = new RenASeatContext();
            var values = new GetCheckAppUserDto();

            var userList = await _renASeatContext.AppUsers
                .Where(x => x.Username == request.Username && x.Password == request.Password)
                .ToListAsync();

            var user = userList.FirstOrDefault();

            if (user == null)
            {
                values.IsExist = false;
            }
            else
            {
                values.IsExist = true;
                values.Username = user.Username;
                values.Id = user.AppUserId;

                var role = await _renASeatContext.AppRoles
                    .Where(x => x.AppRoleId == user.AppRoleId)
                    .Select(x => x.AppRoleName)
                    .FirstOrDefaultAsync();

                values.Role = role;
            }

            return values;
        }

    }
}