Feature: Create User
  In order to manage my contacts
  As a potential user
  I want to register my data in the api

  Scenario: Successful creation
    Given a potential user who has access to the creation endpoint
    When they fill in a username "henrydepaula", an e-mail "henrydepaula@eyejoy.com.br", a valid password "S3cur3P@ssW0rd"
    And they confirm the password with "S3cur3P@ssW0rd" 
    Then the API should add the new user to its database