USE [MediGroup]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[Name] [nvarchar](256) NULL,
	[IsManager] [bit] NOT NULL,
	[DateWork] [datetime] NULL,
	[Image] [nvarchar](max) NULL,
	[Files] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[Home] [nvarchar](256) NULL,
	[Address] [nvarchar](max) NULL,
	[Province] [nvarchar](256) NULL,
	[District] [nvarchar](256) NULL,
	[CompanyName] [nvarchar](256) NULL,
	[IdentityCard] [nvarchar](256) NULL,
	[IssuedBy] [nvarchar](256) NULL,
	[IssuedDate] [datetime] NULL,
	[Level] [nvarchar](256) NULL,
	[GraduationYear] [nvarchar](256) NULL,
	[Male] [nvarchar](256) NULL,
	[School] [nvarchar](256) NULL,
	[Folk] [nvarchar](256) NULL,
	[NoContract] [nvarchar](256) NULL,
	[NoBHXH] [nvarchar](256) NULL,
	[CurrentContract] [nvarchar](256) NULL,
	[Position] [nvarchar](256) NULL,
	[UserCode] [nvarchar](256) NULL,
	[ContractType] [nvarchar](256) NULL,
	[TGHD] [nvarchar](256) NULL,
	[TGBH] [nvarchar](256) NULL,
	[Bank] [nvarchar](256) NULL,
	[Matrimony] [nvarchar](256) NULL,
	[Salary] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookingMeetingRoom]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BookingMeetingRoom](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[AppointmentDate] [datetime] NOT NULL,
	[Hour] [varchar](50) NOT NULL,
	[Durations] [float] NOT NULL,
	[UserCreated] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Room] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BookingMeetingRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BusinessTrip]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessTrip](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Timer] [nvarchar](100) NULL,
	[Customer] [nvarchar](500) NULL,
	[Contact] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[UserApproved] [nvarchar](450) NULL,
	[Status] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[UserCreated] [nvarchar](450) NULL,
	[Note] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Token] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BusinessTrip] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsLocked] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[ModerateTopics] [bit] NOT NULL,
	[ModeratePosts] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Slug] [nvarchar](450) NOT NULL,
	[Path] [nvarchar](2500) NULL,
	[MetaDescription] [nvarchar](1000) NULL,
	[Image] [nvarchar](200) NULL,
	[SeoTitle] [nvarchar](max) NULL,
	[IsMediHubSc] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ComboDetails]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComboDetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CsScreenId] [bigint] NOT NULL,
	[CsScreenName] [nvarchar](500) NOT NULL,
	[AmountFull] [int] NOT NULL,
	[PriceFull] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_ComboDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Combos]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](500) NULL,
	[Speciality] [nvarchar](500) NOT NULL,
	[Note] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Combos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContactFormPlan]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactFormPlan](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](500) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[VideoLink] [nvarchar](500) NOT NULL,
	[BudgetTotal] [decimal](18, 0) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[ProductName] [nvarchar](500) NULL,
	[ProductType] [nvarchar](500) NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_ContactFormPlan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contract]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CustomerId] [bigint] NULL,
	[UserCreated] [nvarchar](450) NULL,
	[Note] [nvarchar](max) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Files] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[NoContract] [nvarchar](100) NULL,
	[TotalMoney] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CsCreens]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CsCreens](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[Address] [nvarchar](400) NULL,
	[Speciality] [nvarchar](200) NULL,
	[Province] [nvarchar](200) NULL,
	[District] [nvarchar](200) NULL,
	[TotalLed] [int] NOT NULL,
	[TrafficDaily] [int] NOT NULL,
	[IsMonopoly] [bit] NOT NULL,
	[PriceWeekly] [decimal](18, 0) NOT NULL,
	[PriceMonopoly] [decimal](18, 0) NOT NULL,
	[AdvertiseProductsType] [nvarchar](500) NULL,
	[IsHospital] [bit] NOT NULL,
	[Lat] [float] NOT NULL,
	[Ln] [float] NOT NULL,
	[Email] [nvarchar](200) NULL,
	[Phone] [nvarchar](200) NULL,
	[Images] [nvarchar](max) NULL,
	[NumberApprovedDay] [int] NOT NULL,
	[DescriptionPoint] [nvarchar](max) NULL,
	[LinkProfile] [nvarchar](max) NULL,
	[AmountScreenDetails] [nvarchar](max) NULL,
	[AmountWifi] [int] NULL,
 CONSTRAINT [PK_CsCreens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Type] [nvarchar](200) NULL,
	[Phone] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[Status] [nvarchar](200) NULL,
	[Note] [nvarchar](max) NULL,
	[Contact] [nvarchar](200) NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[PIC] [nvarchar](200) NULL,
	[Position] [nvarchar](max) NULL,
	[UserPic] [nvarchar](450) NULL,
	[IsAgency] [bit] NOT NULL,
 CONSTRAINT [PK_Customer_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DictionaryData]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictionaryData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Type] [nvarchar](300) NOT NULL,
	[Note] [nvarchar](300) NULL,
 CONSTRAINT [PK_DictionaryData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[E_ContactForm]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[E_ContactForm](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](500) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[Note] [nvarchar](500) NULL,
	[ProductId] [bigint] NULL,
	[Status] [nvarchar](500) NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserUpdate] [nvarchar](450) NULL,
	[DateUpdate] [datetime] NULL,
 CONSTRAINT [PK_E_ContactForm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[E_Product]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[E_Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Slug] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[IsPublished] [bit] NOT NULL,
	[PublishedOn] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [nvarchar](450) NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedById] [nvarchar](450) NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Specification] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[OldPrice] [decimal](18, 2) NULL,
	[SpecialPrice] [decimal](18, 2) NULL,
	[SpecialPriceStart] [datetimeoffset](7) NULL,
	[SpecialPriceEnd] [datetimeoffset](7) NULL,
	[HasOptions] [bit] NOT NULL,
	[IsVisibleIndividually] [bit] NOT NULL,
	[IsFeatured] [bit] NOT NULL,
	[IsCallForPricing] [bit] NOT NULL,
	[IsAllowToOrder] [bit] NOT NULL,
	[StockQuantity] [int] NOT NULL,
	[Sku] [nvarchar](max) NULL,
	[Gtin] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[VendorId] [bigint] NULL,
	[ThumbnailImage] [nvarchar](max) NULL,
	[ReviewsCount] [int] NOT NULL,
	[RatingAverage] [float] NULL,
	[BrandId] [bigint] NULL,
	[TaxClassId] [bigint] NULL,
 CONSTRAINT [PK_Catalog_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmaiSended]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmaiSended](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ForId] [bigint] NOT NULL,
	[Type] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_EmaiSended] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Files]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[DateCreated] [date] NULL,
	[UserId] [nvarchar](450) NULL,
	[UserName] [nvarchar](max) NULL,
	[IsPublic] [bit] NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogAction]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogAction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](100) NOT NULL,
	[LogContent] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_LogAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[M_Component]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Component](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](128) NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Slug] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_dbo.Component] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[M_DrugLabel]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_DrugLabel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](128) NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Slug] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.DrugLabel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[M_DrugStore]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[M_DrugStore](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[District] [nvarchar](200) NULL,
	[Province] [nvarchar](200) NULL,
	[Area] [nvarchar](400) NULL,
	[DistrictCode] [varchar](50) NULL,
	[ProvinceCode] [varchar](50) NULL,
	[SizeCode] [varchar](50) NULL,
	[Latitue] [varchar](50) NULL,
	[Longtitue] [nvarchar](50) NULL,
	[MediGroupCode] [nvarchar](128) NULL,
	[DrugStoreName] [nvarchar](150) NULL,
	[DrugStoreAddress] [nvarchar](500) NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[StoreOwner] [nvarchar](128) NULL,
	[BankAccount] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](128) NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[StorePhoneNumber] [nvarchar](50) NULL,
	[StandardLevelCode] [varchar](50) NULL,
	[StaffNumm] [int] NULL,
	[Note] [nvarchar](500) NULL,
	[AvatarUrl] [nvarchar](500) NULL,
	[UnitCost] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_dbo.DrugStore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[M_Project]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[M_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[ProjectType] [varchar](50) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ParentProjectId] [int] NULL,
	[ParentProjectTypeId] [varchar](50) NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](128) NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[IsCompleted] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Slug] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[M_ProjectDetail]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_ProjectDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Component] [nvarchar](200) NOT NULL,
	[Label] [nvarchar](200) NOT NULL,
	[Position] [nvarchar](400) NOT NULL,
	[HSize] [float] NOT NULL,
	[VSize] [float] NOT NULL,
	[NumOfArea] [float] NOT NULL,
	[Unit] [nvarchar](200) NOT NULL,
	[CostPayForDrugStore] [decimal](18, 0) NOT NULL,
	[CostPayForProduction] [decimal](18, 0) NOT NULL,
	[DateBeginRent] [datetime] NOT NULL,
	[MonthRent] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Note] [nvarchar](200) NULL,
	[Images] [nvarchar](max) NULL,
 CONSTRAINT [PK_M_ProjectDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewsPost]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsPost](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedById] [nvarchar](450) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[FullContent] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NULL,
	[IsPublished] [bit] NULL,
	[PublishedOn] [datetime] NULL,
	[SeoTitle] [nvarchar](max) NULL,
	[ShortContent] [nvarchar](max) NOT NULL,
	[ImageId] [bigint] NULL,
	[LinkImage] [nvarchar](max) NULL,
	[UpdatedById] [varchar](450) NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[Slug] [nvarchar](500) NOT NULL,
	[Category_Id] [bigint] NULL,
	[BannerLink] [nvarchar](max) NULL,
	[IsSticky] [bit] NOT NULL,
 CONSTRAINT [PK_NewsPost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsPostCategory]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsPostCategory](
	[CategoryId] [bigint] NOT NULL,
	[NewsPostId] [bigint] NOT NULL,
 CONSTRAINT [PK_NewsPostCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,
	[NewsPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentContract]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentContract](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[TotalMoney] [decimal](18, 0) NOT NULL,
	[PaymentPeriod] [nvarchar](100) NOT NULL,
	[DatePayment] [datetime] NOT NULL,
	[DateUpdate] [datetime] NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[Status] [nvarchar](100) NULL,
 CONSTRAINT [PK_PaymentContract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RegisterDateOff]    Script Date: 26/09/2018 10:55:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisterDateOff](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Type] [nvarchar](500) NULL,
	[Reason] [nvarchar](500) NULL,
	[Status] [nvarchar](50) NULL,
	[UserCreate] [nvarchar](450) NOT NULL,
	[UserApproved] [nvarchar](450) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateApproved] [datetime] NULL,
	[DateOffNumber] [float] NOT NULL,
	[FromDateOff] [datetime] NOT NULL,
	[ToDateOff] [datetime] NOT NULL,
	[Token] [nvarchar](450) NULL,
	[Refuse] [nvarchar](450) NULL,
 CONSTRAINT [PK_RegisterDateOff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BookingMeetingRoom]  WITH CHECK ADD  CONSTRAINT [FK_BookingMeetingRoom_AspNetUsers] FOREIGN KEY([UserCreated])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[BookingMeetingRoom] CHECK CONSTRAINT [FK_BookingMeetingRoom_AspNetUsers]
GO
ALTER TABLE [dbo].[BusinessTrip]  WITH CHECK ADD  CONSTRAINT [FK_BusinessTrip_AspNetUsers] FOREIGN KEY([UserCreated])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[BusinessTrip] CHECK CONSTRAINT [FK_BusinessTrip_AspNetUsers]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_AspNetUsers] FOREIGN KEY([UserCreated])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_AspNetUsers]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Customer]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_AspNetUsers] FOREIGN KEY([UserPic])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_AspNetUsers]
GO
ALTER TABLE [dbo].[E_ContactForm]  WITH CHECK ADD  CONSTRAINT [FK_E_ContactForm_E_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[E_Product] ([Id])
GO
ALTER TABLE [dbo].[E_ContactForm] CHECK CONSTRAINT [FK_E_ContactForm_E_Product]
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_File_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_File_AspNetUsers]
GO
ALTER TABLE [dbo].[M_ProjectDetail]  WITH CHECK ADD  CONSTRAINT [FK_M_ProjectDetail_M_ProjectDetail] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[M_Project] ([Id])
GO
ALTER TABLE [dbo].[M_ProjectDetail] CHECK CONSTRAINT [FK_M_ProjectDetail_M_ProjectDetail]
GO
ALTER TABLE [dbo].[NewsPost]  WITH CHECK ADD  CONSTRAINT [FK_NewsPost_AspNetUsers] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[NewsPost] CHECK CONSTRAINT [FK_NewsPost_AspNetUsers]
GO
ALTER TABLE [dbo].[PaymentContract]  WITH CHECK ADD  CONSTRAINT [FK_PaymentContract_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([Id])
GO
ALTER TABLE [dbo].[PaymentContract] CHECK CONSTRAINT [FK_PaymentContract_Contract]
GO
ALTER TABLE [dbo].[RegisterDateOff]  WITH CHECK ADD  CONSTRAINT [FK_RegisterDateOff_AspNetUsers] FOREIGN KEY([UserApproved])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[RegisterDateOff] CHECK CONSTRAINT [FK_RegisterDateOff_AspNetUsers]
GO
ALTER TABLE [dbo].[RegisterDateOff]  WITH CHECK ADD  CONSTRAINT [FK_RegisterDateOff_RegisterDateOff] FOREIGN KEY([UserCreate])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[RegisterDateOff] CHECK CONSTRAINT [FK_RegisterDateOff_RegisterDateOff]
GO
