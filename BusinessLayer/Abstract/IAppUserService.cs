using DtoLayer.AppUserDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAppUserService:IGenericService<AppUser>
    {
        Task<GetCheckAppUserDto> TGetCheckAppUserAsync(GetCheckAppUserQuery request);
        Task<CreateAppUserDto> TCreateAppUser(CreateAppUserDto createAppUserDto);
    }
}
