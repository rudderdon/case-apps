<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_RevisionClouds
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_RevisionClouds))
    Me.DataGridViewRevs = New System.Windows.Forms.DataGridView()
    Me.colRevSheetNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRevSheetName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRevViewName = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRevNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRevDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRevIssuedTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRevIssuedBy = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRevComment = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.colRevMark = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.GroupBoxRevs = New System.Windows.Forms.GroupBox()
    Me.SaveFileDialogTXT = New System.Windows.Forms.SaveFileDialog()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ButtonExport = New System.Windows.Forms.Button()
    Me.ButtonHelp = New System.Windows.Forms.Button()
    CType(Me.DataGridViewRevs, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxRevs.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'DataGridViewRevs
    '
    Me.DataGridViewRevs.AllowUserToAddRows = False
    Me.DataGridViewRevs.AllowUserToDeleteRows = False
    Me.DataGridViewRevs.AllowUserToResizeRows = False
    Me.DataGridViewRevs.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
    Me.DataGridViewRevs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.DataGridViewRevs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridViewRevs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRevSheetNumber, Me.colRevSheetName, Me.colRevViewName, Me.colRevNumber, Me.Column1, Me.colRevDate, Me.colRevIssuedTo, Me.colRevIssuedBy, Me.colRevComment, Me.colRevMark, Me.Column2})
    DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
    DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight
    DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
    DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
    Me.DataGridViewRevs.DefaultCellStyle = DataGridViewCellStyle1
    Me.DataGridViewRevs.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DataGridViewRevs.GridColor = System.Drawing.Color.LightGray
    Me.DataGridViewRevs.Location = New System.Drawing.Point(3, 16)
    Me.DataGridViewRevs.Name = "DataGridViewRevs"
    Me.DataGridViewRevs.ReadOnly = True
    Me.DataGridViewRevs.RowHeadersVisible = False
    Me.DataGridViewRevs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    Me.DataGridViewRevs.Size = New System.Drawing.Size(707, 369)
    Me.DataGridViewRevs.TabIndex = 1
    '
    'colRevSheetNumber
    '
    Me.colRevSheetNumber.DataPropertyName = "SheetNumber"
    Me.colRevSheetNumber.HeaderText = "Sheet #"
    Me.colRevSheetNumber.Name = "colRevSheetNumber"
    Me.colRevSheetNumber.ReadOnly = True
    Me.colRevSheetNumber.Width = 75
    '
    'colRevSheetName
    '
    Me.colRevSheetName.DataPropertyName = "SheetName"
    Me.colRevSheetName.HeaderText = "Sheet Name"
    Me.colRevSheetName.Name = "colRevSheetName"
    Me.colRevSheetName.ReadOnly = True
    Me.colRevSheetName.Width = 200
    '
    'colRevViewName
    '
    Me.colRevViewName.DataPropertyName = "ViewName"
    Me.colRevViewName.HeaderText = "View Name"
    Me.colRevViewName.Name = "colRevViewName"
    Me.colRevViewName.ReadOnly = True
    Me.colRevViewName.Width = 200
    '
    'colRevNumber
    '
    Me.colRevNumber.DataPropertyName = "RevisionNumber"
    Me.colRevNumber.HeaderText = "Rev #"
    Me.colRevNumber.Name = "colRevNumber"
    Me.colRevNumber.ReadOnly = True
    Me.colRevNumber.Width = 50
    '
    'Column1
    '
    Me.Column1.DataPropertyName = "Description"
    Me.Column1.HeaderText = "Description"
    Me.Column1.Name = "Column1"
    Me.Column1.ReadOnly = True
    Me.Column1.Width = 150
    '
    'colRevDate
    '
    Me.colRevDate.DataPropertyName = "RevisionDate"
    Me.colRevDate.HeaderText = "Rev Date"
    Me.colRevDate.Name = "colRevDate"
    Me.colRevDate.ReadOnly = True
    Me.colRevDate.Width = 70
    '
    'colRevIssuedTo
    '
    Me.colRevIssuedTo.DataPropertyName = "IssuedTo"
    Me.colRevIssuedTo.HeaderText = "Issued To"
    Me.colRevIssuedTo.Name = "colRevIssuedTo"
    Me.colRevIssuedTo.ReadOnly = True
    Me.colRevIssuedTo.Width = 70
    '
    'colRevIssuedBy
    '
    Me.colRevIssuedBy.DataPropertyName = "IssuedBy"
    Me.colRevIssuedBy.HeaderText = "Issued By"
    Me.colRevIssuedBy.Name = "colRevIssuedBy"
    Me.colRevIssuedBy.ReadOnly = True
    Me.colRevIssuedBy.Width = 70
    '
    'colRevComment
    '
    Me.colRevComment.DataPropertyName = "Comments"
    Me.colRevComment.HeaderText = "Comment"
    Me.colRevComment.Name = "colRevComment"
    Me.colRevComment.ReadOnly = True
    '
    'colRevMark
    '
    Me.colRevMark.DataPropertyName = "Mark"
    Me.colRevMark.HeaderText = "Mark"
    Me.colRevMark.Name = "colRevMark"
    Me.colRevMark.ReadOnly = True
    Me.colRevMark.Width = 50
    '
    'Column2
    '
    Me.Column2.DataPropertyName = "_v"
    Me.Column2.HeaderText = "View"
    Me.Column2.Name = "Column2"
    Me.Column2.ReadOnly = True
    Me.Column2.Visible = False
    '
    'GroupBoxRevs
    '
    Me.GroupBoxRevs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBoxRevs.Controls.Add(Me.DataGridViewRevs)
    Me.GroupBoxRevs.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxRevs.Name = "GroupBoxRevs"
    Me.GroupBoxRevs.Size = New System.Drawing.Size(713, 388)
    Me.GroupBoxRevs.TabIndex = 2
    Me.GroupBoxRevs.TabStop = False
    Me.GroupBoxRevs.Text = "All Revision Cloud Elements in Model"
    '
    'SaveFileDialogTXT
    '
    Me.SaveFileDialogTXT.DefaultExt = "txt"
    Me.SaveFileDialogTXT.Filter = "Tab Delimited File *.txt|*.txt"
    Me.SaveFileDialogTXT.Title = "Export Revision Cloud Data to Tab Delimited Txt File"
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Image = Global.[Case].RevClouds.My.Resources.Resources.Case_Clearly
    Me.PictureBox1.Location = New System.Drawing.Point(12, 410)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(127, 40)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.PictureBox1.TabIndex = 4
    Me.PictureBox1.TabStop = False
    '
    'ButtonExport
    '
    Me.ButtonExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.ButtonExport.Location = New System.Drawing.Point(620, 410)
    Me.ButtonExport.Name = "ButtonExport"
    Me.ButtonExport.Size = New System.Drawing.Size(105, 40)
    Me.ButtonExport.TabIndex = 3
    Me.ButtonExport.Text = "Export"
    Me.ButtonExport.UseVisualStyleBackColor = True
    '
    'ButtonHelp
    '
    Me.ButtonHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ButtonHelp.Location = New System.Drawing.Point(564, 410)
    Me.ButtonHelp.Name = "ButtonHelp"
    Me.ButtonHelp.Size = New System.Drawing.Size(50, 40)
    Me.ButtonHelp.TabIndex = 38
    Me.ButtonHelp.Text = "?"
    Me.ButtonHelp.UseVisualStyleBackColor = True
    '
    'form_RevisionClouds
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(737, 462)
    Me.Controls.Add(Me.ButtonHelp)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.ButtonExport)
    Me.Controls.Add(Me.GroupBoxRevs)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.MinimumSize = New System.Drawing.Size(753, 500)
    Me.Name = "form_RevisionClouds"
    Me.Text = "Revision Cloud Reporting"
    CType(Me.DataGridViewRevs, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxRevs.ResumeLayout(False)
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents DataGridViewRevs As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxRevs As System.Windows.Forms.GroupBox
    Friend WithEvents SaveFileDialogTXT As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonExport As System.Windows.Forms.Button
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents colRevSheetNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRevSheetName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRevViewName As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRevNumber As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRevDate As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRevIssuedTo As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRevIssuedBy As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRevComment As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents colRevMark As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents ButtonHelp As System.Windows.Forms.Button
End Class
