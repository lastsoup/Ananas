using System;
using System.Net.Mail;

namespace Ananas.Web.Mvc.Extensions
{
    /// <summary>
    /// Emial收发帮助类
    /// </summary>
    public class EmailHelper
    {

        #region 发送邮件
        public static bool SendSuccess()
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new MailAddress("qingtang166@163.com", "someone");//必须是提供smtp服务的邮件服务器 
            message.To.Add(new MailAddress("qingtang166@qq.com"));
            message.Subject = "测试邮件";
            message.CC.Add(new MailAddress("qingtang166@qq.com"));
            message.Bcc.Add(new MailAddress("qingtang166@qq.com"));
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = "邮件发送测试";
            message.Priority = System.Net.Mail.MailPriority.High;
            SmtpClient client = new SmtpClient("smtp.163.com", 25); // 587;//Gmail使用的端口 
            client.Credentials = new System.Net.NetworkCredential("qingtang166@163.com", "cqy2008813304"); //这里是申请的邮箱和密码 
            client.EnableSsl = true; //必须经过ssl加密 
            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return false;
            }
        }

        public static bool SendSuccess(string tomail,string user,string code)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new MailAddress("qingtang166@163.com", "菠萝工作室");//必须是提供smtp服务的邮件服务器 
            message.To.Add(new MailAddress(tomail));
            message.Subject = "邮件验证码";
            message.CC.Add(new MailAddress(tomail));
            message.Bcc.Add(new MailAddress(tomail));
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = "亲爱的" + user + ":您的密码找回的验证码为"+code;
            message.Priority = System.Net.Mail.MailPriority.High;
            SmtpClient client = new SmtpClient("smtp.163.com", 25); // 587;//Gmail使用的端口 
            client.Credentials = new System.Net.NetworkCredential("qingtang166@163.com", "cqy2008813304"); //这里是申请的邮箱和密码 
            client.EnableSsl = true; //必须经过ssl加密 
            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return false;
            }
        }
        #endregion 

    }
}
