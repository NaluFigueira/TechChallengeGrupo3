Feature: Delete Contact
  In order to manage my contacts
  As a user
  I want to delete an existing contact

  Scenario: Successful deletion
    Given a user who has access to the deletion endpoint
    When they send the desired contact id through the endpoint
    Then the API should remove the contact from the list