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
    public class CompanyFacdeAPIController : ApiController
    {
        private static LoggedInAirlineFacade F = new LoggedInAirlineFacade();
        private static LoginToken<AirlineCompany> AirLineToken;

        private LoginToken<AirlineCompany> GetLoginToken()
        {
            AirLineToken = (LoginToken<AirlineCompany>)Request.Properties["AirLine"];
            return AirLineToken;
        }

        [Route("api/CompanyFacdeAPIController/GetAllTickets")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult GetAllTickets()
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.GetAllTickets(AirLineToken);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/GetAllFlights")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult GetAllFlights()
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.GetAllFlights(AirLineToken);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/CancelFlight")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult CancelFlight([FromBody]Flight flight)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.CancelFlight(AirLineToken, flight);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/CreateFlight")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult CreateFlight([FromBody]Flight flight)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.CreateFlight(AirLineToken, flight);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/UpdateFlight")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult UpdateFlight([FromBody]Flight flight)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.UpdateFlight(AirLineToken, flight);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/ChangeMyPassword")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult ChangeMyPassword([FromBody] string oldPassword, string newPassword)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.ChangeMyPassword(AirLineToken, AirLineToken.User , oldPassword, newPassword);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/MofidyAirlineDetails")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult MofidyAirlineDetails([FromBody]AirlineCompany airline)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.MofidyAirlineDetails(AirLineToken, airline);
                return Ok();
            }

            return NotFound();
        }
    }
}
