using Holamundo.Models.DataModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Holamundo.Helpers
{
    public class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss: tt"))
            };

            if(userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            }else if (userAccounts.UserName == "User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));

            }
            return claims;
        }
       
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, out Id);

        }


        public static UserTokens GenTokenKeys(UserTokens model, JwtSettings jwtsettings)
        {
            try
            {

                var userToken = userToken();
                if(model == null)
                {
                    throw new ArgumentNullException(nameof(model));

                }
                //Obtain Secret Key
                var Key = System.Text.Encoding.ASCII.GetBytes(jwtsettings.IssuerSigninKey);

                Guid Id;

                // Expires in 1 Day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //Validity of our Token
                userToken.Validity = expireTime.TimeOfDay;

                //Generate our JWT
                var jwtToken = new JwtSecurityToken(
                    
                    issuer : jwtsettings.ValidIssuer,
                    audience : jwtsettings.ValidAudience,
                    claims: GetClaims(model,out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Key),
                        SecurityAlgorithms.HmacSha256));

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;

                return userToken;
            
            
          
            }

            catch(Exception exception)
            {

                throw new Exception("Error Generating the JWT", exception);
            }


        }





    }
}
