using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManagerBackEnd.Utils {

    public class TokenUtils {


        private static readonly SymmetricSecurityKey SecretKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ExpenseManager!SWEN325#er"));

        private static readonly SigningCredentials SigninCredentials =
            new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);


        public static string GetNewUserToken(string email, string userId) {
           
            var tokenOptions = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddDays(30),
                signingCredentials: SigninCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }


        public static string GetNewUserId() {
            return Guid.NewGuid().ToString("N");
        }
    }
}