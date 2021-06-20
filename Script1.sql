

/*** First, Please create a database in SQL SERVER with name 'customerdb' or anything (change the application's
     'Web.Config' file accordingly) and then run the script below
     of create table 'customer' ***/



DROP TABLE [dbo].[customer]
GO

CREATE TABLE [dbo].[customer](
	[SrlNo] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[PanNo] [nvarchar](10) NULL,
	[Address] [nvarchar](50) NULL,
	[PinCode] [int] NULL,
	[City] [nvarchar](30) NULL,
	[State] [nvarchar](20) NULL,
	[Paidfee] [bit] NULL,
	[Amount] [float] NULL,
	[DateofPayment] [date] NULL,
	[LastName] [nvarchar](50) NULL,
	[CustNo] [text] NULL,
 CONSTRAINT [PK__customer__20F7398AA48799AB] PRIMARY KEY CLUSTERED 
(
	[SrlNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


