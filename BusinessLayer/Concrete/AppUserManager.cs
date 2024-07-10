using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DtoLayer.AppUserDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDal _appUserDal;

        public AppUserManager(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }

        public void TAdd(AppUser entity)
        {
            _appUserDal.Add(entity);
        }

        public Task<CreateAppUserDto> TCreateAppUser(CreateAppUserDto createAppUserDto)
        {
            var values= _appUserDal.CreateAppUser(createAppUserDto);
            return values;
        }

        public void TDelete(AppUser entity)
        {
            _appUserDal.Delete(entity);
        }

        public List<AppUserByRoleNameDto> TGetAllAppUsersWithRoles()
        {
          var values= _appUserDal.GetAllAppUsersWithRoles();
            return values;
        }

        public AppUser TGetbyID(int ID)
        {
            var values= _appUserDal.GetbyID(ID);
            return values;
        }

        public Task<GetCheckAppUserDto> TGetCheckAppUserAsync(GetCheckAppUserQuery request)
        {
            var values= _appUserDal.GetCheckAppUserAsync(request);
            return values;
        }

        public List<AppUser> TGetListAll()
        {
            var values= _appUserDal.GetListAll();
            return values;
        }

        public void TUpdate(AppUser entity)
        {
           _appUserDal.Update(entity);
        }

    

        Task<bool> IAppUserService.TUpdateAppUserRole(string userName, int newRoleId)
        {
            var values= _appUserDal.UpdateAppUserRole(userName, newRoleId);
            return values;
        }
    }
}
