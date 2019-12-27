using Flights_project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API_Web_For_Flights_Project.Controllers
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
      //  [ThreadStatic]

        public static LoginToken<Administrator> CurrentAdministrator = null;
        public static LoginToken<Customer> CurrentCustomer = null;
        public static LoginToken<AirlineCompany> CurrentAiLine = null;

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string adminpas = flightCenterConfig.ADMIN_PASSWORD;
            string adminusername = flightCenterConfig.ADMIN_NAME;
            CurrentAdministrator = new LoginToken<Administrator>() { User = new Administrator($"{adminusername}", $"{adminpas}") };
            LoggedInAdministratorFacade F = new LoggedInAdministratorFacade();

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "username & password is nulls ");
            }
           
            string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

            string decodedAuthenticationToken = Encoding.UTF8.GetString(
                Convert.FromBase64String(authenticationToken));

            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];

            CurrentCustomer = new LoginToken<Customer>() {User = F.GetCustomerByUserName(CurrentAdministrator, username) } ;
            CurrentAiLine = new LoginToken<AirlineCompany>() { User = F.GetAirLineByUserName(CurrentAdministrator, username) } ;

            FlyingCenterSystem.Login(username, password, out LoginTokenBase loginToken, out FacadeBase facadeBase );

            var uri = actionContext.Request.RequestUri.ToString();

            if (loginToken is LoginToken<Administrator>)
           {
                Thread.CurrentPrincipal = new GenericPrincipal(
                       new GenericIdentity(username), null);
                actionContext.Request.Properties["Admin"] = CurrentAdministrator;

                if (!uri.ToUpper().Contains("AdministratorFacdeAPIController".ToUpper()) &&
                    !uri.ToUpper().Contains("AnonymousFacadeApi".ToUpper()))  
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
           else if (loginToken is LoginToken<AirlineCompany>)
           {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(username), null);
                actionContext.Request.Properties["AirLine"] = CurrentAiLine;

                if (!uri.ToUpper().Contains("CompanyFacdeAPIController".ToUpper()) &&
                    !uri.ToUpper().Contains("AnonymousFacadeApi".ToUpper()))
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
           else if (loginToken is LoginToken<Customer>)
           {
                Thread.CurrentPrincipal = new GenericPrincipal(
                 new GenericIdentity(username), null);
                actionContext.Request.Properties["Customer"] = CurrentCustomer;

                if (!uri.ToUpper().Contains("CustomerFacadeApiController".ToUpper()) &&
                   !uri.ToUpper().Contains("AnonymousFacadeApi".ToUpper()))
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }    
        }
    }
}