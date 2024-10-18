IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TechChallengeContactsQuery')
BEGIN
    CREATE DATABASE TechChallengeContactsQuery;
END;
GO