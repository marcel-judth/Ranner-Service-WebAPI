/****** Object:  Table [dbo].[Invoices]    Script Date: 23.01.2021 09:19:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoices](
	[invoiceId] [int] IDENTITY(1,1) NOT NULL,
	[orderNr] [int] NOT NULL,
	[orderDate] [date] NOT NULL,
	[invoiceNr] [int] NULL,
	[invoiceDate] [date] NULL,
	[customerId] [int] NULL,
	[referenceNumber] [varchar](50) NULL,
	[freighterName] [varchar](30) NULL,
	[freighterInvNr] [varchar](20) NULL,
	[freighterInvArrived] [date] NULL,
	[freighterPaidOn] [date] NULL,
	[customerPaidOn] [date] NULL,
	[deliveryDate] [date] NULL,
	[shipDate] [date] NULL,
	[product] [varchar](100) NULL,
	[priceFreighter] [float] NULL,
	[priceCustomer] [float] NULL,
	[amount] [int] NULL,
	[unit] [varchar](10) NULL,
	[palletChange] [int] NULL,
	[note] [varchar](150) NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[invoiceId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Customers] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customers] ([customerID])
GO

ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Customers]
GO

