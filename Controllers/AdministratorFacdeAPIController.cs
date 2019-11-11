using Flights_project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;

namespace API_Web_For_Flights_Project.Controllers
{
    [BasicAuthentication]
    public class AdministratorFacdeAPIController : ApiController
    {
        private static List<Flight> AllFlights = new List<Flight>();
        private static LoggedInAdministratorFacade F = new LoggedInAdministratorFacade();
        private static LoginToken<Administrator> AdminToken;

        private LoginToken<Administrator> GetLoginToken()
        {
            // AdminToken.User = (Administrator)Request.Properties["Admin"];
            AdminToken =(LoginToken<Administrator>) Request.Properties["Admin"];
            return AdminToken;
        }

        [Route("api/AdministratorFacdeAPIController/CreateNewAirline")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult CreateNewAirline([FromBody] AirlineCompany airline)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                F.CreateNewAirline(AdminToken, airline);
                return Ok();
            }

                return NotFound();
        }

        [Route("api/AdministratorFacdeAPIController/UpdateAirlineDetails")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult UpdateAirlineDetails([FromBody] AirlineCompany airline)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                airline.ID = F.GetAirLineByUserName(AdminToken, airline.UserName).ID;
                F.UpdateAirlineDetails(AdminToken, airline);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/AdministratorFacdeAPIController/RemoveAirline")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult RemoveAirline([FromBody] AirlineCompany airline)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                F.RemoveAirline(AdminToken, airline);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/AdministratorFacdeAPIController/CreateNewCustomer")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult CreateNewCustomer([FromBody] Customer customer)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                F.CreateNewCustomer(AdminToken, customer);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/AdministratorFacdeAPIController/UpdateCustomerDetails")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult UpdateCustomerDetails([FromBody] Customer customer)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                customer.ID = F.GetCustomerByName(AdminToken, customer.FirstName).ID;
                F.UpdateCustomerDetails(AdminToken, customer);
                return Ok();
            }

            return NotFound();
        }

        [Route("api/AdministratorFacdeAPIController/RemoveCustomer")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult RemoveCustomer([FromBody] Customer customer)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                customer.ID = F.GetCustomerByName(AdminToken, customer.FirstName).ID;
                F.RemoveCustomer(AdminToken, customer);
                return Ok();
            }

            return NotFound();
        }
    }
}
