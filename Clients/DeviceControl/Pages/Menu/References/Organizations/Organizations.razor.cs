// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.Organizations;

namespace DeviceControl.Pages.Menu.References.Organizations;

public sealed partial class Organizations : SectionBase<WsSqlOrganizationModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlOrganizationRepository().GetList(SqlCrudConfigSection);
    }
}