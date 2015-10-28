USE [tatabouf]
GO
/****** Object:  Table [dbo].[choices]    Script Date: 14/09/2015 06:04:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[choices](
	[id_user] [int] NOT NULL,
	[id_place] [int] NOT NULL,
	[other_idea] [varchar](20) NULL,
 CONSTRAINT [PK_choices] PRIMARY KEY CLUSTERED
(
	[id_user] ASC,
	[id_place] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF

GO
/****** Object:  Table [dbo].[place]    Script Date: 14/09/2015 06:04:38 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[place](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[display_order] [tinyint] NOT NULL,
	[input_text] [bit] NOT NULL,
	[css_class] [varchar](10) NOT NULL,
	[priority] [tinyint] NOT NULL,
 CONSTRAINT [PK_place_id] PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user]    Script Date: 14/09/2015 06:04:38 ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[available_seats] [smallint] NULL,
	[inscription_date] [datetime] NOT NULL,
	[departure_time] [datetime] NULL,
	[ip_address] [varchar](20) NOT NULL,
 CONSTRAINT [PK_user_id] PRIMARY KEY CLUSTERED
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[choices]  WITH CHECK ADD  CONSTRAINT [FK_choices_place] FOREIGN KEY([id_place])
REFERENCES [dbo].[place] ([id])
GO

ALTER TABLE [dbo].[choices] CHECK CONSTRAINT [FK_choices_place]
GO

ALTER TABLE [dbo].[choices]  WITH CHECK ADD  CONSTRAINT [FK_choices_user] FOREIGN KEY([id_user])
REFERENCES [dbo].[user] ([id])
GO

ALTER TABLE [dbo].[choices] CHECK CONSTRAINT [FK_choices_user]
GO

INSERT place(name, display_order, input_text, css_class, [priority]) VALUES('J''ai ma<br/>bouffe', 1, 0, 'i-have-it', 2)
INSERT place(name, display_order, input_text, css_class, [priority]) VALUES('Carrefour', 2, 0, 'want-to-go', 0)
INSERT place(name, display_order, input_text, css_class, [priority]) VALUES('Quick', 3, 0, 'want-to-go', 0)
INSERT place(name, display_order, input_text, css_class, [priority]) VALUES('Marie<br/>Blachère', 4, 0, 'want-to-go', 0)
INSERT place(name, display_order, input_text, css_class, [priority]) VALUES('Kébab', 5, 0, 'want-to-go', 0)
INSERT place(name, display_order, input_text, css_class, [priority]) VALUES('Autre', 6, 1, 'other', 1)

GO

SELECT * FROM [place]

SELECT * FROM [choices]

SELECT * FROM [user]