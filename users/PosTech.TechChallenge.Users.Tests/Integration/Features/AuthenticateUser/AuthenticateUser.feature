Feature: Authenticate User
  In order to manage my contacts
  As a potential user
  I want to log in into the application
  
  Scenario: Successful authentication
    Given an existing user who has access to the log in endpoint
    When they fill in a username "testauthentication" and password "S3cur3P@ssW0rd"
    Then the API should authenticate correctly