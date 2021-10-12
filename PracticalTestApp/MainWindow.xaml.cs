using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticalTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            StreamReader inputFile = new StreamReader(@"..\..\orders.txt");
            StreamWriter outputFile = new StreamWriter(@"..\..\output.txt");

            string line = " ";
            string currCust = " ";

            while ((line = inputFile.ReadLine()) != null)
            {
                // outputFile.WriteLine(line);
                if (currCust != line.Substring(0, 6))
                {
                    string custID = line.Substring(0, 6);
                    currCust = custID;
                    string custName = line.Substring(6, 25);
                    string custAdd1 = line.Substring(31, 25);
                    string custAdd2 = line.Substring(56, 25).Trim();
                    string custCity = line.Substring(81, 20).Trim();
                    string custState = line.Substring(101, 2);
                    string custZip = line.Substring(103, 5);
                    bool add2 = true;
                    if (custAdd2.Length == 0)
                    {
                        add2 = false;
                    }

                    if (line.Substring(108, 4) != "0000")
                    {
                        custZip += "-" + line.Substring(112, 4);
                    }

                    string msg = custName + "\n" + custAdd1 + "\n";
                    if (add2)
                    {
                        msg += custAdd2 + "\n";
                    }

                    msg = msg + custCity + ", " + custState + " " + custZip + "\n";

                    //string msg = string.Format($"{custName}\n{custAdd1");
                    string shipCode = line.Substring(112, 2);
                    outputFile.WriteLine(msg);
                    string header = string.Format("  SKU   Quantity Description Price\n");
                    outputFile.WriteLine(header);
                }
                
                string sku = line.Substring(114, 4);
                int quantity = Convert.ToInt32(line.Substring(118, 2));
                string descr = line.Substring(120, 10);
                double price = Convert.ToDouble(line.Substring(130));

                string item = string.Format($"{sku, 10} {quantity, 6} {descr, -15} {price, 10:C2}");
                outputFile.WriteLine(item);
            }
            inputFile.Close();
            outputFile.Close();
        }
    }
}
