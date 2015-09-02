USE [tatabouf]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[equipage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](30) NOT NULL,
	[marie_blachere] [bit] NOT NULL,
	[carrefour] [bit] NOT NULL,
	[kebab] [bit] NOT NULL,
	[quick] [bit] NOT NULL,
	[autre] [bit] NOT NULL,
	[j_ai_ma_bouffe] [bit] NOT NULL,
	[nb_places_dispo] [tinyint] NULL,
	[date_inscription] [datetime] NOT NULL,
	[ip] [varchar](20) NOT NULL
 CONSTRAINT [PK_dbo.equipage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
