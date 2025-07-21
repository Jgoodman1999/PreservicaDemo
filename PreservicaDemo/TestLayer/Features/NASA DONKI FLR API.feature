Feature: NASA DONKI FLR API

  Scenario: Valid FLR API request returns 200 with flare data
    Given the FLR API endpoint is available
    When I send a request with start date "2023-01-01", end date "2023-01-07", and a valid API key
    Then the response status should be 200
    And the response should contain flare event data

#This test fails because even after providing no date, the endpoint still returns a 200
  Scenario: Missing start date returns 400 from FLR API
    Given the FLR API endpoint is available
    When I send a request with no start date, end date "2023-01-07", and a valid API key
    Then the response status should be 400