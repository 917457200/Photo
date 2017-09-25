using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class TeUser
    {
        /// <summary>
        // 获取用户名
        /// </summary>
        /// <returns></returns>
        public User GetUserNameForSerVice( string Josn )
        {
            Newtonsoft.Json.Linq.JObject LoginUser = (Newtonsoft.Json.Linq.JObject) Newtonsoft.Json.JsonConvert.DeserializeObject<object>( Josn );
            User user = new User();
            if( LoginUser != null )
            {
                string U = LoginUser["data"]["result"].ToString().Replace( "[", "" ).Replace( "]", "" );
                user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>( U );
            }
            return user;
        }
        [Serializable]
        public partial class User
        {
            public string userCode { get; set; }
            public string userType { get; set; }
            public string userName { get; set; }
            public string roleCode { get; set; }
            public string unitCode { get; set; }
            public string unitName { get; set; }
            public string schoolCode { get; set; }
            public string rootCode { get; set; }
            public string rootName { get; set; }
            public string rootType { get; set; }
        }
    }
}
