SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[place](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[display_order] [tinyint] NULL,
 CONSTRAINT [PK_place_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[choices](
	[id_user] [int] NOT NULL,
	[id_place] [int] NOT NULL,
 CONSTRAINT [PK_choices] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC,
	[id_place] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[i_got_my_lunch] [bit] NOT NULL,
	[available_seats] [tinyint] NULL,
	[inscription_date] [datetime] NOT NULL,
	[ip_address] [varchar](20) NOT NULL,
 CONSTRAINT [PK_user_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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

INSERT place(name, display_order) VALUES('Carrefour', 1)
INSERT place(name, display_order) VALUES('Marie Blachère', 3)
INSERT place(name, display_order) VALUES('Quick', 2)
INSERT place(name, display_order) VALUES('Kébab', 4)
INSERT place(name, display_order) VALUES('Autre', 5)