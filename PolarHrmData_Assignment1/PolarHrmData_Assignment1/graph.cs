﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace PolarHrmData_Assignment1
{
    public partial class graph : Form
    {
        GraphPane gp = new GraphPane();
        List<hrdata> hrRef;
        Form1 appRef;


        public graph(List<hrdata> hrRef, Form1 appRef)
        {
            this.hrRef = hrRef;
            this.appRef = appRef;

            this.appRef.messg();
            MessageBox.Show("sdf");
        }

        private void zedGraphControl2_Load(object sender, EventArgs e)
        {


        }

        private void graph_Load(object sender, EventArgs e)
        {
            
            gp = zedGraphControl2.GraphPane;

        }
    }
}
