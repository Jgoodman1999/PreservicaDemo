using Microsoft.Playwright;
using System.Threading.Tasks;

public class NasaPage
{
    public readonly IPage _page;

    public NasaPage(IPage page)
    {
        _page = page;
    }

    public ILocator FirstNameInput => _page.Locator("#user_first_name");
    public ILocator LastNameInput => _page.Locator("#user_last_name");
    public ILocator EmailInput => _page.Locator("#user_email");
    public ILocator ReasonInput => _page.Locator("#user_use_description");
    public ILocator SignUpButton => _page.Locator("button:has-text('Sign up')");
    public ILocator SignupEmailText => _page.Locator("#apidatagov_signup").Locator("strong");
    public ILocator ConfirmationText => _page.Locator("text=has been e-mailed to you. You can use your API key to begin making web service requests immediately.");

}
