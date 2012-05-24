USE [dbknow]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[trn_Databases](
	[DatabaseID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Description] [ntext] NULL,
	[DatabaseTypeInd] [int] NOT NULL,
	[ConnectionString] [nchar](255) NULL,
	[Active] [int] NOT NULL,
	[LastScanned] [datetime] NOT NULL,
	[LastModified] [datetime] NOT NULL,
	[Created] [datetime] NOT NULL,
	[LockNum] [int] NOT NULL,
 CONSTRAINT [PK_trn_Databases] PRIMARY KEY CLUSTERED 
(
	[DatabaseID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


CREATE TABLE [dbo].[trn_Tables](
	[TableID] [int] IDENTITY(1,1) NOT NULL,
	[DatabaseID] [int] NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Description] [ntext] NULL,
	[Active] [int] NOT NULL,
	[LastScanned] [datetime] NOT NULL,
	[LastModified] [datetime] NOT NULL,
	[Created] [datetime] NOT NULL,
	[LockNum] [int] NOT NULL,
 CONSTRAINT [PK_trn_Tables] PRIMARY KEY CLUSTERED 
(
	[TableID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[trn_Fields](
	[FieldID] [int] IDENTITY(1,1) NOT NULL,
	[TableID] [int] NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Description] [ntext] NULL,
	[Sequence] [int] NOT NULL,
	[Nullable] [int] NOT NULL,
	[FieldType] [nchar](50) NOT NULL,
	[MaxLength] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[LastScanned] [datetime] NOT NULL,
	[LastModified] [datetime] NOT NULL,
	[Created] [datetime] NOT NULL,
	[LockNum] [int] NOT NULL,
 CONSTRAINT [PK_trn_Fields] PRIMARY KEY CLUSTERED 
(
	[FieldID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

