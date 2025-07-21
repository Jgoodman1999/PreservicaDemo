Feature: NASA API Signup UI

Scenario: Successful registration on NASA API sign-up page
  Given I navigate to the NASA Open APIs sign-up page
  And I should see the registration form elements
  When I fill in the form with the following details
    | FirstName | LastName | Email                   | Reason          |
    | Test      | User     | testuser@exampledemo.com| Test automation |
  Then I should see a confirmation message indicating successful registration
