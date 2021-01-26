/****** Object:  Table [dbo].[Addresses]    Script Date: 23.01.2021 09:16:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Addresses](
	[addressID] [int] IDENTITY(1,1) NOT NULL,
	[shipName] [varchar](50) NULL,
	[address] [varchar](100) NULL,
	[zipCode] [varchar](10) NULL,
	[city] [varchar](20) NULL,
	[country] [varchar](3) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[addressID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

