Feature: Calculator

  Scenario: Check database connection state
    Given the database connection is open
    Then verify the connection state
