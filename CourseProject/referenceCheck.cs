﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseProject {
    public partial class referenceCheck : Form {
        public referenceCheck() {
            InitializeComponent();
            string path = Application.StartupPath + @"\" + "reference.html";
            webBrowser1.Navigate(path);
        }
    }
}
