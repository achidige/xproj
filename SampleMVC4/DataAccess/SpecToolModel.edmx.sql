
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/03/2013 01:36:35
-- Generated from EDMX file: C:\Users\achidige\documents\visual studio 2012\Projects\SampleMVC4\DataAccess\SpecToolModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_StudySpecification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Specifications] DROP CONSTRAINT [FK_StudySpecification];
GO
IF OBJECT_ID(N'[dbo].[FK_ComponentStudy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Studies] DROP CONSTRAINT [FK_ComponentStudy];
GO
IF OBJECT_ID(N'[dbo].[FK_CodeListCodeListValues]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CodeListValues] DROP CONSTRAINT [FK_CodeListCodeListValues];
GO
IF OBJECT_ID(N'[dbo].[FK_DomainDomainVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DomainVariables] DROP CONSTRAINT [FK_DomainDomainVariable];
GO
IF OBJECT_ID(N'[dbo].[FK_VariableDomainVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DomainVariables] DROP CONSTRAINT [FK_VariableDomainVariable];
GO
IF OBJECT_ID(N'[dbo].[FK_MDRStandardDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domains_StandardDomain] DROP CONSTRAINT [FK_MDRStandardDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_MDRStandardVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Variables_StandardVariable] DROP CONSTRAINT [FK_MDRStandardVariable];
GO
IF OBJECT_ID(N'[dbo].[FK_MDRStandardCodeList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CodeLists_StandardCodeList] DROP CONSTRAINT [FK_MDRStandardCodeList];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecDomainDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecDomains] DROP CONSTRAINT [FK_SpecDomainDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecificationSpecDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecDomains] DROP CONSTRAINT [FK_SpecificationSpecDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_DomainVariableSpecDomainVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecDomainVariables] DROP CONSTRAINT [FK_DomainVariableSpecDomainVariable];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecificationSpecDomainVariable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecDomainVariables] DROP CONSTRAINT [FK_SpecificationSpecDomainVariable];
GO
IF OBJECT_ID(N'[dbo].[FK_CodeListValuesSpecCodeListValues]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecCodeListValues] DROP CONSTRAINT [FK_CodeListValuesSpecCodeListValues];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecificationSpecCodeListValues]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecCodeListValues] DROP CONSTRAINT [FK_SpecificationSpecCodeListValues];
GO
IF OBJECT_ID(N'[dbo].[FK_StandardDomain_inherits_Domain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domains_StandardDomain] DROP CONSTRAINT [FK_StandardDomain_inherits_Domain];
GO
IF OBJECT_ID(N'[dbo].[FK_StandardVariable_inherits_Variable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Variables_StandardVariable] DROP CONSTRAINT [FK_StandardVariable_inherits_Variable];
GO
IF OBJECT_ID(N'[dbo].[FK_StandardCodeList_inherits_CodeList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CodeLists_StandardCodeList] DROP CONSTRAINT [FK_StandardCodeList_inherits_CodeList];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomDomain_inherits_Domain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domains_CustomDomain] DROP CONSTRAINT [FK_CustomDomain_inherits_Domain];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomVariable_inherits_Variable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Variables_CustomVariable] DROP CONSTRAINT [FK_CustomVariable_inherits_Variable];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomCodeList_inherits_CodeList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CodeLists_CustomCodeList] DROP CONSTRAINT [FK_CustomCodeList_inherits_CodeList];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MDRs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MDRs];
GO
IF OBJECT_ID(N'[dbo].[Specifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specifications];
GO
IF OBJECT_ID(N'[dbo].[Components]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Components];
GO
IF OBJECT_ID(N'[dbo].[Domains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Domains];
GO
IF OBJECT_ID(N'[dbo].[Variables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Variables];
GO
IF OBJECT_ID(N'[dbo].[Studies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Studies];
GO
IF OBJECT_ID(N'[dbo].[DomainVariables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DomainVariables];
GO
IF OBJECT_ID(N'[dbo].[CodeLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CodeLists];
GO
IF OBJECT_ID(N'[dbo].[CodeListValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CodeListValues];
GO
IF OBJECT_ID(N'[dbo].[SpecDomains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecDomains];
GO
IF OBJECT_ID(N'[dbo].[SpecDomainVariables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecDomainVariables];
GO
IF OBJECT_ID(N'[dbo].[SpecCodeListValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecCodeListValues];
GO
IF OBJECT_ID(N'[dbo].[Domains_StandardDomain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Domains_StandardDomain];
GO
IF OBJECT_ID(N'[dbo].[Variables_StandardVariable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Variables_StandardVariable];
GO
IF OBJECT_ID(N'[dbo].[CodeLists_StandardCodeList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CodeLists_StandardCodeList];
GO
IF OBJECT_ID(N'[dbo].[Domains_CustomDomain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Domains_CustomDomain];
GO
IF OBJECT_ID(N'[dbo].[Variables_CustomVariable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Variables_CustomVariable];
GO
IF OBJECT_ID(N'[dbo].[CodeLists_CustomCodeList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CodeLists_CustomCodeList];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MDRs'
CREATE TABLE [dbo].[MDRs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Version] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Specifications'
CREATE TABLE [dbo].[Specifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [StudyId] int  NOT NULL
);
GO

-- Creating table 'Components'
CREATE TABLE [dbo].[Components] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Domains'
CREATE TABLE [dbo].[Domains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Classification] int  NOT NULL,
    [Purpose] int  NOT NULL
);
GO

-- Creating table 'Variables'
CREATE TABLE [dbo].[Variables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [DomainVariablePattern] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Studies'
CREATE TABLE [dbo].[Studies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ComponentId] int  NOT NULL
);
GO

-- Creating table 'DomainVariables'
CREATE TABLE [dbo].[DomainVariables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Required] nvarchar(max)  NOT NULL,
    [HasCodeList] bit  NOT NULL,
    [DomainId] int  NOT NULL,
    [VariableId] int  NOT NULL
);
GO

-- Creating table 'CodeLists'
CREATE TABLE [dbo].[CodeLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
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

-- Creating table 'SpecDomains'
CREATE TABLE [dbo].[SpecDomains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SpecificationId] int  NOT NULL,
    [DomainId] int  NOT NULL
);
GO

-- Creating table 'SpecDomainVariables'
CREATE TABLE [dbo].[SpecDomainVariables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DomainVariableId] int  NOT NULL,
    [SpecificationId] int  NOT NULL
);
GO

-- Creating table 'SpecCodeListValues'
CREATE TABLE [dbo].[SpecCodeListValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodeListValuesId] int  NOT NULL,
    [SpecificationId] int  NOT NULL
);
GO

-- Creating table 'Domains_StandardDomain'
CREATE TABLE [dbo].[Domains_StandardDomain] (
    [MDRId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Variables_StandardVariable'
CREATE TABLE [dbo].[Variables_StandardVariable] (
    [MDRId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'CodeLists_StandardCodeList'
CREATE TABLE [dbo].[CodeLists_StandardCodeList] (
    [MDRId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Domains_CustomDomain'
CREATE TABLE [dbo].[Domains_CustomDomain] (
    [CreatedBy] nvarchar(max)  NOT NULL,
    [Reference] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Variables_CustomVariable'
CREATE TABLE [dbo].[Variables_CustomVariable] (
    [CreatedBy] nvarchar(max)  NOT NULL,
    [Reference] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'CodeLists_CustomCodeList'
CREATE TABLE [dbo].[CodeLists_CustomCodeList] (
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MDRs'
ALTER TABLE [dbo].[MDRs]
ADD CONSTRAINT [PK_MDRs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Specifications'
ALTER TABLE [dbo].[Specifications]
ADD CONSTRAINT [PK_Specifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Components'
ALTER TABLE [dbo].[Components]
ADD CONSTRAINT [PK_Components]
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

-- Creating primary key on [Id] in table 'Studies'
ALTER TABLE [dbo].[Studies]
ADD CONSTRAINT [PK_Studies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DomainVariables'
ALTER TABLE [dbo].[DomainVariables]
ADD CONSTRAINT [PK_DomainVariables]
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

-- Creating primary key on [Id] in table 'SpecDomains'
ALTER TABLE [dbo].[SpecDomains]
ADD CONSTRAINT [PK_SpecDomains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SpecDomainVariables'
ALTER TABLE [dbo].[SpecDomainVariables]
ADD CONSTRAINT [PK_SpecDomainVariables]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SpecCodeListValues'
ALTER TABLE [dbo].[SpecCodeListValues]
ADD CONSTRAINT [PK_SpecCodeListValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Domains_StandardDomain'
ALTER TABLE [dbo].[Domains_StandardDomain]
ADD CONSTRAINT [PK_Domains_StandardDomain]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Variables_StandardVariable'
ALTER TABLE [dbo].[Variables_StandardVariable]
ADD CONSTRAINT [PK_Variables_StandardVariable]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CodeLists_StandardCodeList'
ALTER TABLE [dbo].[CodeLists_StandardCodeList]
ADD CONSTRAINT [PK_CodeLists_StandardCodeList]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Domains_CustomDomain'
ALTER TABLE [dbo].[Domains_CustomDomain]
ADD CONSTRAINT [PK_Domains_CustomDomain]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Variables_CustomVariable'
ALTER TABLE [dbo].[Variables_CustomVariable]
ADD CONSTRAINT [PK_Variables_CustomVariable]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CodeLists_CustomCodeList'
ALTER TABLE [dbo].[CodeLists_CustomCodeList]
ADD CONSTRAINT [PK_CodeLists_CustomCodeList]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StudyId] in table 'Specifications'
ALTER TABLE [dbo].[Specifications]
ADD CONSTRAINT [FK_StudySpecification]
    FOREIGN KEY ([StudyId])
    REFERENCES [dbo].[Studies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StudySpecification'
CREATE INDEX [IX_FK_StudySpecification]
ON [dbo].[Specifications]
    ([StudyId]);
GO

-- Creating foreign key on [ComponentId] in table 'Studies'
ALTER TABLE [dbo].[Studies]
ADD CONSTRAINT [FK_ComponentStudy]
    FOREIGN KEY ([ComponentId])
    REFERENCES [dbo].[Components]
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

-- Creating foreign key on [DomainId] in table 'DomainVariables'
ALTER TABLE [dbo].[DomainVariables]
ADD CONSTRAINT [FK_DomainDomainVariable]
    FOREIGN KEY ([DomainId])
    REFERENCES [dbo].[Domains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DomainDomainVariable'
CREATE INDEX [IX_FK_DomainDomainVariable]
ON [dbo].[DomainVariables]
    ([DomainId]);
GO

-- Creating foreign key on [VariableId] in table 'DomainVariables'
ALTER TABLE [dbo].[DomainVariables]
ADD CONSTRAINT [FK_VariableDomainVariable]
    FOREIGN KEY ([VariableId])
    REFERENCES [dbo].[Variables]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_VariableDomainVariable'
CREATE INDEX [IX_FK_VariableDomainVariable]
ON [dbo].[DomainVariables]
    ([VariableId]);
GO

-- Creating foreign key on [MDRId] in table 'Domains_StandardDomain'
ALTER TABLE [dbo].[Domains_StandardDomain]
ADD CONSTRAINT [FK_MDRStandardDomain]
    FOREIGN KEY ([MDRId])
    REFERENCES [dbo].[MDRs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MDRStandardDomain'
CREATE INDEX [IX_FK_MDRStandardDomain]
ON [dbo].[Domains_StandardDomain]
    ([MDRId]);
GO

-- Creating foreign key on [MDRId] in table 'Variables_StandardVariable'
ALTER TABLE [dbo].[Variables_StandardVariable]
ADD CONSTRAINT [FK_MDRStandardVariable]
    FOREIGN KEY ([MDRId])
    REFERENCES [dbo].[MDRs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MDRStandardVariable'
CREATE INDEX [IX_FK_MDRStandardVariable]
ON [dbo].[Variables_StandardVariable]
    ([MDRId]);
GO

-- Creating foreign key on [MDRId] in table 'CodeLists_StandardCodeList'
ALTER TABLE [dbo].[CodeLists_StandardCodeList]
ADD CONSTRAINT [FK_MDRStandardCodeList]
    FOREIGN KEY ([MDRId])
    REFERENCES [dbo].[MDRs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MDRStandardCodeList'
CREATE INDEX [IX_FK_MDRStandardCodeList]
ON [dbo].[CodeLists_StandardCodeList]
    ([MDRId]);
GO

-- Creating foreign key on [SpecificationId] in table 'SpecDomains'
ALTER TABLE [dbo].[SpecDomains]
ADD CONSTRAINT [FK_SpecificationSpecDomain]
    FOREIGN KEY ([SpecificationId])
    REFERENCES [dbo].[Specifications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecificationSpecDomain'
CREATE INDEX [IX_FK_SpecificationSpecDomain]
ON [dbo].[SpecDomains]
    ([SpecificationId]);
GO

-- Creating foreign key on [DomainVariableId] in table 'SpecDomainVariables'
ALTER TABLE [dbo].[SpecDomainVariables]
ADD CONSTRAINT [FK_DomainVariableSpecDomainVariable]
    FOREIGN KEY ([DomainVariableId])
    REFERENCES [dbo].[DomainVariables]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DomainVariableSpecDomainVariable'
CREATE INDEX [IX_FK_DomainVariableSpecDomainVariable]
ON [dbo].[SpecDomainVariables]
    ([DomainVariableId]);
GO

-- Creating foreign key on [SpecificationId] in table 'SpecDomainVariables'
ALTER TABLE [dbo].[SpecDomainVariables]
ADD CONSTRAINT [FK_SpecificationSpecDomainVariable]
    FOREIGN KEY ([SpecificationId])
    REFERENCES [dbo].[Specifications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecificationSpecDomainVariable'
CREATE INDEX [IX_FK_SpecificationSpecDomainVariable]
ON [dbo].[SpecDomainVariables]
    ([SpecificationId]);
GO

-- Creating foreign key on [CodeListValuesId] in table 'SpecCodeListValues'
ALTER TABLE [dbo].[SpecCodeListValues]
ADD CONSTRAINT [FK_CodeListValuesSpecCodeListValues]
    FOREIGN KEY ([CodeListValuesId])
    REFERENCES [dbo].[CodeListValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CodeListValuesSpecCodeListValues'
CREATE INDEX [IX_FK_CodeListValuesSpecCodeListValues]
ON [dbo].[SpecCodeListValues]
    ([CodeListValuesId]);
GO

-- Creating foreign key on [SpecificationId] in table 'SpecCodeListValues'
ALTER TABLE [dbo].[SpecCodeListValues]
ADD CONSTRAINT [FK_SpecificationSpecCodeListValues]
    FOREIGN KEY ([SpecificationId])
    REFERENCES [dbo].[Specifications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecificationSpecCodeListValues'
CREATE INDEX [IX_FK_SpecificationSpecCodeListValues]
ON [dbo].[SpecCodeListValues]
    ([SpecificationId]);
GO

-- Creating foreign key on [DomainId] in table 'SpecDomains'
ALTER TABLE [dbo].[SpecDomains]
ADD CONSTRAINT [FK_DomainSpecDomain]
    FOREIGN KEY ([DomainId])
    REFERENCES [dbo].[Domains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DomainSpecDomain'
CREATE INDEX [IX_FK_DomainSpecDomain]
ON [dbo].[SpecDomains]
    ([DomainId]);
GO

-- Creating foreign key on [Id] in table 'Domains_StandardDomain'
ALTER TABLE [dbo].[Domains_StandardDomain]
ADD CONSTRAINT [FK_StandardDomain_inherits_Domain]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Domains]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Variables_StandardVariable'
ALTER TABLE [dbo].[Variables_StandardVariable]
ADD CONSTRAINT [FK_StandardVariable_inherits_Variable]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Variables]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'CodeLists_StandardCodeList'
ALTER TABLE [dbo].[CodeLists_StandardCodeList]
ADD CONSTRAINT [FK_StandardCodeList_inherits_CodeList]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[CodeLists]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Domains_CustomDomain'
ALTER TABLE [dbo].[Domains_CustomDomain]
ADD CONSTRAINT [FK_CustomDomain_inherits_Domain]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Domains]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Variables_CustomVariable'
ALTER TABLE [dbo].[Variables_CustomVariable]
ADD CONSTRAINT [FK_CustomVariable_inherits_Variable]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Variables]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'CodeLists_CustomCodeList'
ALTER TABLE [dbo].[CodeLists_CustomCodeList]
ADD CONSTRAINT [FK_CustomCodeList_inherits_CodeList]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[CodeLists]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------