using AutoMate.ServiceHost;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1
{
    public class Program
    {

        private static IHubContext<SignalRHub>? _hubContext;
        static JobDetails jobDetails = new JobDetails()
        {
            Id = 11,
            UserName = "radu",
            MachineName = "Desktop",
            State = 1,
            Password = "password"
        };
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSignalR();
            builder.Services.AddControllers();
           // builder.Services.AddSingleton<IHubContext<SignalRHub>>();
            var app = builder.Build();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {

            }
            var webSocketOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            };

            app.UseWebSockets(webSocketOptions);
            app.MapHub<SignalRHub>("/orch");
            app.MapGet("/", () => "Hello World!");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Values}");

            new Thread(app.Run).Start();

            _hubContext = app.Services.GetService<IHubContext<SignalRHub>>();

            

            new Thread(SendCommands).Start();
           

             static void SendCommands(object? MachineName)
            {

                //while (true)
                //{
                    Console.WriteLine("Enter your command");
                    string command = Console.ReadLine();
                    _hubContext.Clients.All.SendAsync("ReceiveMessageServer", jobDetails);
                //_hubContext.Clients.Client.SendAsync()
                //}
            }
        }


    }

}