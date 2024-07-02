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
using System.Security.Cryptography;

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

            // Hash the password and generate salt
            var (passwordHash, passwordSalt) = HashPassword(createAppUserDto.Password);

            var newUser = new AppUser
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Username = createAppUserDto.Username,
                AppRoleId = (int)RolesType.Admin,
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

            var user = await _renASeatContext.AppUsers
                .FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
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

        private (string hash, string salt) HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var salt = Convert.ToBase64String(hmac.Key);
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return (hash, salt);
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));
                return computedHash == storedHash;
            }
        }
    }
}
