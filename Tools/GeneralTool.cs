using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EAromaWebUI.Tools
{
    public class GeneralTool
    {
        public static string URLConvert(string _text)
        {
            return _text.ToLower().Replace(" ", "-").Replace("ö", "o").Replace("ü", "u").Replace("ç", "ç").Replace("ı", "i").Replace("ğ", "g").Replace("ş", "s");
        }


        public static string SendMail(string mailSunucusu, int mailSunucuPortu, string mailAdresi, string mailsifresi, bool sslVarmi, string kimden, string[] kimlere, string konu, string mailIcerigi)
        {
            string rtn = "OK";
            SmtpClient smtpClient = new SmtpClient(mailSunucusu, mailSunucuPortu);
            smtpClient.Credentials = new NetworkCredential(mailAdresi, mailsifresi);
            smtpClient.EnableSsl = sslVarmi;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(kimden);
            foreach (string kim in kimlere) mailMessage.To.Add(kim);
            mailMessage.Subject = konu;
            mailMessage.Body = mailIcerigi;
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
            return rtn;
        }


        public static string getMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
