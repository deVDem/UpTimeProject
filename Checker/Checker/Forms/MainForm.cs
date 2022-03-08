using Checker.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace Checker
{
    public partial class MainForm : Form
    {
        Thread thread;
        Action Action;

        bool go = false;

        public MainForm()
        {
            InitializeComponent();
            Action = new Action(UpdateText);
            thread = new Thread(() => myBestFunc(Action));
            thread.Start();
        }


        public void UpdateText()
        {
            label1.Text = "хуй";
        }


        public void myBestFunc(Action action)
        {
            while(true)
            {
                if(go)
                {
                    action.Invoke();
                    go = false;
                }
                Thread.Sleep(1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            go = true;
        }
    }
}
