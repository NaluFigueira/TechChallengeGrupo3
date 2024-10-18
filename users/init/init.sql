IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TechChallengeUsers')
BEGIN
    CREATE DATABASE TechChallengeUsers;
END;
GO