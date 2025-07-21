public static class NasaAssertions
{
    public static async Task AssertFormElementsVisible(NasaPage signupPage)
    {
        Assert.True(await signupPage.FirstNameInput.IsVisibleAsync());
        Assert.True(await signupPage.LastNameInput.IsVisibleAsync());
        Assert.True(await signupPage.EmailInput.IsVisibleAsync());
        Assert.True(await signupPage.ReasonInput.IsVisibleAsync());
        Assert.True(await signupPage.SignUpButton.IsVisibleAsync());
    }

    public static async Task AssertSignupConfirmation(NasaPage signupPage, string expectedEmail)
    {
        await signupPage.SignupEmailText.WaitForAsync();
        var email = await signupPage.SignupEmailText.InnerTextAsync();
        var confirmationText = await signupPage.ConfirmationText.InnerTextAsync();

        Assert.AreEqual(expectedEmail, email);
        Assert.AreEqual($"Your API key for {expectedEmail} has been e-mailed to you. You can use your API key to begin making web service requests immediately.", confirmationText);
    }
}
