USE [WNPP66]
GO

/****** Object:  Table [dbo].[T_Monk]    Script Date: 27/9/2566 15:05:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_Monk]') AND type in (N'U'))
DROP TABLE [dbo].[T_Monk]
GO

/****** Object:  Table [dbo].[T_Monk]    Script Date: 27/9/2566 15:05:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Monk](
	-- HEAD -----------------------------------------------
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActiveStatus] [bit] NULL,
	[Language] [int] NULL,
	[RecordStatus] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedByName] [nvarchar](100) NULL,
	[ModifiedByName] [nvarchar](100) NULL,
	[Notation] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	-- Body -----------------------------------------------
	[FirstName] [nvarchar](100) NULL,
	[SurName] [nvarchar](100) NULL,
	[NickName] [nvarchar](50) NULL,
	[FullName] [nvarchar](250) NULL,
	[PID] [nvarchar](15) NULL,
	[PassportNo] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[BloodType] [nvarchar](50) NULL,
	[ContactNumber] [nvarchar](50) NULL,
	[EmergencyNumber] [nvarchar](50) NULL,
	[DrugAllergy] [nvarchar](100) NULL,
	[CongenitalDisease] [nvarchar](100) NULL,
	-- Address -----------------------------------------------
	[AddressText] [nvarchar](500) NULL,
	[Country] [nvarchar](100) NULL,
	[Moo] [nvarchar](2) NULL,
	[Village] [nvarchar](50) NULL,
	[Road] [nvarchar](50) NULL,
	[SubDistrict] [nvarchar](100) NULL,
	[District] [nvarchar](100) NULL,
	[Province] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](5) NULL,
	-- Ecclesiastical -----------------------------------------------
	[MonkRank] [nvarchar](100) NULL,
	[TitleConferredByTheKing] [nvarchar](100) NULL,
	[Preceptor] [nvarchar](250) NULL,
	[OrdinationDate] [datetime] NULL,
	[Monastery] [nvarchar](100) NULL,
	[BelongingTemple] [nvarchar](100) NULL,
	-- Academic standing -----------------------------------------------
	-- [BA, BSc, MA, MSc, PhD]
	[AcademicStanding] [nvarchar](100) NULL,
	[AcademicStandingNo] [nvarchar](100) NULL,
	-- [Graduates of Pali]
	[GraduateOfTheology] [nvarchar](100) NULL,
	[GraduateOfTheologyNo] [nvarchar](100) NULL,
	-- [dhamma scholar advanced level]
	[Theologian] [nvarchar](100) NULL,
	[TheologianNo] [nvarchar](100) NULL,
	-- Social -----------------------------------------------
	[EMail] [nvarchar](100) NULL,
	[LineID] [nvarchar](100) NULL,
	[FaceBook] [nvarchar](100) NULL,
	-- Activity -----------------------------------------------
	-- BaseRequestTable ---------------------------------------
 CONSTRAINT [PK_T_Monk] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[T_Branch_Register]    Script Date: 27/9/2566 15:05:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_Branch_Register]') AND type in (N'U'))
DROP TABLE [dbo].[T_Branch_Register]
GO
/****** Object:  Table [dbo].[T_Branch_Register]    Script Date: 27/9/2566 15:05:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Branch_Register](
	-- HEAD -----------------------------------------------
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActiveStatus] [bit] NULL,
	[LanguageID] [int] NULL,
	[RecordStatus] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedByName] [nvarchar](100) NULL,
	[ModifiedByName] [nvarchar](100) NULL, 
	[Notation] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	
	-- Activity -----------------------------------------------
	
	
	-- Monastery -----------------------------------------------
	[MonasteryName] [nvarchar](250) NULL,
	[MonasteryType] [int] NULL,
	[MonasteryPhoneNO] [nvarchar](50) NULL,
	[DepositaryName] [nvarchar](50) NULL,
	[DepositaryPhoneNO] [nvarchar](50) NULL,
	[MonasteryAreaRai] DECIMAL(5,2)  NULL,
	[MonasteryAreaNgan] DECIMAL(5,2)  NULL,
	[MonasteryAreaWa] DECIMAL(5,2)  NULL,
	[LandRightsDocuments] [nvarchar](50) NULL,
	[DateOfFounding] [datetime] NULL,	
	
	-- Abbot -----------------------------------------------
	[EcclesiasticalTitle] [nvarchar](50) NULL,
	[EcclesiasticalType] [int] NULL,
	[AbbotTitle] [nvarchar](50) NULL,
	[AbbotName] [nvarchar](250) NULL,
	[AbbotType] [int] NULL,
	[AbbotTemple] [nvarchar](250) NULL,
	[Preceptor] [nvarchar](250) NULL,
	[PreceptorTemple] [nvarchar](250) NULL,
	[DateOfAcceptPosition] [datetime] NULL,
	[AbbotPhoneNO] [nvarchar](50) NULL,
	[AbbotEMail] [nvarchar](50) NULL,
	[AbbotLineID] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[DateOfOrdination] [datetime] NULL,
	[AbbotSignDate] [datetime] NULL,
	
	-- Certifier -----------------------------------------------
	[CertifierTitle] [nvarchar](50) NULL,
	[CertifierName] [nvarchar](250) NULL,
	[CertifierTemple] [nvarchar](250) NULL,
	[CertifierTempleType] [int] NULL,
	[CertifierBranch] [nvarchar](50) NULL,
	[CertifierPhoneNO] [nvarchar](50) NULL,
	[CertifierEMail] [nvarchar](50) NULL,
	[CertifierLineID] [nvarchar](50) NULL,
	[CertifierSignDate] [datetime] NULL,
	
	-- Address of Monastery-----------------------------------------------
	[AddressText_Monatery] [nvarchar](500) NULL,
	[HouseNo_Monatery] [nvarchar](50) NULL,
	[Moo_Monatery] [nvarchar](2) NULL,
	[Village_Monatery] [nvarchar](50) NULL,
	[Road_Monatery] [nvarchar](50) NULL,
	[SubDistrict_Monatery] [nvarchar](100) NULL,
	[District_Monatery] [nvarchar](100) NULL,
	[Province_Monatery] [nvarchar](100) NULL,
	[Country_Monatery] [nvarchar](100) NULL,
	[PostCode_Monatery] [nvarchar](5) NULL,
	
	-- Address of Abbot-----------------------------------------------
	[AddressText_Abbot] [nvarchar](500) NULL,
	[HouseNo_Abbot] [nvarchar](50) NULL,
	[Moo_Abbot] [nvarchar](2) NULL,
	[Village_Abbot] [nvarchar](50) NULL,
	[Road_Abbot] [nvarchar](50) NULL,
	[SubDistrict_Abbot] [nvarchar](100) NULL,
	[District_Abbot] [nvarchar](100) NULL,
	[Province_Abbot] [nvarchar](100) NULL,
	[Country_Abbot] [nvarchar](100) NULL,
	[PostCode_Abbot] [nvarchar](5) NULL,
	
	-- Address of Preceptor-----------------------------------------------
	[AddressText_Preceptor] [nvarchar](500) NULL,
	[HouseNo_Preceptor] [nvarchar](50) NULL,
	[Moo_Preceptor] [nvarchar](2) NULL,
	[Village_Preceptor] [nvarchar](50) NULL,
	[Road_Preceptor] [nvarchar](50) NULL,
	[SubDistrict_Preceptor] [nvarchar](100) NULL,
	[District_Preceptor] [nvarchar](100) NULL,
	[Province_Preceptor] [nvarchar](100) NULL,
	[Country_Preceptor] [nvarchar](100) NULL,
	[PostCode_Preceptor] [nvarchar](5) NULL,

	-- Address of Certifier-----------------------------------------------
	[AddressText_Certifier] [nvarchar](500) NULL,
	[HouseNo_Certifier] [nvarchar](50) NULL,
	[Moo_Certifier] [nvarchar](2) NULL,
	[Village_Certifier] [nvarchar](50) NULL,
	[Road_Certifier] [nvarchar](50) NULL,
	[SubDistrict_Certifier] [nvarchar](100) NULL,
	[District_Certifier] [nvarchar](100) NULL,
	[Province_Certifier] [nvarchar](100) NULL,
	[Country_Certifier] [nvarchar](100) NULL,
	[PostCode_Certifier] [nvarchar](5) NULL,

	-- BaseRequestTable ---------------------------------------
 CONSTRAINT [PK_T_Branch_Register] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[T_Branch]') AND type in (N'U'))
DROP TABLE [dbo].[T_Branch]
GO
/****** Object:  Table [dbo].[T_Branch]    Script Date: 27/9/2566 15:05:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Branch](
	-- HEAD -----------------------------------------------
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActiveStatus] [bit] NULL,
	[LanguageID] [int] NULL,
	[RecordStatus] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedByName] [nvarchar](100) NULL,
	[ModifiedByName] [nvarchar](100) NULL, 
	[Notation] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	
	-- Activity -----------------------------------------------
	
	
	-- Monastery -----------------------------------------------
	[BranchName] [nvarchar](250) NULL,
	[BranchType] [int] NULL,

	[MonasteryName] [nvarchar](250) NULL,
	[MonasteryType] [int] NULL,
	[MonasteryPhoneNO] [nvarchar](50) NULL,
	[DepositaryName] [nvarchar](50) NULL,
	[DepositaryPhoneNO] [nvarchar](50) NULL,
	[MonasteryAreaRai] DECIMAL(5,2)  NULL,
	[MonasteryAreaNgan] DECIMAL(5,2)  NULL,
	[MonasteryAreaWa] DECIMAL(5,2)  NULL,
	[LandRightsDocuments] [nvarchar](50) NULL,
	[DateOfFounding] [datetime] NULL,	
	
	-- Abbot -----------------------------------------------
	[EcclesiasticalTitle] [nvarchar](50) NULL,
	[EcclesiasticalType] [int] NULL,
	[AbbotTitle] [nvarchar](50) NULL,
	[AbbotName] [nvarchar](250) NULL,
	[AbbotType] [int] NULL,
	[AbbotTemple] [nvarchar](250) NULL,
	[Preceptor] [nvarchar](250) NULL,
	[PreceptorTemple] [nvarchar](250) NULL,
	[DateOfAcceptPosition] [datetime] NULL,
	[AbbotPhoneNO] [nvarchar](50) NULL,
	[AbbotEMail] [nvarchar](50) NULL,
	[AbbotLineID] [nvarchar](50) NULL,
	[AbbotImageUrl] [nvarchar](250) NULL,
	[DateOfBirth] [datetime] NULL,
	[DateOfOrdination] [datetime] NULL,
	[AbbotSignDate] [datetime] NULL,
	
	-- Address of Monastery-----------------------------------------------
	[AddressText_Monatery] [nvarchar](500) NULL,
	[HouseNo_Monatery] [nvarchar](50) NULL,
	[Moo_Monatery] [nvarchar](2) NULL,
	[Village_Monatery] [nvarchar](50) NULL,
	[Road_Monatery] [nvarchar](50) NULL,
	[SubDistrict_Monatery] [nvarchar](100) NULL,
	[District_Monatery] [nvarchar](100) NULL,
	[Province_Monatery] [nvarchar](100) NULL,
	[Country_Monatery] [nvarchar](100) NULL,
	[PostCode_Monatery] [nvarchar](5) NULL,

	-- BaseRequestTable ---------------------------------------
 CONSTRAINT [PK_T_Branch] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[M_DropDown]') AND type in (N'U'))
DROP TABLE [dbo].[M_DropDown]
GO
/****** Object:  Table [dbo].[M_DropDown]    Script Date: 27/9/2566 15:05:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[M_DropDown](
	-- HEAD -----------------------------------------------
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActiveStatus] [bit] NULL,
	[LanguageID] [int] NULL,
	[RecordStatus] [nvarchar](10) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedByName] [nvarchar](100) NULL,
	[ModifiedByName] [nvarchar](100) NULL, 
	[Notation] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	
	-- Activity -----------------------------------------------
	
	
	-- DropDown -----------------------------------------------
	[DropDownID] [int] NULL,
	[DropDownType] [nvarchar](50) NULL,
	[DropDownTypeSeq] [int] NULL,
	[DropDownName] [nvarchar](250) NULL,
	
	-- BaseRequestTable ---------------------------------------
 CONSTRAINT [PK_M_DropDown] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
