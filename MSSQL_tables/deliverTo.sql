/****** Object:  Table [dbo].[deliverTo]    Script Date: 23.01.2021 09:18:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[deliverTo](
	[invoiceId] [int] NOT NULL,
	[deliveryAddressId] [int] NOT NULL,
	[deliveryTime] [datetime] NULL,
 CONSTRAINT [PK_deliverTo] PRIMARY KEY CLUSTERED 
(
	[invoiceId] ASC,
	[deliveryAddressId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[deliverTo]  WITH CHECK ADD  CONSTRAINT [FK_deliverTo_Addresses] FOREIGN KEY([deliveryAddressId])
REFERENCES [dbo].[Addresses] ([addressID])
GO

ALTER TABLE [dbo].[deliverTo] CHECK CONSTRAINT [FK_deliverTo_Addresses]
GO

ALTER TABLE [dbo].[deliverTo]  WITH CHECK ADD  CONSTRAINT [FK_deliverTo_Invoices] FOREIGN KEY([invoiceId])
REFERENCES [dbo].[Invoices] ([invoiceId])
GO

ALTER TABLE [dbo].[deliverTo] CHECK CONSTRAINT [FK_deliverTo_Invoices]
GO

