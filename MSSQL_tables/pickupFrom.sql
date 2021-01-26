/****** Object:  Table [dbo].[pickupFrom]    Script Date: 23.01.2021 09:29:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pickupFrom](
	[invoiceId] [int] NOT NULL,
	[pickupAddressId] [int] NOT NULL,
	[deliveryTime] [datetime] NULL,
 CONSTRAINT [PK_pickupFrom] PRIMARY KEY CLUSTERED 
(
	[invoiceId] ASC,
	[pickupAddressId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[pickupFrom]  WITH CHECK ADD  CONSTRAINT [FK_pickupFrom_Addresses] FOREIGN KEY([pickupAddressId])
REFERENCES [dbo].[Addresses] ([addressID])
GO

ALTER TABLE [dbo].[pickupFrom] CHECK CONSTRAINT [FK_pickupFrom_Addresses]
GO

ALTER TABLE [dbo].[pickupFrom]  WITH CHECK ADD  CONSTRAINT [FK_pickupFrom_Invoices] FOREIGN KEY([invoiceId])
REFERENCES [dbo].[Invoices] ([invoiceId])
GO

ALTER TABLE [dbo].[pickupFrom] CHECK CONSTRAINT [FK_pickupFrom_Invoices]
GO

