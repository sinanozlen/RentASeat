using DtoLayer.AppUserDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        Task<GetCheckAppUserDto> GetCheckAppUserAsync(GetCheckAppUserQuery request);
        Task<CreateAppUserDto>CreateAppUser(CreateAppUserDto createAppUserDto);
        Task<bool> UpdateAppUserRole(string userName, int newRoleId);
        List<AppUserByRoleNameDto> GetAllAppUsersWithRoles();
    }
}
