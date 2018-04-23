using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomRangeControlClient {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            rangeControl1.Client = new CustomRangeClient(101, -100, 100);

            rangeControl1.SelectedRange.Maximum = 30;
            rangeControl1.SelectedRange.Minimum = 10;
        }
    }
}
