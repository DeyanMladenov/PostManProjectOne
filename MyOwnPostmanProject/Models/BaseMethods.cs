using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MyOwnPostmanProject.Models
{
    public partial class BaseMethods
    {
        public IRestRequest CreateUserRequest(string endPoint, Users createNewUser)
        {
            var request = new RestRequest(endPoint, Method.POST);
            request.AddParameter("application/json", createNewUser.ToJson(), ParameterType.RequestBody);
            return request;
        }

        public IRestRequest CreateHouseholdRequest(string endPoint, Household createNewHousehold)
        {
            var request = new RestRequest(endPoint, Method.POST);
            request.AddParameter("application/json", createNewHousehold.ToJson(), ParameterType.RequestBody);
            return request;
        }
       
        public IRestRequest MakeSimpleGetRequest(string endPoint)
        {
            var request = new RestRequest($"{endPoint}", Method.GET);
            return request;
        }
        
        



    }
}
