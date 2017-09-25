using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class StuUser
    {
        /// <summary>
        // 获取用户名
        /// </summary>
        /// <returns></returns>
        public StudentUser GetUserNameForSerVice( string Josn )
        {
            StudentUser user = new StudentUser();
            user = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentUser>( Josn );

            return user;
        }
        [Serializable]
        public partial class StudentUser
        {
            ///学生学号
            public string Code { get; set; }
            ///学生姓名
            public string Name { get; set; }
            //学生性别
            public string SexText { get; set; }
            //班级代码
            public string ClassCode { get; set; }
            //所在班级
            public string ClassName { get; set; }
            //照片路径
            public string PhotoUrl { get; set; }
            //联系电话
            public string Telephone { get; set; }
            //IC卡唯一码
            public string CustomerCardSN { get; set; }
            //餐厅卡号
            public string DiningCardCode { get; set; }
            //返回状态ID
            public string StatusID { get; set; }
            //返回状态说明
            public string StatusText { get; set; }

        }
    }
}
