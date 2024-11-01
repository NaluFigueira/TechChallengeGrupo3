IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TechChallenge')
BEGIN
    CREATE DATABASE TechChallenge;
END;
GO