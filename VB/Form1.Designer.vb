Imports Microsoft.VisualBasic
Imports System
Namespace CustomRangeControlClient
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.rangeControl1 = New DevExpress.XtraEditors.RangeControl()
			Me.SuspendLayout()
			' 
			' rangeControl1
			' 
			Me.rangeControl1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.rangeControl1.Location = New System.Drawing.Point(12, 23)
			Me.rangeControl1.Name = "rangeControl1"
			Me.rangeControl1.Size = New System.Drawing.Size(635, 90)
			Me.rangeControl1.TabIndex = 0
			Me.rangeControl1.Text = "rangeControl1"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(662, 145)
			Me.Controls.Add(Me.rangeControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private rangeControl1 As DevExpress.XtraEditors.RangeControl

	End Class
End Namespace

