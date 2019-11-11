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

        [Route("api/CompanyFacdeAPIController/GetAllMyFlights")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult GetAllMyFlights()
        {
            if (GetLoginToken() == null)
                return Content(HttpStatusCode.Unauthorized, "User must be customer");
            if (CustomerToken != null)
            {
                F.GetAllMyFlights(CustomerToken);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/PurchaseTicket")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult PurchaseTicket(Flight flight)
        {
            GetLoginToken();
            if (CustomerToken != null)
            {
                F.PurchaseTicket(CustomerToken, flight);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/CancelTicket")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult CancelTicket(Ticket ticket)
        {
            GetLoginToken();
            if (CustomerToken != null)
            {
                F.CancelTicket(CustomerToken, ticket);
                return Ok();
            }

            return NotFound();
        }
    }
}
