﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.TableDirectModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Forms
{
    public partial class SelectPluForm : Form
    {
        #region Private fields and properties

        private DebugHelper Debug { get; } = DebugHelper.Instance;
        private FontsSettingsHelper FontsSettings { get; } = FontsSettingsHelper.Instance;
        private short PageNumber { get; set; }
        private List<PluDirect> OrderList { get; set; }
        private List<PluDirect> PluList { get; set; }
        private static ushort ColumnCount => 4;
        private static ushort PageSize => 20;
        private static ushort RowCount => 5;
        private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

        #endregion

        #region Constructor and destructor

        public SelectPluForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void PluListForm_Load(object sender, EventArgs e)
        {
            try
            {
                PluList = new PluDirect().GetPluList(UserSession.Scale);
                OrderList = new PluDirect().GetPluList(UserSession.Scale);

                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {PageNumber}";
                buttonClose.Text = LocaleCore.Buttons.Close;
                buttonLeftRoll.Text = LocaleCore.Buttons.Previous;
                buttonRightRoll.Text = LocaleCore.Buttons.Next;

                TopMost = !Debug.IsDebug;
                Width = Owner.Width;
                Height = Owner.Height;
                Left = Owner.Left;
                Top = Owner.Top;

                ControlPluEntity[,] controls = CreateControls(PluList.Skip(PageNumber * PageSize).Take(PageSize).ToArray());
                Setup(tableLayoutPanelPlu, controls);
                SetupSizes(controls);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private ControlPluEntity[,] CreateControls(IReadOnlyList<PluDirect> plus)
        {
            ControlPluEntity[,] controls = new ControlPluEntity[ColumnCount, RowCount];
            try
            {
                for (ushort rowNumber = 0, buttonNumber = 0; rowNumber < RowCount; ++rowNumber)
                {
                    for (ushort columnNumber = 0; columnNumber < ColumnCount; ++columnNumber)
                    {
                        if (buttonNumber >= plus.Count) break;
                        ControlPluEntity control = NewControlGroup(plus[buttonNumber], PageNumber, buttonNumber);
                        controls[columnNumber, rowNumber] = control;
                        buttonNumber++;
                    }
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            return controls;
        }

        private ControlPluEntity NewControlGroup(PluDirect plu, short pageNumber, ushort buttonNumber)
        {
            int tabIndex = buttonNumber + pageNumber * PageSize;
            Button buttonPlu = NewButtonPlu(plu, tabIndex);
            Label labelPluNumber = NewLabelPluNumber(plu, tabIndex, buttonPlu);
            Label labelPluType = NewLabelPluType(plu, tabIndex, buttonPlu);
            Label labelPluGtin = NewLabelPluGtin(plu, tabIndex, buttonPlu);
            return new(buttonPlu, labelPluNumber, labelPluType, labelPluGtin);
        }

        private Button NewButtonPlu(PluDirect plu, int tabIndex)
        {
            const ushort buttonWidth = 150;
            const ushort buttonHeight = 30;
            Button button = new()
            {
                Name = $@"buttonPlu{tabIndex}",
                Font = FontsSettings.FontLabelsBlack,
                AutoSize = false,
                Text = Environment.NewLine + plu.GoodsName,
                Dock = DockStyle.Fill,
                Size = new(buttonWidth, buttonHeight),
                Visible = true,
                Parent = tableLayoutPanelPlu,
                FlatStyle = FlatStyle.Flat,
                Location = new(0, 0),
                UseVisualStyleBackColor = true,
                BackColor = SystemColors.Control,
                TabIndex = tabIndex,
            };
            button.Click += ButtonPlu_Click;
            return button;
        }

        private Label NewLabelPluNumber(PluDirect plu, int tabIndex, Control buttonPlu)
        {
            Label labelPluNumber = new()
            {
                Name = $@"labelPluNumber{tabIndex}",
                Font = FontsSettings.FontLabelsBlack,
                AutoSize = false,
                Text = $@"{LocaleCore.Table.Number} {plu.PLU}",
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Dock = DockStyle.None,
                BackColor = plu.PLU > 0 ? Color.LightGreen : Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = tabIndex,
            };
            labelPluNumber.MouseClick += ButtonPlu_Click;
            return labelPluNumber;
        }

        private Label NewLabelPluType(PluDirect plu, int tabIndex, Control buttonPlu)
        {
            Label labelPluType = new()
            {
                Name = $@"labelPluType{tabIndex}",
                Font = FontsSettings.FontLabelsBlack,
                AutoSize = false,
                Text = plu.IsCheckWeight == false ? LocaleCore.Scales.PluIsPiece : LocaleCore.Scales.PluIsWeight,
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Dock = DockStyle.None,
                BackColor = plu.IsCheckWeight ? Color.LightGreen : Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = tabIndex,
            };
            labelPluType.MouseClick += ButtonPlu_Click;
            return labelPluType;
        }

        private Label NewLabelPluGtin(PluDirect plu, int tabIndex, Control buttonPlu)
        {
            Label labelPluGtin = new()
            {
                Name = $@"labelPluGtin{tabIndex}",
                Font = FontsSettings.FontMinimum,
                AutoSize = false,
                Text = !string.IsNullOrEmpty(plu.GTIN) ? @$"{LocaleCore.Scales.PluGtin} {plu.GTIN}" : LocaleCore.Scales.PluGtinNotSet,
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Dock = DockStyle.None,
                BackColor = !string.IsNullOrEmpty(plu.GTIN) ? Color.LightGreen : Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = tabIndex,
            };
            labelPluGtin.MouseClick += ButtonPlu_Click;
            return labelPluGtin;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonPlu_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.Order = null;
                ushort tabIndex = 0;
                if (sender is Control control)
                    tabIndex = (ushort)control.TabIndex;
                if (OrderList?.Count >= tabIndex)
                {
                    UserSession.SetCurrentPlu(OrderList[tabIndex]);
                    UserSession.Plu.LoadTemplate();
                    DialogResult = DialogResult.OK;
                }
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonPreviousRoll_Click(object sender, EventArgs e)
        {
            try
            {
                tableLayoutPanelPlu.Visible = false;
                short saveCurrentPage = PageNumber;
                PageNumber = (short)(PageNumber > 0 ? PageNumber - 1 : 0);
                if (PageNumber < 0) PageNumber = 0;
                if (PageNumber == saveCurrentPage)
                    return;

                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {PageNumber}";
                ControlPluEntity[,] controls = CreateControls(PluList.Skip(PageNumber * PageSize).Take(PageSize).ToArray());
                Setup(tableLayoutPanelPlu, controls);
                SetupSizes(controls);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                tableLayoutPanelPlu.Visible = true;
            }
        }

        private void ButtonNextRoll_Click(object sender, EventArgs e)
        {
            try
            {
                tableLayoutPanelPlu.Visible = false;
                short saveCurrentPage = PageNumber;
                short countPage = (short)(PluList.Count / PageSize);
                PageNumber = (short)(PageNumber < countPage ? PageNumber + 1 : countPage);
                if (PageNumber > countPage) PageNumber = (short)(countPage - 1);
                if (PageNumber == saveCurrentPage)
                    return;

                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {PageNumber}";

                ControlPluEntity[,] controls = CreateControls(PluList.Skip(PageNumber * PageSize).Take(PageSize).ToArray());
                Setup(tableLayoutPanelPlu, controls);
                SetupSizes(controls);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                tableLayoutPanelPlu.Visible = true;
            }
        }

        private void SetupPanel(TableLayoutPanel panel, ushort columnCount, ushort rowCount)
        {
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();
            panel.ColumnCount = 0;
            panel.RowCount = 0;
            AddColumns(panel, columnCount);
            AddRows(panel, rowCount);
        }

        private void AddColumns(TableLayoutPanel panel, ushort columnCount)
        {
            panel.ColumnStyles.Clear();
            panel.ColumnCount += columnCount;
            ushort width = (ushort)(100 / columnCount);
            for (ushort i = 0; i < panel.ColumnCount; i++)
            {
                panel.ColumnStyles.Add(new(SizeType.Percent, width));
            }
        }

        private void AddRows(TableLayoutPanel panel, ushort rowCount)
        {
            panel.RowStyles.Clear();
            panel.RowCount += rowCount;
            ushort height = (ushort)(100 / panel.RowCount);
            for (ushort i = 0; i < panel.RowCount; i++)
            {
                panel.RowStyles.Add(new(SizeType.Percent, height));
            }
        }

        private void ClearChilds(TableLayoutPanel panel)
        {
            foreach (object control in panel.Controls)
            {
                if (control is TableLayoutPanel subPanel)
                {
                    tableLayoutPanelActions = subPanel;
                    tableLayoutPanelActions.Parent = null;
                }
            }
            panel.Controls.Clear();
        }

        private void Setup(TableLayoutPanel panelPlu, ControlPluEntity[,] controls)
        {
            panelPlu.Visible = false;
            ClearChilds(panelPlu);
            SetupPanel(panelPlu, (ushort)(controls.GetUpperBound(0) + 1), (ushort)(controls.GetUpperBound(1) + 1));

            for (ushort column = 0; column <= controls.GetUpperBound(0); column++)
            {
                for (ushort row = 0; row <= controls.GetUpperBound(1); row++)
                {
                    ControlPluEntity control = controls[column, row];
                    if (control != null)
                    {
                        panelPlu.Controls.Add(control.ButtonPlu, column, row);
                    }
                }
            }

            if (tableLayoutPanelActions != null)
            {
                AddRows(panelPlu, 1);
                tableLayoutPanelActions.Parent = panelPlu;
                panelPlu.SetColumn(tableLayoutPanelActions, 0);
                panelPlu.SetRow(tableLayoutPanelActions, panelPlu.RowCount - 1);
                panelPlu.SetColumnSpan(tableLayoutPanelActions, panelPlu.ColumnCount);
                tableLayoutPanelActions.Dock = DockStyle.Fill;
            }

            panelPlu.Refresh();
            panelPlu.Visible = true;
        }

        private void SetupSizes(ControlPluEntity[,] controls)
        {
            foreach (ControlPluEntity control in controls)
            {
                control?.SetupSizes();
            }
        }

        #endregion
    }
}
