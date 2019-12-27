using Flights_project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace API_Web_For_Flights_Project.Controllers
{
    [BasicAuthentication]
    public class CustomerFacadeApiController : ApiController
    {
        private static LoggedInCustomerFacade F = new LoggedInCustomerFacade();
        private static LoginToken<Customer> CustomerToken;

        private LoginToken<Customer> GetLoginToken()
        {
            if (Request.Properties["Customer"] == null || Request.Properties["Customer"].GetType() != typeof(LoginToken<Customer>))
                return null;
            CustomerToken = (LoginToken<Customer>)Request.Properties["Customer"];
            return CustomerToken;
        }

        [Route("api/CustomerFacadeApiController/GetAllMyFlights")]
        [ResponseType(typeof(List<Flight>))]
        [HttpGet]
        public IHttpActionResult GetAllMyFlights()
        {
            if (GetLoginToken() == null)
                return Content(HttpStatusCode.Unauthorized, "User must be customer");
            if (CustomerToken != null)
            {
                List<Flight> myFLights  = F.GetAllMyFlights(CustomerToken).ToList();
                return Ok(myFLights);
            }

            return NotFound();
        }

        [Route("api/CustomerFacadeApiController/PurchaseTicket/{id}")]
        [HttpPost]
        public IHttpActionResult PurchaseTicket([FromUri]int id)
        {
            GetLoginToken();
            if (CustomerToken != null)
            {
                Flight flight = F.GetFlightById(id);
                F.PurchaseTicket(CustomerToken, flight);
                return Ok($"{CustomerToken.User.LastName} {CustomerToken.User.FirstName} purchased ticket to flight:{flight.ID}");
            }

            return NotFound();
        }

        [Route("api/CustomerFacadeApiController/CancelTicket/{id}")]
        [HttpDelete]
        public IHttpActionResult CancelTicket([FromUri] int id)
        {
            GetLoginToken();
            if (CustomerToken != null)
            {
                Ticket T = F.GetAllMyTickets(CustomerToken).ToList().Find(c => c.ID == id);
                F.CancelTicket(CustomerToken, T);
                return Ok($"{CustomerToken.User.LastName} {CustomerToken.User.FirstName} canceled ticket No':{T.ID}");
            }

            return NotFound();
        }
    }
}
