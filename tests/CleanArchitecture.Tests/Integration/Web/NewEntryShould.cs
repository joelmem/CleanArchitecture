using CleanArchitecture.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace CleanArchitecture.Tests.Integration.Web
{
    public class NewEntryShould : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _testServerFixture;
        public NewEntryShould(TestServerFixture testServerFixture)
        {
            _testServerFixture = testServerFixture;
        }

        [Fact]
        public void Return404GivenInvalidId()
        {

            int inValidId = 100;
            var entryToPost = new { EmailAddress = "test@test.com", Message = "test" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(entryToPost), Encoding.UTF8,
                "application/json");
            var call = _testServerFixture.Client.PostAsync($"/api/guestbook/{inValidId}/NewEntry", jsonContent);
            var response = call.Result;

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            Assert.Equal(inValidId.ToString(), stringResponse);
        }

        [Fact]
        public void ReturnGuestbookWithOneItem()
        {
            int validId = 2;
            string message = Guid.NewGuid().ToString();
            var entryToPost = new { EmailAddress = "test@test.com", Message = message };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(entryToPost), Encoding.UTF8, "application/json");
            var response = _testServerFixture.Client.PostAsync($"/api/guestbook/{validId}/NewEntry", jsonContent).Result;
            response.EnsureSuccessStatusCode();
            var stringResponse = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<Guestbook>(stringResponse);
            Assert.Equal(validId, result.Id);
            Assert.True(result.Entries.Any(e => e.Message == message));
        }
    }
}
