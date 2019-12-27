using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Flights_project;

namespace API_Web_For_Flights_Project.Controllers
{
    // api/AnonymousFacadeApi
    public class AnonymousFacadeApiController : ApiController
    {
        private static List<Flight> AllFlights = new List<Flight>();
        private static List<AirlineCompany> AllCompanies = new List<AirlineCompany>();
        Dictionary<Flight, int> FlightsVacanc = new Dictionary<Flight, int>();
        private static AnonymousUserFacade F = new AnonymousUserFacade();

        /// <summary>
        /// get all flights 
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(List<Flight>))]
        [Route("api/AnonymousFacadeApi/GetAllFlights")]
        [HttpGet]
        public IHttpActionResult GetAllFlights()
        {
            AllFlights = F.GetAllFlights().ToList();

            if (AllFlights.Count == 0)
            {
                return NotFound();
            }
            return Ok(AllFlights);
        }

        /// <summary>
        /// get all Airline Companies 
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(List<AirlineCompany>))]
        [Route("api/AnonymousFacadeApi/GetAllAirlineCompanies")]
        [HttpGet]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            AllCompanies = F.GetAllAirlineCompanies().ToList();

            if (AllCompanies.Count == 0)
            {
                return NotFound();
            }
            return Ok(AllCompanies);
        }

        /// <summary>
        /// Gets All Flights Vacancy into a Dictionary
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Dictionary<Flight, int>))]
        [Route("api/AnonymousFacadeApi/GetAllFlightsVacancy")]
        [HttpGet]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            FlightsVacanc = F.GetAllFlightsVacancy();

            if (FlightsVacanc.Count == 0)
            {
                return NotFound();
            }
            return Ok(FlightsVacanc);
        }

        /// <summary>
        /// Get Flight By Id - using Path Parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Flight))]
        // do i need to declare of the Route here? or wold it work on the id without it?
        [Route("api/AnonymousFacadeApi/GetFlightById/{id}", Name = "GeById")]
        [HttpGet]
        public IHttpActionResult GetFlightById([FromUri] int id)
        {
            AllFlights = F.GetAllFlights().ToList();
            Flight flight = AllFlights.FirstOrDefault(f => f.ID == id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        /// <summary>
        /// Get List of Flights By Origin Country code - using Path Parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(List<Flight>))]
        [Route("api/AnonymousFacadeApi/GetFlightsByOriginCountry/{country}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByOriginCountry([FromUri] int country)
        {
            List<Flight> flightsByOC = new List<Flight>();
            AllFlights = F.GetAllFlights().ToList();

            for (int i = 0; i < AllFlights.Count; i++)
            {
                if (AllFlights[i].OriginCountryCode == country)
                    flightsByOC.Add(AllFlights[i]);
            }

            if (flightsByOC == null)
            {
                return NotFound();
            }
            return Ok(flightsByOC);
        }

        /// <summary>
        /// Get List of Flights By Destination Country code - using Path Parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(List<Flight>))]
        [Route("api/AnonymousFacadeApi/GetFlightsByDestinationCountry/{country}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByDestinationCountry([FromUri] int country)
        {
            List<Flight> flightsByDC = new List<Flight>();
            AllFlights = F.GetAllFlights().ToList();

            for (int i = 0; i < AllFlights.Count; i++)
            {
                if (AllFlights[i].DestinationCountryCode == country)
                    flightsByDC.Add(AllFlights[i]);
            }

            if (flightsByDC == null)
            {
                return NotFound();
            }
            return Ok(flightsByDC);
        }

        /// <summary>
        /// Get List of Flights By Depatrure Date - using Path Parameter
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [ResponseType(typeof(List<Flight>))]
        [Route("api/AnonymousFacadeApi/GetFlightsByDepatrureDate/{date}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByDepatrureDate(DateTime date)
        {
            List<Flight> flightsByDD = new List<Flight>();
            AllFlights = F.GetAllFlights().ToList();

            for (int i = 0; i < AllFlights.Count; i++)
            {
               if (AllFlights[i].DepartureTime == date)
                    flightsByDD.Add(AllFlights[i]);
            }

            if (flightsByDD == null)
            {
                return NotFound();
            }
            return Ok(flightsByDD);
        }

        /// <summary>
        /// Get List of Flights By Landing Date - using Path Parameter
        /// format:
        /// /api/AnonymousFacadeApi/GetFlightsByLandingDate/2019-1-12
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [ResponseType(typeof(List<Flight>))]
        [Route("api/AnonymousFacadeApi/GetFlightsByLandingDate/{date}")]
        [HttpGet]
        public IHttpActionResult GetFlightsByLandingDate(DateTime date)
        {
            List<Flight> flightsByLD = new List<Flight>();
            AllFlights = F.GetAllFlights().ToList();

            for (int i = 0; i < AllFlights.Count; i++)
            {
                if (AllFlights[i].LandingTime == date)
                    flightsByLD.Add(AllFlights[i]);
            }

            if (flightsByLD == null)
            {
                return NotFound();
            }
            return Ok(flightsByLD);
        }
    }
}
