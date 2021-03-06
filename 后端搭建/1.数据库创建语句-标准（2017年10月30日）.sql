/****** Object:  Table [dbo].[SYS_WorkCenterResources]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_WorkCenterResources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[WorkCenterResourcesID] [varchar](30) NOT NULL,
	[WorkCenterID] [varchar](30) NOT NULL,
	[ResourceID] [varchar](30) NOT NULL,
	[ProcessID] [varchar](30) NULL,
	[OperationID] [varchar](30) NULL,
	[IfMain] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_WorkCenterResources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[MES_Parameter]    Script Date: 08/04/2017 15:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MES_Parameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ParameterID] [varchar](30) NOT NULL,
	[Module] [nvarchar](30) NULL,
	[Code] [nvarchar](30) NULL,
	[Name] [nvarchar](120) NULL,
	[Setting] [varchar](30) NULL,
	[Option] [nvarchar](max) NULL,
	[Value] [nvarchar](30) NULL,
	[Status] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL  DEFAULT  GETUTCDATE(),
	[ModifiedLocalTime] [datetime] NOT NULL  DEFAULT  GETDATE(),
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL  DEFAULT  GETUTCDATE(),
	[CreateLocalTime] [datetime] NOT NULL  DEFAULT  GETDATE(),
 CONSTRAINT [PK_MES_Parameter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]



/****** Object:  Table [dbo].[SYS_WorkCenterProcess]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_WorkCenterProcess](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[WorkCenterProcessID] [varchar](30) NOT NULL,
	[WorkCenterID] [varchar](30) NOT NULL,
	[ProcessID] [varchar](30) NOT NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_WorkCenterProcess] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_WorkCenter]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_WorkCenter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[WorkCenterID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NULL,
	[Name] [nvarchar](120) NULL,
	[CalendarID] [varchar](30) NULL,
	[InoutMark] [varchar](30) NULL,
	[DepartmentID] [varchar](30) NULL,
	[ResourceReport] [bit] NULL,
	[IsClass] [bit] NULL,
	[DispatchMode] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_WorkCenter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_UserRoleMapping]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_UserRoleMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[UserID] [varchar](30) NOT NULL,
	[RoleID] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__UserRoleMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_UserOrganizationMapping]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_UserOrganizationMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[UserID] [varchar](30) NOT NULL,
	[OrganizationID] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__UserOrganizationMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_TableProperty]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_TableProperty](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[TablePropertyID] [int] NOT NULL,
	[TableName] [varchar](100) NOT NULL,
	[Database] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__TableProperty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Systems]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Systems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[SystemName] [varchar](100) NOT NULL,
	[SystemNameEN] [varchar](100) NOT NULL,
	[CustomerName] [varchar](100) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[IsInit] [bit] NOT NULL,
	[Sequence] [int] NULL,
	[Zone] [int] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__Systems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[SystemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_SerialNumber]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_SerialNumber](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[SNID] [varchar](30) NOT NULL,
	[TableName] [varchar](100) NOT NULL,
	[TablePropertyID] [int] NOT NULL,
	[FirstSN] [varchar](10) NULL,
	[SplitOne] [varchar](10) NULL,
	[DateID] [varchar](10) NULL,
	[SplitTwo] [varchar](10) NULL,
	[Number] [varchar](10) NOT NULL,
	[IsClear] [bit] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__SerialNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Roles]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[RoleID] [varchar](30) NOT NULL,
	[Name] [nvarchar](120) NOT NULL,
	[Code] [varchar](30) NULL,
	[Level] [tinyint] NULL,
	[Status] [varchar](30) NULL,
	[Description] [varchar](2048) NULL,
	[DescriptionOne] [varchar](50) NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_RoleMenuMapping]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_RoleMenuMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MenuID] [varchar](30) NOT NULL,
	[RoleID] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__RoleMenuMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_RoleMenuButtonMapping]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_RoleMenuButtonMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MenuID] [varchar](30) NOT NULL,
	[RoleID] [varchar](30) NOT NULL,
	[ButtonID] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__RoleMenuButtonMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_RoleMenuActionMapping]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_RoleMenuActionMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MenuID] [varchar](30) NOT NULL,
	[RoleID] [varchar](30) NOT NULL,
	[ActionID] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__RoleMenuActionMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Resources]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Resources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ResourceID] [varchar](30) NOT NULL,
	[Code] [varchar](30) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ClassID] [varchar](30) NULL,
	[GroupID] [varchar](30) NULL,
	[Quantity] [numeric](14, 4) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_Resources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_ResourceDetails]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_ResourceDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ResourceDetailID] [varchar](30) NOT NULL,
	[ResourceID] [varchar](30) NOT NULL,
	[DetailID] [varchar](30) NOT NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_ResourceDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Projects]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Projects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ProjectID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Attribute] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_Projects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_ProcessOperation]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_ProcessOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ProcessOperationID] [varchar](30) NOT NULL,
	[ProcessID] [varchar](30) NOT NULL,
	[OperationID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_ProcessOperation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_PlantArea]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_PlantArea](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[PlantAreaID] [varchar](30) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](120) NULL,
	[PlantID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_PlantArea] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_ParameterType]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_ParameterType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ParameterTypeID] [varchar](30) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
	[DescriptionOne] [varchar](50) NULL,
	[ParentParameterTypeID] [varchar](30) NULL,
	[IsSystem] [bit] NOT NULL,
	[PageNumber] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK__SYS_ParameterType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Parameters]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Parameters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ParameterID] [varchar](30) NOT NULL,
	[ParentParameterID] [varchar](30) NULL,
	[Name] [nvarchar](120) NULL,
	[Code] [nvarchar](30) NULL,
	[Description] [nvarchar](120) NULL,
	[DescriptionOne] [nvarchar](120) NULL,
	[ParameterTypeID] [varchar](30) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsEnable] [tinyint] NOT NULL,
	[UsingType] [tinyint] NULL,
	[IsSystem] [bit] NOT NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK__SYS_Parameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_OrganizationRoleMapping]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_OrganizationRoleMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[OrganizationID] [varchar](30) NOT NULL,
	[RoleID] [varchar](30) NOT NULL,
	[Order] [tinyint] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__OrganizationRoleMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_OrganizationClass]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_OrganizationClass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[OrganizationClassID] [varchar](30) NOT NULL,
	[OrganizationID] [varchar](30) NULL,
	[ClassID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_OrganizationClass] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Organization]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Organization](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[OrganizationID] [varchar](30) NOT NULL,
	[ParentOrganizationID] [varchar](30) NULL,
	[Name] [nvarchar](120) NOT NULL,
	[Code] [nvarchar](30) NULL,
	[Manager] [varchar](30) NULL,
	[Type] [varchar](30) NULL,
	[Level] [tinyint] NULL,
	[FeeType] [tinyint] NULL,
	[HeadCount] [int] NULL,
	[RealHeadCount] [int] NULL,
	[Status] [tinyint] NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsLegal] [bit] NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[PlantID] [varchar](30) NULL,
	[IfTop] [bit] NULL,
 CONSTRAINT [PK__SYS_Organization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_MFCLog]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_MFCLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[UserID] [varchar](30) NOT NULL,
	[UserName] [varchar](30) NULL,
	[Tag] [varchar](2048) NULL,
	[Position] [varchar](100) NULL,
	[Target] [varchar](100) NULL,
	[Type] [varchar](100) NULL,
	[Message] [nvarchar](max) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__MFCLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_MESUsers]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_MESUsers](
[ID] [int] IDENTITY(1,1) NOT NULL,
[SystemID] [varchar](50)  NOT NULL,
[MESUserID] [varchar](30) NOT NULL,
[Account] [nvarchar](50) NOT NULL,
[Password] [varchar](32) NOT NULL,
[UserType] [tinyint]  NULL DEFAULT 10,
[Status] [tinyint]  NULL DEFAULT 0,
[UserName] [nvarchar](50) NULL,
[EnglishName] [nvarchar](60) NULL,
[Emplno] [nvarchar](50) NULL,
[CardCode] [nvarchar](50) NULL,
[JobTitle] [varchar](30) NULL,
[Sex] [bit] NULL,
[Email] [nvarchar](60) NULL,
[Brith] [varchar](10) NULL,
[IDcard] [varchar](18) NULL,
[InTime] [varchar](10) NULL,
[Type] [varchar](30) NULL,
[Mobile] [varchar](11) NULL,
[LogonCount] [int] NULL,
[LastLogonTime] [datetime] NULL,
[ConfigJSON] [varchar](2048)  NULL,
[Sequence] [int]  NULL,
[Language] [int]  NULL DEFAULT 3,
[Comments] [nvarchar](max) NULL,
[Modifier] [varchar](30) NOT NULL,
[ModifiedTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[ModifiedLocalTime] [datetime] NULL DEFAULT  GETDATE(),
[Creator] [varchar](30) NOT NULL,
[CreateTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[CreateLocalTime] [datetime] NULL DEFAULT  GETDATE(),
CONSTRAINT [PK_SYS_MESUsers] PRIMARY KEY CLUSTERED 
(
[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Menus]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_Menus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MenuID] [varchar](30) NOT NULL,
	[ParentMenuID] [varchar](30) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[URL] [varchar](128) NULL,
	[IconClass] [varchar](50) NULL,
	[IconURL] [varchar](100) NULL,
	[IsParameter] [bit] NULL,
	[IsVisible] [bit] NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__Menus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_MenuColumn]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_MenuColumn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MenuID] [varchar](30) NOT NULL,
	[Column] [nvarchar](max) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_MenuColumn] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_MenuButtonMapping]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_MenuButtonMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MenuID] [varchar](30) NOT NULL,
	[ButtonID] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__MenuButtonMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_MenuActionMapping]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_MenuActionMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MenuID] [varchar](30) NOT NULL,
	[ActionID] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__MenuActionMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Manufacturers]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Manufacturers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ManufacturerID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](120) NULL,
	[Type] [varchar](30) NULL,
	[Contacts] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[MESUserID] [varchar](30) NULL,
	[ClassOne] [varchar](30) NULL,
	[ClassTwo] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_LogonHistory]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_LogonHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[UserID] [varchar](30) NOT NULL,
	[UserName] [varchar](30) NOT NULL,
	[Tag] [varchar](2048) NULL,
	[HostName] [varchar](50) NULL,
	[HostIP] [varchar](50) NULL,
	[LogonCity] [varchar](50) NULL,
	[LogonTime] [datetime] NOT NULL,
 CONSTRAINT [PK__LogonHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_LanguageLib]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_LanguageLib](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[LanguageLibID] [varchar](30) NOT NULL,
	[TableID] [varchar](30) NOT NULL,
	[RowID] [varchar](30) NOT NULL,
	[Field] [nvarchar](128) NULL,
	[OriginalLanguage] [varchar](30) NULL,
	[OriginalContent] [nvarchar](512) NULL,
	[LanguageCode] [varchar](30) NULL,
	[LanguageContentOne] [nvarchar](512) NULL,
	[LanguageContentTwo] [nvarchar](512) NULL,
	[IsDefault] [bit] NULL,
	[Tag] [varchar](2048) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__SYS_LanguageLib] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Items]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Items](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ItemID] [varchar](30) NOT NULL,
	[Code] [nvarchar](60) NOT NULL,
	[Name] [nvarchar](120) NULL,
	[Specification] [nvarchar](120) NULL,
	[Status] [varchar](30) NULL,
	[Unit] [varchar](30) NULL,
	[ClassOne] [varchar](30) NULL,
	[ClassTwo] [varchar](30) NULL,
	[ClassThree] [varchar](30) NULL,
	[ClassFour] [varchar](30) NULL,
	[ClassFive] [varchar](30) NULL,
	[AuxUnit] [varchar](30) NULL,
	[AuxUnitRatio] [numeric](9, 4) NULL,
	[IsCutMantissa] [bit] NULL,
	[CutMantissa] [varchar](30) NULL,
	[Type] [varchar](30) NULL,
	[Drawing] [nvarchar](30) NULL,
	[PartSource] [varchar](30) NULL,
	[BarCord] [nvarchar](50) NULL,
	[GroupID] [varchar](50) NULL,
	[Lot] [bit] NULL,
	[OverRate] [numeric](5, 2) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[SerialPart] [bit] NULL,
	[KeyPart] [bit] NULL,
	[LotMethod] [varchar](30) NULL,
	[LotClassID] [varchar](30) NULL,
 CONSTRAINT [PK_SYS_Items] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_ItemAttributes]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_ItemAttributes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ItemAttributeID] [varchar](30) NOT NULL,
	[ItemID] [varchar](30) NOT NULL,
	[AttributeID] [varchar](30) NOT NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_ItemAttributes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_InterfaceConfiguration]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_InterfaceConfiguration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[InterfaceConfigurationID] [varchar](30) NOT NULL,
	[EMOUserID] [varchar](30) NOT NULL,
	[MenuID] [varchar](30) NOT NULL,
	[Content] [varchar](128) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__InterfaceConfiguration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_DocumentTypeSetting]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_DocumentTypeSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[DTSID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Status] [varchar](30) NULL,
	[TypeID] [varchar](30) NULL,
	[IfDefault] [bit] NULL,
	[GiveWay] [varchar](30) NULL,
	[YearLength] [tinyint] NULL,
	[MonthLength] [tinyint] NULL,
	[DateLength] [tinyint] NULL,
	[Attribute] [bit] NULL,
	[Length] [tinyint] NULL,
	[YearType] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[CodeLength] [tinyint] NULL,
 CONSTRAINT [PK_SYS_DocumentTypeSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_DocumentAutoNumber]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_DocumentAutoNumber](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[DocumentAutoNumberID] [varchar](30) NOT NULL,
	[ClassID] [varchar](30) NULL,
	[DefaultCharacter] [nvarchar](30) NULL,
	[Num] [int] NULL,
	[Attribute] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_DocumentAutoNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_DocumentAuthority]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_DocumentAuthority](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[DocumentAuthorityID] [varchar](30) NOT NULL,
	[ClassID] [varchar](30) NULL,
	[Attribute] [bit] NULL,
	[AuthorityID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_DocumentAuthority] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Customers]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Customers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CustomerID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](120) NULL,
	[Contacts] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[MESUserID] [varchar](30) NULL,
	[ClassOne] [varchar](30) NULL,
	[ClassTwo] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_Customers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_ControlParameters]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_ControlParameters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ControlParameterID] [varchar](30) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](2048) NULL,
	[DescriptionOne] [varchar](50) NULL,
	[Key] [varchar](50) NULL,
	[value] [varchar](2048) NULL,
	[DateType] [tinyint] NULL,
	[ParameterTypeID] [varchar](30) NOT NULL,
	[PageNumber] [varchar](30) NULL,
	[IsEnable] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsSystem] [bit] NOT NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK__SYS_ControlParameters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Class]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Class](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ClassID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[CalendarID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[Name] [nvarchar](120) NULL,
	[OnTime] [varchar](5) NULL,
	[OffTime] [varchar](5) NULL,
	[OffHour] [numeric](6, 4) NULL,
	[WorkHour] [numeric](6, 4) NULL,
	[CrossDay] [bit] NULL,
 CONSTRAINT [PK_SYS_Class] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_CalendarDetails]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_CalendarDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CalendarDetailID] [varchar](30) NOT NULL,
	[CalendarID] [varchar](30) NOT NULL,
	[Yeardate] [datetime] NULL,
	[Wkhour] [numeric](14, 2) NULL,
	[Status] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_CalendarDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Calendar]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Calendar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CalendarID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](120) NULL,
	[Ifdefault] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_Calendar] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Buttons]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_Buttons](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ButtonID] [varchar](30) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Icon] [varchar](50) NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__Buttons] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_AutoNumberRecord]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_AutoNumberRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[AutoNumberRecordID] [varchar](30) NOT NULL,
	[AutoNumberID] [varchar](30) NOT NULL,
	[Prevchar] [varchar](50) NOT NULL,
	[Num] [int] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_AutoNumberRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_AutoNumber]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_AutoNumber](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[AutoNumberID] [varchar](30) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](128) NULL,
	[DefaultCharacter] [varchar](50) NULL,
	[YearLength] [tinyint] NULL,
	[MonthLength] [tinyint] NULL,
	[DateLength] [tinyint] NULL,
	[NumLength] [tinyint] NULL,
	[Length] [tinyint] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_AutoNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Attachments]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_Attachments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[AttachmentID] [varchar](30) NOT NULL,
	[Name] [varchar](1024) NOT NULL,
	[OriginalName] [varchar](1024) NOT NULL,
	[Path] [varchar](1024) NOT NULL,
	[UploadTime] [datetime] NOT NULL,
	[IsNotInit] [bit] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Default] [bit] NULL,
	[Status] [varchar](30) NULL,
	[ObjectID] [varchar](30) NULL,
	[Type] [varchar](30) NULL,
 CONSTRAINT [PK__Attachments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_AlternativeGroupDetails]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SYS_AlternativeGroupDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[AGDetailID] [varchar](30) NOT NULL,
	[GroupID] [varchar](30) NULL,
	[DetailID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SYS_AlternativeGroupDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SYS_Actions]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[SYS_Actions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ActionID] [varchar](30) NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Icon] [varchar](50) NULL,
	[Sequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__Actions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_TransferOrder]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_TransferOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[TransferOrderID] [varchar](30) NOT NULL,
	[TransferNo] [nvarchar](30) NOT NULL,
	[Date] [datetime] NULL,
	[Sequence] [int] NULL,
	[Type] [varchar](30) NULL,
	[CompletionOrderID] [varchar](30) NULL,
	[InspectionDocumentID] [varchar](30) NULL,
	[TaskDispatchID] [varchar](30) NULL,
	[FabricatedMotherID] [varchar](30) NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[FabMoOperationID] [varchar](30) NULL,
	[ItemID] [varchar](30) NULL,
	[ProcessID] [varchar](30) NULL,
	[OperationID] [varchar](30) NULL,
	[TransferQuantity] [numeric](18, 6) NULL,
	[ActualTransferQuantity] [numeric](18, 6) NULL,
	[Status] [varchar](30) NOT NULL,
	[AcceptUser] [varchar](30) NULL,
	[NextFabMoProcessID] [varchar](30) NULL,
	[NextFabMoOperationID] [varchar](30) NULL,
	[NextProcessID] [varchar](30) NULL,
	[NextOperationID] [varchar](30)  NULL,
	[IPQCSequence] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_TransferOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_TaskEquipmentProject]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_TaskEquipmentProject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[TaskEquipmentProjectID] [varchar](30) NOT NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[FabMoOperationID] [varchar](30) NULL,
	[TaskDispatchID] [varchar](30) NULL,
	[EquipmentID] [varchar](30) NULL,
	[ProjectID] [nvarchar](50) NOT NULL,
	[IfCollection] [bit] NULL,
	[CollectionWay] [varchar](30) NULL,
	[StandardValue] [nvarchar](120) NULL,
	[MaxValue] [nvarchar](120) NULL,
	[MinValue] [nvarchar](120) NULL,
	[RecordValue] [nvarchar](120) NULL,
	[IfEntryRecord] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_TaskEquipmentProject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_TaskDispatchResource]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_TaskDispatchResource](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[TaskDispatchResourceID] [varchar](30) NOT NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[FabMoOperationID] [varchar](30) NULL,
	[TaskDispatchID] [varchar](30) NULL,
	[Sequence] [varchar](4) NULL,
	[ExpectedDate] [datetime] NULL,
	[ExpectedTime] [datetime] NULL,
	[ActualDate] [datetime] NULL,
	[ActualTime] [datetime] NULL,
	[Type] [varchar](30) NULL,
	[ResourceID] [varchar](30) NULL,
	[EquipmentID] [varchar](30) NULL,
	[IfMain] [bit] NULL,
	[ResourceClassID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_TaskDispatchResource] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_TaskDispatch]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_TaskDispatch](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[TaskDispatchID] [varchar](30) NOT NULL,
	[TaskNo] [nvarchar](120) NOT NULL,
	[Sequence] [varchar](4) NULL,
	[FabricatedMotherID] [varchar](30) NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[FabMoOperationID] [varchar](30) NULL,
	[MoSequence] [int] NULL,
	[ItemID] [varchar](30) NULL,
	[ProcessID] [varchar](30) NULL,
	[OperationID] [varchar](30) NULL,
	[StartDate] [datetime] NULL,
	[FinishDate] [datetime] NULL,
	[IsDispatch] [bit] NULL,
	[MESUserID] [varchar](30) NULL,
	[DispatchDate] [datetime] NULL,
	[DispatchQuantity] [numeric](18, 6) NULL,
	[ClassID] [varchar](30) NULL,
	[InDateTime] [datetime] NULL,
	[OutDateTime] [datetime] NULL,
	[IsIP] [bit] NULL,
	[IsFPI] [bit] NULL,
	[IsOSI] [bit] NULL,
	[InspectionGroupID] [varchar](30) NULL,
	[FPIPass] [bit] NULL,
	[FinishQuantity] [numeric](18, 6) NULL,
	[ScrapQuantity] [numeric](18, 6) NULL,
	[DiffQuantity] [numeric](18, 6) NULL,
	[RepairQuantity] [numeric](18, 6) NULL,
	[LaborHour] [numeric](18, 0) NULL,
	[UnLaborHour] [numeric](18, 0) NULL,
	[MachineHour] [numeric](18, 0) NULL,
	[UnMachineHour] [numeric](18, 0) NULL,
	[InMESUserID] [varchar](30) NULL,
	[OutMESUserID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_TaskDispatch] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_ProcessOperationRelationShip]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_ProcessOperationRelationShip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[PORSID] [varchar](30) NOT NULL,
	[ItemOperationID] [varchar](30) NOT NULL,
	[PreItemOperationID] [varchar](30) NULL,
	[FinishOperation] [bit] NULL,
	[IfMain] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[ItemProcessID] [varchar](30) NULL,
 CONSTRAINT [PK_SFC_ProcessOperationRelationShip] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_ItemResource]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_ItemResource](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ItemResourceID] [varchar](30) NOT NULL,
	[ItemProcessID] [varchar](30) NULL,
	[ItemOperationID] [varchar](30) NULL,
	[ResourceID] [varchar](30) NULL,
	[Type] [varchar](30) NULL,
	[IfMain] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_ItemResource] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_ItemProcessRelationShip]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_ItemProcessRelationShip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[IPRSID] [varchar](30) NOT NULL,
	[ItemID] [varchar](30) NOT NULL,
	[ItemProcessID] [varchar](30) NOT NULL,
	[PreItemProcessID] [varchar](30) NULL,
	[FinishProcess] [bit] NULL,
	[IfMain] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_ItemProcessRelationShip] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_ItemProcessAlternativeRelationShip]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_ItemProcessAlternativeRelationShip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[IPARSID] [varchar](30) NOT NULL,
	[ItemProcessID] [varchar](30) NOT NULL,
	[Sequence] [varchar](4) NULL,
	[ProcessID] [varchar](30) NULL,
	[WorkCenterID] [varchar](30) NULL,
	[Unit] [varchar](30) NULL,
	[UnitRatio] [numeric](10, 4) NULL,
	[Price] [numeric](10, 4) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_ItemProcessAlternativeRelationShip] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_ItemProcess]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_ItemProcess](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ItemProcessID] [varchar](30) NOT NULL,
	[ItemID] [varchar](30) NOT NULL,
	[Sequence] [varchar](4) NULL,
	[ProcessID] [varchar](30) NULL,
	[WorkCenterID] [varchar](30) NULL,
	[AuxUnit] [varchar](30) NULL,
	[AuxUnitRatio] [numeric](10, 4) NULL,
	[Price] [numeric](10, 4) NULL,
	[ResourceReport] [bit] NULL,
	[StandardTime] [int] NULL,
	[PrepareTime] [int] NULL,
	[IsIP] [bit] NULL,
	[IsFPI] [bit] NULL,
	[IsOSI] [bit] NULL,
	[InspectionGroupID] [varchar](30) NULL,
	[IfRC] [bit] NULL,
	[RoutID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_ItemProcess] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_ItemOperation]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_ItemOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ItemOperationID] [varchar](30) NOT NULL,
	[ItemProcessID] [varchar](30) NOT NULL,
	[Sequence] [varchar](4) NULL,
	[OperationID] [varchar](30) NULL,
	[ProcessID] [varchar](30) NULL,
	[WorkCenterID] [varchar](30) NULL,
	[Unit] [varchar](30) NULL,
	[UnitRatio] [numeric](10, 4) NULL,
	[StandardTime] [int] NULL,
	[PrepareTime] [int] NULL,
	[IsIP] [bit] NULL,
	[IsFPI] [bit] NULL,
	[IsOSI] [bit] NULL,
	[InspectionGroupID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_ItemOperation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_ItemMaterial]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_ItemMaterial](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ItemMaterialID] [varchar](30) NOT NULL,
	[ItemProcessID] [varchar](30) NULL,
	[ItemOperationID] [varchar](30) NULL,
	[Sequence] [varchar](4) NULL,
	[ItemID] [varchar](30) NULL,
	[BasicQuantity] [numeric](16, 8) NULL,
	[AttritionRate] [numeric](5, 3) NULL,
	[UseQuantity] [numeric](16, 8) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_ItemMaterial] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_FabricatedMother]    Script Date: 07/27/2017 01:00:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_FabricatedMother](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[FabricatedMotherID] [varchar](30) NOT NULL,
	[MoNo] [varchar](30) NOT NULL,
	[Date] [datetime] NULL,
	[Version] [varchar](30) NULL,
	[SplitSequence] [int] NULL,
	[BatchNumber] [varchar](30) NULL,
	[ItemID] [varchar](30) NULL,
	[Quantity] [numeric](14, 4) NULL,
	[UnitID] [varchar](30) NULL,
	[StartDate] [datetime] NULL,
	[FinishDate] [datetime] NULL,
	[OrderNo] [varchar](30) NULL,
	[OrderQuantity] [numeric](14, 4) NULL,
	[CustomerID] [varchar](30) NULL,
	[MESUserID] [varchar](30) NULL,
	[ShipmentDate] [datetime] NULL,
	[OrganizationID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[StorageQuantity] [numeric](14, 4) NULL,
	[SeparateQuantity] [numeric](14, 4) NULL,
	[OverRate] [numeric](5, 2) NULL,
	[Source] [varchar](30) NULL,
	[OriginalFabricatedMotherID] [varchar](30) NULL,
	[ControlUserID] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[ApproveUserID] [varchar](30) NULL,
	[ApproveDate] [datetime] NULL,
	[RoutID] [varchar](30) NULL,
 CONSTRAINT [PK_SFC_FabricatedMother] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_FabMoResource]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_FabMoResource](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[FabMoResourceID] [varchar](30) NOT NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[FabMoOperationID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[ResourceID] [varchar](30) NOT NULL,
	[Quantity] [int] NULL,
	[Type] [varchar](30) NULL,
	[IfMain] [bit] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_FabMoResource] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_FabMoRelationship]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_FabMoRelationship](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[FabMoRelationshipID] [varchar](30) NOT NULL,
	[FabricatedMotherID] [varchar](30) NOT NULL,
	[FabMoProcessID] [varchar](30) NOT NULL,
	[PreFabMoProcessID] [varchar](30) NULL,
	[IfMain] [bit] NULL,
	[IfLastProcess] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_FabMoRelationship] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_FabMoProcess]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_FabMoProcess](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[FabMoProcessID] [varchar](30) NOT NULL,
	[FabricatedMotherID] [varchar](30) NULL,
	[ProcessID] [varchar](30) NOT NULL,
	[Sequence] [varchar](4) NULL,
	[WorkCenterID] [varchar](30) NULL,
	[StartDate] [datetime] NULL,
	[FinishDate] [datetime] NULL,
	[Quantity] [numeric](14, 4) NULL,
	[SeparateQuantity] [numeric](14, 4) NULL,
	[OrderQuantity] [numeric](14, 4) NULL,
	[FinProQuantity] [numeric](14, 4) NULL,
	[OutProQuantity] [numeric](14, 4) NULL,
	[ScrappedQuantity] [numeric](14, 4) NULL,
	[DifferenceQuantity] [numeric](14, 4) NULL,
	[RepairQuantity] [numeric](18, 6) NULL DEFAULT 0,
	[PreProQuantity] [numeric](14, 4) NULL,
	[AssignQuantity] [numeric](14, 4) NULL,
	[UnitID] [varchar](30) NULL,
	[UnitRate] [numeric](10, 4) NULL,
	[StandardTime] [int] NULL,
	[PrepareTime] [int] NULL,
	[Status] [varchar](30) NULL,
	[BeginDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[TaskNo] [varchar](60) NULL,
	[OriginalFabMoProcessID] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[IsEnableOperation] [bit] NULL,
	[Price] [numeric](10, 4) NULL,
	[SourceID] [varchar](30) NULL,
 CONSTRAINT [PK_SFC_FabMoProcess] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_FabMoOperationRelationship]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_FabMoOperationRelationship](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[FabMoOperationRelationshipID] [varchar](30) NOT NULL,
	[FabricatedMotherID] [varchar](30) NULL,
	[FabMoProcessID] [varchar](30) NOT NULL,
	[FabMoOperationID] [varchar](30) NOT NULL,
	[PreFabMoOperationID] [varchar](30) NULL,
	[IsLastOperation] [bit] NULL,
	[IfMain] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_FabMoOperationRelationship] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_FabMoOperation]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_FabMoOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[FabMoOperationID] [varchar](30) NOT NULL,
	[FabricatedMotherID] [varchar](30) NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[OperationID] [varchar](30) NULL,
	[Sequence] [varchar](4) NOT NULL,
	[UnitID] [varchar](30) NULL,
	[UnitRate] [numeric](10, 4) NULL,
	[StartDate] [datetime] NULL,
	[FinishDate] [datetime] NULL,
	[Quantity] [numeric](14, 4) NULL,
	[FinProQuantity] [numeric](14, 4) NULL,
	[OutProQuantity] [numeric](14, 4) NULL,
	[ScrappedQuantity] [numeric](14, 4) NULL,
	[DifferenceQuantity] [numeric](14, 4) NULL,
        [RepairQuantity] [numeric](18, 6) NULL DEFAULT 0,
	[PreProQuantity] [numeric](14, 4) NULL,
	[AssignQuantity] [numeric](14, 4) NULL,
	[ResourceReport] [bit] NULL,
	[StandardTime] [int] NULL,
	[PrepareTime] [int] NULL,
	[TaskNo] [varchar](60) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[BeginDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[SourceID] [varchar](30) NULL,
 CONSTRAINT [PK_SFC_FabMoOperation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_FabMoItem]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_FabMoItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[FabMoItemID] [varchar](30) NOT NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[FabMoOperationID] [varchar](30) NULL,
	[Sequence] [varchar](30) NULL,
	[ItemID] [varchar](30) NULL,
	[BaseQuantity] [numeric](16, 8) NULL,
	[AttritionRate] [numeric](5, 3) NULL,
	[UseQuantity] [numeric](16, 8) NULL,
	[ActualQuantity] [numeric](16, 8) NULL,
	[Crityn] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_FabMoItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_FabMoAltRelShip]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_FabMoAltRelShip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[FabMoAltRelShipID] [varchar](30) NOT NULL,
	[FabricatedMotherID] [varchar](30) NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[ProcessID] [varchar](30) NULL,
	[WorkCenterID] [varchar](30) NULL,
	[Unit] [varchar](30) NULL,
	[UnitRatio] [numeric](10, 4) NULL,
	[Price] [numeric](10, 4) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_FabMoAltRelShip] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_CompletionResource]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_CompletionResource](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CompletionResourceID] [varchar](30) NOT NULL,
	[CompletionOrderID] [varchar](30) NOT NULL,
	[Sequence] [int] NULL,
	[ResourceClassID] [varchar](30) NULL,
	[EquipmentID] [varchar](30) NULL,
	[Hour] [BigInt] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_CompletionResource] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_CompletionOrder]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_CompletionOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CompletionOrderID] [varchar](30) NOT NULL,
	[CompletionNo] [nvarchar](30) NULL,
	[Date] [datetime] NULL,
	[Sequence] [int] NULL,
	[Type] [varchar](30) NULL,
	[TaskDispatchID] [varchar](30) NULL,
	[FabricatedMotherID] [varchar](30) NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[FabMoOperationID] [varchar](30) NULL,
	[OriginalCompletionOrderID] [varchar](30) NULL,
	[InspectionID] [varchar](30) NULL,
	[ItemID] [varchar](30) NULL,
	[ProcessID] [varchar](30) NULL,
	[OperationID] [varchar](30) NULL,
	[ClassID] [varchar](30) NULL,
	[NextFabMoProcessID] [varchar](30) NULL,
	[NextFabMoOperationID] [varchar](30) NULL,
	[NextProcessID] [varchar](30) NULL,
	[NextOperationID] [varchar](30) NULL,
	[IsIF] [bit] NULL,
	[InspectionGroupID] [varchar](30) NULL,
	[FinProQuantity] [numeric](18, 6) NULL,
	[ScrappedQuantity] [numeric](18, 6) NULL,
	[DifferenceQuantity] [numeric](18, 6) NULL,
	[RepairQuantity] [numeric](18, 6) NULL,
	[InspectionQuantity] [numeric](18, 6) NULL,
	[LaborHour] [BigInt] NULL,
	[UnLaborHour] [BigInt] NULL,
	[MachineHour] [BigInt] NULL,
	[UnMachineHour] [BigInt] NULL,
	[DTSID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[ReasonID] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_CompletionOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_BatchAttributeDetails]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_BatchAttributeDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[BatchAttributeDetailID] [varchar](30) NOT NULL,
	[CompletionOrderID] [varchar](30) NOT NULL,
	[BatchAttributeID] [varchar](30) NOT NULL,
	[Sequence] [int] NULL,
	[AttributeID] [varchar](30) NULL,
	[AttributeValue] [nvarchar](60) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_BatchAttributeDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_BatchAttribute]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_BatchAttribute](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[BatchAttributeID] [varchar](30) NOT NULL,
	[CompletionOrderID] [varchar](30) NOT NULL,
	[Sequence] [int] NULL,
	[BatchNo] [nvarchar](50) NULL,
	[EffectDate] [datetime] NULL,
	[Quantity] [numeric](18, 6) NULL,
	[AutoNumberRecordID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_BatchAttribute] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_AbnormalQuantity]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_AbnormalQuantity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[AbnormalQuantityID] [varchar](30) NOT NULL,
	[CompletionOrderID] [varchar](30) NOT NULL,
	[Sequence] [int] NULL,
	[Type] [varchar](30) NULL,
	[ReasonID] [varchar](30) NULL,
	[Quantity] [numeric](18, 6) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_AbnormalQuantity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_AbnormalHour]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_AbnormalHour](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[AbnormalHourID] [varchar](30) NOT NULL,
	[CompletionOrderID] [varchar](30) NOT NULL,
	[Sequence] [int] NULL,
	[Type] [varchar](30) NULL,
	[GroupID] [varchar](30) NULL,
	[ReasonID] [varchar](30) NULL,
	[Hour] [BigInt] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_AbnormalHour] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SFC_AbnormalDetails]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SFC_AbnormalDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[AbnormalDetailID] [varchar](30) NOT NULL,
	[FabMoProcessID] [varchar](30) NULL,
	[FabMoOperationID] [varchar](30) NULL,
	[TaskDispatchID] [varchar](30) NULL,
	[ReasonID] [varchar](30) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[EquipmentID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SFC_AbnormalDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_StaInsSpeSetting]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_StaInsSpeSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[StaInsSpeSettingID] [varchar](30) NOT NULL,
	[SettingType] [varchar](30) NULL,
	[PartID] [varchar](30) NULL,
	[InspectionType] [varchar](30) NULL,
	[ProcessID] [varchar](30) NULL,
	[OperationID] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[CategoryID] [varchar](30) NULL,
	[InspectionMethod] [varchar](30) NULL,
	[InspectionDay] [int] NULL,
	[InspectionStandard] [nvarchar](120) NULL,
	[InspectionProjectID] [varchar](30) NULL,
	[Attribute] [varchar](30) NULL,
	[AQL] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_StaInsSpeSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_SamplingSetting]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_SamplingSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[SamplingSettingID] [varchar](30) NOT NULL,
	[CategoryID] [varchar](30) NOT NULL,
	[InspectionMethod] [varchar](30) NOT NULL,
	[Disadvantages] [varchar](30) NULL,
	[AQL] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_SamplingSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_InspectionProject]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_InspectionProject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[InspectionProjectID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NULL,
	[Name] [nvarchar](120) NULL,
	[InspectionStandard] [nvarchar](120) NULL,
	[InspectionLevel] [varchar](30) NULL,
	[Disadvantages] [varchar](30) NULL,
	[Attribute] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_InspectionProject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_InspectionDocumentRemark]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_InspectionDocumentRemark](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[InspectionDocumentRemarkID] [varchar](30) NOT NULL,
	[Sequence] [smallint] NULL,
	[Remark] [varchar](max) NULL,
	[Status] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[InspectionDocumentID] [varchar](30) NOT NULL,
	[InspectionDocumentDetailID] [varchar](30) NULL,
 CONSTRAINT [PK_QCS_InspectionDocumentRemark] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_InspectionDocumentReason]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_InspectionDocumentReason](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[InspectionDocumentReasonID] [varchar](30) NOT NULL,
	[Sequence] [int] NOT NULL,
	[ReasonID] [varchar](30) NULL,
	[Status] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[InspectionDocumentID] [varchar](30) NULL,
	[InspectionDocumentDetailID] [varchar](30) NULL,
 CONSTRAINT [PK_QCS_InspectionDocumentReason] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_InspectionDocumentDetails]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_InspectionDocumentDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[InspectionDocumentDetailID] [varchar](30) NOT NULL,
	[InspectionDocumentID] [varchar](30) NOT NULL,
	[Sequence] [int] NOT NULL,
	[InspectionMethod] [varchar](30) NOT NULL,
	[InspectionClassID] [varchar](30) NOT NULL,
	[InspectionMethodID] [varchar](30) NOT NULL,
	[InspectionItemID] [varchar](30) NOT NULL,
	[InspectionLevelID] [varchar](30) NOT NULL,
	[InspectionFaultID] [varchar](30) NOT NULL,
	[SampleQuantity] [numeric](10, 4) NULL,
	[Aql] [varchar](30) NULL,
	[AcQuantity] [numeric](10, 4) NULL,
	[ReQuantity] [numeric](10, 4) NULL,
	[NGquantity] [numeric](10, 4) NULL,
	[Attribute] [varchar](max) NULL,
	[QualityControlDecision] [varchar](30) NULL,
	[Status] [varchar](30) NOT NULL,
	[AttributeType] [varchar](30) NULL,
	[InspectionStandard] [nvarchar](120) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_InspectionDocumentDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_InspectionDocument]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_InspectionDocument](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[InspectionDocumentID] [varchar](30) NOT NULL,
	[InspectionNo] [nvarchar](30) NOT NULL,
	[DocumentDate] [datetime] NULL,
	[ItemID] [varchar](30) NOT NULL,
	[InspectionMethod] [varchar](30) NOT NULL,
	[CompletionOrderID] [varchar](30) NULL,
	[TaskDispatchID] [varchar](30) NOT NULL,
	[InspectionDate] [datetime] NULL,
	[InspectionUserID] [varchar](50) NULL,
	[QualityControlDecision] [varchar](30) NULL,
	[FinQuantity] [numeric](18, 6) NOT NULL,
	[InspectionQuantity] [numeric](18, 6) NOT NULL,
	[ScrappedQuantity] [numeric](18, 6) NULL,
	[NGquantity] [numeric](18, 6) NULL,
	[OKQuantity] [numeric](18, 6) NULL,
	[InspectionFlag] [bit] NOT NULL,
	[Status] [varchar](30) NOT NULL,
	[ConfirmUserID] [varchar](30) NULL,
	[ConfirmDate] [datetime] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_InspectionDocument] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_GroupItem]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_GroupItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[GroupItemID] [varchar](30) NOT NULL,
	[GroupID] [varchar](30) NOT NULL,
	[ItemID] [varchar](30) NOT NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_GroupItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_ComplaintReason]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_ComplaintReason](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ComplaintReasonID] [varchar](30) NOT NULL,
	[ComplaintID] [varchar](30) NULL,
	[ComplaintDetailID] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[ReasonGroupID] [varchar](30) NULL,
	[ReasonID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[Quantity] [numeric](18, 6) NULL,
 CONSTRAINT [PK_QCS_ComplaintReason] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_ComplaintHandle]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_ComplaintHandle](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ComplaintHandleID] [varchar](30) NOT NULL,
	[ComplaintID] [varchar](30) NULL,
	[ComplaintDetailID] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[Method] [nvarchar](max) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_ComplaintHandle] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_ComplaintDetails]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_ComplaintDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ComplaintDetailID] [varchar](30) NOT NULL,
	[ComplaintID] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[ItemID] [varchar](30) NULL,
	[BatchNumber] [nvarchar](30) NULL,
	[ShipperNo] [nvarchar](30) NULL,
	[Quantity] [numeric](18, 6) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[OrderNo] [nvarchar](30) NULL,
 CONSTRAINT [PK_QCS_ComplaintDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_Complaint]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_Complaint](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ComplaintID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NULL,
	[Date] [datetime] NULL,
	[CustomerID] [varchar](30) NULL,
	[CustomerName] [nvarchar](120) NULL,
	[Complaintor] [nvarchar](30) NULL,
	[ApplicantID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_Complaint] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_CheckTestSettingDetails]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_CheckTestSettingDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CTSDID] [varchar](30) NOT NULL,
	[CheckTestSettingID] [varchar](30) NOT NULL,
	[Sequence] [int] NULL,
	[StartBatch] [numeric](18, 4) NULL,
	[EndBatch] [numeric](18, 4) NULL,
	[SamplingQuantity] [numeric](18, 4) NULL,
	[AcQuantity] [numeric](18, 4) NULL,
	[ReQuantity] [numeric](18, 4) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_CheckTestSettingDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QCS_CheckTestSetting]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QCS_CheckTestSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CheckTestSettingID] [varchar](30) NOT NULL,
	[InspectionStandard] [nvarchar](30) NOT NULL,
	[InspectionLevel] [varchar](30) NOT NULL,
	[InspectionMethod] [varchar](30) NOT NULL,
	[AQL] [varchar](30) NOT NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_QCS_CheckTestSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IOT_Sensor]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IOT_Sensor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[SensorID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](120) NULL,
	[Status] [varchar](30) NULL,
	[EnabledDate] [datetime] NULL,
	[FailureDate] [datetime] NULL,
	[Brand] [nvarchar](120) NULL,
	[Type] [nvarchar](120) NULL,
	[ManufacturerID] [varchar](30) NULL,
	[IsWarning] [bit] NULL,
	[MaxAlarmTime] [int] NULL,
	[MinAlarmTime] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[MaxValue] [numeric](18, 6) NULL,
	[MinValue] [numeric](18, 6) NULL,
 CONSTRAINT [PK_IOT_Sensor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_ServiceReasonLogDetails]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_ServiceReasonLogDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[SRLDID] [varchar](30) NOT NULL,
	[ServiceReasonLogID] [nvarchar](30) NOT NULL,
	[Sequence] [int] NULL,
	[MESUserID] [varchar](30) NULL,
	[OrganizationID] [varchar](30) NULL,
	[ManufacturerID] [varchar](30) NULL,
	[IsFee] [bit] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Hour] [numeric](14, 6) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_ServiceReasonLogDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_ServiceReasonLog]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_ServiceReasonLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[ServiceReasonLogID] [varchar](30) NOT NULL,
	[CalledRepairOrderID] [nvarchar](30) NOT NULL,
	[ReasonID] [nvarchar](30) NULL,
	[ReasonDescription] [nvarchar](120) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[ReasonGroupID] [varchar](30) NULL,
 CONSTRAINT [PK_EMS_ServiceReasonLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_MaiOrderProject]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_MaiOrderProject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MaiOrderProjectID] [varchar](30) NOT NULL,
	[MaintenanceOrderID] [varchar](30) NULL,
	[MaiOrderEquipmentID] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[MaiProjectID] [varchar](30) NULL,
	[Attribute] [varchar](30) NULL,
	[AttributeValue] [nvarchar](120) NULL,
	[Status] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_MaiOrderProject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_MaiOrderEquipment]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_MaiOrderEquipment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MaiOrderEquipmentID] [varchar](30) NOT NULL,
	[MaintenanceOrderID] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[EquipmentID] [varchar](30) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_MaiOrderEquipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_MaintenanceOrder]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_MaintenanceOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[MaintenanceOrderID] [varchar](30) NOT NULL,
	[MaintenanceNo] [nvarchar](30) NULL,
	[Date] [datetime] NULL,
	[MaintenanceDate] [datetime] NULL,
	[Type] [varchar](30) NULL,
	[EquipmentMaintenanceListID] [varchar](30) NULL,
	[OrganizationID] [varchar](30) NULL,
	[ManufacturerID] [varchar](30) NULL,
	[MESUserID] [varchar](30) NULL,
	[Status] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_MaintenanceOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_EquipmentProject]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_EquipmentProject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[EquipmentProjectID] [varchar](30) NOT NULL,
	[EquipmentID] [varchar](30) NOT NULL,
	[ProjectID] [nvarchar](50) NOT NULL,
	[IfCollection] [bit] NULL,
	[CollectionWay] [varchar](30) NULL,
	[SensorID] [varchar](30) NULL,
	[StandardValue] [nvarchar](120) NULL,
	[MaxValue] [nvarchar](120) NULL,
	[MinValue] [nvarchar](120) NULL,
	[MaxAlarmTime] [int] NULL,
	[MinAlarmTime] [int] NULL,
	[MaxAlarmValue] [nvarchar](120) NULL,
	[MinAlarmValue] [nvarchar](120) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[Status] [varchar](30) NULL,
 CONSTRAINT [PK_EMS_EquipmentProject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_EquipmentMaintenanceListDetails]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_EquipmentMaintenanceListDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[EquipmentMaintenanceListDetailID] [varchar](30) NOT NULL,
	[EquipmentMaintenanceListID] [varchar](30) NOT NULL,
	[Sequence] [smallint] NULL,
	[Type] [tinyint] NULL,
	[DetailID] [varchar](max) NULL,
	[Status] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_EquipmentMaintenanceListDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_EquipmentMaintenanceList]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_EquipmentMaintenanceList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[EquipmentMaintenanceListID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NULL,
	[Name] [nvarchar](120) NULL,
	[Type] [varchar](30) NULL,
	[Status] [varchar](30) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_EquipmentMaintenanceList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_EquipmentInspectionRecordDetails]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_EquipmentInspectionRecordDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[EIRDID] [varchar](30) NOT NULL,
	[EquipmentInspectionRecordID] [varchar](30) NOT NULL,
	[Sequence] [int] NULL,
	[EIProjectID] [varchar](30) NULL,
	[ProjectID] [varchar](30) NULL,
	[Value] [nvarchar](120) NULL,
	[StandardValue] [nvarchar](120) NULL,
	[MaxValue] [nvarchar](120) NULL,
	[MinValue] [nvarchar](120) NULL,
	[IsHand] [bit] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[EquipmentProjectID] [varchar](30) NULL,
 CONSTRAINT [PK_EMS_EquipmentInspectionRecordDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_EquipmentInspectionRecord]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_EquipmentInspectionRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[EquipmentInspectionRecordID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Date] [datetime] NULL,
	[EquipmentID] [varchar](30) NULL,
	[ClassID] [varchar](30) NULL,
	[MESUserID] [varchar](30) NULL,
	[TaskID] [varchar](30) NULL,
	[ItemID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_EquipmentInspectionRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_EquipmentInspectionProject]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_EquipmentInspectionProject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[EIProjectID] [varchar](30) NOT NULL,
	[EquipmentID] [varchar](30) NOT NULL,
	[EquipmentProjectID] [varchar](30) NULL,
	[Sequence] [int] NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
	[ProjectID] [varchar](30) NULL,
 CONSTRAINT [PK_EMS_EquipmentInspectionProject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_Equipment]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_Equipment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[EquipmentID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Name] [nvarchar](120) NULL,
	[ResourceCategory] [varchar](30) NULL,
	[PlantID] [varchar](30) NULL,
	[PlantAreaID] [varchar](30) NULL,
	[FixedAssets] [nvarchar](30) NULL,
	[PurchaseDate] [datetime] NULL,
	[ManufacturerID] [varchar](30) NULL,
	[Model] [nvarchar](40) NULL,
	[MachineNo] [nvarchar](30) NULL,
	[ClassOne] [varchar](30) NULL,
	[ClassTwo] [varchar](30) NULL,
	[OrganizationID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[Condition] [varchar](30) NULL,
	[ExpireDate] [datetime] NULL,
	[StdCapacity] [numeric](14, 6) NULL,
	[MaintenanceTime] [numeric](14, 0) NULL,
	[MaintenanceNum] [numeric](14, 0) NULL,
	[TotalOutput] [numeric](14, 6) NULL,
	[UsedTime] [numeric](14, 6) NULL,
	[UsableTime] [numeric](14, 6) NULL,
	[CavityMold] [numeric](14, 6) NULL,
	[UsedTimes] [numeric](14, 6) NULL,
	[UsableTimes] [numeric](14, 6) NULL,
	[StatisticsFlag] [bit] NULL,
	[DescriptionOne] [varchar](256) NULL,
	[DescriptionTwo] [varchar](256) NULL,
	[DateOne] [datetime] NULL,
	[DateTwo] [datetime] NULL,
	[NumOne] [numeric](14, 6) NULL,
	[NumTwo] [numeric](14, 6) NULL,
	[AbnormalStatus] [bit] NULL,
        [DAQMachID] [nvarchar](50) NULL,
	[AttachmentID] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_Equipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_CalledRepairReason]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_CalledRepairReason](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CalledRepairReasonID] [varchar](30) NOT NULL,
	[CalledRepairOrderID] [nvarchar](30) NOT NULL,
	[ReasonID] [nvarchar](30) NULL,
	[ReasonDescription] [nvarchar](120) NULL,
	[DealWithDescription] [nvarchar](120) NULL,
	[Status] [varchar](30) NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_CalledRepairReason] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EMS_CalledRepairOrder]    Script Date: 07/27/2017 01:00:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EMS_CalledRepairOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SystemID] [varchar](50) NOT NULL,
	[CalledRepairOrderID] [varchar](30) NOT NULL,
	[Code] [nvarchar](30) NOT NULL,
	[Date] [datetime] NULL,
	[EquipmentID] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
	[CallMESUserID] [varchar](30) NULL,
	[CallOrganizationID] [varchar](30) NULL,
	[MESUserID] [varchar](30) NULL,
	[InOutRepair] [varchar](30) NULL,
	[ManufacturerID] [varchar](30) NULL,
	[CloseMESUserID] [varchar](30) NULL,
	[CloseDate] [datetime] NULL,
	[Comments] [nvarchar](max) NULL,
	[Modifier] [varchar](30) NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedLocalTime] [datetime] NOT NULL,
	[Creator] [varchar](30) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateLocalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EMS_CalledRepairOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF__EMS_Calle__Modif__5D60DB10]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_CalledRepairOrder] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Calle__Modif__5E54FF49]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_CalledRepairOrder] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Calle__Creat__5F492382]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_CalledRepairOrder] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Calle__Creat__603D47BB]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_CalledRepairOrder] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Calle__Modif__6319B466]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_CalledRepairReason] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Calle__Modif__640DD89F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_CalledRepairReason] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Calle__Creat__6501FCD8]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_CalledRepairReason] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Calle__Creat__65F62111]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_CalledRepairReason] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF_EMS_Equipment_ExpireDate]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  CONSTRAINT [DF_EMS_Equipment_ExpireDate]  DEFAULT (((2099)-(12))-(31)) FOR [ExpireDate]
GO
/****** Object:  Default [DF__EMS_Equip__StdCa__2057CCD0]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [StdCapacity]
GO
/****** Object:  Default [DF__EMS_Equip__Maint__214BF109]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [MaintenanceTime]
GO
/****** Object:  Default [DF__EMS_Equip__Maint__22401542]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [MaintenanceNum]
GO
/****** Object:  Default [DF__EMS_Equip__Total__2334397B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [TotalOutput]
GO
/****** Object:  Default [DF__EMS_Equip__UsedT__24285DB4]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [UsedTime]
GO
/****** Object:  Default [DF__EMS_Equip__Usabl__251C81ED]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [UsableTime]
GO
/****** Object:  Default [DF__EMS_Equip__Cavit__2610A626]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [CavityMold]
GO
/****** Object:  Default [DF__EMS_Equip__UsedT__2704CA5F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [UsedTimes]
GO
/****** Object:  Default [DF__EMS_Equip__Usabl__27F8EE98]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((0)) FOR [UsableTimes]
GO
/****** Object:  Default [DF__EMS_Equip__Stati__28ED12D1]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT ((1)) FOR [StatisticsFlag]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__29E1370A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__2AD55B43]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__2BC97F7C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__2CBDA3B5]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_Equipment] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__42ACE4D4]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionProject] ADD  CONSTRAINT [DF__EMS_Equip__Modif__42ACE4D4]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__43A1090D]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionProject] ADD  CONSTRAINT [DF__EMS_Equip__Modif__43A1090D]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__44952D46]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionProject] ADD  CONSTRAINT [DF__EMS_Equip__Creat__44952D46]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__4589517F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionProject] ADD  CONSTRAINT [DF__EMS_Equip__Creat__4589517F]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__30242045]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecord] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__3118447E]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecord] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__320C68B7]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecord] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__33008CF0]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecord] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Seque__35DCF99B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__Seque__35DCF99B]  DEFAULT ((0)) FOR [Sequence]
GO
/****** Object:  Default [DF__EMS_Equip__Value__36D11DD4]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__Value__36D11DD4]  DEFAULT ((0)) FOR [Value]
GO
/****** Object:  Default [DF__EMS_Equip__Stand__37C5420D]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__Stand__37C5420D]  DEFAULT ((0)) FOR [StandardValue]
GO
/****** Object:  Default [DF__EMS_Equip__MaxVa__38B96646]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__MaxVa__38B96646]  DEFAULT ((0)) FOR [MaxValue]
GO
/****** Object:  Default [DF__EMS_Equip__MinVa__39AD8A7F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__MinVa__39AD8A7F]  DEFAULT ((0)) FOR [MinValue]
GO
/****** Object:  Default [DF__EMS_Equip__IsHan__3AA1AEB8]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__IsHan__3AA1AEB8]  DEFAULT ((0)) FOR [IsHand]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__3B95D2F1]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__Modif__3B95D2F1]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__3C89F72A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__Modif__3C89F72A]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__3D7E1B63]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__Creat__3D7E1B63]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__3E723F9C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentInspectionRecordDetails] ADD  CONSTRAINT [DF__EMS_Equip__Creat__3E723F9C]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__69E6AD86]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentMaintenanceList] ADD  CONSTRAINT [DF__EMS_Equip__Modif__69E6AD86]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__6ADAD1BF]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentMaintenanceList] ADD  CONSTRAINT [DF__EMS_Equip__Modif__6ADAD1BF]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__6BCEF5F8]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentMaintenanceList] ADD  CONSTRAINT [DF__EMS_Equip__Creat__6BCEF5F8]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__6CC31A31]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentMaintenanceList] ADD  CONSTRAINT [DF__EMS_Equip__Creat__6CC31A31]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__75586032]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentMaintenanceListDetails] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__764C846B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentMaintenanceListDetails] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__7740A8A4]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentMaintenanceListDetails] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__7834CCDD]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentMaintenanceListDetails] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__IfCol__2F9A1060]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentProject] ADD  DEFAULT ((1)) FOR [IfCollection]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__308E3499]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentProject] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Equip__Modif__318258D2]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentProject] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__32767D0B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentProject] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Equip__Creat__336AA144]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_EquipmentProject] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Maint__Modif__1F4E99FE]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaintenanceOrder] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Maint__Modif__2042BE37]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaintenanceOrder] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Maint__Creat__2136E270]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaintenanceOrder] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Maint__Creat__222B06A9]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaintenanceOrder] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_MaiOr__Modif__25077354]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaiOrderEquipment] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_MaiOr__Modif__25FB978D]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaiOrderEquipment] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_MaiOr__Creat__26EFBBC6]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaiOrderEquipment] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_MaiOr__Creat__27E3DFFF]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaiOrderEquipment] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_MaiOr__Modif__2AC04CAA]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaiOrderProject] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_MaiOr__Modif__2BB470E3]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaiOrderProject] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_MaiOr__Creat__2CA8951C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaiOrderProject] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_MaiOr__Creat__2D9CB955]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_MaiOrderProject] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Servi__Modif__4277DAAA]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_ServiceReasonLog] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Servi__Modif__436BFEE3]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_ServiceReasonLog] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Servi__Creat__4460231C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_ServiceReasonLog] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Servi__Creat__45544755]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_ServiceReasonLog] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__EMS_Servi__Modif__4830B400]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_ServiceReasonLogDetails] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__EMS_Servi__Modif__4924D839]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_ServiceReasonLogDetails] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__EMS_Servi__Creat__4A18FC72]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_ServiceReasonLogDetails] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__EMS_Servi__Creat__4B0D20AB]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[EMS_ServiceReasonLogDetails] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__IOT_Senso__Modif__3CF40B7E]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[IOT_Sensor] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__IOT_Senso__Modif__3DE82FB7]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[IOT_Sensor] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__IOT_Senso__Creat__3EDC53F0]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[IOT_Sensor] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__IOT_Senso__Creat__3FD07829]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[IOT_Sensor] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Check__Modif__4DE98D56]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSetting] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Check__Modif__4EDDB18F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSetting] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Check__Creat__4FD1D5C8]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSetting] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Check__Creat__50C5FA01]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSetting] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Check__Seque__53A266AC]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT ((0)) FOR [Sequence]
GO
/****** Object:  Default [DF__QCS_Check__Start__310335E5]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT ((0)) FOR [StartBatch]
GO
/****** Object:  Default [DF__QCS_Check__EndBa__2E26C93A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT ((0)) FOR [EndBatch]
GO
/****** Object:  Default [DF__QCS_Check__Sampl__300F11AC]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT ((0)) FOR [SamplingQuantity]
GO
/****** Object:  Default [DF__QCS_Check__AcQua__2D32A501]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT ((0)) FOR [AcQuantity]
GO
/****** Object:  Default [DF__QCS_Check__ReQua__2F1AED73]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT ((0)) FOR [ReQuantity]
GO
/****** Object:  Default [DF__QCS_Check__Modif__595B4002]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Check__Modif__5A4F643B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Check__Creat__5B438874]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Check__Creat__5C37ACAD]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_CheckTestSettingDetails] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Compl__Modif__60C757A0]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_Complaint] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Compl__Modif__61BB7BD9]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_Complaint] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Compl__Creat__62AFA012]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_Complaint] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Compl__Creat__63A3C44B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_Complaint] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Compl__Modif__668030F6]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintDetails] ADD  CONSTRAINT [DF__QCS_Compl__Modif__668030F6]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Compl__Modif__6774552F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintDetails] ADD  CONSTRAINT [DF__QCS_Compl__Modif__6774552F]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Compl__Creat__68687968]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintDetails] ADD  CONSTRAINT [DF__QCS_Compl__Creat__68687968]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Compl__Creat__695C9DA1]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintDetails] ADD  CONSTRAINT [DF__QCS_Compl__Creat__695C9DA1]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Compl__Modif__7E57BA87]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintHandle] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Compl__Modif__7F4BDEC0]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintHandle] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Compl__Creat__004002F9]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintHandle] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Compl__Creat__01342732]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintHandle] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Compl__Modif__77AABCF8]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintReason] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Compl__Modif__789EE131]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintReason] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Compl__Creat__7993056A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintReason] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Compl__Creat__7A8729A3]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintReason] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF_QCS_ComplaintReason_Quantity]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_ComplaintReason] ADD  CONSTRAINT [DF_QCS_ComplaintReason_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
/****** Object:  Default [DF__QCS_Group__Modif__1E3A7A34]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_GroupItem] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Group__Modif__1F2E9E6D]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_GroupItem] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Group__Creat__2022C2A6]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_GroupItem] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Group__Creat__2116E6DF]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_GroupItem] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Inspe__3D14070F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocument] ADD  DEFAULT ((0)) FOR [InspectionDate]
GO
/****** Object:  Default [DF__QCS_Inspe__Inspe__3E082B48]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocument] ADD  DEFAULT ((0)) FOR [InspectionFlag]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__3EFC4F81]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocument] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__3FF073BA]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocument] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__40E497F3]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocument] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__41D8BC2C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocument] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__44B528D7]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentDetails] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__45A94D10]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentDetails] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__469D7149]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentDetails] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__47919582]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentDetails] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__511AFFBC]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentReason] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__520F23F5]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentReason] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__5303482E]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentReason] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__53F76C67]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentReason] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__56D3D912]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentRemark] ADD  CONSTRAINT [DF__QCS_Inspe__Modif__56D3D912]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__57C7FD4B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentRemark] ADD  CONSTRAINT [DF__QCS_Inspe__Modif__57C7FD4B]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__58BC2184]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentRemark] ADD  CONSTRAINT [DF__QCS_Inspe__Creat__58BC2184]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__59B045BD]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionDocumentRemark] ADD  CONSTRAINT [DF__QCS_Inspe__Creat__59B045BD]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__5555A4F4]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionProject] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Modif__5649C92D]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionProject] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__573DED66]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionProject] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Inspe__Creat__5832119F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_InspectionProject] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_Sampl__Modif__5B0E7E4A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_SamplingSetting] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_Sampl__Modif__5C02A283]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_SamplingSetting] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_Sampl__Creat__5CF6C6BC]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_SamplingSetting] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_Sampl__Creat__5DEAEAF5]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_SamplingSetting] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__QCS_StaIn__Modif__00CA12DE]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_StaInsSpeSetting] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__QCS_StaIn__Modif__01BE3717]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_StaInsSpeSetting] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__QCS_StaIn__Creat__02B25B50]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_StaInsSpeSetting] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__QCS_StaIn__Creat__03A67F89]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[QCS_StaInsSpeSetting] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Modif__033C6B35]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalDetails] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Modif__04308F6E]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalDetails] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Creat__0524B3A7]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalDetails] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Creat__0618D7E0]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalDetails] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Modif__5EFF0ABF]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalHour] ADD  CONSTRAINT [DF__SFC_Abnor__Modif__5EFF0ABF]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Modif__5FF32EF8]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalHour] ADD  CONSTRAINT [DF__SFC_Abnor__Modif__5FF32EF8]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Creat__60E75331]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalHour] ADD  CONSTRAINT [DF__SFC_Abnor__Creat__60E75331]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Creat__61DB776A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalHour] ADD  CONSTRAINT [DF__SFC_Abnor__Creat__61DB776A]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Modif__59463169]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalQuantity] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Modif__5A3A55A2]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalQuantity] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Creat__5B2E79DB]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalQuantity] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Abnor__Creat__5C229E14]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_AbnormalQuantity] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Batch__Modif__64B7E415]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_BatchAttribute] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Batch__Modif__65AC084E]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_BatchAttribute] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Batch__Creat__66A02C87]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_BatchAttribute] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Batch__Creat__679450C0]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_BatchAttribute] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Batch__Modif__6A70BD6B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_BatchAttributeDetails] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Batch__Modif__6B64E1A4]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_BatchAttributeDetails] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Batch__Creat__6C5905DD]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_BatchAttributeDetails] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Batch__Creat__6D4D2A16]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_BatchAttributeDetails] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Compl__Seque__08F5448B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [Sequence]
GO
/****** Object:  Default [DF__SFC_Comple__IsIF__09E968C4]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [IsIF]
GO
/****** Object:  Default [DF__SFC_Compl__FinPr__0ADD8CFD]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [FinProQuantity]
GO
/****** Object:  Default [DF__SFC_Compl__Scrap__0BD1B136]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [ScrappedQuantity]
GO
/****** Object:  Default [DF__SFC_Compl__Diffe__0CC5D56F]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [DifferenceQuantity]
GO
/****** Object:  Default [DF__SFC_Compl__Repai__0DB9F9A8]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [RepairQuantity]
GO
/****** Object:  Default [DF__SFC_Compl__Inspe__0EAE1DE1]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [InspectionQuantity]
GO
/****** Object:  Default [DF__SFC_Compl__Labor__0FA2421A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [LaborHour]
GO
/****** Object:  Default [DF__SFC_Compl__UnLab__10966653]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [UnLaborHour]
GO
/****** Object:  Default [DF__SFC_Compl__Machi__118A8A8C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [MachineHour]
GO
/****** Object:  Default [DF__SFC_Compl__UnMac__127EAEC5]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT ((0)) FOR [UnMachineHour]
GO
/****** Object:  Default [DF__SFC_Compl__Modif__1372D2FE]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Compl__Modif__1466F737]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Compl__Creat__155B1B70]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Compl__Creat__164F3FA9]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionOrder] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Compl__Modif__538D5813]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionResource] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Compl__Modif__54817C4C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionResource] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Compl__Creat__5575A085]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionResource] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Compl__Creat__5669C4BE]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_CompletionResource] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Seque__0559BDD1]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoAltRelShip] ADD  DEFAULT ((0)) FOR [Sequence]
GO
/****** Object:  Default [DF__SFC_FabMo__Price__064DE20A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoAltRelShip] ADD  DEFAULT ((0)) FOR [Price]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__07420643]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoAltRelShip] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__08362A7C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoAltRelShip] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__092A4EB5]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoAltRelShip] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__0A1E72EE]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoAltRelShip] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__5A6F5FCC]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoItem] ADD  CONSTRAINT [DF__SFC_FabMo__Modif__5A6F5FCC]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__5B638405]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoItem] ADD  CONSTRAINT [DF__SFC_FabMo__Modif__5B638405]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__5C57A83E]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoItem] ADD  CONSTRAINT [DF__SFC_FabMo__Creat__5C57A83E]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__5D4BCC77]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoItem] ADD  CONSTRAINT [DF__SFC_FabMo__Creat__5D4BCC77]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__4944D3CA]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoOperation] ADD  CONSTRAINT [DF__SFC_FabMo__Modif__4944D3CA]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__4A38F803]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoOperation] ADD  CONSTRAINT [DF__SFC_FabMo__Modif__4A38F803]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__4B2D1C3C]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoOperation] ADD  CONSTRAINT [DF__SFC_FabMo__Creat__4B2D1C3C]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__4C214075]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoOperation] ADD  CONSTRAINT [DF__SFC_FabMo__Creat__4C214075]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__7FA0E47B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoOperationRelationship] ADD  CONSTRAINT [DF__SFC_FabMo__Modif__7FA0E47B]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__009508B4]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoOperationRelationship] ADD  CONSTRAINT [DF__SFC_FabMo__Modif__009508B4]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__01892CED]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoOperationRelationship] ADD  CONSTRAINT [DF__SFC_FabMo__Creat__01892CED]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__027D5126]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoOperationRelationship] ADD  CONSTRAINT [DF__SFC_FabMo__Creat__027D5126]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__54B68676]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoProcess] ADD  CONSTRAINT [DF__SFC_FabMo__Modif__54B68676]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__55AAAAAF]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoProcess] ADD  CONSTRAINT [DF__SFC_FabMo__Modif__55AAAAAF]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__569ECEE8]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoProcess] ADD  CONSTRAINT [DF__SFC_FabMo__Creat__569ECEE8]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__5792F321]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoProcess] ADD  CONSTRAINT [DF__SFC_FabMo__Creat__5792F321]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__IfMai__7246E95D]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoRelationship] ADD  DEFAULT ((0)) FOR [IfMain]
GO
/****** Object:  Default [DF__SFC_FabMo__IfLas__733B0D96]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoRelationship] ADD  DEFAULT ((0)) FOR [IfLastProcess]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__742F31CF]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoRelationship] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__75235608]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoRelationship] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__76177A41]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoRelationship] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__770B9E7A]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoRelationship] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__60283922]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoResource] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Modif__611C5D5B]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoResource] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__62108194]    Script Date: 07/27/2017 01:00:45 ******/
ALTER TABLE [dbo].[SFC_FabMoResource] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_FabMo__Creat__6304A5CD]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_FabMoResource] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Fabri__Modif__55209ACA]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_FabricatedMother] ADD  CONSTRAINT [DF__SFC_Fabri__Modif__55209ACA]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Fabri__Modif__5614BF03]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_FabricatedMother] ADD  CONSTRAINT [DF__SFC_Fabri__Modif__5614BF03]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Fabri__Creat__5708E33C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_FabricatedMother] ADD  CONSTRAINT [DF__SFC_Fabri__Creat__5708E33C]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Fabri__Creat__57FD0775]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_FabricatedMother] ADD  CONSTRAINT [DF__SFC_Fabri__Creat__57FD0775]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemM__Basic__4B973090]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemMaterial] ADD  CONSTRAINT [DF__SFC_ItemM__Basic__4B973090]  DEFAULT ((1)) FOR [BasicQuantity]
GO
/****** Object:  Default [DF__SFC_ItemM__Attri__4C8B54C9]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemMaterial] ADD  CONSTRAINT [DF__SFC_ItemM__Attri__4C8B54C9]  DEFAULT ((0)) FOR [AttritionRate]
GO
/****** Object:  Default [DF__SFC_ItemM__UseQu__4D7F7902]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemMaterial] ADD  CONSTRAINT [DF__SFC_ItemM__UseQu__4D7F7902]  DEFAULT ((1)) FOR [UseQuantity]
GO
/****** Object:  Default [DF__SFC_ItemM__Modif__4E739D3B]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemMaterial] ADD  CONSTRAINT [DF__SFC_ItemM__Modif__4E739D3B]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_ItemM__Modif__4F67C174]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemMaterial] ADD  CONSTRAINT [DF__SFC_ItemM__Modif__4F67C174]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemM__Creat__505BE5AD]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemMaterial] ADD  CONSTRAINT [DF__SFC_ItemM__Creat__505BE5AD]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_ItemM__Creat__515009E6]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemMaterial] ADD  CONSTRAINT [DF__SFC_ItemM__Creat__515009E6]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemO__Modif__00FF1D08]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemOperation] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_ItemO__Modif__01F34141]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemOperation] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemO__Creat__02E7657A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemOperation] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_ItemO__Creat__03DB89B3]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemOperation] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemP__AuxUn__2665ABE1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcess] ADD  DEFAULT ((1)) FOR [AuxUnitRatio]
GO
/****** Object:  Default [DF__SFC_ItemP__Price__2759D01A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcess] ADD  DEFAULT ((0)) FOR [Price]
GO
/****** Object:  Default [DF__SFC_ItemP__Modif__284DF453]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcess] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Modif__2942188C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcess] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Creat__2A363CC5]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcess] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Creat__2B2A60FE]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcess] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Price__7A521F79]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessAlternativeRelationShip] ADD  DEFAULT ((0)) FOR [Price]
GO
/****** Object:  Default [DF__SFC_ItemP__Modif__7B4643B2]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessAlternativeRelationShip] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Modif__7C3A67EB]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessAlternativeRelationShip] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Creat__7D2E8C24]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessAlternativeRelationShip] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Creat__7E22B05D]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessAlternativeRelationShip] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Finis__06B7F65E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessRelationShip] ADD  CONSTRAINT [DF__SFC_ItemP__Finis__06B7F65E]  DEFAULT ((0)) FOR [FinishProcess]
GO
/****** Object:  Default [DF__SFC_ItemP__Modif__07AC1A97]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessRelationShip] ADD  CONSTRAINT [DF__SFC_ItemP__Modif__07AC1A97]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Modif__08A03ED0]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessRelationShip] ADD  CONSTRAINT [DF__SFC_ItemP__Modif__08A03ED0]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Creat__09946309]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessRelationShip] ADD  CONSTRAINT [DF__SFC_ItemP__Creat__09946309]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_ItemP__Creat__0A888742]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemProcessRelationShip] ADD  CONSTRAINT [DF__SFC_ItemP__Creat__0A888742]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemR__IfMai__72B0FDB1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemResource] ADD  CONSTRAINT [DF__SFC_ItemR__IfMai__72B0FDB1]  DEFAULT ((0)) FOR [IfMain]
GO
/****** Object:  Default [DF__SFC_ItemR__Modif__73A521EA]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemResource] ADD  CONSTRAINT [DF__SFC_ItemR__Modif__73A521EA]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_ItemR__Modif__74994623]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemResource] ADD  CONSTRAINT [DF__SFC_ItemR__Modif__74994623]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_ItemR__Creat__758D6A5C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemResource] ADD  CONSTRAINT [DF__SFC_ItemR__Creat__758D6A5C]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_ItemR__Creat__76818E95]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ItemResource] ADD  CONSTRAINT [DF__SFC_ItemR__Creat__76818E95]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Proce__Finis__0D64F3ED]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ProcessOperationRelationShip] ADD  DEFAULT ((0)) FOR [FinishOperation]
GO
/****** Object:  Default [DF__SFC_Proce__IfMai__0E591826]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ProcessOperationRelationShip] ADD  DEFAULT ((0)) FOR [IfMain]
GO
/****** Object:  Default [DF__SFC_Proce__Modif__0F4D3C5F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ProcessOperationRelationShip] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Proce__Modif__10416098]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ProcessOperationRelationShip] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Proce__Creat__113584D1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ProcessOperationRelationShip] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Proce__Creat__1229A90A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_ProcessOperationRelationShip] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_TaskD__IsDis__2AF556D4]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [IsDispatch]
GO
/****** Object:  Default [DF__SFC_TaskDi__IsIP__2BE97B0D]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [IsIP]
GO
/****** Object:  Default [DF__SFC_TaskD__IsFPI__2CDD9F46]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [IsFPI]
GO
/****** Object:  Default [DF__SFC_TaskD__IsOSI__2DD1C37F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [IsOSI]
GO
/****** Object:  Default [DF__SFC_TaskD__FPIPa__2EC5E7B8]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [FPIPass]
GO
/****** Object:  Default [DF__SFC_TaskD__Finis__2FBA0BF1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [FinishQuantity]
GO
/****** Object:  Default [DF__SFC_TaskD__Scrap__30AE302A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [ScrapQuantity]
GO
/****** Object:  Default [DF__SFC_TaskD__DiffQ__31A25463]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [DiffQuantity]
GO
/****** Object:  Default [DF__SFC_TaskD__Repai__3296789C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [RepairQuantity]
GO
/****** Object:  Default [DF__SFC_TaskD__Labor__338A9CD5]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [LaborHour]
GO
/****** Object:  Default [DF__SFC_TaskD__UnLab__347EC10E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [UnLaborHour]
GO
/****** Object:  Default [DF__SFC_TaskD__Machi__3572E547]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [MachineHour]
GO
/****** Object:  Default [DF__SFC_TaskD__UnMac__36670980]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT ((0)) FOR [UnMachineHour]
GO
/****** Object:  Default [DF__SFC_TaskD__Modif__375B2DB9]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_TaskD__Modif__384F51F2]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_TaskD__Creat__3943762B]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_TaskD__Creat__3A379A64]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatch] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_TaskD__IfMai__3CDEFCE5]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatchResource] ADD  DEFAULT ((0)) FOR [IfMain]
GO
/****** Object:  Default [DF__SFC_TaskD__Modif__3DD3211E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatchResource] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_TaskD__Modif__3EC74557]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatchResource] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_TaskD__Creat__3FBB6990]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatchResource] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_TaskD__Creat__40AF8DC9]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskDispatchResource] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_TaskE__IfCol__5C8CB268]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskEquipmentProject] ADD  DEFAULT ((1)) FOR [IfCollection]
GO
/****** Object:  Default [DF__SFC_TaskE__Modif__5D80D6A1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskEquipmentProject] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_TaskE__Modif__5E74FADA]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskEquipmentProject] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_TaskE__Creat__5F691F13]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskEquipmentProject] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_TaskE__Creat__605D434C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TaskEquipmentProject] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SFC_Trans__Seque__702996C1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TransferOrder] ADD  DEFAULT ((0)) FOR [Sequence]
GO
/****** Object:  Default [DF__SFC_Trans__Modif__711DBAFA]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TransferOrder] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SFC_Trans__Modif__7211DF33]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TransferOrder] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SFC_Trans__Creat__7306036C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TransferOrder] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SFC_Trans__Creat__73FA27A5]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SFC_TransferOrder] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Actio__Modif__239E4DCF]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Actions] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Alter__Modif__373B3228]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AlternativeGroupDetails] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Alter__Modif__382F5661]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AlternativeGroupDetails] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Alter__Creat__39237A9A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AlternativeGroupDetails] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Alter__Creat__3A179ED3]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AlternativeGroupDetails] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Attac__IsNot__5165187F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Attachments] ADD  CONSTRAINT [DF__SYS_Attac__IsNot__5165187F]  DEFAULT ((0)) FOR [IsNotInit]
GO
/****** Object:  Default [DF__SYS_Attac__Modif__52593CB8]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Attachments] ADD  CONSTRAINT [DF__SYS_Attac__Modif__52593CB8]  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_AutoN__YearL__7FEAFD3E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumber] ADD  DEFAULT ((4)) FOR [YearLength]
GO
/****** Object:  Default [DF__SYS_AutoN__Month__00DF2177]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumber] ADD  DEFAULT ((2)) FOR [MonthLength]
GO
/****** Object:  Default [DF__SYS_AutoN__DayLe__01D345B0]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumber] ADD  DEFAULT ((2)) FOR [DateLength]
GO
/****** Object:  Default [DF__SYS_AutoN__NumLe__02C769E9]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumber] ADD  DEFAULT ((4)) FOR [NumLength]
GO
/****** Object:  Default [DF__SYS_AutoN__Modif__03BB8E22]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumber] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_AutoN__Modif__04AFB25B]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumber] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_AutoN__Creat__05A3D694]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumber] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_AutoN__Creat__0697FACD]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumber] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_AutoNum__Num__09746778]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumberRecord] ADD  DEFAULT ((0)) FOR [Num]
GO
/****** Object:  Default [DF__SYS_AutoN__Modif__0A688BB1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumberRecord] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_AutoN__Modif__0B5CAFEA]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumberRecord] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_AutoN__Creat__0C50D423]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumberRecord] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_AutoN__Creat__0D44F85C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_AutoNumberRecord] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Butto__Modif__20C1E124]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Buttons] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Calen__Ifdef__0662F0A3]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Calendar] ADD  CONSTRAINT [DF__SYS_Calen__Ifdef__0662F0A3]  DEFAULT ((0)) FOR [Ifdefault]
GO
/****** Object:  Default [DF__SYS_Calen__Modif__075714DC]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Calendar] ADD  CONSTRAINT [DF__SYS_Calen__Modif__075714DC]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Calen__Modif__084B3915]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Calendar] ADD  CONSTRAINT [DF__SYS_Calen__Modif__084B3915]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Calen__Creat__093F5D4E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Calendar] ADD  CONSTRAINT [DF__SYS_Calen__Creat__093F5D4E]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Calen__Creat__0A338187]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Calendar] ADD  CONSTRAINT [DF__SYS_Calen__Creat__0A338187]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Calen__Modif__0D0FEE32]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_CalendarDetails] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Calen__Modif__0E04126B]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_CalendarDetails] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Calen__Creat__0EF836A4]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_CalendarDetails] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Calen__Creat__0FEC5ADD]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_CalendarDetails] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Class__Modif__1DB06A4F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Class] ADD  CONSTRAINT [DF__SYS_Class__Modif__1DB06A4F]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Class__Modif__1EA48E88]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Class] ADD  CONSTRAINT [DF__SYS_Class__Modif__1EA48E88]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Class__Creat__1F98B2C1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Class] ADD  CONSTRAINT [DF__SYS_Class__Creat__1F98B2C1]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Class__Creat__208CD6FA]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Class] ADD  CONSTRAINT [DF__SYS_Class__Creat__208CD6FA]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Contr__IsEna__48CFD27E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ControlParameters] ADD  DEFAULT ((0)) FOR [IsEnable]
GO
/****** Object:  Default [DF__SYS_Contr__IsDef__49C3F6B7]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ControlParameters] ADD  DEFAULT ((0)) FOR [IsDefault]
GO
/****** Object:  Default [DF_SYS_ControlParameters_IsSystem]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ControlParameters] ADD  CONSTRAINT [DF_SYS_ControlParameters_IsSystem]  DEFAULT ((0)) FOR [IsSystem]
GO
/****** Object:  Default [DF__SYS_Contr__Modif__4AB81AF0]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ControlParameters] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Custo__Modif__02FC7413]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Customers] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Custo__Modif__03F0984C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Customers] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Custo__Creat__04E4BC85]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Customers] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Custo__Creat__05D8E0BE]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Customers] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Docum__Attri__3AD6B8E2]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAuthority] ADD  DEFAULT ((0)) FOR [Attribute]
GO
/****** Object:  Default [DF__SYS_Docum__Modif__3BCADD1B]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAuthority] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Docum__Modif__3CBF0154]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAuthority] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Docum__Creat__3DB3258D]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAuthority] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Docum__Creat__3EA749C6]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAuthority] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Documen__Num__3335971A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAutoNumber] ADD  DEFAULT ((0)) FOR [Num]
GO
/****** Object:  Default [DF__SYS_Docum__Attri__3429BB53]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAutoNumber] ADD  DEFAULT ((0)) FOR [Attribute]
GO
/****** Object:  Default [DF__SYS_Docum__Modif__351DDF8C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAutoNumber] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Docum__Modif__361203C5]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAutoNumber] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Docum__Creat__370627FE]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAutoNumber] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Docum__Creat__37FA4C37]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentAutoNumber] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Docum__IfDef__27C3E46E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT ((0)) FOR [IfDefault]
GO
/****** Object:  Default [DF__SYS_Docum__YearL__28B808A7]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT ((2)) FOR [YearLength]
GO
/****** Object:  Default [DF__SYS_Docum__Month__29AC2CE0]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT ((2)) FOR [MonthLength]
GO
/****** Object:  Default [DF__SYS_Docum__DateL__2AA05119]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT ((2)) FOR [DateLength]
GO
/****** Object:  Default [DF__SYS_Docum__Attri__2B947552]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT ((0)) FOR [Attribute]
GO
/****** Object:  Default [DF__SYS_Docum__Lengt__2C88998B]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT ((12)) FOR [Length]
GO
/****** Object:  Default [DF__SYS_Docum__Modif__2D7CBDC4]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Docum__Modif__2E70E1FD]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Docum__Creat__2F650636]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Docum__Creat__30592A6F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_DocumentTypeSetting] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Inter__Modif__5535A963]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_InterfaceConfiguration] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_ItemA__Modif__671F4F74]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ItemAttributes] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_ItemA__Modif__681373AD]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ItemAttributes] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_ItemA__Creat__690797E6]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ItemAttributes] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_ItemA__Creat__69FBBC1F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ItemAttributes] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Items__Lot__5F7E2DAC]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Items] ADD  CONSTRAINT [DF__SYS_Items__Lot__5F7E2DAC]  DEFAULT ((0)) FOR [Lot]
GO
/****** Object:  Default [DF__SYS_Items__OverR__607251E5]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Items] ADD  CONSTRAINT [DF__SYS_Items__OverR__607251E5]  DEFAULT ((0)) FOR [OverRate]
GO
/****** Object:  Default [DF__SYS_Items__Modif__6166761E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Items] ADD  CONSTRAINT [DF__SYS_Items__Modif__6166761E]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Items__Modif__625A9A57]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Items] ADD  CONSTRAINT [DF__SYS_Items__Modif__625A9A57]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Items__Creat__634EBE90]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Items] ADD  CONSTRAINT [DF__SYS_Items__Creat__634EBE90]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Items__Creat__6442E2C9]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Items] ADD  CONSTRAINT [DF__SYS_Items__Creat__6442E2C9]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Langu__Modif__5812160E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_LanguageLib] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Manuf__Modif__08B54D69]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Manufacturers] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Manuf__Modif__09A971A2]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Manufacturers] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Manuf__Creat__0A9D95DB]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Manufacturers] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Manuf__Creat__0B91BA14]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Manufacturers] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_MenuA__Modif__3B75D760]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_MenuActionMapping] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_MenuB__Modif__32E0915F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_MenuButtonMapping] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_MenuC__Modif__38996AB5]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_MenuColumn] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Menus__IsVis__1BFD2C07]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Menus] ADD  DEFAULT ((0)) FOR [IsVisible]
GO
/****** Object:  Default [DF__SYS_Menus__IsEna__1CF15040]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Menus] ADD  DEFAULT ((0)) FOR [IsEnable]
GO
/****** Object:  Default [DF__SYS_Menus__Modif__1DE57479]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Menus] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Organ__Modif__164452B1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Organization] ADD  CONSTRAINT [DF__SYS_Organ__Modif__164452B1]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF_SYS_Organization_ModifiedLocalTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Organization] ADD  CONSTRAINT [DF_SYS_Organization_ModifiedLocalTime]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF_SYS_Organization_CreateLocalTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Organization] ADD  CONSTRAINT [DF_SYS_Organization_CreateLocalTime]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF_SYS_Organization_IfTop]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Organization] ADD  CONSTRAINT [DF_SYS_Organization_IfTop]  DEFAULT ((0)) FOR [IfTop]
GO
/****** Object:  Default [DF__SYS_Organ__Modif__2962141D]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_OrganizationClass] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Organ__Modif__2A563856]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_OrganizationClass] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Organ__Creat__2B4A5C8F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_OrganizationClass] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Organ__Creat__2C3E80C8]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_OrganizationClass] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Organ__Order__2C3393D0]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_OrganizationRoleMapping] ADD  DEFAULT ((0)) FOR [Order]
GO
/****** Object:  Default [DF__SYS_Organ__Modif__2D27B809]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_OrganizationRoleMapping] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Param__IsEna__44FF419A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Parameters] ADD  CONSTRAINT [DF__SYS_Param__IsEna__44FF419A]  DEFAULT ((0)) FOR [IsEnable]
GO
/****** Object:  Default [DF_SYS_Parameters_IsSystem]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Parameters] ADD  CONSTRAINT [DF_SYS_Parameters_IsSystem]  DEFAULT ((1)) FOR [IsSystem]
GO
/****** Object:  Default [DF__SYS_Param__Modif__45F365D3]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Parameters] ADD  CONSTRAINT [DF__SYS_Param__Modif__45F365D3]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF_SYS_Parameters_ModifiedLocalTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Parameters] ADD  CONSTRAINT [DF_SYS_Parameters_ModifiedLocalTime]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF_SYS_Parameters_CreateTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Parameters] ADD  CONSTRAINT [DF_SYS_Parameters_CreateTime]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_SYS_Parameters_CreateLocalTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Parameters] ADD  CONSTRAINT [DF_SYS_Parameters_CreateLocalTime]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Param__IsSys__412EB0B6]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ParameterType] ADD  DEFAULT ((1)) FOR [IsSystem]
GO
/****** Object:  Default [DF__SYS_Param__Modif__4222D4EF]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ParameterType] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF_SYS_ParameterType_ModifiedLocalTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ParameterType] ADD  CONSTRAINT [DF_SYS_ParameterType_ModifiedLocalTime]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF_SYS_ParameterType_CreateTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ParameterType] ADD  CONSTRAINT [DF_SYS_ParameterType_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_SYS_ParameterType_CreateLocalTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ParameterType] ADD  CONSTRAINT [DF_SYS_ParameterType_CreateLocalTime]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Plant__Modif__76969D2E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_PlantArea] ADD  CONSTRAINT [DF__SYS_Plant__Modif__76969D2E]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Plant__Modif__778AC167]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_PlantArea] ADD  CONSTRAINT [DF__SYS_Plant__Modif__778AC167]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Plant__Creat__787EE5A0]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_PlantArea] ADD  CONSTRAINT [DF__SYS_Plant__Creat__787EE5A0]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Plant__Creat__797309D9]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_PlantArea] ADD  CONSTRAINT [DF__SYS_Plant__Creat__797309D9]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Proce__Modif__7FB5F314]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ProcessOperation] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Proce__Modif__00AA174D]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ProcessOperation] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Proce__Creat__019E3B86]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ProcessOperation] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Proce__Creat__02925FBF]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ProcessOperation] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Proje__Modif__3B40CD36]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Projects] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Proje__Modif__3C34F16F]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Projects] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Proje__Creat__3D2915A8]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Projects] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Proje__Creat__3E1D39E1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Projects] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Resou__Modif__4F47C5E3]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ResourceDetails] ADD  CONSTRAINT [DF__SYS_Resou__Modif__4F47C5E3]  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Resou__Modif__503BEA1C]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ResourceDetails] ADD  CONSTRAINT [DF__SYS_Resou__Modif__503BEA1C]  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Resou__Creat__51300E55]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ResourceDetails] ADD  CONSTRAINT [DF__SYS_Resou__Creat__51300E55]  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Resou__Creat__5224328E]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_ResourceDetails] ADD  CONSTRAINT [DF__SYS_Resou__Creat__5224328E]  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_Resou__Quant__489AC854]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Resources] ADD  DEFAULT ((1)) FOR [Quantity]
GO
/****** Object:  Default [DF__SYS_Resou__Modif__498EEC8D]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Resources] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Resou__Modif__4A8310C6]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Resources] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_Resou__Creat__4B7734FF]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Resources] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Resou__Creat__4C6B5938]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Resources] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_RoleM__Modif__3E52440B]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_RoleMenuActionMapping] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_RoleM__Modif__35BCFE0A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_RoleMenuButtonMapping] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_RoleM__Modif__300424B4]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_RoleMenuMapping] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__tmp_ms_xx__Modif__114A936A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Roles] ADD  CONSTRAINT [DF__tmp_ms_xx__Modif__114A936A]  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Seria__IsCle__07020F21]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_SerialNumber] ADD  CONSTRAINT [DF__SYS_Seria__IsCle__07020F21]  DEFAULT ((0)) FOR [IsClear]
GO
/****** Object:  Default [DF__SYS_Seria__Modif__07F6335A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_SerialNumber] ADD  CONSTRAINT [DF__SYS_Seria__Modif__07F6335A]  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF_SYS_SerialNumber_CreateTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_SerialNumber] ADD  CONSTRAINT [DF_SYS_SerialNumber_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_Syste__Statu__023D5A04]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Systems] ADD  DEFAULT ((2)) FOR [Status]
GO
/****** Object:  Default [DF__SYS_Syste__IsIni__03317E3D]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Systems] ADD  DEFAULT ((0)) FOR [IsInit]
GO
/****** Object:  Default [DF__SYS_Syste__Modif__0425A276]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_Systems] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_Table__Modif__0AD2A005]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_TableProperty] ADD  CONSTRAINT [DF__SYS_Table__Modif__0AD2A005]  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF_SYS_TableProperty_CreateTime]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_TableProperty] ADD  CONSTRAINT [DF_SYS_TableProperty_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_UserO__Modif__267ABA7A]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_UserOrganizationMapping] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_UserR__Modif__29572725]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_UserRoleMapping] ADD  DEFAULT (getdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Modif__6E8B6712]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenter] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Modif__6F7F8B4B]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenter] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Creat__7073AF84]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenter] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Creat__7167D3BD]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenter] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF_SYS_WorkCenter_ResourceReport]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenter] ADD  CONSTRAINT [DF_SYS_WorkCenter_ResourceReport]  DEFAULT ((0)) FOR [ResourceReport]
GO
/****** Object:  Default [DF__SYS_WorkC__Modif__74444068]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenterProcess] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Modif__753864A1]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenterProcess] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Creat__762C88DA]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenterProcess] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Creat__7720AD13]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenterProcess] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Modif__79FD19BE]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenterResources] ADD  DEFAULT (getutcdate()) FOR [ModifiedTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Modif__7AF13DF7]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenterResources] ADD  DEFAULT (getdate()) FOR [ModifiedLocalTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Creat__7BE56230]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenterResources] ADD  DEFAULT (getutcdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__SYS_WorkC__Creat__7CD98669]    Script Date: 07/27/2017 01:00:46 ******/
ALTER TABLE [dbo].[SYS_WorkCenterResources] ADD  DEFAULT (getdate()) FOR [CreateLocalTime]
GO
