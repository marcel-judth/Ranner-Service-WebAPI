/****** Object:  Table [dbo].[Customers]    Script Date: 23.01.2021 09:18:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers](
	[customerID] [int] IDENTITY(1,1) NOT NULL,
	[customerName] [varchar](50) NULL,
	[address] [varchar](100) NULL,
	[zipcode] [varchar](10) NULL,
	[city] [varchar](30) NULL,
	[customerUID] [varchar](10) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[customerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

