CREATE TABLE [dbo].[Infomation](
	[Id] [bigint] NOT NULL,
	[Address] [nvarchar](400) NULL,
	[Phone] [nvarchar](400) NULL,
	[Email] [nvarchar](400) NULL,
	[About] [nvarchar](max) NULL,
	[Work] [nvarchar](400) NULL,
	[MainPhone] [nvarchar](400) NULL,
 CONSTRAINT [PK_Infomation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO