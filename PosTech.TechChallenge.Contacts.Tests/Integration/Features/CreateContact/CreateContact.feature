Feature: Create Contact
  In order to manage my contacts
  As a user
  I want to create a new contact

  Scenario: Successful creation
    Given a user who has access to the creation endpoint
    When they fill in valid contact information
    And they send the inputted contact information through the endpoint
    Then the API should add new contact to list