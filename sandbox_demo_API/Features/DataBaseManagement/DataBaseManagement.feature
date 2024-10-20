Feature: DataBaseManagement

As a user
I want to be able to management data base data
So I can initialize or clean DB

@DB-1
  Scenario: Clean the database
    When I send a request to '/cleanDB' the database
    Then I should see status code 204

@DB-2
  Scenario: Initialize the database
    When I send a request to '/initializeDB' the database
    Then I should see status code 204