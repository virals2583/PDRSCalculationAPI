USE [PDRS]
GO
/****** Object:  Table [dbo].[Procurement]    Script Date: 30-08-2021 11:50:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Procurement](
	[Id] [int] NOT NULL,
	[Status] [nchar](25) NULL,
	[Progress] [int] NULL,
	[Amount] [float] NULL,
 CONSTRAINT [PK_Procurement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Procurement] ([Id], [Status], [Progress], [Amount]) VALUES (1, N'Completed', 100, 50)
