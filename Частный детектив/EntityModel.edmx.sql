
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/05/2023 21:37:44
-- Generated from EDMX file: C:\Users\1234\Desktop\Частный детектив модель первая\Частный детектив\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\Users\1234\Documents\detectiveagent.mdf];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserDetective]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Detectives] DROP CONSTRAINT [FK_UserDetective];
GO
IF OBJECT_ID(N'[dbo].[FK_UserClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clients] DROP CONSTRAINT [FK_UserClient];
GO
IF OBJECT_ID(N'[dbo].[FK_DetectiveZayavka]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Zayavkas] DROP CONSTRAINT [FK_DetectiveZayavka];
GO
IF OBJECT_ID(N'[dbo].[FK_DetectiveClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clients] DROP CONSTRAINT [FK_DetectiveClient];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Detectives]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Detectives];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Smss]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Smss];
GO
IF OBJECT_ID(N'[dbo].[Uslugas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Uslugas];
GO
IF OBJECT_ID(N'[dbo].[Zayavkas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zayavkas];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Role] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Detectives'
CREATE TABLE [dbo].[Detectives] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [DetectiveId] int  NOT NULL,
    [User_Id] int  NULL
);
GO

-- Creating table 'Smss'
CREATE TABLE [dbo].[Smss] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Sender] nvarchar(max)  NOT NULL,
    [DetectiveId] int  NOT NULL,
    [ClientId] int  NOT NULL
);
GO

-- Creating table 'Uslugas'
CREATE TABLE [dbo].[Uslugas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Zayavkas'
CREATE TABLE [dbo].[Zayavkas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [TextZayavki] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [DetectiveId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Detectives'
ALTER TABLE [dbo].[Detectives]
ADD CONSTRAINT [PK_Detectives]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Smss'
ALTER TABLE [dbo].[Smss]
ADD CONSTRAINT [PK_Smss]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Uslugas'
ALTER TABLE [dbo].[Uslugas]
ADD CONSTRAINT [PK_Uslugas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Zayavkas'
ALTER TABLE [dbo].[Zayavkas]
ADD CONSTRAINT [PK_Zayavkas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'Detectives'
ALTER TABLE [dbo].[Detectives]
ADD CONSTRAINT [FK_UserDetective]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetective'
CREATE INDEX [IX_FK_UserDetective]
ON [dbo].[Detectives]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [FK_UserClient]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserClient'
CREATE INDEX [IX_FK_UserClient]
ON [dbo].[Clients]
    ([User_Id]);
GO

-- Creating foreign key on [DetectiveId] in table 'Zayavkas'
ALTER TABLE [dbo].[Zayavkas]
ADD CONSTRAINT [FK_DetectiveZayavka]
    FOREIGN KEY ([DetectiveId])
    REFERENCES [dbo].[Detectives]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetectiveZayavka'
CREATE INDEX [IX_FK_DetectiveZayavka]
ON [dbo].[Zayavkas]
    ([DetectiveId]);
GO

-- Creating foreign key on [DetectiveId] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [FK_DetectiveClient]
    FOREIGN KEY ([DetectiveId])
    REFERENCES [dbo].[Detectives]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetectiveClient'
CREATE INDEX [IX_FK_DetectiveClient]
ON [dbo].[Clients]
    ([DetectiveId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------