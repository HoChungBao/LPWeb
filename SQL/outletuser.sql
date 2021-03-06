USE [MediGroup]
GO
/****** Object:  Table [dbo].[M_OutletStore_User]    Script Date: 10/23/2018 2:48:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[M_OutletStore_User](
	[Id] [bigint] NOT NULL,
	[District] [nvarchar](200) NULL,
	[Province] [nvarchar](200) NULL,
	[Area] [nvarchar](400) NULL,
	[DistrictCode] [varchar](50) NULL,
	[ProvinceCode] [varchar](50) NULL,
	[SizeCode] [varchar](50) NULL,
	[Latitue] [varchar](50) NULL,
	[Longtitue] [varchar](50) NULL,
	[MediGroupCode] [nvarchar](128) NULL,
	[DrugStoreName] [nvarchar](150) NULL,
	[DrugStoreAddress] [nvarchar](500) NULL,
	[CreatedBy] [nvarchar](128) NULL,
	[CreatedDate] [datetime] NULL,
	[StoreOwner] [nvarchar](128) NULL,
	[BankAccount] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](128) NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[StorePhoneNumber] [nvarchar](50) NULL,
	[StandardLevelCode] [varchar](50) NULL,
	[StaffNumm] [int] NULL,
	[Note] [nvarchar](500) NULL,
	[AvatarUrl] [nvarchar](500) NULL,
	[UnitCost] [decimal](18, 0) NULL,
	[WardCode] [varchar](50) NULL,
	[Ward] [nvarchar](200) NULL,
	[Type] [nvarchar](200) NULL,
 CONSTRAINT [PK_M_OutletStore_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
