using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;

namespace CustomRangeControlClient {
    public class CustomRangeClient : IRangeControlClient {

        const int rulerDeltaConst = 2;

        int[] data;
        int minValue;
        int maxValue;

        public CustomRangeClient(int dataCount, int minDataValue, int maxDataValue) {
            InitData(dataCount, minDataValue, maxDataValue);

            ruler = new List<object>(dataCount/rulerDeltaConst + 1);
            for (int i = 0; i < ruler.Count; i++) {
                ruler.Add(i);
            }
        }

        void InitData(int dataCount, int minDataValue, int maxDataValue) {
            Data = new int[dataCount];
            MinValue = minDataValue;
            MaxValue = maxDataValue;
            Random r = new Random();
            for (int i = 0; i < Data.Length; i++) {
                Data[i] = minDataValue + r.Next(maxDataValue - minDataValue);
            }
        }

        public int[] Data {
            get {
                return data;
            }
            private set {
                data = value;
            }
        }
        public int MaxValue {
            get {
                return maxValue;
            }
            private set {
                maxValue = value;
            }
        }

        public int MinValue {
            get {
                return minValue;
            }
            private set {
                minValue = value;
            }
        }

        EventHandlerList events;
        protected EventHandlerList Events {
            get {
                if (events == null)
                    events = new EventHandlerList();
                return events;
            }
        }

        private static readonly object rangeChanged = new object();

        // Fires the RangeChanged event.
        protected void RaiseRangeChanged() {
            RangeChangedEventHandler handler = Events[rangeChanged] as RangeChangedEventHandler;
            if (handler != null) {
                RangeControlRangeEventArgs e = new RangeControlRangeEventArgs();
                e.Range = new RangeControlRange();
                handler(this, e);
            }
        }

        #region IRangeControlClient Members
        // Checks if the specified type of the ruler values is supported.
        // This method is called when a new value is set to the Minimum and Maximum properties.
        bool IRangeControlClient.IsValidType(Type type) { 
            return true; 
        }
        //This method is fired when you move the mouse cursor over the viewport.
        void IRangeControlClient.UpdateHotInfo(RangeControlHitInfo hitInfo) { 
        }
        //This method is fired when you press with the mouse within the viewport (without releasing the mouse button).
        void IRangeControlClient.UpdatePressedInfo(RangeControlHitInfo hitInfo) { 
        }
        //This method is fired when you click within the viewport.
        void IRangeControlClient.OnClick(RangeControlHitInfo hitInfo) { 
        }
        // Returns true if the Client's state is valid and the Client should render itself within the viewport;
        // Returns false if a message specified by the InvalidText property should be painted instead of the Client.
        bool IRangeControlClient.IsValid { 
            get { 
                return true; 
            } 
        }
        // Specifies text painted when the Client's state is invalid.
        string IRangeControlClient.InvalidText { 
            get { 
                return "i n v a l i d"; 
            } 
        }
        
        // Return the object that will be accessible via the RangeControl.ClientOptions property.
        object IRangeControlClient.GetOptions() { 
            return this; 
        }
        
        //The event that fires when the range has been changed via the Client.
        event ClientRangeChangedEventHandler IRangeControlClient.RangeChanged {
            add { Events.AddHandler(rangeChanged, value); }
            remove { Events.RemoveHandler(rangeChanged, value); }
        }
        // Fires when the range is changed via the RangeControl.
        void IRangeControlClient.OnRangeChanged(object rangeMinimum, object rangeMaximum) {
        }

        // Return true for a specific orientation if the Client supports this orientation.
        bool IRangeControlClient.SupportOrientation(Orientation orientation) { 
            return (orientation != Orientation.Vertical);
        }
        
        // Return true if the Client draws the ruler itself.
        bool IRangeControlClient.DrawRuler(RangeControlPaintEventArgs e) {
            return false;
        }
        
        //Returns false if the RangeControl should reserve drawing space for the ruler.
        bool IRangeControlClient.IsCustomRuler { 
            get { 
                return false; 
            } 
        }

        // Returns text representation of the ruler values
        string IRangeControlClient.RulerToString(int index) {
            return (index * (int)(rulerDeltaConst)).ToString();
        }

        List<object> ruler;
        // If ruler values are not equally spaced, return custom ruler values; 
        // If the ruler has equally spaced increments specified by the RulerDelta property, return null.
        List<object> IRangeControlClient.GetRuler(RulerInfoArgs e) {
            return null;
            //return ruler;
        }

        
        // Returns a ruler increment (when values are equally distributed).
        object IRangeControlClient.RulerDelta {
            get { return rulerDeltaConst; }
        }

        // Returns a normalized ruler increment.
        double IRangeControlClient.NormalizedRulerDelta {
            get {
                return (double)rulerDeltaConst / BarCount;
            }
        }
        
        //Gets a ruler value (between Minimum and Maximum) from a normalized value (between 0 and 1).
        object IRangeControlClient.GetValue(double normalizedValue) {
            int index = (int)(normalizedValue * BarCount);
            return index;
        }
        // Performs the opposite conversion.
        double IRangeControlClient.GetNormalizedValue(object value) {
            int index = (int)value;
            return ((double)index) / BarCount;
        }

        string IRangeControlClient.ValueToString(double normalizedValue) {
            return string.Empty;
        }

        // Renders the Range Control's viewport.
        void IRangeControlClient.DrawContent(RangeControlPaintEventArgs e) {
            Rectangle rect = e.ContentBounds;
            rect.Inflate(0, -3);
            rect.Height -= ((IRangeControlClient)this).RangeBoxBottomIndent;
            DrawZeroLine(e, rect);
            DrawGraph(e, rect);
        }

        protected virtual void DrawZeroLine(RangeControlPaintEventArgs e, Rectangle contentBounds) {
            double zeroLine = (double)(MaxValue - 0) / (MaxValue - MinValue);
            if (zeroLine < 0.0 || zeroLine >= 1.0f)
                return;
            int y = (int)(contentBounds.Y + zeroLine * contentBounds.Height);
            e.Graphics.DrawLine(Pens.Gray, new Point(contentBounds.X, y), new Point(contentBounds.Right, y));
        }

        protected virtual void DrawGraph(RangeControlPaintEventArgs e, Rectangle contentBounds) {
            e.Graphics.Clear(Color.Transparent);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            int start = Math.Max(0, (int)(e.RangeControl.VisibleRangeStartPosition * BarCount) - 2);
            int end = Math.Min(Data.Length, start + ((int)(e.RangeControl.VisibleRangeWidth * BarCount) + 4));
            Point? prevPoint = null;
            using (Pen pen = new Pen(Color.Blue, 1)) {
                for (int i = start; i < end; i++) {
                    int y = contentBounds.Y + contentBounds.Height - (int)((double)(Data[i] - MinValue) / (MaxValue - MinValue) * contentBounds.Height);
                    int x = e.CalcX((double)i / BarCount);
                    if (prevPoint.HasValue) {
                        e.Graphics.DrawLine(pen, new Point(prevPoint.Value.X, prevPoint.Value.Y), new Point(x, y));
                    }
                    prevPoint = new Point(x, y);
                }
            }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }

        int BarCount { get { return Data.Length - 1; } }

        
        // The top and bottom indents for the selection area within the viewport.
        // These limit the bounds of the selection thumb lines that mark the current selection. 
        int IRangeControlClient.RangeBoxTopIndent { get { return 0; } }
        int IRangeControlClient.RangeBoxBottomIndent { get { return 0; } }


        // Validates a range when it is changed.
        void IRangeControlClient.ValidateRange(NormalizedRangeInfo info) {
            int start = (int)(info.Range.Minimum * BarCount);
            int end = (int)(info.Range.Maximum * BarCount);
            if (end == start) end = start + 2;

            info.Range.Minimum = (double)start / BarCount;
            info.Range.Maximum = (double)end / BarCount;
        }
        //This method is fired by the RangeControl when a client is added to or removed from the RangeControl.
        void IRangeControlClient.OnRangeControlChanged(IRangeControl rangeControl) { }
        
        //This method is fired when the RangeControl is resized.
        void IRangeControlClient.OnResize() { }
        
        //This method is fired when the RangeControl's state or settings are changed
        void IRangeControlClient.Calculate(Rectangle contentRect) { }
        
        //Validates a scale factor
        double IRangeControlClient.ValidateScale(double newScale) { 
            // Limit the maximum scale factor to 10:
            return Math.Min(10, newScale); 
        }

        #endregion
    }
}
