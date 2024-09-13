using System.Net.Mail;
using System.Net;
using EmailServices;

public class Program
{
    const string IMAGE_URL = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Jenkins_logo_with_title.svg/1280px-Jenkins_logo_with_title.svg.png";
    public static void CommitEmail(Commit commit)
    {
        var toEmail = "kpachac@ulasalle.edu.pe";
        var displayName = "Karlo";

        var fromAddress = new MailAddress("daoblur.business@gmail.com", "Jenkins.Demo");
        var toAddress = new MailAddress(toEmail, displayName);

        const string fromPassword = "kdntpikwwxpysmhx";
        const string subject = "New commit";

        string htmlContent = string.Empty;
        htmlContent += "<html> <body>";

        htmlContent += "<div style='margin: 20px auto; max-width: 600px; padding: 20px; border: 1px solid #ccc; background-color: #FFFFFF; font-family: Arial, sans-serif;'>";
        htmlContent += "<div style='margin-bottom: 20px; text-align: center;'>";
        htmlContent += $"<img src='{ IMAGE_URL}' alt='Blasterify Logo' style='max-width: 300px; margin-bottom: 10px;' >";
        htmlContent += "</div>";

        htmlContent += "<div style='text-align: center; margin-bottom: 20px;'><h1>Commit Details</h1></div>";

        htmlContent += "<table style='width: 100%; border-collapse: collapse;'>";
        /*
        htmlContent += "<thead><tr>";
        htmlContent += "<th style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>Movies</th>";
        htmlContent += "<th style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>Price</th>";
        htmlContent += "</tr><hr/></thead>";
        */
        htmlContent += "<tbody>";

        // Commit Id
        htmlContent += "<tr>";
        htmlContent += $"<td style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>Id:</td>";
        htmlContent += $"<td style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>{ commit.Id }</td>";
        htmlContent += "</tr>";

        // Commit Author
        htmlContent += "<tr>";
        htmlContent += $"<td style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>Author:</td>";
        htmlContent += $"<td style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>{ commit.Author }</td>";
        htmlContent += "</tr>";

        // Commit Message
        htmlContent += "<tr>";
        htmlContent += $"<td style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>Message:</td>";
        htmlContent += $"<td style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>{ commit.Message }</td>";
        htmlContent += "</tr>";

        // Commit Date
        htmlContent += "<tr>";
        htmlContent += $"<td style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>Date:</td>";
        htmlContent += $"<td style='padding: 8px; text-align: left; border-bottom: 1px solid #ddd;'>{ commit.Date }</td>";
        htmlContent += "</tr>";

        htmlContent += "</tbody>";

        htmlContent += $"</table></div>";

        htmlContent += "</body> </html>";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        var result = string.Empty;

        using var stream = new MemoryStream();
        {
            result = Convert.ToBase64String(stream.ToArray());
            stream.Position = 0;
        }

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = htmlContent,
            IsBodyHtml = true,
        };

        try
        {
            if (message != null && smtp != null)
            {
                smtp.Send(message!);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending email: {e.Message}");
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("------------------------------------");

        if(args.Length > 0)
        {
            var stringDate = string.Empty;

            for (int i = 2; i < 8; i++)
            {
                stringDate += args[i] + " ";
            }

            var message = string.Empty;

            for (int i = 8; i < args.Length; i++)
            {
                message += args[i] + " ";
            }

            var commit = new Commit
            {
                Id = args[0],
                Author = args[1],
                Date = stringDate,
                Message = message
            };

            CommitEmail(commit);
        }

        Console.WriteLine("------------------------------------");
    }
}