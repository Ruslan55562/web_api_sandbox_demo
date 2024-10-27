Feature: AccountData

As a user 
I want to make some manipulations with Account Data of parabank application
So I can do transfers between accounts or pay a bill

Background: 
 Given I send a request to '/cleanDB' the database
	And I send a request to '/initializeDB' the database

@ACA-1
Scenario: The user can send funds
	Given I create a request to endpoint "/transfer" with "POST" method
	When I set the request parameters from file "transferParams.json"
		And I send the request
	Then I should see status code 200
		And The response message should be "Successfully transferred $1 from account #12345 to account #12789" 
		And I verify balance change of account "12345" from initial balance "-2300" and account "12789" from initial balance "100" with amount "1"


@ACA-2
Scenario: The user can pay a bill
	Given I create a request to endpoint "/billpay" with "POST" method
	When I set the request parameters and body from file "billPay.json"
		And I send the request
	Then I should see status code 200
		And The bill payment result should be:
			| accountId | amount | payeeName  |
			| 12345     | 10     | John Smith |
		And I verify balance change of account "12345" from initial balance "-2300" with amount "10"