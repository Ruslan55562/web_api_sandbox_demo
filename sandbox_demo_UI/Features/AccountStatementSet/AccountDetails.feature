Feature: AccountDetails

As a user 
I want to check Account Details of parabank application
So I can see all the transactions and statements

Background:
	Given I open Main page
		And I am logged in with username 'john' and password 'demo'

@ADU-1
Scenario: User is able to see account details
	When I click on '9' account number link
	Then I can see account details with the next data:
		| Account Number | Account Type | Balance   | Available |
		| 13233          | CHECKING     | $100.00   | $100.00   |
		And I can see account activity with the next data:
			| Activity Period | Type |
			| All             | All  |

@ADU-2
Scenario: User is able to see statement for the account
	When I click on '1' account number link
	Then I can see account activity transaction table with the next data:
		| Date       | Transaction    | Debit (-) | Credit (+) |
		| 12-11-2023 | 	Check # 1111  |           | $300.00    |
		 
@ADU-3
Scenario: User can see details for transaction in account
	When I click on '1' account number link
		And I click on '1' transaction
	Then I can see trasaction details with the next data:
		| Transaction ID | Date       | Description  | Type   | Amount  |
		| 12145          | 12-11-2023 | Check # 1111 | Credit | $300,00 |