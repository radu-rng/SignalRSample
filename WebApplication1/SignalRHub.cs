using Microsoft.AspNetCore.SignalR;

namespace AutoMate.ServiceHost
{
    public class SignalRHub : Hub
    {


        public async Task ProcessMessageFromCLient(JobSession command)
        {
            await Clients.All.SendAsync("ReceiveMessage", command);
            Console.WriteLine(command);
        }

    }

    public class JobSession
    {
        public Guid Id { get => Guid.NewGuid(); }
        public string RdpSession { get; set; }
        public string PipeServer { get; set; }
        public JobDetails JobDetails { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public JobStatus State { get; set; }
    }

    public class JobDetails
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MachineName { get; set; }
    }

    public enum JobStatus
    {
        Pending = 0,
        Running = 1,
        Stopping = 2,
        Terminating = 3,
        Faulted = 4,
        Successful = 5,
        Stoped = 6,
        Suspended = 7,
        Resumed = 8
    }
}
