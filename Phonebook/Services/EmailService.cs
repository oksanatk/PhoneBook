using System.Net;
using System.Net.Mail;

namespace PhoneBook.Services;

class EmailService
{
    private readonly string _smtpServer;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;

    public EmailService(string smtpServer, int port, string username, string password)
    {
        _smtpServer = smtpServer;
        _port = port;
        _username = username;
        _password = password;
    }

    public string SendEmail(string recipient, string subject, string body)
    {
        try
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_username),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
                
            };
            mail.To.Add(recipient);

            SmtpClient client = new SmtpClient(_smtpServer, _port)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = true
            };

            client.Send(mail);

            return $"Success! Email sent to {recipient}";
        }catch(Exception e)
        {
            return $"[maroon]There was an issue while trying to send the email. \nError Message: {e.Message}[/]";
        }
    }
}
