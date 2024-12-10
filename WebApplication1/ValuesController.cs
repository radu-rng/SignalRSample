using AutoMate.ServiceHost;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static IHubContext<SignalRHub>? _hubContext;

        public ValuesController(IHubContext<SignalRHub>? hubContext)
        {
            _hubContext = hubContext;
        }
        // GET api/<ValuesController>/5
        [HttpGet]
        public string Get(int? id, string machinename, string username, string password, int? state)
        {
            JobDetails jobDetails = new JobDetails()
            {
                Id = id,
                UserName = username,
                MachineName = machinename,
                State = 1,
                Password = password
            };


            _hubContext.Clients.All.SendAsync("ReceiveMessageServer", jobDetails);

            return $"{machinename} - {username}";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
