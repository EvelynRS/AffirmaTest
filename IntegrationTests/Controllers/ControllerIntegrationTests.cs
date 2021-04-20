


using CarDealership;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.IntegrationTests;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using System;
using System.IO;

namespace IntegrationTests
{
    public class ControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>> 
    {

        //==================================================NOTICE=======================================================
        // If the data you are testing  for doesn't exist in the database, add them as needed.
        // Ensure that all of your tests are passing, and that interactions between two or more tests do not cause a fail
        //===============================================================================================================

        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public ControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            //Create a the client to handle HTTP requests.
            _client = factory.CreateClient();

            //Helpers
            _output = output;
        }

        
        [Fact]
        public async Task GetAllCars()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/Cars");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize result to be readable
            var content = await httpResponse.Content.ReadAsStringAsync();

            var readable = JsonConvert.DeserializeObject<List<CarDealership.Models.Car>>(content);
            var count = 0;
            foreach (CarDealership.Models.Car c in readable)
            {
                _output.WriteLine(c.ToString());
                count++;
            }


            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(6, count);
            // Assert that we've retrieved all cars from the database
        }


        
        // Create a car and assert that it exists in the DB
        [Fact]
        public async Task CreateNewCar()
        {
            var json = "{\"id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa8\",\"dateCreated\":\"2021-04-20T19:54:27.606Z\",\"dateModified\":\"2021-04-20T19:54:27.606Z\",\"make\":\"string\",\"model\":\"string\",\"year\":0,\"price\":0,\"horsepower\":0,\"bodyStyle\":0,\"bodyStyleName\":\"string\",\"transmissionType\":0,\"transmissionTypeName\":\"string\",\"milesPerGallon\":0}";
            //var test = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json");

            var postResponse = await _client.PostAsJsonAsync("/Cars", json);
            _output.WriteLine(postResponse.StatusCode.ToString());

            postResponse.EnsureSuccessStatusCode();

            //Assert.True(postResponse.IsSuccessStatusCode);
        }
        
        
        
        // Retrieve all values of a car by its ID and assert
        [Fact]
        public async Task RetrieveByID()
        {
            var httpResponse = await _client.GetAsync("/Cars/?e8b20a71-c309-41c3-9d3d-ee16d6914aa2");
            Assert.True(httpResponse.IsSuccessStatusCode);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var readable = JsonConvert.DeserializeObject<List<CarDealership.Models.Car>>(content);
            var count = 0;
            foreach (CarDealership.Models.Car c in readable)
            {
                _output.WriteLine(c.ToString());
                count++;
            }

            Assert.Equal(1, count);

        }
        
        

        //TODO: Update (patch) the model of a specific car by its Id and assert that the update was successful

        //TODO: Update (put) all properties of a single car by its Id and assert that the changes were successful
        // Updates Corrola
        // 400 Error... Needs to take DTO?
        [Fact]
        public async Task updateCar()
        {
            var json = "{\"id\":\"d06c4430-5e90-47c2-bf84-10344d5b3a41\",\"dateCreated\":\"2021-04-20T19:59:51.384Z\",\"dateModified\":\"2021-04-20T19:59:51.384Z\",\"make\":\"Chevy\",\"model\":\"string\",\"year\":0,\"price\":0,\"horsepower\":0,\"bodyStyle\":0,\"bodyStyleName\":\"string\",\"transmissionType\":0,\"transmissionTypeName\":\"string\",\"milesPerGallon\":0}";
            //json = JsonConvert.SerializeObject(json);
            CarDealership.DTOs.CarDTO cardto = new CarDealership.DTOs.CarDTO
            {
                Make = "Chevy",
                Model = "string",
                Year = 0,
                Price = 0,
                Horsepower = 0,
                BodyStyle = 0,
                BodyStyleName = "string",
                TransmissionType = 0,
                TransmissionTypeName = "string",
                MilesPerGallon = 0
            };
            //byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(json);
            //var content = new ByteArrayContent(messageBytes);

            var putResponse = await _client.PutAsJsonAsync("/Cars", json);
            putResponse.EnsureSuccessStatusCode();
        }

        //TODO: Delete a car and assert that it was deleted
        // Deletes F150
        // 405 error method not allowed
        [Fact]
        public async Task deleteCar()
        {
            var Response = await _client.DeleteAsync("/Cars/?81641855-648f-4343-a28d-b944aa42e083");
            _output.WriteLine(Response.StatusCode.ToString());
            Response.EnsureSuccessStatusCode();


            // Checks for deletion
            var httpResponse = await _client.GetAsync("/Cars");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize result to be readable
            var content = await httpResponse.Content.ReadAsStringAsync();

            var readable = JsonConvert.DeserializeObject<List<CarDealership.Models.Car>>(content);
            var count = 0;
            foreach (CarDealership.Models.Car c in readable)
            {
                _output.WriteLine(c.ToString());
                count++;
            }


            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(5, count);

        }


        // Get all cars with MilePerGallon in between 150 to 300 and assert that each fall within this range
        [Fact]
        public async Task getMPG()
        {
            var Response = await _client.GetAsync("/Cars/getmpgrange/150/300");
            Response.EnsureSuccessStatusCode();

            var content = await Response.Content.ReadAsStringAsync();

            var readable = JsonConvert.DeserializeObject<List<CarDealership.Models.Car>>(content);
            var count = 0;
            foreach (CarDealership.Models.Car c in readable)
            {
                _output.WriteLine(c.ToString());
                count++;
            }

            Assert.Equal(0, count);
        }

        //TODO: Get all cars that are of a specific body style ex: coupe, minivan, etc.
        [Fact]
        public async Task getBody()
        {
            var Response = await _client.GetAsync("/Cars/getbybody/Coupe");
            Response.EnsureSuccessStatusCode();

            var content = await Response.Content.ReadAsStringAsync();

            var readable = JsonConvert.DeserializeObject<List<CarDealership.Models.Car>>(content);
            var count = 0;
            foreach (CarDealership.Models.Car c in readable)
            {
                _output.WriteLine(c.ToString());
                count++;
            }

            Assert.Equal(2, count);

        }

        // Get all Ford cars and assert that each car is of this make
        [Fact]
        public async Task getMake()
        {
            var Response = await _client.GetAsync("/Cars/getbymake/Ford");
            Response.EnsureSuccessStatusCode();

            var content = await Response.Content.ReadAsStringAsync();

            var readable = JsonConvert.DeserializeObject<List<CarDealership.Models.Car>>(content);
            var count = 0;
            foreach (CarDealership.Models.Car c in readable)
            {
                _output.WriteLine(c.ToString());
                count++;
            }

            Assert.Equal(2, count);
        }




        //TODO: Get all cars that are 2 years or older and assert 


    }
}
