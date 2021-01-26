/****** Object:  Table [dbo].[Pallets]    Script Date: 23.01.2021 09:28:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pallets](
	[palletId] [int] IDENTITY(1,1) NOT NULL,
	[invoiceId] [int] NULL,
	[amount] [int] NULL,
	[type] [varchar](10) NULL,
	[place] [varchar](1) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pallets]  WITH CHECK ADD  CONSTRAINT [FK_Pallets_Invoices] FOREIGN KEY([invoiceId])
REFERENCES [dbo].[Invoices] ([invoiceId])
GO

ALTER TABLE [dbo].[Pallets] CHECK CONSTRAINT [FK_Pallets_Invoices]
GO

