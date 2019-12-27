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
        [HttpGet]
        public IHttpActionResult GetAllTickets()
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
               List<Ticket> tickets =  F.GetAllTickets(AirLineToken).ToList();
                return Ok(tickets);
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/GetAllFlights")]
        [ResponseType(typeof(List<Flight>))]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                List<Flight> flights = F.GetAllFlights(AirLineToken).ToList();
                return Ok(flights);
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/CancelFlight/{id}")]
        [ResponseType(typeof(List<Flight>))]
        [HttpDelete]
        public IHttpActionResult CancelFlight([FromUri] int id)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                Flight flight = F.GetFlightById(id);
                F.CancelFlight(AirLineToken, flight);
                return Ok($"Flight:{flight.ID} airline company:{flight.AirLineCompanyID} has been canceled!");
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
                return Ok($"{flight.ID} has been created");
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/UpdateFlight")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPut]
        public IHttpActionResult UpdateFlight([FromBody]Flight flight)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.UpdateFlight(AirLineToken, flight);
                return Ok($"{flight.ID} has been Update!");
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/ChangeMyPassword/{newPassword}")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPut]
        public IHttpActionResult ChangeMyPassword([FromUri] string newPassword)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.ChangeMyPassword(AirLineToken, AirLineToken.User , AirLineToken.User.Password, newPassword);
                return Ok($"airline company:{AirLineToken.User.AirlineName}. password has been changed");
            }

            return NotFound();
        }

        [Route("api/CompanyFacdeAPIController/MofidyAirlineDetails")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPut]
        public IHttpActionResult MofidyAirlineDetails([FromBody]AirlineCompany airline)
        {
            GetLoginToken();
            if (AirLineToken != null)
            {
                F.MofidyAirlineDetails(AirLineToken, airline);
                return Ok($"airline company:{AirLineToken.User.AirlineName} details has changed");
            }

            return NotFound();
        }
    }
}
