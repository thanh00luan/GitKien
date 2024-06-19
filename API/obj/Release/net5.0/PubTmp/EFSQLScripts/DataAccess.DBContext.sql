IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    CREATE TABLE [Accounts] (
        [AccountID] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [UserName] nvarchar(max) NULL,
        [PassWord] nvarchar(max) NULL,
        [IsVerify] nvarchar(max) NULL,
        [Role] nvarchar(max) NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    CREATE TABLE [Blogs] (
        [BlogId] int NOT NULL IDENTITY,
        [AccountID] int NOT NULL,
        [Title] nvarchar(max) NULL,
        [Content] nvarchar(max) NULL,
        [CreateAt] datetime2 NOT NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Blogs] PRIMARY KEY ([BlogId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    CREATE TABLE [Exams] (
        [ExamId] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [StartTime] datetime2 NOT NULL,
        [EndTime] datetime2 NOT NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Exams] PRIMARY KEY ([ExamId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    CREATE TABLE [Options] (
        [OptionId] int NOT NULL IDENTITY,
        [QuestionId] int NOT NULL,
        [Content] nvarchar(max) NULL,
        [IsCorrect] bit NOT NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Options] PRIMARY KEY ([OptionId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    CREATE TABLE [Questions] (
        [QuestionId] int NOT NULL IDENTITY,
        [ExamId] int NOT NULL,
        [Content] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Questions] PRIMARY KEY ([QuestionId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    CREATE TABLE [UserAnswers] (
        [UserAnswerId] int NOT NULL IDENTITY,
        [UserExamId] int NOT NULL,
        [QuestionId] int NOT NULL,
        [ChosenOptionId] int NOT NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_UserAnswers] PRIMARY KEY ([UserAnswerId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    CREATE TABLE [UserExams] (
        [UserExamId] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ExamId] int NOT NULL,
        [StartTime] datetime2 NOT NULL,
        [EndTime] datetime2 NOT NULL,
        [Score] decimal(18,2) NOT NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_UserExams] PRIMARY KEY ([UserExamId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [FullName] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240505082218_InitialDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240505082218_InitialDB', N'5.0.17');
END;
GO

COMMIT;
GO

