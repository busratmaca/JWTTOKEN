using System;
namespace JWTTOKEN
{
	public class JWTSettings
	{
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public double Expire { get; set; }
    }
}

