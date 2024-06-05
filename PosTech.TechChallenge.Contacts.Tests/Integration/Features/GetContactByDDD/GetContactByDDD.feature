Feature: Get Contact By DDD
  In order to manage my contacts
  As a user
  I want to find an existing contact by ddd

  Scenario: Find contact by its DDD successfully
    Given a user who has access to the get by ddd endpoint
    And user has two contacts with ddd 11 and one with ddd 18
    When they search a contact with ddd 11
    Then the API should return only contacts with ddd 11