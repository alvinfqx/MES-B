/*设备关键参数标准值记录表头，对应INFO_EQUIPMENT_KEYSTAND表*/
CREATE TABLE [dbo].[EMS_EquipmentKeyStand](
[ID] [int] IDENTITY(1,1) NOT NULL,
[SystemID] [varchar](50)  NOT NULL,
[EKeyStandID] [varchar](30)  NOT NULL,
[EquipmentID] [varchar](30)  NULL,
[ClassID] [varchar](30) NULL,
[ItemID] [varchar](30) NULL,
[Model] [nvarchar](40) NULL,
[Status] [varchar](30)  NOT NULL,
[Comments] [nvarchar](max) NULL,
[Modifier] [varchar](30) NOT NULL,
[ModifiedTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[ModifiedLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
[Creator] [varchar](30) NOT NULL,
[CreateTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[CreateLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
CONSTRAINT [PK_EMS_EquipmentKeyStand] PRIMARY KEY CLUSTERED 
(
[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
create unique nonclustered index  [uniqueEKeyStandID] on [EMS_EquipmentKeyStand] ([SystemID] asc,  [EKeyStandID] asc );
INSERT[dbo].[SYS_TableProperty]([SystemID], [TablePropertyID], [TableName], [Database],[Modifier],[Creator]) VALUES(N'10042',130, N'EMS_EquipmentKeyStand', N'10042', N'adminy', N'adminy');
INSERT [dbo].[SYS_SerialNumber] ([SystemID], [SNID], [TableName], [TablePropertyID], [FirstSN], [SplitOne], [DateID], [SplitTwo], [Number], [IsClear], [Sequence],[Modifier], [Creator]) VALUES (N'10042', N'100420021213000130', N'EMS_EquipmentKeyStand', 130, N'10042', NULL, N'yyMM', NULL, N'1', 0, 1, N'adminy', N'adminy');



/*设备关键参数标准值记录表头明细*/
CREATE TABLE [dbo].[EMS_EquipmentKeyStandDetails](
[ID] [int] IDENTITY(1,1) NOT NULL,
[SystemID] [varchar](50)  NOT NULL,
[EKSDetailID] [varchar](30)  NOT NULL,
[EKeyStandID] [varchar](30)  NOT NULL,
[ProjectID] [varchar](30) NOT NULL,
[Attribute] [varchar](30) NULL,
[Standard] [nvarchar](30) NULL,
[NumStar] [numeric](8,2) NULL,
[NumEnd] [numeric](8,2) NULL,
[YValue] [nvarchar](2) NULL,
[NValue] [nvarchar](2) NULL,
[WordValue] [nvarchar](120) NULL,
[Status] [varchar](30)  NOT NULL,
[Comments] [nvarchar](max) NULL,
[Modifier] [varchar](30) NOT NULL,
[ModifiedTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[ModifiedLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
[Creator] [varchar](30) NOT NULL,
[CreateTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[CreateLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
CONSTRAINT [PK_EMS_EquipmentKeyStandDetails] PRIMARY KEY CLUSTERED 
(
[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
create unique nonclustered index  [uniqueEKSDetailID] on [EMS_EquipmentKeyStandDetails] ([SystemID] asc,  [EKSDetailID] asc );

/*设备关键参数记录表INFO_EQUIPMENT_KEYRECORD*/
CREATE TABLE [dbo].[EMS_EquipmentKeyRecord](
[ID] [int] IDENTITY(1,1) NOT NULL,
[SystemID] [varchar](50)  NOT NULL,
[EKRecordID] [varchar](30)  NOT NULL,
[Date] [datetime] NOT NULL DEFAULT  GETDATE(),
[ClassID] [varchar](30) NULL,
[OperationID] [varchar](30) NULL,
[TaskDispatchID] [varchar](30) NULL,
[EquipmentID] [varchar](30) NULL,
[ItemID] [varchar](30) NULL,
[Comments] [nvarchar](max) NULL,
[ResultOne] [nvarchar](60) NULL,
[ResultTwo] [nvarchar](60) NULL,
[ResultThree] [nvarchar](60) NULL,
[ListRemarkOne] [varchar](30) NULL,
[ListRemarkTwo] [varchar](30) NULL,
[ListRemarkThree] [varchar](30) NULL,
[WriteUserOne] [varchar](30) NULL,
[WriteUserTwo] [varchar](30) NULL,
[WriteUserThree] [varchar](30) NULL,
[QcCheckOne] [varchar](30) NULL,
[QcCheckTwo] [varchar](30) NULL,
[QcCheckThree] [varchar](30) NULL,
[QcUserOne] [varchar](30) NULL,
[QcUserTwo] [varchar](30) NULL,
[QcUserThree] [varchar](30) NULL,
[Modifier] [varchar](30) NOT NULL,
[ModifiedTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[ModifiedLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
[Creator] [varchar](30) NOT NULL,
[CreateTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[CreateLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
CONSTRAINT [PK_EMS_EquipmentKeyRecord] PRIMARY KEY CLUSTERED 
(
[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
create unique nonclustered index  [uniqueEKRecordID] on [EMS_EquipmentKeyRecord] ([SystemID] asc,  [EKRecordID] asc );

/*设备关键参数记录明细表*/
CREATE TABLE [dbo].[EMS_EquipmentKeyRecordDetail](
[ID] [int] IDENTITY(1,1) NOT NULL,
[SystemID] [varchar](50)  NOT NULL,
[EKRDetailID] [varchar](30)  NOT NULL,
[EKRecordID] [varchar](30) NOT NULL,
[EKSDetailID] [varchar](30) NULL,
[ProjectID] [varchar](30) NULL,
[Standard] [nvarchar](60) NULL,
[RecordOne] [nvarchar](60) NULL,
[RecordTwo] [nvarchar](60) NULL,
[RecordThree] [nvarchar](60) NULL,
[Comments] [nvarchar](max) NULL,
[Modifier] [varchar](30) NOT NULL,
[ModifiedTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[ModifiedLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
[Creator] [varchar](30) NOT NULL,
[CreateTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[CreateLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
CONSTRAINT [PK_EMS_EquipmentKeyRecordDetail] PRIMARY KEY CLUSTERED 
(
[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
create unique nonclustered index  [uniqueEKRDetailID] on [EMS_EquipmentKeyRecordDetail] ([SystemID] asc,  [EKRDetailID] asc );
/*细生管分派資料表，对应任務卡分派資料表、任務卡資源明細表*/
CREATE TABLE [dbo].[TDSFC_TaskDispatch](
[ID] [int] IDENTITY(1,1) NOT NULL,
[SystemID] [varchar](50)  NOT NULL,
[TDTaskDispatchID] [varchar](30)  NOT NULL,
[TaskID] [varchar](30)  NULL,
[TaskResourcID] [varchar](30) NULL,
[Week] [int] NULL,
[Series] [nvarchar](50) NULL,
[PlantHR] [Numeric](2,1)  NULL,
[PlantTime] [Numeric](3,1) NULL,
[KPartID] [nvarchar](50) NULL,
[SP] [int] NULL,
[MonoType] [nvarchar](50) NULL,
[HCapacity] [Numeric](8,2) NULL,
[Comments] [nvarchar](max) NULL,
[Modifier] [varchar](30) NOT NULL,
[ModifiedTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[ModifiedLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
[Creator] [varchar](30) NOT NULL,
[CreateTime] [datetime] NOT NULL DEFAULT  GETUTCDATE(),
[CreateLocalTime] [datetime] NOT NULL DEFAULT  GETDATE(),
CONSTRAINT [PK_TDSFC_TaskDispatch] PRIMARY KEY CLUSTERED 
(
[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
create unique nonclustered index  [uniqueTDTaskDispatchID] on [TDSFC_TaskDispatch] ([SystemID] asc,  [TDTaskDispatchID] asc );