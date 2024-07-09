Feature: HomePage

As a user 
I want to check the basic functionality of parabank application
So I can see all the major elements and sections of the page

Background:
	Given I open Main page

@SMU-1
Scenario: The Home page is displayed
	Then I can see login form with 'Customer Login' header
		And I can see 'ATM Services,Online Services' sections
		And I can see '06/27/2024' sections

@SMU-2
Scenario: The services sections are displayed on the Home Page
	Then I can see login form with 'Customer Login' header
		And I can see 'Check Balances,Transfer Funds,Withdraw Funds,Make Deposits' section items under 'ATM Services' section
		And I can see 'Bill Pay,Account History,Transfer Funds' section items under 'Online Services' section

@SMU-3
Scenario: The news section is displayed on Home Page
	Then I can see login form with 'Customer Login' header
		 And the news section contains '06/27/2024' date
		 And the news section title 'Latest News' is above the background
		 And I can see 'New! Online Bill Pay,New! Online Account Transfers' section items under '06/27/2024' section
