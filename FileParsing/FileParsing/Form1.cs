using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileParsing
{
    public partial class Form1 : Form
    {
        private string sourcePath;
        int count = 0;
        int count1 = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            using (OpenFileDialog dlg = new OpenFileDialog())
            {



                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    sourcePath = dlg.FileName;




                    var fileName = Path.GetFileNameWithoutExtension(dlg.FileName);

                    string ext = Path.GetExtension(dlg.FileName);


                    System.IO.StreamReader file = new System.IO.StreamReader(sourcePath);



                    if (ext == ".hrm")
                    {
                        MessageBox.Show("Thank You for uploading");
                        var searchTarget = "[Params]";

                        foreach (var line in File.ReadLines(sourcePath))
                        {


                            if (line.StartsWith("[") && line.EndsWith("]"))
                            {
                                string dataone = line.Substring(1, line.Length - 2);
                                MessageBox.Show(dataone);
                                count = count + 1;
                            }
                            //if (line.Contains(searchTarget))
                            //{
                            //    MessageBox.Show("Found Something");
                            //    // found it!
                            //    // do something...
                            //    break;

                            //}
                            else
                            {

                                count1 = count1 + 1;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please Upload only hrm file");
                    }
                    MessageBox.Show("" + count);




                }
            }




        }
    }
}