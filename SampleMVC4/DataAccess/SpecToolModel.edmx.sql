
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/04/2013 15:12:41
-- Generated from EDMX file: C:\Users\achidige\Documents\GitHub\xproj\SampleMVC4\DataAccess\SpecToolModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SpecToolDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ComponentStudy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Studies] DROP CONSTRAINT [FK_ComponentStudy];
GO
IF OBJECT_ID(N'[dbo].[FK_CodeListCodeListValues]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CodeListValues] DROP CONSTRAINT [FK_CodeListCodeListValues];
GO
IF OBJECT_ID(N'[dbo].[FK_CodeListVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Variables] DROP CONSTRAINT [FK_CodeListVariable];
GO
IF OBJECT_ID(N'[dbo].[FK_DomainVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Variables] DROP CONSTRAINT [FK_DomainVariable];
GO
IF OBJECT_ID(N'[dbo].[FK_StudyDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domains] DROP CONSTRAINT [FK_StudyDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_SourceDomainDomainRelation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domains] DROP CONSTRAINT [FK_SourceDomainDomainRelation];
GO
IF OBJECT_ID(N'[dbo].[FK_VariableVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Variables] DROP CONSTRAINT [FK_VariableVariable];
GO
IF OBJECT_ID(N'[dbo].[FK_CodeListCodeList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CodeLists] DROP CONSTRAINT [FK_CodeListCodeList];
GO
IF OBJECT_ID(N'[dbo].[FK_MetaDataVersionDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domains] DROP CONSTRAINT [FK_MetaDataVersionDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_MetaDataVersionCodeList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CodeLists] DROP CONSTRAINT [FK_MetaDataVersionCodeList];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Compounds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Compounds];
GO
IF OBJECT_ID(N'[dbo].[Studies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Studies];
GO
IF OBJECT_ID(N'[dbo].[CodeLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CodeLists];
GO
IF OBJECT_ID(N'[dbo].[CodeListValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CodeListValues];
GO
IF OBJECT_ID(N'[dbo].[Domains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Domains];
GO
IF OBJECT_ID(N'[dbo].[Variables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Variables];
GO
IF OBJECT_ID(N'[dbo].[MetaDataVersions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MetaDataVersions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Compounds'
CREATE TABLE [dbo].[Compounds] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Studies'
CREATE TABLE [dbo].[Studies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ComponentId] int  NOT NULL
);
GO

-- Creating table 'CodeLists'
CREATE TABLE [dbo].[CodeLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [VariableId] int  NULL,
    [DefinitionText] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NOT NULL,
    [DataType] int  NOT NULL,
    [IsStandard] bit  NOT NULL,
    [IsTemplate] bit  NOT NULL,
    [CodeListId] int  NULL,
    [MetaDataVersionId] int  NULL
);
GO

-- Creating table 'CodeListValues'
CREATE TABLE [dbo].[CodeListValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [CodeListId] int  NOT NULL
);
GO

-- Creating table 'Domains'
CREATE TABLE [dbo].[Domains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Class] int  NOT NULL,
    [StructureDescription] nvarchar(max)  NOT NULL,
    [CommentText] nvarchar(max)  NOT NULL,
    [IsStandard] bit  NOT NULL,
    [IsTemplate] bit  NOT NULL,
    [StudyId] int  NULL,
    [DomainId] int  NULL,
    [MetaDataVersionId] int  NULL
);
GO

-- Creating table 'Variables'
CREATE TABLE [dbo].[Variables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [BaseName] nvarchar(max)  NOT NULL,
    [LableText] nvarchar(max)  NOT NULL,
    [Core] int  NOT NULL,
    [Origin] int  NOT NULL,
    [Mandatory] int  NOT NULL,
    [Role] int  NOT NULL,
    [CodeListId] int  NULL,
    [CommentText] nvarchar(max)  NULL,
    [DomainId] int  NOT NULL,
    [IsStandard] bit  NOT NULL,
    [IsTemplate] bit  NOT NULL,
    [VariableId] int  NULL,
    [Length] int  NOT NULL,
    [SignificantDigits] int  NULL,
    [DataType] int  NOT NULL
);
GO

-- Creating table 'MetaDataVersions'
CREATE TABLE [dbo].[MetaDataVersions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Version] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Compounds'
ALTER TABLE [dbo].[Compounds]
ADD CONSTRAINT [PK_Compounds]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Studies'
ALTER TABLE [dbo].[Studies]
ADD CONSTRAINT [PK_Studies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CodeLists'
ALTER TABLE [dbo].[CodeLists]
ADD CONSTRAINT [PK_CodeLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CodeListValues'
ALTER TABLE [dbo].[CodeListValues]
ADD CONSTRAINT [PK_CodeListValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Domains'
ALTER TABLE [dbo].[Domains]
ADD CONSTRAINT [PK_Domains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Variables'
ALTER TABLE [dbo].[Variables]
ADD CONSTRAINT [PK_Variables]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MetaDataVersions'
ALTER TABLE [dbo].[MetaDataVersions]
ADD CONSTRAINT [PK_MetaDataVersions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ComponentId] in table 'Studies'
ALTER TABLE [dbo].[Studies]
ADD CONSTRAINT [FK_ComponentStudy]
    FOREIGN KEY ([ComponentId])
    REFERENCES [dbo].[Compounds]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ComponentStudy'
CREATE INDEX [IX_FK_ComponentStudy]
ON [dbo].[Studies]
    ([ComponentId]);
GO

-- Creating foreign key on [CodeListId] in table 'CodeListValues'
ALTER TABLE [dbo].[CodeListValues]
ADD CONSTRAINT [FK_CodeListCodeListValues]
    FOREIGN KEY ([CodeListId])
    REFERENCES [dbo].[CodeLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CodeListCodeListValues'
CREATE INDEX [IX_FK_CodeListCodeListValues]
ON [dbo].[CodeListValues]
    ([CodeListId]);
GO

-- Creating foreign key on [CodeListId] in table 'Variables'
ALTER TABLE [dbo].[Variables]
ADD CONSTRAINT [FK_CodeListVariable]
    FOREIGN KEY ([CodeListId])
    REFERENCES [dbo].[CodeLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CodeListVariable'
CREATE INDEX [IX_FK_CodeListVariable]
ON [dbo].[Variables]
    ([CodeListId]);
GO

-- Creating foreign key on [DomainId] in table 'Variables'
ALTER TABLE [dbo].[Variables]
ADD CONSTRAINT [FK_DomainVariable]
    FOREIGN KEY ([DomainId])
    REFERENCES [dbo].[Domains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DomainVariable'
CREATE INDEX [IX_FK_DomainVariable]
ON [dbo].[Variables]
    ([DomainId]);
GO

-- Creating foreign key on [StudyId] in table 'Domains'
ALTER TABLE [dbo].[Domains]
ADD CONSTRAINT [FK_StudyDomain]
    FOREIGN KEY ([StudyId])
    REFERENCES [dbo].[Studies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StudyDomain'
CREATE INDEX [IX_FK_StudyDomain]
ON [dbo].[Domains]
    ([StudyId]);
GO

-- Creating foreign key on [DomainId] in table 'Domains'
ALTER TABLE [dbo].[Domains]
ADD CONSTRAINT [FK_SourceDomainDomainRelation]
    FOREIGN KEY ([DomainId])
    REFERENCES [dbo].[Domains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SourceDomainDomainRelation'
CREATE INDEX [IX_FK_SourceDomainDomainRelation]
ON [dbo].[Domains]
    ([DomainId]);
GO

-- Creating foreign key on [VariableId] in table 'Variables'
ALTER TABLE [dbo].[Variables]
ADD CONSTRAINT [FK_VariableVariable]
    FOREIGN KEY ([VariableId])
    REFERENCES [dbo].[Variables]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_VariableVariable'
CREATE INDEX [IX_FK_VariableVariable]
ON [dbo].[Variables]
    ([VariableId]);
GO

-- Creating foreign key on [CodeListId] in table 'CodeLists'
ALTER TABLE [dbo].[CodeLists]
ADD CONSTRAINT [FK_CodeListCodeList]
    FOREIGN KEY ([CodeListId])
    REFERENCES [dbo].[CodeLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CodeListCodeList'
CREATE INDEX [IX_FK_CodeListCodeList]
ON [dbo].[CodeLists]
    ([CodeListId]);
GO

-- Creating foreign key on [MetaDataVersionId] in table 'Domains'
ALTER TABLE [dbo].[Domains]
ADD CONSTRAINT [FK_MetaDataVersionDomain]
    FOREIGN KEY ([MetaDataVersionId])
    REFERENCES [dbo].[MetaDataVersions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MetaDataVersionDomain'
CREATE INDEX [IX_FK_MetaDataVersionDomain]
ON [dbo].[Domains]
    ([MetaDataVersionId]);
GO

-- Creating foreign key on [MetaDataVersionId] in table 'CodeLists'
ALTER TABLE [dbo].[CodeLists]
ADD CONSTRAINT [FK_MetaDataVersionCodeList]
    FOREIGN KEY ([MetaDataVersionId])
    REFERENCES [dbo].[MetaDataVersions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MetaDataVersionCodeList'
CREATE INDEX [IX_FK_MetaDataVersionCodeList]
ON [dbo].[CodeLists]
    ([MetaDataVersionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------