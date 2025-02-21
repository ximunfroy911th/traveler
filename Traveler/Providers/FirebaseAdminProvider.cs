using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Traveler.Providers;

public class FirebaseAdminProvider(IServiceProvider serviceProvider) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("project_id-firebase-adminsdk-hash.json"),
            });
            Console.WriteLine(defaultApp.Name);
            Console.WriteLine("Initialized Firebase Admin Provider.");
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
