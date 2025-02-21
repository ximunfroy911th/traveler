using FirebaseAdmin.Messaging;

namespace Traveler.Services;

public class MessagingService: IMessagingService
{
    public async Task SendMessage(string token)
    {
        var message = new Message()
        {
            Data = new Dictionary<string, string>()
            {
                { "score", "850" },
                { "time", "2:45" },
            },
            Token = token,
        };
        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        Console.WriteLine("Successfully sent message: " + response);
    }
}

interface IMessagingService
{
    Task SendMessage(string token);
}