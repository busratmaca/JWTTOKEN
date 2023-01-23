using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JWTTOKEN.Entities;

namespace JWTTOKEN.Services
{
    public interface IUserService
    {
        User AuthenticateUser(string firstname, string password);
        Claim[] GetClaims(User user);
    }
    public class UserService : IUserService
    {
        private readonly List<User> Users = new List<User>
        {
            new User
            {
                Id=13,
                Name="Busra",
                Surname="Atmaca",
                Username = "busratmaca",
                Password="1q2w3e4r5t",
                IsActive =true,
                IsAdmin =true
            }
        };
        public User AuthenticateUser(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive == true);
        }

        public Claim[] GetClaims(User user)
        {
            var claims = new[]
           {
            new Claim(JwtRegisteredClaimNames.UniqueName,user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var claimList = new List<Claim>(claims);
            claimList.Add(new Claim("username", user.Username));
            claimList.Add(new Claim("displayname", user.Name));
            if (user.IsAdmin)
                claimList.Add(new Claim(ClaimTypes.Role, "admin"));
            claims = claimList.ToArray();

            return claims;

        }
    }


}
