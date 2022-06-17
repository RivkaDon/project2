using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private IInvitationService invitationService;
        public Boolean invited = false;

        public InvitationsController(IInvitationService iService)
        {
            invitationService = iService;
        }

        /// <summary>
        /// Sends an invitation to join a new chat (creates chat -> and then creates contact).
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestOfNewInvitation request)
        {
            if (request == null) return;
            int num = invitationService.invite(request.From, request.To, request.Server);
            if (num == 0) invited = true;
        }
    }
}
