Feature: Calculator

  Scenario: Check database connection state
    Given the database connection is open
    Then verify the connection state


  Scenario: Open Main Page
  Given I open Main page
