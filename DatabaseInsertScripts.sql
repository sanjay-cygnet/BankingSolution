USE [CustomerDatabase]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 07-04-2023 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 07-04-2023 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [nvarchar](50) NOT NULL,
	[BankBranchId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[OpeningDate] [datetime2](7) NOT NULL,
	[Balance] [float] NOT NULL,
	[Type] [smallint] NOT NULL,
	[Status] [smallint] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [nvarchar](50) NOT NULL,
	[LastModifiedById] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankBranch]    Script Date: 07-04-2023 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankBranch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
	[City] [nvarchar](255) NOT NULL,
	[OpenedOn] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedById] [nvarchar](max) NOT NULL,
	[LastModifiedById] [nvarchar](max) NULL,
	[LastModifiedDate] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Currency] [smallint] NOT NULL,
 CONSTRAINT [PK_BankBranch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 07-04-2023 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[City] [nvarchar](255) NOT NULL,
	[Mobile] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Dob] [datetime2](7) NOT NULL,
	[CreatedById] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[LastModifiedById] [nvarchar](50) NULL,
	[LastModifiedDate] [datetime2](7) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 07-04-2023 16:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[Amount] [float] NOT NULL,
	[TransactionType] [smallint] NOT NULL,
	[EffectiveDate] [datetime2](7) NOT NULL,
	[TransactionNumber] [nvarchar](100) NOT NULL,
	[ActualTransactionDate] [datetime2](7) NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230407083736_Initial', N'6.0.15')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230407101921_Branch_Transaction_TableChanges', N'6.0.15')
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([Id], [AccountNumber], [BankBranchId], [CustomerId], [OpeningDate], [Balance], [Type], [Status], [CreatedDate], [IsActive], [CreatedById], [LastModifiedById], [LastModifiedDate], [IsDeleted]) VALUES (3, N'ACT009', 2, 1, CAST(N'2010-12-31T00:00:00.0000000' AS DateTime2), 10000, 1, 1, CAST(N'2018-01-01T00:00:00.0000000' AS DateTime2), 1, N'ba9f5f2d-b984-43ae-928a-feadb801ff33', NULL, NULL, 0)
GO
INSERT [dbo].[Accounts] ([Id], [AccountNumber], [BankBranchId], [CustomerId], [OpeningDate], [Balance], [Type], [Status], [CreatedDate], [IsActive], [CreatedById], [LastModifiedById], [LastModifiedDate], [IsDeleted]) VALUES (4, N'ACT008', 2, 2, CAST(N'2008-04-15T00:00:00.0000000' AS DateTime2), 200000, 1, 1, CAST(N'2018-01-01T00:00:00.0000000' AS DateTime2), 1, N'ba9f5f2d-b984-43ae-928a-feadb801ff33', NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[BankBranch] ON 
GO
INSERT [dbo].[BankBranch] ([Id], [Name], [Code], [City], [OpenedOn], [CreatedDate], [IsActive], [CreatedById], [LastModifiedById], [LastModifiedDate], [IsDeleted], [Currency]) VALUES (2, N'ANB Branch', N'ANB001', N'LA', CAST(N'1995-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2001-01-06T00:00:00.0000000' AS DateTime2), 1, N'ba9f5f2d-b984-43ae-928a-feadb801ff33', NULL, NULL, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[BankBranch] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Address], [City], [Mobile], [Email], [Dob], [CreatedById], [CreatedDate], [LastModifiedById], [LastModifiedDate], [IsActive], [IsDeleted]) VALUES (1, N'Alex', N'Martin', N'110', N'New York', N'4455667788', N'alex@test.com', CAST(N'1989-06-09T00:00:00.0000000' AS DateTime2), N'ba9f5f2d-b984-43ae-928a-feadb801ff33', CAST(N'2020-05-03T00:00:00.0000000' AS DateTime2), NULL, NULL, 1, 0)
GO
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Address], [City], [Mobile], [Email], [Dob], [CreatedById], [CreatedDate], [LastModifiedById], [LastModifiedDate], [IsActive], [IsDeleted]) VALUES (2, N'Minaxi', N'George', N'8989', N'New York', N'4567895345', N'minaxi@test.com', CAST(N'1990-09-02T00:00:00.0000000' AS DateTime2), N'ba9f5f2d-b984-43ae-928a-feadb801ff33', CAST(N'2018-09-01T00:00:00.0000000' AS DateTime2), NULL, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([Id], [AccountId], [Amount], [TransactionType], [EffectiveDate], [TransactionNumber], [ActualTransactionDate], [Status]) VALUES (1, 3, 244, 1, CAST(N'2023-04-07T09:47:06.1302956' AS DateTime2), N'26882544-b789-4111-ae10-7dda74175650', NULL, 1)
GO
INSERT [dbo].[Transaction] ([Id], [AccountId], [Amount], [TransactionType], [EffectiveDate], [TransactionNumber], [ActualTransactionDate], [Status]) VALUES (2, 3, 7756, 1, CAST(N'2023-04-07T09:48:24.7962893' AS DateTime2), N'e1a3db2c-2422-43eb-acc9-3138c4e303e7', NULL, 1)
GO
INSERT [dbo].[Transaction] ([Id], [AccountId], [Amount], [TransactionType], [EffectiveDate], [TransactionNumber], [ActualTransactionDate], [Status]) VALUES (3, 4, 1000, 1, CAST(N'2023-04-07T10:43:52.4927180' AS DateTime2), N'72737a31-19c8-42de-995d-f7d65ea6bafc', CAST(N'2023-04-07T10:43:52.4940235' AS DateTime2), 2)
GO
INSERT [dbo].[Transaction] ([Id], [AccountId], [Amount], [TransactionType], [EffectiveDate], [TransactionNumber], [ActualTransactionDate], [Status]) VALUES (4, 4, 99000, 1, CAST(N'2023-04-07T10:45:57.7620344' AS DateTime2), N'09962289-dfab-4eaa-97f3-11c0ef6aafb2', CAST(N'2023-04-07T10:45:57.7620407' AS DateTime2), 2)
GO
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
ALTER TABLE [dbo].[BankBranch] ADD  DEFAULT (CONVERT([smallint],(1))) FOR [Currency]
GO
ALTER TABLE [dbo].[Transaction] ADD  DEFAULT (CONVERT([smallint],(1))) FOR [Status]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_BankBranch_BankBranchId] FOREIGN KEY([BankBranchId])
REFERENCES [dbo].[BankBranch] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_BankBranch_BankBranchId]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Accounts_AccountId]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Saving, 2=Current' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Active, 2=Block, 3=Suspended' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Debit, 2=Credit' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Transaction', @level2type=N'COLUMN',@level2name=N'TransactionType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'/1-Pending, 2-Success, 3-Hold,4-Failed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Transaction', @level2type=N'COLUMN',@level2name=N'Status'
GO
