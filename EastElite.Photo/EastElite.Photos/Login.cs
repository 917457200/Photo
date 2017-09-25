using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EastElite.Photos
{
    public partial class Login : Form
    {
        private Form1 anotherForm;
        private  Bll.TeUser User =new Bll.TeUser();

        public Login()
        {
            InitializeComponent();
           
        }
       
        private void button1_Click( object sender, EventArgs e )
        {
            //登录接口加载
            DeviceScan.EastEliteSMSWS.EastEliteSMSWSSoapClient Service = new DeviceScan.EastEliteSMSWS.EastEliteSMSWSSoapClient();

            //登录错误信息
            string err = "";
            //加载数据
            string UserCode = textBox1.Text;
            string Password = textBox2.Text;
            byte userType = 1;
            //验证
            if( string.IsNullOrEmpty( UserCode ) )
            {
                err = "姓名或代码不能为空！";
                MessageBox.Show(err);
                return ;
            }
            if( string.IsNullOrEmpty( Password ) )
            {
                err = "密码不能为空！";
                MessageBox.Show(err);
                return;
            }

            string result = Service.CheckUserLoginPhotoItem( UserCode, userType, Password );//访问接口验证登录
            ///登录失败
            if( result.IndexOf( "FAIL" ) > -1 )
            {
                int Start = result.IndexOf( "FAIL" ) + 9;
                int End = result.IndexOf( "!" ) + 1;
                err = result.Substring( Start, End - Start );
                MessageBox.Show( err );
                return ;
            }
            else if( result.IndexOf( "SUCC" ) > -1 )//登录成功
            {
                Bll.TeUser.User U = User.GetUserNameForSerVice( result );
                anotherForm = new Form1( U.userCode,U.schoolCode);
                this.Hide();
                anotherForm.ShowDialog();
                Application.ExitThread();   
            }
            else
            {
                //非法登录
                MessageBox.Show( err );
                return ;
            }
        }

        private void Close_Click( object sender, EventArgs e )
        {
            System.Environment.Exit( 0 ); 
        }
        private Point mPoint = new Point();
        private void Login_MouseDown( object sender, MouseEventArgs e )
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;
        }

        private void Login_MouseMove( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset( -mPoint.X, -mPoint.Y );
                Location = myPosittion;
            }
        }
      
    }
}
