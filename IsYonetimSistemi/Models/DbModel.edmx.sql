
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/09/2022 15:33:16
-- Generated from EDMX file: D:\Projects\IsYonetimSistemi\IsYonetimSistemi\Models\DbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [IsYonetimDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[fk_personnel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [fk_personnel];
GO
IF OBJECT_ID(N'[dbo].[fk_manager]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [fk_manager];
GO
IF OBJECT_ID(N'[dbo].[fk_permitting_manager]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Leaves] DROP CONSTRAINT [fk_permitting_manager];
GO
IF OBJECT_ID(N'[dbo].[fk_personnel_on_leave]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Leaves] DROP CONSTRAINT [fk_personnel_on_leave];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[Personeller]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personeller];
GO
IF OBJECT_ID(N'[dbo].[Managers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Managers];
GO
IF OBJECT_ID(N'[dbo].[Leaves]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leaves];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [task_id] int IDENTITY(1,1) NOT NULL,
    [personnel_id] int  NOT NULL,
    [manager_id] int  NOT NULL,
    [task_name] varchar(50)  NULL,
    [task_detail] varchar(50)  NULL
);
GO

-- Creating table 'Personnels'
CREATE TABLE [dbo].[Personnels] (
    [user_id] int IDENTITY(1,1) NOT NULL,
    [username] varchar(50)  NOT NULL,
    [password] varchar(250)  NOT NULL,
    [first_name] varchar(50)  NULL,
    [last_name] varchar(50)  NULL,
    [email] varchar(50)  NULL,
    [salary] int  NULL
);
GO

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [user_id] int IDENTITY(1,1) NOT NULL,
    [username] varchar(50)  NOT NULL,
    [password] varchar(250)  NOT NULL,
    [first_name] varchar(50)  NULL,
    [last_name] varchar(50)  NULL,
    [email] varchar(50)  NULL,
    [salary] int  NULL
);
GO

-- Creating table 'Leaves'
CREATE TABLE [dbo].[Leaves] (
    [leave_id] int IDENTITY(1,1) NOT NULL,
    [personnel_id] int  NOT NULL,
    [manager_id] int  NOT NULL,
    [leave_reason] varchar(50)  NULL,
    [leave_start_date] datetime  NOT NULL,
    [leave_end_date] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [task_id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([task_id] ASC);
GO

-- Creating primary key on [user_id] in table 'Personnels'
ALTER TABLE [dbo].[Personnels]
ADD CONSTRAINT [PK_Personnels]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [user_id] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [PK_Managers]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [leave_id] in table 'Leaves'
ALTER TABLE [dbo].[Leaves]
ADD CONSTRAINT [PK_Leaves]
    PRIMARY KEY CLUSTERED ([leave_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [personnel_id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [fk_personnel]
    FOREIGN KEY ([personnel_id])
    REFERENCES [dbo].[Personnels]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_personnel'
CREATE INDEX [IX_fk_personnel]
ON [dbo].[Tasks]
    ([personnel_id]);
GO

-- Creating foreign key on [manager_id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [fk_manager]
    FOREIGN KEY ([manager_id])
    REFERENCES [dbo].[Managers]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_manager'
CREATE INDEX [IX_fk_manager]
ON [dbo].[Tasks]
    ([manager_id]);
GO

-- Creating foreign key on [manager_id] in table 'Leaves'
ALTER TABLE [dbo].[Leaves]
ADD CONSTRAINT [fk_permitting_manager]
    FOREIGN KEY ([manager_id])
    REFERENCES [dbo].[Managers]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_permitting_manager'
CREATE INDEX [IX_fk_permitting_manager]
ON [dbo].[Leaves]
    ([manager_id]);
GO

-- Creating foreign key on [personnel_id] in table 'Leaves'
ALTER TABLE [dbo].[Leaves]
ADD CONSTRAINT [fk_personnel_on_leave]
    FOREIGN KEY ([personnel_id])
    REFERENCES [dbo].[Personnels]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_personnel_on_leave'
CREATE INDEX [IX_fk_personnel_on_leave]
ON [dbo].[Leaves]
    ([personnel_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------