Feature: Update Contact
  In order to manage my contacts
  As a user
  I want to update an existing contact

  Scenario: Successful update
    Given a user who has access to the update endpoint
    When they update a contact name to NewName
    Then the API should update contact correctly