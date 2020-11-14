using MyOwnPostmanProject.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnPostmanProject
{
    public class Tests : BaseMethods
    {
        //RestSharp
        private RestClient _restClient;
        private RestRequest _restRequest;
        private RestResponse _restResponse;
        private Household _household;
        private Users _user;
        private Book _book;
        private List<Book> _books;


        //private readonly Dictionary<string, string> _headers = new Dictionary<string, string>()
        //{
        //    { "G-Token", "ROM831ESV" },
        //    { "Content-Type","application/json" }
        //};

        [SetUp]
        public void Setup()
        {
            _restClient = new RestClient();
            _restClient.BaseUrl = new Uri("http://localhost:3000");
            _restClient.AddDefaultHeader("G-Token", "ROM831ESV");
            //_restClient.AddDefaultQueryParameter("author", "John Piper");
            _household = new Household();
            _user = new Users();
            _book = new Book();
            _books = new List<Book>();


        }

        [Test]
        public void GetAllBooks()
        {
            var request = new RestRequest("/books", Method.GET);
            var response = _restClient.Execute(request);

            Assert.IsTrue(response.IsSuccessful);

            if (response != null)
            {
                var content = JsonConvert.DeserializeObject<List<Book>>(response.Content);
            }

        }

        [Test]
        public void ChekingForCorrectTitle()
        {

            var request = new RestRequest("/books/6", Method.GET);
            var response = _restClient.Execute(request);

            Assert.IsTrue(response.IsSuccessful);

            if (response != null && (response.StatusCode == System.Net.HttpStatusCode.OK))
            {
                var title = Book.FromJson(response.Content);

                Assert.AreEqual("New Begining", title.Title);
            }

        }
        [Test]
        public void PostBooks()
        {
            var book = new Book() { Title = "I'm next QA hacker" };
            var request = new RestRequest("/books", Method.POST);
            request.AddParameter("application/json", book.ToJson(), ParameterType.RequestBody);

            IRestResponse response = _restClient.Execute(request);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("I'm next QA hacker", book.Title);

        }
        [Test]
        public void GetAllUsers()
        {
            var request = new RestRequest("/users", Method.GET);

            var response = _restClient.Execute(request);
            var user = JsonConvert.DeserializeObject<List<Users>>(response.Content);
            Assert.IsTrue(response.IsSuccessful);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
        [Test]
        public void ChekingForCorrectUserName()
        {

            var request = new RestRequest("/users/2", Method.GET);

            var response = _restClient.Execute(request);
            var users = Users.FromJson(response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("MyNameIsPesho", users.FirstName);
        }
        [Test]
        public void ChekingForCorrectUser()
        {
            var book = new Book();
            var request = new RestRequest("/users/3", Method.GET);
            var response = _restClient.Execute(request);
            request.AddParameter("application/json", book.ToJson(), ParameterType.RequestBody);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("MyNameIsGosho", _user.FirstName);

        }
        [Test]
        public void ChekingForCorrectSearch()
        {

            var book = new Book() { Author = "John Piper" };
            var request = new RestRequest("/books/search", Method.GET);
            var response = _restClient.Execute(request);

            Assert.IsTrue(response.IsSuccessful);

            Assert.AreEqual("John Piper", book.Author);

        }
        [Test]
        public void CreateNewHousehold()
        {
            var household = new Household() { Name = "Pachkata" };
            var request = CreateHouseholdRequest("/households", household);
            var response = _restClient.Execute(request);
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("Pachkata", household.Name);
        }

        [Test]
        public void AddTwoDifferentUsers()
        {
            var householdID = _household.Id;

            var userOne = new Users()
            {
                Email = "naturalniq@gmail.com",
                FirstName = "Naturalniq",
                LastName = "bodybuilder",
                HouseholdId = 42
            };

            var userTwo = new Users()
            {
                Email = "nazobeniqA@gmail.com",
                FirstName = "Nazobeniq",
                LastName = "bodybuilder",
                HouseholdId = 42
            };

            var createFirstUser = CreateUserRequest("/users", userOne);
            var firstResponse = _restClient.Execute(createFirstUser);

            var createSecondtUser = CreateUserRequest("/users", userTwo);
            var secondResponse = _restClient.Execute(createSecondtUser);
            
            if (firstResponse != null && secondResponse != null && firstResponse.StatusCode == System.Net.HttpStatusCode.OK && secondResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Assert.AreEqual("Naturalniq", userOne.FirstName);
                Assert.AreEqual("Nazobeniq", userTwo.FirstName);
            }
        }
        [Test]
        public void AddTwoBookForEachUser()
        {
            int userOneWishlish = 69;
            int userTwoWishlish = 69;

            for (int i = 1; i < 3; i++)
            {
                var request = new RestRequest($"/wishlists/{userOneWishlish}/books/{i}", Method.POST);
                var response = _restClient.Execute(request);
                Assert.IsTrue(response.IsSuccessful);
            }
            for (int i = 1; i < 3; i++)
            {
                var request = new RestRequest($"/wishlists/{userTwoWishlish}/books/{i}", Method.POST);
                var response = _restClient.Execute(request);
                Assert.IsTrue(response.IsSuccessful);
            }
        }


        [Test]
        public void GetWishistForHousehold()
        {
            var request = MakeSimpleGetRequest($"/households/70/wishlistBooks");
            var response = _restClient.Execute(request);
            Assert.IsTrue(response.IsSuccessful);
        }
        [Test]
        public void Delete(int id)
        {
            var request = new RestRequest($"/books/{2}",Method.DELETE);
            var response = _restClient.Execute(request);


        }
        










































    }

}