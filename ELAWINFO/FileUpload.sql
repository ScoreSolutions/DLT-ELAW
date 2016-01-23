USE [DLT]
GO

/****** Object:  Table [dbo].[File_Upload]    Script Date: 11/08/2010 17:48:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[File_Upload](
	[file_id] [nvarchar](15) NOT NULL,
	[name] [nchar](10) NULL,
	[Detail] [nchar](10) NULL,
	[file_path] [nvarchar](250) NULL,
	[mime_type] [nvarchar](50) NULL,
	[created_date] [datetime] NULL,
	[creation_by] [nvarchar](50) NULL
) ON [PRIMARY]

GO

