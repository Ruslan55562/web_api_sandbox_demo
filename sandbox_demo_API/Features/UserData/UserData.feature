Feature: UserData

As a user
I want to check the user data manipulation of parabank application
So I can create, edit and confirm default values for accounts

Background:
	Given I send a request to '/cleanDB' the database
		And I send a request to '/initializeDB' the database

@RGA-1
Scenario: Verify that the user can register a new account
	Given I prepare registration with the following data
		| FirstName | LastName | Street     | City   | State | ZipCode | PhoneNumber | SSN         | Username | Password  |
		| John      | Doe      | 123 Elm St | City A | ST    | 12345   | 123-45-6789 | 123-45-6789 | john_doe | secret123 |
		And I create a registration request to endpoint "/register.htm" with "POST" method
	When I send the request
	Then I should see status code 200
	When I create a request to endpoint "/login/john_doe/secret123" with "GET" method
		And I send the request
	Then the response should have the following data
		| FirstName | LastName | Street     | City   | State | ZipCode | PhoneNumber | SSN         |
		| John      | Doe      | 123 Elm St | City A | ST    | 12345   | 123-45-6789 | 123-45-6789 |

@RGA-2
Scenario: Verify that the user can edit account data
	Given I create a request to endpoint "/customers/update/12323" with "POST" method
	When I set the request parameters from file "userUpdate.json"
		And I send the request
	Then I should see status code 200
		And The response message should be "Successfully updated customer profile"
	When I create a request to endpoint "/login/parasoft/demo2" with "GET" method
		And I send the request
	Then the response should have the following data
		| FirstName | LastName | Street              | City     | State | ZipCode | PhoneNumber  | SSN         |
		| Robert    | Parasoft | 101 E Huntington Dr | Monrovia | CA    | 91016   | 626-256-3680 | 123-45-6789 |

@RGA-3
Scenario: User has an account after creation
	Given I prepare registration with the following data
		| FirstName | LastName | Street     | City   | State | ZipCode | PhoneNumber | SSN         | Username | Password  |
		| John      | Doe      | 123 Elm St | City A | ST    | 12345   | 123-45-6789 | 123-45-6789 | john_doe | secret123 |
		And I create a registration request to endpoint "/register.htm" with "POST" method
	When I send the request
	Then I should see status code 200
	When I create a request to endpoint "/customers/12434/accounts" with "GET" method
		And I send the request
	Then The default amount of newly created account is "515.5"