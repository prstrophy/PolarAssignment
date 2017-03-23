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

namespace PolarHrm_Assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        //Declaring Array for storing different line data into it
        private string[] LinesOfFile;

        //Creating list and object for different class
        List<header> heading = new List<header>();
        public List<hrdata> hr = new List<hrdata>();
        List<parameters> parameters = new List<parameters>();
        parameters differentParameters = new parameters();


        

        //Declaring a variable
        int lineno;

        //creating datatables 

        DataTable dt = new DataTable();
     

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void browseFile_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog fileBrowser = new OpenFileDialog())
            {
                if (fileBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filename = fileBrowser.FileName;
                    //Reading the File
                    textPathh.Text = filename;
                    

                    LinesOfFile = File.ReadAllLines(filename);
                    
                    //Executing the loop only when the file is not empty.
                    for (int i = 0; i < LinesOfFile.Length; i++)
                    {
                        if (LinesOfFile[i].Length > 0)
                        {
                            //Finding The Header and assigning it to Header Class, Here header is between []
                            if (LinesOfFile[i][0] == '[' && LinesOfFile[i][LinesOfFile[i].Length - 1] == ']')
                            {
                                //Assigning Headername to header list
                                heading.Add(new header { headername = LinesOfFile[i].Substring(1, LinesOfFile[i].Length - 2), headerline = i });

                            }
                        }
                    }


                }
                //calling method to store particular set of data in objects

                dataParsing();

            }
        }

        public void dataParsing()
        {
            //checking the filelist array with HRData header
            foreach (header a in heading)
            {
                lineno = a.headerline;

                //checking the condition
                switch (a.headername)
                {
                    case "Params":
                        {
                            int j = lineno + 1;
                            //seperating the values with tabs
                            string[] newline = LinesOfFile[j].Split('\t');
                            string value = newline[0];
                            int add = 0;
                            int b = 1;
                            string[] paramsValue = new string[22];
                            do
                            {
                                // for (int b = 0; b < value.Length; b++)
                                // {
                                //if (newline[b] == "=")
                                foreach (char ab in value)
                                {
                                    if (ab == '=')
                                    {
                                        paramsValue[add] = value.Substring(b, value.Length - b);
                                        // paramsValue[add] = "" + add;
                                    }

                                    b++;
                                }

                                //  }
                                b = 1;
                                add++;
                                j++;
                                newline = LinesOfFile[j].Split('\t');
                                value = newline[0];

                            } while (j < 24);//newline!=null);


                            //paramsData.Add(new Params
                            //inserting data in the params
                            differentParameters.Version = paramsValue[0];
                            differentParameters.Monitor = paramsValue[1];
                            differentParameters.SMode = paramsValue[2];
                            differentParameters.Date = paramsValue[3];
                            differentParameters.StartTime = paramsValue[4];
                            differentParameters.Length = paramsValue[5];
                            differentParameters.Interval = paramsValue[6];
                            differentParameters.Upper1 = paramsValue[7];
                            differentParameters.Lower1 = paramsValue[8];
                            differentParameters.Upper2 = paramsValue[9];
                            differentParameters.Lower2 = paramsValue[10];
                            differentParameters.Upper3 = paramsValue[11];
                            differentParameters.Lower3 = paramsValue[12];
                            differentParameters.Timer1 = paramsValue[13];
                            differentParameters.Timer2 = paramsValue[14];
                            differentParameters.Timer3 = paramsValue[15];
                            differentParameters.ActiveLimit = paramsValue[16];
                            differentParameters.MaxHR = paramsValue[17];
                            differentParameters.RestHR = paramsValue[18];
                            differentParameters.StartDelay = paramsValue[19];
                            differentParameters.VO2max = paramsValue[20];
                            differentParameters.Weight = paramsValue[21];


                            //adding the getter and setter
                            label1.Text = "Date::";
                            label2.Text = "Start Time::";
                            label3.Text = "Interval::";
                               label4.Text = differentParameters.Date;
                              label5.Text = differentParameters.StartTime;
                             label6.Text = differentParameters.Interval;

                            break;
                        }
                    case "HRData":
                        {


                            //using the loop to till the end of array to get values 
                            for (int j = lineno + 1; j < LinesOfFile.Length; j++)
                            {
                                //Spliting chars with tabs
                                string[] newline = LinesOfFile[j].Split('\t');


                                //Switching different versions 
                                switch (differentParameters.Version)
                                {

                                    case "105":
                                        {
                                            hr.Add(new hrdata
                                            {
                                                HeartRate = int.Parse(newline[0]),
                                                Speed = int.Parse(newline[1]),
                                                Cadence = int.Parse(newline[2])
                                            });

                                            break;
                                        }
                                    case "106":
                                        {


                                            //adding the values 
                                            hr.Add(new hrdata
                                            {
                                                HeartRate = int.Parse(newline[0]),
                                                Speed = int.Parse(newline[1]),
                                                Cadence = int.Parse(newline[2]),
                                                Altitude = int.Parse(newline[3]),
                                                Power = int.Parse(newline[4]),
                                                PowerBalancePedalling = int.Parse(newline[5])
                                            });

                                            break;
                                        }
                                    case "107":
                                        {
                                            //adding the values to the parameters
                                            hr.Add(new hrdata
                                            {
                                                HeartRate = int.Parse(newline[0]),
                                                Speed = int.Parse(newline[1]),
                                                Cadence = int.Parse(newline[2]),
                                                Altitude = int.Parse(newline[3]),
                                                Power = int.Parse(newline[4]),
                                                PowerBalancePedalling = int.Parse(newline[5]),
                                                AirPressure = int.Parse(newline[6])
                                            });
                                            break;
                                        }
                                }
                            }
                            break;
                        }

                }
            }
            

            //putting column name is data grid view as per version of parmas
            string[] columnNames = { " HeartRate", " Speed", " Cadence", " Altitude", " Power", "PowerBalancePedalling" };


            foreach (string col in columnNames)
            {
                dt.Columns.Add(col);
            }


            foreach (hrdata hd in hr)
            {



                dt.Rows.Add(hd.HeartRate, hd.Speed, hd.Cadence, hd.Altitude, hd.Power, hd.PowerBalancePedalling);
            }

            //variables initiated for heart rate
            int minHeartRate = 1000;
            int maxHeartRate = 0, sum = 0;
            int count = 0;

            //variables for altitude ,power,speed

            int minAltitude = 1000;
            int maxAltitude = 0;
            int minPower = 1000;
            int maxPower = 0;
            int minSpeed = 1000;
            int maxSpeed = 0;
            //calculating the data to find maximum,minimum

            foreach (hrdata value in hr)
            {
                //for heart rate
                int hrValue = value.HeartRate;
                if (hrValue < minHeartRate)
                {
                    minHeartRate = hrValue;
                }
                else if (hrValue > maxHeartRate)
                {
                    maxHeartRate = hrValue;
                }
                sum += hrValue;
                count++;
                //for altitude 
                int altValue = value.Altitude;
                if (altValue < minAltitude)
                {
                    minAltitude = altValue;
                }
                else if (altValue > maxAltitude)
                {
                    maxAltitude = altValue;
                }

                //for power
                int PowerValue = value.Power;
                if (PowerValue < minPower)
                {
                    minPower = PowerValue;
                }
                else if (altValue > maxPower)
                {
                    maxPower = PowerValue;
                }
                //speed

                int SpeedValue = value.Speed;
                if (SpeedValue < minSpeed)
                {
                    minSpeed = SpeedValue;
                }
                else if (SpeedValue > maxSpeed)
                {
                    maxSpeed = SpeedValue;
                }


            }
            //calculating the stats of heart rate
            label7.Text ="Average Heart Rate::"+ (sum / count).ToString();
            label8.Text ="Maximum Heart Rate::"+maxHeartRate.ToString();
            label9.Text = "Minimum Heart Rate::" + minHeartRate.ToString();

            //calculating for altitude ,power ,speed
            label10.Text ="Maximun Power::" + maxPower.ToString()+"Watt";
            label11.Text ="Minimum Power::" +minPower.ToString()+"Watt";

            label12.Text = "Maximum Altitude::" + maxAltitude.ToString()+"Meter";
            label13.Text = "Minimum Altitude::" + minAltitude.ToString()+"Meter";

            label14.Text = "Maximum Speed::" + maxSpeed.ToString()+"Km/Hr";
            label15.Text = "Minimum Speed::" + minSpeed.ToString() + "Km/Hr";


            ParseData.DataSource = dt;
        }
        public void heartrate()
        {
            


        }

        private void ParseData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void ParseData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void staticGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph gr = new graph(hr, this);
            gr.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        //public List<hrdata> getHRDataList()
        //{
        //    return hr;
        //}

    }

}
