Feature: UserCreation

As a user
I want to check the registration functionallity of parabank application
So I can check different combinations of data when registering a user


Background:
	Given I open Main page
		And I click on 'Register' button

@RGU1
Scenario: The user with correct data is created
	When I fill in input fields with the next data
		| First Name | Last Name | Address        | City        | State | Zip Code | Phone        | SSN         | Username | Password          | Confirm           |
		| John       | Doe       | 123 Elm Street | Springfield | IL    | 62704    | 217-555-1234 | 123-45-6789 | johndoe  | SecurePassword123 | SecurePassword123 |
		And I send the registration form
	Then I can see welcome 'Welcome johndoe' message
		And I can see 'Welcome John Doe' message above the nav panel

@RGU2
Scenario: The user with duplicated name isn't created
	When I fill in input fields with the next data
		| First Name | Last Name | Address       | City      | State | Zip Code | Phone        | SSN         | Username         | Password       | Confirm        |
		| Michael    | Johnson   | 789 Pine Road | Pineville |  TX   | 75201    | 214-555-1234 | 456-78-9012 | michael.johnson  | StrongPass789! | StrongPass789! |
		And I send the registration form
		And I click on 'Log Out' button under Account Services panel
		And I click on 'Register' button
		And I fill in input fields with the next data
		| First Name | Last Name | Address       | City        | State | Zip Code | Phone        | SSN         | Username         | Password       | Confirm        |
		| Mike       | Johnson   | 755 Pine Road | Springfield |  TX   | 75201    | 214-555-1234 | 456-78-9012 | michael.johnson  | StrongPass789! | StrongPass789! |
		And I send the registration form
	Then The error message 'This username already exists' is displayed

@RGU3
Scenario: The user isn't created if password field is empty
	When I fill in input fields with the next data
		| First Name  | Last Name   | Address        | City     | State | Zip Code | Phone        | SSN         | Username    | Password | Confirm |
		| Emily       | Smith       | 456 Oak Avenue | Oakville | CA    | 94016    | 408-555-9876 | 987-65-4321 | emily.smith |          |         |
		And I send the registration form
	Then The error message 'Password is required' is displayed
		And The error message 'Password confirmation is required' is displayed

@RGU4
Scenario: The user isn't created if password isn't confirmed
	When I fill in input fields with the next data
		| First Name  | Last Name   | Address        | City     | State | Zip Code | Phone        | SSN         | Username    | Password          | Confirm |
		| Emily       | Smith       | 456 Oak Avenue | Oakville | CA    | 94016    | 408-555-9876 | 987-65-4321 | emily.smith | SecurePassword123 | Pass123 |
		And I send the registration form
	Then The error message 'Passwords did not match' is displayed