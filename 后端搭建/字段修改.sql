--更新数据

INSERT [dbo].[MES_Parameter] ([SystemID], [ParameterID], [Module], [Code], [Name], [Setting], [Option], [Value], [Status], [Comments], [Modifier], [ModifiedTime], [ModifiedLocalTime], [Creator], [CreateTime], [CreateLocalTime]) VALUES (N'100AA', N'100AA1101213000001', N'100AA0201213000040', N'SFC001', N'是否手動開立工單', N'100AA02012130000A7', N'', N'0', N'100AA0201213000001', N'控管製令單維護是否有新增功能', N'100AA0041213000000', GETUTCDATE(), GETDATE(), N'100AA0041213000000', GETUTCDATE(),  GETDATE());
INSERT [dbo].[MES_Parameter] ([SystemID], [ParameterID], [Module], [Code], [Name], [Setting], [Option], [Value], [Status], [Comments], [Modifier], [ModifiedTime], [ModifiedLocalTime], [Creator], [CreateTime], [CreateLocalTime]) VALUES (N'100AA', N'100AA1101213000002', N'100AA0201213000042', N'QCS001', N'品質判定方式', N'100AA02012130000A8', N'100AA0191213000069', N'100AA02012130000B1', N'100AA0201213000001', N'影響檢驗單(表頭)判定方式',  N'100AA0041213000000', GETUTCDATE(), GETDATE(), N'100AA0041213000000', GETUTCDATE(),  GETDATE());
INSERT [dbo].[MES_Parameter] ([SystemID], [ParameterID], [Module], [Code], [Name], [Setting], [Option], [Value], [Status], [Comments], [Modifier], [ModifiedTime], [ModifiedLocalTime], [Creator], [CreateTime], [CreateLocalTime]) VALUES (N'100AA', N'100AA1101213000003', N'100AA0201213000042', N'QCS002', N'檢驗項目判定方式', N'100AA02012130000A8', N'100AA019121300006A', N'100AA02012130000B3', N'100AA0201213000001', N'影響檢驗單項目(明細)判定方式', N'100AA0041213000000', GETUTCDATE(), GETDATE(), N'100AA0041213000000', GETUTCDATE(),  GETDATE());

alter table [QCS_InspectionDocumentDetails] alter column [Aql] [varchar](30);
alter table [QCS_InspectionDocumentDetails] alter column [InspectionStandard] [varchar](30);
alter table [QCS_InspectionDocumentDetails] alter column [AttributeType] [varchar](30);

alter table [SFC_AbnormalHour] alter column [Hour] [BIGINT];

alter table [SFC_CompletionOrder] alter column [LaborHour] [BIGINT];
alter table [SFC_CompletionOrder] alter column [UnLaborHour] [BIGINT];
alter table [SFC_CompletionOrder] alter column [MachineHour] [BIGINT];
alter table [SFC_CompletionOrder] alter column [UnMachineHour] [BIGINT];
EXEC sp_rename 'SFC_CompletionOrder.NextOPerationID',NextOperationID,'column';
EXEC sp_rename 'SFC_TransferOrder.FabricatedProcessID', FabMoProcessID,'column';
EXEC sp_rename 'SFC_TransferOrder.FabricatedOperationID', FabMoOperationID,'column';
alter table [SFC_TransferOrder] ADD [AcceptUser] [varchar](30);
alter table [SFC_TransferOrder] ADD [NextFabMoProcessID] [varchar](30);
alter table [SFC_TransferOrder] ADD [NextFabMoOperationID] [varchar](30);
alter table [SFC_TransferOrder] ADD [NextProcessID] [varchar](30);
alter table [SFC_TransferOrder] ADD [NextOperationID] [varchar](30);
alter table [SFC_TransferOrder] ADD [IPQCSequence] [INT];

alter table [SFC_FabricatedMother] ADD [ControlUserID] [varchar](30);

alter table [SFC_FabMoProcess] ADD [RepairQuantity] [numeric](18, 6) NULL DEFAULT 0;
alter table [SFC_FabMoOperation] ADD [RepairQuantity] [numeric](18, 6) NULL DEFAULT 0;


alter table [SFC_ItemProcess] alter column [Sequence] [varchar](4);
alter table [SFC_ItemOperation] alter column [Sequence] [varchar](4);




--2017年9月5日11:05:29
alter table [SFC_ItemProcessAlternativeRelationShip] drop constraint [DF__SFC_ItemP__Seque__314D4EA8];（不同的数据库约束位数不同）
alter table [SFC_ItemProcessAlternativeRelationShip] alter column [Sequence] [varchar](4);
alter table [SFC_ItemMaterial] alter column [Sequence] [varchar](4);


EXEC sp_rename 'SFC_FabricatedMother.FisishDate',FinishDate,'column';


2017年9月11日21:40:20
alter table [QCS_InspectionDocumentDetails] alter column [InspectionStandard] [nvarchar](120);


2017年9月12日11:08:50
update [SYS_Parameters] set [Code]='0.65',[Name]='0.65' where [ParameterID]='100AA0201213000066'

2017年9月14日17:57:42
alter table [EMS_Equipment] ADD [DAQMachID] [nvarchar](50);


2017年9月18日17:37:32
INSERT [dbo].[MES_Parameter] ([SystemID], [ParameterID], [Module], [Code], [Name], [Setting], [Option], [Value], [Status], [Comments], [Modifier], [ModifiedTime], [ModifiedLocalTime], [Creator], [CreateTime], [CreateLocalTime]) 
VALUES (N'100AA', N'100AA1101213000004', N'100AA0201213000040', N'SFC002', N'异常说明是否必输', N'100AA02012130000A7', N'', N'True', N'100AA0201213000001', N'异常说明是否必输', N'100AA0041213000000', GETUTCDATE(), GETDATE(), N'100AA0041213000000', GETUTCDATE(),  GETDATE());
alter table [SFC_CompletionResource] alter column [Hour] [BigInt];

2017年9月25日10:42:44
alter table [SFC_TaskDispatch] alter column [DispatchQuantity] [numeric](18,6);


2017年9月26日15:22:25
EXEC sp_rename 'SFC_BatchAttribute.AutoNumberID',AutoNumberRecordID,'column';  //先检查是否已经修改过这个字段
alter table [SFC_FabricatedMother] alter column [SplitSequence] [int];

2017年9月27日15:33:46
update [SYS_Parameters] set [Code]='S' where [ParameterID]='100AA0201213000010'
update [SYS_Parameters] set [Code]='V' where [ParameterID]='100AA0201213000011'
update [SYS_Parameters] set [Name]='共用' where [ParameterID]='100AA0201213000012'
create unique nonclustered index  [uniqueParameterID] on [SYS_Parameters] ([SystemID] asc,  [ParameterID] asc )
create unique nonclustered index  [uniqueParameterID] on [MES_Parameter] ([SystemID] asc,  [ParameterID] asc )
（台湾部分更新到这里）

2017年9月28日11:57:48
update [SYS_Parameters] set [Code]='D' where [ParameterID]='100AA0201213000017'
update [SYS_Parameters] set [Code]='C' where [ParameterID]='100AA0201213000018'
update [SYS_Parameters] set [Code]='N' where [ParameterID]='100AA0201213000019'
update [SYS_Parameters] set [Code]='C' where [ParameterID]='100AA0201213000012'
update [SYS_Parameters] set [Code]='1' where [ParameterID]='100AA0201213000099'
update [SYS_Parameters] set [Code]='2' where [ParameterID]='100AA020121300009A'

INSERT [SYS_Parameters] ([SystemID],[ParameterID],[ParameterTypeID],[UsingType],[Code],[Description],[Name],[DescriptionOne],[IsSystem], [IsEnable], [IsDefault], [Sequence], [Modifier], [Creator]) VALUES(N'100AA', N'100AA02012130000A3', N'100AA0191213000066', '0', N'1',N'', N'生管派工', N'', 0, 1, 0, 10, N'100AA0041213000000', N'100AA0041213000000');
INSERT [SYS_Parameters] ([SystemID],[ParameterID],[ParameterTypeID],[UsingType],[Code],[Description],[Name],[DescriptionOne],[IsSystem], [IsEnable], [IsDefault], [Sequence], [Modifier], [Creator]) VALUES(N'100AA', N'100AA02012130000A4', N'100AA0191213000066', '0', N'2',N'', N'现场派工', N'', 0, 1, 0, 20, N'100AA0041213000000', N'100AA0041213000000');
INSERT [SYS_Parameters] ([SystemID],[ParameterID],[ParameterTypeID],[UsingType],[Code],[Description],[Name],[DescriptionOne],[IsSystem], [IsEnable], [IsDefault], [Sequence], [Modifier], [Creator]) VALUES(N'100AA', N'100AA02012130000A5', N'100AA0191213000055', '0', N'0',N'', N'待机', N'', 0, 1, 0, 30, N'100AA0041213000000', N'100AA0041213000000');
INSERT [SYS_Parameters] ([SystemID],[ParameterID],[ParameterTypeID],[UsingType],[Code],[Description],[Name],[DescriptionOne],[IsSystem], [IsEnable], [IsDefault], [Sequence], [Modifier], [Creator]) VALUES(N'100AA', N'100AA02012130000A6', N'100AA0191213000055', '0', N'6',N'', N'关机', N'', 0, 1, 0, 40, N'100AA0041213000000', N'100AA0041213000000');

2017年10月17日18:16:14
alter table [SYS_Projects] alter column [Code] [nvarchar](30);


2017年10月19日16:32:11
alter table [SYS_CalendarDetails] alter column [Wkhour] numeric(14, 2);

2017年10月30日19:41:08
alter table [SFC_TaskDispatch] ADD [InMESUserID] [varchar](30);
alter table [SFC_TaskDispatch] ADD [OutMESUserID] [varchar](30);