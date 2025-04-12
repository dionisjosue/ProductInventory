using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecurityDomain.DTO;
using SharedItems.JWT;
using SecurityDomain.Models;
using SecurityDomain.Repositories;
using SecurityInfrastructure.Database;

namespace SecurityInfrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {

        private JwtSettings _settings { get; }

        public AppUserRepository(SecurityDbContext context, JwtSettings settings) : base(context)
        {
            _settings = settings;
        }

        public async Task<UserAuthDTO> BuildUserAuthObject(AppUser appUser)
        {
            UserAuthDTO ret = new UserAuthDTO();
            ret.Id = appUser.Id;
            ret.UserName = appUser.UserName;
            ret.FirstName = appUser.FirstName;
            ret.LastName = appUser.LastName;
            ret.Email = appUser.Email;
            ret.PhoneNumber = appUser.PhoneNumber;
            ret.IsAuthenticated = true;
            // Get all claims for this user
            var appClaimDtos = await GetRoleClaims(appUser);

            // Set JWT bearer token
            ret.BearerToken = BuildJwtToken(ret, appClaimDtos, appUser.Id);
            return ret;
        }

        private async Task<List<string>> GetRoleClaims(AppUser appUser)
        {

            var rolesIds = await _productDbContext.UserRoles.Where(c => c.UserId == appUser.Id)
                .Select(c => c.RoleId)
                .ToListAsync();

            List<RoleAction> lstRoleClaim = new List<RoleAction>();

            //changed c.ClaimType to c.Action.ClaimName
            var roleactions = await _productDbContext.RoleActions.Include(t => t.Action).Where
                (c => rolesIds.Contains(c.RoleId)).Select(c => c.Action.Name).ToListAsync();

            return roleactions;
        }
        protected string BuildJwtToken(UserAuthDTO authUser, List<string> actions, Guid Id)
        {
            var claims = new List<Claim>();
            SymmetricSecurityKey key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_settings.Key));

            claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub.ToString(), Id.ToString()));
            claims.Add(new Claim("Email", authUser.Email));
            claims.Add(new Claim("Id", authUser.Id.ToString()));

            foreach (var action in actions)
            {
                claims.Add(new Claim("actions", action));
            }

            // Create the JwtSecurityToken object
            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              claims: claims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(
                  _settings.MinutesToExpiration),
              signingCredentials: new SigningCredentials(key,
                          SecurityAlgorithms.HmacSha256)
            );
            // Create a string representation of the Jwt token
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}

