-- Scales - Import 1 Nomenclature
-- 1. Connect from PALYCH\LUTON
-- use [ScalesDB]
-- After all see the script: Scales - PLU insert new
set nocount on
declare @products table ([code] nvarchar(255), [plu] int, [num] int)
-- 2. Setup settings.
declare @commit bit = 0
declare @update bit = 1
-- 3. Fill PLU table from DWH - 1C.
declare @nom_changed table ([Id] int)
-- ���� �����.
insert into @products([code],[plu]) values(N'���00053844',631) -- �������� "���� �����" ������� ������
insert into @products([code],[plu]) values(N'���00053845',655) -- �������� "���� �����" ������� ��������
insert into @products([code],[plu]) values(N'���00053846',679) -- �������� "���� �����" ����� �����
insert into @products([code],[plu]) values(N'���00053847',662) -- �������� "���� �����" ������� ���������
insert into @products([code],[plu]) values(N'���00053848',648) -- �������� "���� �����" ������� ������
-- �� ����� �10 - SCALES-MON-004.
insert into @products([code],[plu],[num]) values(N'���00008197', 102, 5) -- ����������  �.�. ����
insert into @products([code],[plu],[num]) values(N'���00002181', 103, 5) -- ������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000431', 104, 5) -- ���������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000477', 106, 5) -- ����������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000472', 107, 5) -- ����������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000478', 108, 5) -- ��������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000470', 109, 5) -- ��������� �.�.
insert into @products([code],[plu],[num]) values(N'���00012800', 112, 5) -- ��������� �.�.
insert into @products([code],[plu],[num]) values(N'���00011413', 113, 5) -- ������������ �.�.
insert into @products([code],[plu],[num]) values(N'000�0000474', 114, 5) -- ������ "��������" �.�.
insert into @products([code],[plu],[num]) values(N'000�0000379', 115, 5) -- ���������� � ���������
insert into @products([code],[plu],[num]) values(N'000�0000475', 117, 5) -- ������ ��-������ �.�.
insert into @products([code],[plu],[num]) values(N'000�0000386', 120, 5) -- ��������� ��������
insert into @products([code],[plu],[num]) values(N'���00037388', 137, 5) -- �������� � ��������� �.�.
insert into @products([code],[plu],[num]) values(N'���00011404', 141, 5) -- ������ �/�
insert into @products([code],[plu],[num]) values(N'���00011405', 142, 4) -- ������ �/� 400 �
insert into @products([code],[plu],[num]) values(N'���00041861', 143, 5) -- ���������� �.�. 1 ����
insert into @products([code],[plu],[num]) values(N'���00041860', 144, 5) -- ��������� �������� �.�. 1 ����
insert into @products([code],[plu],[num]) values(N'���00041859', 145, 5) -- ������������ �.�. 1 ����
insert into @products([code],[plu],[num]) values(N'���00053241', 146, 5) -- � ������� ��-����������
insert into @products([code],[plu],[num]) values(N'���00026237', 147, 5) -- �������� �.�.
insert into @products([code],[plu],[num]) values(N'���00049186', 149, 5) -- ���������� �.�. ���� ��
-- �� ����� �11 - SCALES-MON-005.
insert into @products([code],[plu],[num]) values(N'���00008197', 102, 5) -- ����������  �.�. ����
insert into @products([code],[plu],[num]) values(N'���00002181', 103, 5) -- ������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000431', 104, 5) -- ���������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000477', 106, 5) -- ����������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000472', 107, 5) -- ����������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000478', 108, 5) -- ��������� �.�.
insert into @products([code],[plu],[num]) values(N'000�0000470', 109, 5) -- ��������� �.�.
insert into @products([code],[plu],[num]) values(N'���00012800', 112, 5) -- ��������� �.�.
insert into @products([code],[plu],[num]) values(N'���00011413', 113, 5) -- ������������ �.�.
insert into @products([code],[plu],[num]) values(N'000�0000474', 114, 5) -- ������ "��������" �.�.
insert into @products([code],[plu],[num]) values(N'000�0000379', 115, 5) -- ���������� � ���������
insert into @products([code],[plu],[num]) values(N'000�0000475', 117, 5) -- ������ ��-������ �.�.
insert into @products([code],[plu],[num]) values(N'000�0000386', 120, 5) -- ��������� ��������
insert into @products([code],[plu],[num]) values(N'���00026237', 132, 5) -- �������� �.�.
insert into @products([code],[plu],[num]) values(N'���00037388', 137, 5) -- �������� � ��������� �.�.
insert into @products([code],[plu],[num]) values(N'���00041861', 147, 5) -- ���������� �.�. 1 ����
insert into @products([code],[plu],[num]) values(N'���00041860', 148, 5) -- ��������� �������� �.�. 1 ����
insert into @products([code],[plu],[num]) values(N'���00041859', 149, 5) -- ������������ �.�. 1 ����
insert into @products([code],[plu],[num]) values(N'���00053241', 153, 5) -- � ������� ��-����������
insert into @products([code],[plu],[num]) values(N'���00049186', 155, 5) -- ���������� �.�. ���� ��
insert into @products([code],[plu],[num]) values(N'���00011404', 156, 5) -- ������ �/�
-- Remote data.
declare @codes nvarchar(max)
select @codes = coalesce(@codes + ''''', N''''', 'N''''') + [code] from @products
set @codes = @codes + ''''''
declare @cmd_select nvarchar(max) = '
SELECT 
	 [ID]
	,[Code]
	,[Name] 
	,UPPER([VSDWH].[DW].[fnGetGuid1Cv2] ([CodeInIS])) [IdRRef]
    ,[Parents] [Parents]
	,cast([SerializedRepresentationObject] as nvarchar(4000)) [SRO]
	,[CreateDate]
	,[DLM] [ModifiedDate]
	,[Weighted]
FROM [VSDWH].[DW].[DimNomenclatures] 
WHERE [Marked]=0
AND [Code] in (' + @codes + ')
ORDER BY [Name]
'
-- Table-variable.
declare @remote_data table ([ID] int, [Code] nvarchar(30), [Name] nvarchar(150), [IdRRef] uniqueidentifier, [Parents] nvarchar(1024),
	[SRO] nvarchar(4000), [CreateDate] datetime, [ModifiedDate] datetime, [Weighted] bit)
-- Push remote data into table-variable.
declare @tsql nvarchar(max)
set @tsql = 'SELECT * FROM OPENQUERY([SQLSRSP01\LEEDS], ''' + @cmd_select + ''')'
insert into @remote_data exec(@tsql)
-- Cusrsor for @remote_data.
begin tran
	declare @id int
	declare @code nvarchar(30)
	declare @name nvarchar(150)
	declare @idRRef uniqueidentifier
	declare @parents nvarchar(1024)
	declare @sro xml
	declare @createDate datetime
	declare @modifiedDate datetime
	declare @weighted bit
	declare cur cursor for select [ID], [Code], [Name], [IdRRef], [Parents], [SRO], [CreateDate], [ModifiedDate], [Weighted] 
		from @remote_data
	open cur
	fetch next from cur into @id, @code, @name, @idRRef, @parents, @sro, @createDate, @modifiedDate, @weighted
	while @@fetch_status = 0 begin
		-- Check exists local data.
		if (@update = 1) begin
			if (exists (select 1 from [db_scales].[Nomenclature] where [Id]=@id)) begin
				update [db_scales].[Nomenclature] set 
					 [Code]=@code
					,[Name]=@name
					,[IdRRef]=@idRRef
					,[SerializedRepresentationObject]=@sro
					,[CreateDate]=@createDate
					,[ModifiedDate]=@modifiedDate
					,[Weighted]=@weighted
				where [Id]=@id
				print N'[*] [db_scales].[Nomenclature] updated where [Id]=' + cast(@id as nvarchar(255))
			end else begin
				insert into @nom_changed([Id]) values (@id)
				print N'[!] [db_scales].[Nomenclature] not updated where [Id]=' + cast(@id as nvarchar(255))
			end
		end else begin
			if not(exists (select 1 from [db_scales].[Nomenclature] where [Id]=@id)) begin
				insert into [db_scales].[Nomenclature]([ID], [Code], [Name], [IdRRef], [SerializedRepresentationObject], 
					[CreateDate], [ModifiedDate], [Weighted])
					values (@id, @code, @name, @idRRef, @sro, @createDate, @modifiedDate, @weighted)
				print N'[+] [db_scales].[Nomenclature] inserted where [Id]=' + cast(@id as nvarchar(255))
			end else begin
				insert into @nom_changed([Id]) values (@id)
				print N'[!] [db_scales].[Nomenclature] not inserted where [Id]=' + cast(@id as nvarchar(255))
			end
		end
		-- Next cursor record.
		fetch next from cur into @id, @code, @name, @idRRef, @parents, @sro, @createDate, @modifiedDate, @weighted
	end
	close cur
	deallocate cur
-- Uncommitted local data.
select * from [db_scales].[Nomenclature] [N] 
where [N].[Code] in (select [Code] from @products) and [N].[Id] not in (select [Id] from @nom_changed) order by [Id]
-- Settings.
if (@commit = 1) begin
	commit tran
end else begin
	rollback tran
end
set nocount off
-- Committed local data.
select * from [db_scales].[Nomenclature] [N] where [N].[Code] in (select [Code] from @products) order by [Id]