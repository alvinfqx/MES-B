--��������

INSERT [dbo].[MES_Parameter] ([SystemID], [ParameterID], [Module], [Code], [Name], [Setting], [Option], [Value], [Status], [Comments], [Modifier], [ModifiedTime], [ModifiedLocalTime], [Creator], [CreateTime], [CreateLocalTime]) VALUES (N'100AA', N'100AA1101213000001', N'100AA0201213000040', N'SFC001', N'�Ƿ��ք��_������', N'100AA02012130000A7', N'', N'0', N'100AA0201213000001', N'�ع��u��ξS�o�Ƿ�����������', N'100AA0041213000000', GETUTCDATE(), GETDATE(), N'100AA0041213000000', GETUTCDATE(),  GETDATE());
INSERT [dbo].[MES_Parameter] ([SystemID], [ParameterID], [Module], [Code], [Name], [Setting], [Option], [Value], [Status], [Comments], [Modifier], [ModifiedTime], [ModifiedLocalTime], [Creator], [CreateTime], [CreateLocalTime]) VALUES (N'100AA', N'100AA1101213000002', N'100AA0201213000042', N'QCS001', N'Ʒ�|�ж���ʽ', N'100AA02012130000A8', N'100AA0191213000069', N'100AA02012130000B1', N'100AA0201213000001', N'Ӱ푙z��(���^)�ж���ʽ',  N'100AA0041213000000', GETUTCDATE(), GETDATE(), N'100AA0041213000000', GETUTCDATE(),  GETDATE());
INSERT [dbo].[MES_Parameter] ([SystemID], [ParameterID], [Module], [Code], [Name], [Setting], [Option], [Value], [Status], [Comments], [Modifier], [ModifiedTime], [ModifiedLocalTime], [Creator], [CreateTime], [CreateLocalTime]) VALUES (N'100AA', N'100AA1101213000003', N'100AA0201213000042', N'QCS002', N'�z��Ŀ�ж���ʽ', N'100AA02012130000A8', N'100AA019121300006A', N'100AA02012130000B3', N'100AA0201213000001', N'Ӱ푙z���Ŀ(����)�ж���ʽ', N'100AA0041213000000', GETUTCDATE(), GETDATE(), N'100AA0041213000000', GETUTCDATE(),  GETDATE());

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




--2017��9��5��11:05:29
alter table [SFC_ItemProcessAlternativeRelationShip] drop constraint [DF__SFC_ItemP__Seque__314D4EA8];����ͬ�����ݿ�Լ��λ����ͬ��
alter table [SFC_ItemProcessAlternativeRelationShip] alter column [Sequence] [varchar](4);
alter table [SFC_ItemMaterial] alter column [Sequence] [varchar](4);


EXEC sp_rename 'SFC_FabricatedMother.FisishDate',FinishDate,'column';


2017��9��11��21:40:20
alter table [QCS_InspectionDocumentDetails] alter column [InspectionStandard] [nvarchar](120);


2017��9��12��11:08:50
update [SYS_Parameters] set [Code]='0.65',[Name]='0.65' where [ParameterID]='100AA0201213000066'

2017��9��14��17:57:42
alter table [EMS_Equipment] ADD [DAQMachID] [nvarchar](50);


2017��9��18��17:37:32
INSERT [dbo].[MES_Parameter] ([SystemID], [ParameterID], [Module], [Code], [Name], [Setting], [Option], [Value], [Status], [Comments], [Modifier], [ModifiedTime], [ModifiedLocalTime], [Creator], [CreateTime], [CreateLocalTime]) 
VALUES (N'100AA', N'100AA1101213000004', N'100AA0201213000040', N'SFC002', N'�쳣˵���Ƿ����', N'100AA02012130000A7', N'', N'True', N'100AA0201213000001', N'�쳣˵���Ƿ����', N'100AA0041213000000', GETUTCDATE(), GETDATE(), N'100AA0041213000000', GETUTCDATE(),  GETDATE());
alter table [SFC_CompletionResource] alter column [Hour] [BigInt];

2017��9��25��10:42:44
alter table [SFC_TaskDispatch] alter column [DispatchQuantity] [numeric](18,6);


2017��9��26��15:22:25
EXEC sp_rename 'SFC_BatchAttribute.AutoNumberID',AutoNumberRecordID,'column';  //�ȼ���Ƿ��Ѿ��޸Ĺ�����ֶ�
alter table [SFC_FabricatedMother] alter column [SplitSequence] [int];

2017��9��27��15:33:46
update [SYS_Parameters] set [Code]='S' where [ParameterID]='100AA0201213000010'
update [SYS_Parameters] set [Code]='V' where [ParameterID]='100AA0201213000011'
update [SYS_Parameters] set [Name]='����' where [ParameterID]='100AA0201213000012'
create unique nonclustered index  [uniqueParameterID] on [SYS_Parameters] ([SystemID] asc,  [ParameterID] asc )
create unique nonclustered index  [uniqueParameterID] on [MES_Parameter] ([SystemID] asc,  [ParameterID] asc )
��̨�岿�ָ��µ����

2017��9��28��11:57:48
update [SYS_Parameters] set [Code]='D' where [ParameterID]='100AA0201213000017'
update [SYS_Parameters] set [Code]='C' where [ParameterID]='100AA0201213000018'
update [SYS_Parameters] set [Code]='N' where [ParameterID]='100AA0201213000019'
update [SYS_Parameters] set [Code]='C' where [ParameterID]='100AA0201213000012'
update [SYS_Parameters] set [Code]='1' where [ParameterID]='100AA0201213000099'
update [SYS_Parameters] set [Code]='2' where [ParameterID]='100AA020121300009A'

INSERT [SYS_Parameters] ([SystemID],[ParameterID],[ParameterTypeID],[UsingType],[Code],[Description],[Name],[DescriptionOne],[IsSystem], [IsEnable], [IsDefault], [Sequence], [Modifier], [Creator]) VALUES(N'100AA', N'100AA02012130000A3', N'100AA0191213000066', '0', N'1',N'', N'�����ɹ�', N'', 0, 1, 0, 10, N'100AA0041213000000', N'100AA0041213000000');
INSERT [SYS_Parameters] ([SystemID],[ParameterID],[ParameterTypeID],[UsingType],[Code],[Description],[Name],[DescriptionOne],[IsSystem], [IsEnable], [IsDefault], [Sequence], [Modifier], [Creator]) VALUES(N'100AA', N'100AA02012130000A4', N'100AA0191213000066', '0', N'2',N'', N'�ֳ��ɹ�', N'', 0, 1, 0, 20, N'100AA0041213000000', N'100AA0041213000000');
INSERT [SYS_Parameters] ([SystemID],[ParameterID],[ParameterTypeID],[UsingType],[Code],[Description],[Name],[DescriptionOne],[IsSystem], [IsEnable], [IsDefault], [Sequence], [Modifier], [Creator]) VALUES(N'100AA', N'100AA02012130000A5', N'100AA0191213000055', '0', N'0',N'', N'����', N'', 0, 1, 0, 30, N'100AA0041213000000', N'100AA0041213000000');
INSERT [SYS_Parameters] ([SystemID],[ParameterID],[ParameterTypeID],[UsingType],[Code],[Description],[Name],[DescriptionOne],[IsSystem], [IsEnable], [IsDefault], [Sequence], [Modifier], [Creator]) VALUES(N'100AA', N'100AA02012130000A6', N'100AA0191213000055', '0', N'6',N'', N'�ػ�', N'', 0, 1, 0, 40, N'100AA0041213000000', N'100AA0041213000000');

2017��10��17��18:16:14
alter table [SYS_Projects] alter column [Code] [nvarchar](30);


2017��10��19��16:32:11
alter table [SYS_CalendarDetails] alter column [Wkhour] numeric(14, 2);

2017��10��30��19:41:08
alter table [SFC_TaskDispatch] ADD [InMESUserID] [varchar](30);
alter table [SFC_TaskDispatch] ADD [OutMESUserID] [varchar](30);