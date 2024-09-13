using System.Net.Mail;
using System.Net;

public class Program
{
    public static void CommitEmail(string toEmail, string displayName)
    {
        var fromAddress = new MailAddress("daoblur.business@gmail.com", "Jenkins.Demo");
        var toAddress = new MailAddress(toEmail, displayName);

        const string fromPassword = "kdntpikwwxpysmhx";
        const string subject = "Welcome";
        string body = $"New Commit {displayName}";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };
        smtp.Send(message);
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("------------------------------------");

        if(args.Length > 0)
        {
            foreach(var arg in args)
            {
                Console.WriteLine($"- { arg }");
            }

            CommitEmail(args[1], args[2]);
        }

        Console.WriteLine("------------------------------------");
    }
}