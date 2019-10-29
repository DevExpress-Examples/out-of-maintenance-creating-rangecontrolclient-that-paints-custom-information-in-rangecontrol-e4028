Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace CustomRangeControlClient
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			rangeControl1.Client = New CustomRangeClient(101, -100, 100)

			rangeControl1.SelectedRange.Maximum = 30
			rangeControl1.SelectedRange.Minimum = 10
		End Sub
	End Class
End Namespace
