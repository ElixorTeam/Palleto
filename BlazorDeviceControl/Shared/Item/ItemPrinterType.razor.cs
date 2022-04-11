﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemPrinterType
    {
        #region Public and private fields and properties

        public PrinterTypeEntity? ItemCast { get => Item == null ? null : (PrinterTypeEntity)Item; set => Item = value; }

        #endregion

        #region Constructor and destructor

        public ItemPrinterType() : base()
        {
            //Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersTypes);
                ItemCast = null;
                ButtonSettings = new();
                IsBusy = false;
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    if (!IsBusy)
                    {
                        IsBusy = true;
                        switch (TableAction)
                        {
                            case DbTableAction.New:
                                ItemCast = new();
                                ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
                                ItemCast.IsMarked = false;
                                ItemCast.Name = "NEW PRINTER";
                                break;
                            default:
                                ItemCast = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(
                                    new FieldListEntity(new Dictionary<string, object?>
                                    { { DbField.IdentityId.ToString(), IdentityId } }), null);
                                break;
                        }
                        if (IdentityId != null && TableAction == DbTableAction.New)
                        {
                            ItemCast.IdentityId = (long)IdentityId;
                            ItemCast.Name = "NEW PRINTER_TYPE";
                        }
                        ButtonSettings = new(false, false, false, false, false, true, true);
                        IsBusy = false;
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
