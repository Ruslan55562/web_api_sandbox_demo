Feature: FindTransaction

As a user 
I want it to be possible to find transactions in parabank application
So I can search by ID,date and amount of the transaction

Background:
	Given I open Main page
		And I am logged in with username 'john' and password 'demo'
	When I go to 'Find Transactions' page

@FND1
Scenario: User can find a transaction by ID
	When I select '12345' account
		And I enter '12145' to transaction id field
		And I search transaction by 'ID'
	Then I can see account activity transaction table with the next data:
		| Date       | Transaction  | Debit (-) | Credit (+) |
		| 12-11-2024 | Check # 1111 |           | $300.00    |

@FND2
Scenario: User can find a transaction by Date
	When I select '12345' account
		And I enter '12-12-2024' to transaction date field
		And I search transaction by 'Date'
	Then I can see account activity transaction table with the next data:
		| Date       | Transaction  | Debit (-) | Credit (+) |
		| 12-12-2024 | Check # 1211 | $100.00   |            |

@FND3
Scenario: User can find a transaction by Date Range
	When I select '12345' account
		And I enter '11-12-2024' from to '12-12-2024' date
		And I search transaction by 'Date Range'
	Then I can see account activity transaction table with the next data:
		| Date       | Transaction  | Debit (-) | Credit (+) |
		| 12-11-2024 | Check # 1111 |           | $300.00    |
		| 12-12-2024 | Check # 1211 | $100.00   |            |

@FND4
Scenario: User can find a transaction by Amount
	When I select '12345' account
		And I enter '100' amount
		And I search transaction by 'Amount'
	Then I can see account activity transaction table with the next data:
		| Date       | Transaction        | Debit (-) | Credit (+) |
		| 12-12-2024 | Check # 1211       | $100.00   |            |
		| 06-16-2024 | Funds Transfer Sent| $100.00   |            |