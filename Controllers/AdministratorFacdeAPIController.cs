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

        [Route("api/AdministratorFacdeAPIController/CreateNewAirline/")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPost]
        public IHttpActionResult CreateNewAirline([FromBody] AirlineCompany airline)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                F.CreateNewAirline(AdminToken, airline);
                return Ok($"{ airline.UserName} has been created");
            }           
                return NotFound();
        }

        [Route("api/AdministratorFacdeAPIController/UpdateAirlineDetails/{id}")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPut]
        public IHttpActionResult UpdateAirlineDetails([FromBody] AirlineCompany airline, [FromUri] int id)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                airline.ID = F.GetAirLineByUserName(AdminToken, airline.UserName).ID;
                F.UpdateAirlineDetails(AdminToken, airline);
                return Ok($"{ airline.UserName} Details has been Update!");
            }

            return NotFound();
        }

        [Route("api/AdministratorFacdeAPIController/RemoveAirline/{id}")]
        [ResponseType(typeof(AirlineCompany))]
        [HttpDelete]
        public IHttpActionResult RemoveAirline(int id)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                AirlineCompany A = F.GetAllAirlineCompanies().ToList().Find(c => c.ID == id);
                if (A==null)
                {
                    return Content(HttpStatusCode.NotFound, $"{id} was not found");
                }
                F.RemoveAirline(AdminToken, A);
                return Ok($"{A.UserName} has been Removed");
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
                return Ok($"{ customer.UserName} has been created");
            }

            return NotFound();
        }

        [Route("api/AdministratorFacdeAPIController/UpdateCustomerDetails")]
        [ResponseType(typeof(List<Flight>))]
        [HttpPut]
        public IHttpActionResult UpdateCustomerDetails([FromBody] Customer customer)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                customer.ID = F.GetCustomerByName(AdminToken, customer.FirstName).ID;
                F.UpdateCustomerDetails(AdminToken, customer);
                return Ok($"{customer.UserName} Details has been Update");
            }

            return NotFound();
        }

     
        [Route("api/AdministratorFacdeAPIController/RemoveCustomer/{id}")]
        [ResponseType(typeof(List<Flight>))]
        [HttpDelete]
        public IHttpActionResult RemoveCustomer([FromUri]int id)
        {
            GetLoginToken();

            if (AdminToken != null)
            {
                Customer C = F.GetAllCustomers().ToList().Find(c => c.ID == id);
                if (C == null)
                {
                    return Content(HttpStatusCode.NotFound, $"{id} was not found");
                }
                //customer.ID = F.GetCustomerByName(AdminToken, customer.FirstName).ID;
                F.RemoveCustomer(AdminToken, C);
                return Ok($"{C.UserName} has been Removed");
            }

            return NotFound();
        }
    }
}
