Feature: NASA DONKI CME API

  Scenario: Valid CME API request returns 200 with non-empty list
    Given the CME API endpoint is available
    When I send a request with start date "2023-01-01", end date "2023-01-07", and a valid API key
    Then the response status should be 200
    And the response should contain at least one CME event

#This test fails because even after providing an invalid date, the endpoint still returns a 200
  Scenario: Invalid date format returns 400 from CME API
    Given the CME API endpoint is available
    When I send a request with start date "invalid-date", end date "2023-01-07", and a valid API key
    Then the response status should be 400