Feature: AccountData

As a user 
I want to make some manipulations with Account Data of parabank application
So I can do transfers between accounts or pay a bill

Background: 
	Given I open Main page

@ACU-1
Scenario: Associated with the user account is created
	When I click on 'Admin Page' navigation button
	    And I update initial balance to '600' and minumum balance to '130'
		And I click on 'Register' button
		And I fill in input fields with the next data
		    | First Name | Last Name | Address        | City        | State | Zip Code | Phone        | SSN         | Username | Password          | Confirm           |
		    | John       | Doe       | 123 Elm Street | Springfield | IL    | 62704    | 217-555-1234 | 123-45-6789 | johndoe  | SecurePassword123 | SecurePassword123 |
		And I send the registration form
		And I go to 'Accounts Overview' page
	Then The Account has the following balance data:
         | Balance | Available Amount | Total   |
         | $600.00 | $600.00          | $600.00 |

@ACU-2
Scenario: Additional account can be created for User
    Given I am logged in with username 'john' and password 'demo'
	When I go to 'Open New Account' page
	     And I select 'SAVINGS' type
		 And I open new account
	Then I see 'Account Opened!' title
		 And The new account number is created

@ACU-3 
Scenario: User can transfer money between accounts
    Given I am logged in with username 'john' and password 'demo'
	When I go to 'Transfer Funds' page
		And I enter '15' transfer amount
		And I send a tranfer from '12456' to '12789' account
	Then I see 'Transfer Complete!' title

@ACU-4
Scenario: User can pay a bill 
	Given I am logged in with username 'john' and password 'demo'
	When I go to 'Bill Pay' page
		And I fill in bill input fields with the next data
			|Payee Name  | Address        | City         |  State   | Zip Code | Phone        | Account | Verify Account | Amount     | 
			| John Doe   | 1234 Elm Street|  Springfield |   IL     | 62704    | 217-555-1234 | 99999   | 99999          | 5          | 
	Then I see 'Bill Payment Complete' title
		And I see successfull operation message with 'Bill Payment to John Doe in the amount of $5.00 from account 12345 was successful.' text

@ACU-5
Scenario: User can request a loan
	Given I am logged in with username 'john' and password 'demo'
	When I go to 'Request Loan' page
		And I enter '5' as loan amount and '10' down payment from '12345' account
	Then I see 'Loan Request Processed' title
		 And The loan response has the following data:
		     | Loan Provider						  |  Status   |
		     | Wealth Securities Dynamic Loans (WSDL) |  Approved |
		 And I see successfull operation message with 'Congratulations, your loan has been approved.' text
		 And The new account number is created