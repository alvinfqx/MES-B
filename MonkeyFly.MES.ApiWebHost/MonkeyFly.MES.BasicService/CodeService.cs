using System;

namespace MonkeyFly.MES.BasicService
{
    public class CodeService
    {
        //100-199：表示记录系统内部状态变化
        //200-299：表示请求、验证等成功信息
        //300-299：表示一般请求的操作信息
        //400-499：表示请求、验证等失败信息
        //500-599：表示内部服务器错误
        //900-999：表示不可预测信息

        //100-199：表示记录系统内部状态变化
        public static String C100 = "系统初始化成功！";

        //200-299：表示请求、验证等成功信息
        public static String C200 = "恭喜您，登录授权成功！";

        //300-299：表示一般请求的操作信息
        public static String C300 = "用户发起Ajax访问";

        //400-499：表示请求、验证等失败信息
        public static String C401 = "用户名或密码不能为空！";
        public static String C402 = "用户名或密码不正确！";
        public static String C403 = "您的Token已过期，请重新获取！";
        public static String C404 = "您未通过授权访问，请确认用户名密码，并登录系统！";

        //500-599：表示内部服务器错误
        public static String C500 = "数据库请求错，请速联系管理员，谢谢！";

        //900-999：表示不可预测信息
        public static String C900 = "错误未捕获，请速联系管理，谢谢！";
    }
}
