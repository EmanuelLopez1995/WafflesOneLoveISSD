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

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [LastName] nvarchar(255) NOT NULL,
    [Dni] nvarchar(20) NULL,
    [PhoneNumber] nvarchar(20) NULL,
    [Direction] nvarchar(255) NULL,
    [Email] nvarchar(255) NULL,
    [Salary] decimal(18,2) NULL,
    [Position] nvarchar(100) NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231128204111_CreateDatabase', N'6.0.25');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [EmployeeShifts] (
    [Id] int NOT NULL IDENTITY,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    [EmployeeId] int NOT NULL,
    CONSTRAINT [PK_EmployeeShifts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployeeShifts_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_EmployeeShifts_EmployeeId] ON [EmployeeShifts] ([EmployeeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231128214324_AddEmployeeShift', N'6.0.25');
GO

COMMIT;
GO

