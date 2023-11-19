using System.Windows.Forms;

namespace Ws.LabelCore.Controls;

partial class FormNavigationUserControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.layoutPanelUser = new System.Windows.Forms.TableLayoutPanel();
            this.layoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.layoutPanelUser.SuspendLayout();
            this.layoutPanelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutPanelUser
            // 
            this.layoutPanelUser.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelUser.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.layoutPanelUser.ColumnCount = 1;
            this.layoutPanelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanelUser.Controls.Add(this.layoutPanelTop, 0, 0);
            this.layoutPanelUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelUser.Location = new System.Drawing.Point(0, 0);
            this.layoutPanelUser.Name = "layoutPanelUser";
            this.layoutPanelUser.RowCount = 2;
            this.layoutPanelUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.layoutPanelUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.layoutPanelUser.Size = new System.Drawing.Size(1024, 668);
            this.layoutPanelUser.TabIndex = 0;
            // 
            // layoutPanelTop
            // 
            this.layoutPanelTop.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelTop.ColumnCount = 3;
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanelTop.Controls.Add(this.fieldTitle, 1, 0);
            this.layoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelTop.Location = new System.Drawing.Point(4, 4);
            this.layoutPanelTop.Name = "layoutPanelTop";
            this.layoutPanelTop.RowCount = 1;
            this.layoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelTop.Size = new System.Drawing.Size(1016, 33);
            this.layoutPanelTop.TabIndex = 70;
            // 
            // fieldTitle
            // 
            this.fieldTitle.AutoSize = true;
            this.fieldTitle.BackColor = System.Drawing.Color.Transparent;
            this.fieldTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTitle.ForeColor = System.Drawing.Color.Black;
            this.fieldTitle.Location = new System.Drawing.Point(53, 3);
            this.fieldTitle.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTitle.Name = "fieldTitle";
            this.fieldTitle.Size = new System.Drawing.Size(908, 27);
            this.fieldTitle.TabIndex = 21;
            this.fieldTitle.Text = "fieldTitle";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormNavigationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.layoutPanelUser);
            this.Name = "FormNavigationUserControl";
            this.Size = new System.Drawing.Size(1024, 668);
            this.layoutPanelUser.ResumeLayout(false);
            this.layoutPanelTop.ResumeLayout(false);
            this.layoutPanelTop.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel layoutPanelUser;
    private TableLayoutPanel layoutPanelTop;
    private Label fieldTitle;
}
