Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports System.Windows.Forms

Namespace CustomRangeControlClient
	Public Class CustomRangeClient
		Implements IRangeControlClient

		Private Const rulerDeltaConst As Integer = 2

'INSTANT VB NOTE: The field data was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private data_Renamed() As Integer
'INSTANT VB NOTE: The field minValue was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private minValue_Renamed As Integer
'INSTANT VB NOTE: The field maxValue was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private maxValue_Renamed As Integer

		Public Sub New(ByVal dataCount As Integer, ByVal minDataValue As Integer, ByVal maxDataValue As Integer)
			InitData(dataCount, minDataValue, maxDataValue)

			ruler = New List(Of Object)(dataCount \ rulerDeltaConst + 1)
			Dim i As Integer = 0
			Do While i < ruler.Count
				ruler.Add(i)
				i += 1
			Loop
		End Sub

		Private Sub InitData(ByVal dataCount As Integer, ByVal minDataValue As Integer, ByVal maxDataValue As Integer)
			Data = New Integer(dataCount - 1){}
			MinValue = minDataValue
			MaxValue = maxDataValue
			Dim r As New Random()
			For i As Integer = 0 To Data.Length - 1
				Data(i) = minDataValue + r.Next(maxDataValue - minDataValue)
			Next i
		End Sub

		Public Property Data() As Integer()
			Get
				Return data_Renamed
			End Get
			Private Set(ByVal value As Integer())
				data_Renamed = value
			End Set
		End Property
		Public Property MaxValue() As Integer
			Get
				Return maxValue_Renamed
			End Get
			Private Set(ByVal value As Integer)
				maxValue_Renamed = value
			End Set
		End Property

		Public Property MinValue() As Integer
			Get
				Return minValue_Renamed
			End Get
			Private Set(ByVal value As Integer)
				minValue_Renamed = value
			End Set
		End Property

'INSTANT VB NOTE: The field events was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private events_Renamed As EventHandlerList
		Protected ReadOnly Property Events() As EventHandlerList
			Get
				If events_Renamed Is Nothing Then
					events_Renamed = New EventHandlerList()
				End If
				Return events_Renamed
			End Get
		End Property

'INSTANT VB NOTE: The field rangeChanged was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private Shared ReadOnly rangeChanged_Renamed As New Object()

		' Fires the RangeChanged event.
		Protected Sub RaiseRangeChanged()
			Dim handler As RangeChangedEventHandler = TryCast(Events(rangeChanged_Renamed), RangeChangedEventHandler)
			If handler IsNot Nothing Then
				Dim e As New RangeControlRangeEventArgs()
				e.Range = New RangeControlRange()
				handler(Me, e)
			End If
		End Sub

		#Region "IRangeControlClient Members"
		' Checks if the specified type of the ruler values is supported.
		' This method is called when a new value is set to the Minimum and Maximum properties.
		Private Function IRangeControlClient_IsValidType(ByVal type As Type) As Boolean Implements IRangeControlClient.IsValidType
			Return True
		End Function
		'This method is fired when you move the mouse cursor over the viewport.
		Private Sub IRangeControlClient_UpdateHotInfo(ByVal hitInfo As RangeControlHitInfo) Implements IRangeControlClient.UpdateHotInfo
		End Sub
		'This method is fired when you press with the mouse within the viewport (without releasing the mouse button).
		Private Sub IRangeControlClient_UpdatePressedInfo(ByVal hitInfo As RangeControlHitInfo) Implements IRangeControlClient.UpdatePressedInfo
		End Sub
		'This method is fired when you click within the viewport.
		Private Sub IRangeControlClient_OnClick(ByVal hitInfo As RangeControlHitInfo) Implements IRangeControlClient.OnClick
		End Sub
		' Returns true if the Client's state is valid and the Client should render itself within the viewport;
		' Returns false if a message specified by the InvalidText property should be painted instead of the Client.
		Private ReadOnly Property IRangeControlClient_IsValid() As Boolean Implements IRangeControlClient.IsValid
			Get
				Return True
			End Get
		End Property
		' Specifies text painted when the Client's state is invalid.
		Private ReadOnly Property IRangeControlClient_InvalidText() As String Implements IRangeControlClient.InvalidText
			Get
				Return "i n v a l i d"
			End Get
		End Property
		' Return the object that will be accessible via the RangeControl.ClientOptions property.
		Private Function IRangeControlClient_GetOptions() As Object Implements IRangeControlClient.GetOptions
			Return Me
		End Function
		'The event that fires when the range has been changed via the Client.
		Private Custom Event RangeChanged As ClientRangeChangedEventHandler Implements IRangeControlClient.RangeChanged
			AddHandler(ByVal value As ClientRangeChangedEventHandler)
				Events.AddHandler(rangeChanged_Renamed, value)
			End AddHandler
			RemoveHandler(ByVal value As ClientRangeChangedEventHandler)
				Events.RemoveHandler(rangeChanged_Renamed, value)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
			End RaiseEvent
		End Event
		' Fires when the range is changed via the RangeControl.
		Private Sub IRangeControlClient_OnRangeChanged(ByVal rangeMinimum As Object, ByVal rangeMaximum As Object) Implements IRangeControlClient.OnRangeChanged
		End Sub

		' Return true for a specific orientation if the Client supports this orientation.
		Private Function IRangeControlClient_SupportOrientation(ByVal orientation As Orientation) As Boolean Implements IRangeControlClient.SupportOrientation
			Return (orientation <> System.Windows.Forms.Orientation.Vertical)
		End Function

		' Return true if the Client draws the ruler itself.
		Private Function IRangeControlClient_DrawRuler(ByVal e As RangeControlPaintEventArgs) As Boolean Implements IRangeControlClient.DrawRuler
			Return False
		End Function

		'Returns false if the RangeControl should reserve drawing space for the ruler.
		Private ReadOnly Property IRangeControlClient_IsCustomRuler() As Boolean Implements IRangeControlClient.IsCustomRuler
			Get
				Return False
			End Get
		End Property

		' Returns text representation of the ruler values
		Private Function IRangeControlClient_RulerToString(ByVal index As Integer) As String Implements IRangeControlClient.RulerToString
			Return (index * CInt(rulerDeltaConst)).ToString()
		End Function

		Private ruler As List(Of Object)
		' If ruler values are not equally spaced, return custom ruler values; 
		' If the ruler has equally spaced increments specified by the RulerDelta property, return null.
		Private Function IRangeControlClient_GetRuler(ByVal e As RulerInfoArgs) As List(Of Object) Implements IRangeControlClient.GetRuler
			Return Nothing
			'return ruler;
		End Function


		' Returns a ruler increment (when values are equally distributed).
		Private ReadOnly Property IRangeControlClient_RulerDelta() As Object Implements IRangeControlClient.RulerDelta
			Get
				Return rulerDeltaConst
			End Get
		End Property

		' Returns a normalized ruler increment.
		Private ReadOnly Property IRangeControlClient_NormalizedRulerDelta() As Double Implements IRangeControlClient.NormalizedRulerDelta
			Get
				Return CDbl(rulerDeltaConst) / BarCount
			End Get
		End Property

		'Gets a ruler value (between Minimum and Maximum) from a normalized value (between 0 and 1).
		Private Function IRangeControlClient_GetValue(ByVal normalizedValue As Double) As Object Implements IRangeControlClient.GetValue
			Dim index As Integer = CInt(Math.Truncate(normalizedValue * BarCount))
			Return index
		End Function
		' Performs the opposite conversion.
		Private Function IRangeControlClient_GetNormalizedValue(ByVal value As Object) As Double Implements IRangeControlClient.GetNormalizedValue
			Dim index As Integer = DirectCast(value, Integer)
			Return (CDbl(index)) / BarCount
		End Function

		Private Function IRangeControlClient_ValueToString(ByVal normalizedValue As Double) As String Implements IRangeControlClient.ValueToString
			Return String.Empty
		End Function

		' Renders the Range Control's viewport.
		Private Sub IRangeControlClient_DrawContent(ByVal e As RangeControlPaintEventArgs) Implements IRangeControlClient.DrawContent
			Dim rect As Rectangle = e.ContentBounds
			rect.Inflate(0, -3)
			rect.Height -= DirectCast(Me, IRangeControlClient).RangeBoxBottomIndent
			DrawZeroLine(e, rect)
			DrawGraph(e, rect)
		End Sub

		Protected Overridable Sub DrawZeroLine(ByVal e As RangeControlPaintEventArgs, ByVal contentBounds As Rectangle)
			Dim zeroLine As Double = CDbl(MaxValue - 0) / (MaxValue - MinValue)
			If zeroLine < 0.0 OrElse zeroLine >= 1.0F Then
				Return
			End If
			Dim y As Integer = CInt(Math.Truncate(contentBounds.Y + zeroLine * contentBounds.Height))
			e.Graphics.DrawLine(Pens.Gray, New Point(contentBounds.X, y), New Point(contentBounds.Right, y))
		End Sub

		Protected Overridable Sub DrawGraph(ByVal e As RangeControlPaintEventArgs, ByVal contentBounds As Rectangle)
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
			Dim start As Integer = Math.Max(0, CInt(Math.Truncate(e.RangeControl.VisibleRangeStartPosition * BarCount)) - 2)
			Dim [end] As Integer = Math.Min(Data.Length, start + (CInt(Math.Truncate(e.RangeControl.VisibleRangeWidth * BarCount)) + 4))
			Dim prevPoint? As Point = Nothing
			Using pen As New Pen(Color.Blue, 1)
				For i As Integer = start To [end] - 1
					Dim y As Integer = contentBounds.Y + contentBounds.Height - CInt(Math.Truncate(CDbl(Data(i) - MinValue) / (MaxValue - MinValue) * contentBounds.Height))
					Dim x As Integer = e.CalcX(CDbl(i) / BarCount)
					If prevPoint.HasValue Then
						e.Cache.DrawLine(New Point(prevPoint.Value.X, prevPoint.Value.Y), New Point(x, y), Color.Blue, 1)
					End If
					prevPoint = New Point(x, y)
				Next i
			End Using
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default
		End Sub

		Private ReadOnly Property BarCount() As Integer
			Get
				Return Data.Length - 1
			End Get
		End Property


		' The top and bottom indents for the selection area within the viewport.
		' These limit the bounds of the selection thumb lines that mark the current selection. 
		Private ReadOnly Property IRangeControlClient_RangeBoxTopIndent() As Integer Implements IRangeControlClient.RangeBoxTopIndent
			Get
				Return 0
			End Get
		End Property
		Private ReadOnly Property IRangeControlClient_RangeBoxBottomIndent() As Integer Implements IRangeControlClient.RangeBoxBottomIndent
			Get
				Return 0
			End Get
		End Property


		' Validates a range when it is changed.
		Private Sub IRangeControlClient_ValidateRange(ByVal info As NormalizedRangeInfo) Implements IRangeControlClient.ValidateRange
			Dim start As Integer = CInt(Math.Truncate(info.Range.Minimum * BarCount))
			Dim [end] As Integer = CInt(Math.Truncate(info.Range.Maximum * BarCount))
			If [end] = start Then
				[end] = start + 2
			End If

			info.Range.Minimum = CDbl(start) / BarCount
			info.Range.Maximum = CDbl([end]) / BarCount
		End Sub
		'This method is fired by the RangeControl when a client is added to or removed from the RangeControl.
		Private Sub IRangeControlClient_OnRangeControlChanged(ByVal rangeControl As IRangeControl) Implements IRangeControlClient.OnRangeControlChanged
		End Sub

		'This method is fired when the RangeControl is resized.
		Private Sub IRangeControlClient_OnResize() Implements IRangeControlClient.OnResize
		End Sub

		'This method is fired when the RangeControl's state or settings are changed
		Private Sub IRangeControlClient_Calculate(ByVal contentRect As Rectangle) Implements IRangeControlClient.Calculate
		End Sub

		'Validates a scale factor
		Private Function IRangeControlClient_ValidateScale(ByVal newScale As Double) As Double Implements IRangeControlClient.ValidateScale
			' Limit the maximum scale factor to 10:
			Return Math.Min(10, newScale)
		End Function

		Private Function IRangeControlClient_CalculateSelectionBounds(ByVal e As RangeControlPaintEventArgs, ByVal rect As Rectangle) As Rectangle Implements IRangeControlClient.CalculateSelectionBounds
			Return rect
		End Function

		Private Sub IRangeControlClient_DrawSelection(ByVal e As RangeControlPaintEventArgs) Implements IRangeControlClient.DrawSelection
		End Sub

		#End Region
	End Class
End Namespace
