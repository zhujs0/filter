using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Jose;
using Newtonsoft.Json.Linq;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using JWTService.Attribute;

namespace JWTService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //生成token
            //var identity = new ClaimsIdentity("JWT");
            //identity.AddClaim(new Claim(ClaimTypes.Name,"朱境生"));
            //identity.AddClaim(new Claim("sub", "朱境生"));
            //identity.AddClaim(new Claim("subId", "23517"));
            //identity.AddClaim(new Claim(ClaimTypes.Role, "AppClient"));

            //var props = new AuthenticationProperties(new Dictionary<string, string>
            //{
            //    {"audience","ab7ce2d75193492f9be8fd10ae3e32ff" },
            //    {"userName","朱境生" },
            //    {"userId","23517"}
            //});
            //var ticket = new AuthenticationTicket(identity, props);
            TimeSpan ts = DateTime.UtcNow- new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var payload = new Dictionary<string, object>()
            {
                { "sub", "mr.x@contoso.com" },
                { "exp",  Convert.ToInt64(ts.TotalSeconds).ToString()},
                { "subId",""},
                { "iss","GuanHua"},
                { "aud","ab7ce2d75193492f9be8fd10ae3e32ff"},
                {"user","123" },
                {"name","zhu" },
                {"password","11111" }
            };

            string strKey = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("secert"));
            var key = System.Text.Encoding.UTF8.GetBytes(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("secert")));

            var secretKey = Base64Url.Decode("secertKey123123123123123");

            //var secretKey = System.Text.Encoding.UTF8.GetBytes(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("secert")));

            //secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
            //string aa = "O1eLcx5Re1Y3nrLwqwonYaiHnsg7KZWvBvjiTnTDy4A";
           // byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(aa);


            string token = Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
            

            return new string[] { "value1", token };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string token)
        {
            string json = JWT.Decode(token, Base64Url.Decode("secert"),JwsAlgorithm.HS256);

            var headers = Jose.JWT.Headers(token);
            var payload = Jose.JWT.Payload(token);

            //step 1b: lookup validation key based on header info

            JObject Robj = JObject.Parse(json);
            var aa = Robj["sub"];
            //ActionExecutingContext
            return "value"; 
        }

        // POST api/values
        [HttpPost]
        //[Authorize]
        [Identity (AuthId="123")]
        public string Post()
        {
           // List<System.Security.Claims.Claim> claims =new System.Security.Claims

            var Claims = HttpContext.User.Claims.ToList()[0];
            string Name = HttpContext.User.Identity.Name;   
            return "1";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
