IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TechChallengeContactsCommand')
BEGIN
    CREATE DATABASE TechChallengeContactsCommand;
END;
GO