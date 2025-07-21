using Newtonsoft.Json.Linq;
using RestSharp;
using Reqnroll;
using Microsoft.Playwright;
using NUnit.Framework;

[Binding]
public class NasaSteps
{
    private RestClient _client;
    private RestRequest _request;
    private RestResponse _response;
    private JArray _jsonArray;
    private IBrowser _browser;
    private IPage _page;
    private NasaPage _nasaPage;

    [Given("the CME API endpoint is available")]
    public void GivenTheCMEAPIEndpointIsAvailable()
    {
        _client = NasaEndpoints.CreateClient();
        _request = NasaEndpoints.CreateRequest("/DONKI/CME");
    }

    [Given("the FLR API endpoint is available")]
    public void GivenTheFLRAPIEndpointIsAvailable()
    {
        _client = NasaEndpoints.CreateClient();
        _request = NasaEndpoints.CreateRequest("/DONKI/FLR");
    }

    [When(@"I send a request with start date ""(.*)"", end date ""(.*)"", and a valid API key")]
    public void WhenISendARequestWithDatesAndKey(string start, string end)
    {
        _request.AddParameter("startDate", start);
        _request.AddParameter("endDate", end);
        _request.AddParameter("api_key", "DEMO_KEY");
        _response = _client.Execute(_request);
    }

    [When(@"I send a request with no start date, end date ""(.*)"", and a valid API key")]
    public void WhenISendRequestWithoutStartDate(string end)
    {
        _request.AddParameter("endDate", end);
        _request.AddParameter("api_key", "DEMO_KEY");
        _response = _client.Execute(_request);
    }

    [Then(@"the response status should be (.*)")]
    public void ThenResponseStatusShouldBe(int statusCode)
    {
        Assert.AreEqual(statusCode, (int)_response.StatusCode, $"Expected status code was {statusCode} but endpoint returned {(int)_response.StatusCode}");
    }

    [Then(@"the response should contain at least one CME event")]
    [Then(@"the response should contain flare event data")]
    public void ThenResponseShouldContainAtLeastOneCme()
    {
        _jsonArray = JArray.Parse(_response.Content);
        Assert.IsTrue(_jsonArray.Count > 0);
    }

    [Given(@"I navigate to the NASA Open APIs sign-up page")]
    public async Task GivenINavigateToTheSignUpPage()
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
        _page = await _browser.NewPageAsync();
        _nasaPage = new NasaPage(_page);
        await _page.GotoAsync("https://api.nasa.gov/");
    }

    [Given(@"I should see the registration form elements")]
    public async Task GivenIShouldSeeTheRegistrationFormElements()
    {
        await NasaAssertions.AssertFormElementsVisible(_nasaPage);
    }

    [When(@"I fill in the form with the following details")]
    public async Task WhenIFillInTheFormWithTheFollowingDetails(Table table)
    {
        var data = table.Rows[0];
        await _nasaPage.FirstNameInput.FillAsync(data["FirstName"]);
        await _nasaPage.LastNameInput.FillAsync(data["LastName"]);
        await _nasaPage.EmailInput.FillAsync(data["Email"]);
        await _nasaPage.ReasonInput.FillAsync(data["Reason"]);
        await _nasaPage.SignUpButton.ClickAsync();
    }

    [Then(@"I should see a confirmation message indicating successful registration")]
    public async Task ThenIShouldSeeConfirmation()
    {
        await NasaAssertions.AssertSignupConfirmation(_nasaPage, "testuser@exampledemo.com");
        await _browser.CloseAsync();
    }
}
