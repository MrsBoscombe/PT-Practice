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
    /// 
    

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

            // Added 10/25/2021
            double total = 0.0;        // keep track of order total
            string shipCode = "";      // moved to outside since we're using it in the print 
            // End of Added 10/25/2021

            while ((line = inputFile.ReadLine()) != null)
            {
                // outputFile.WriteLine(line);
                if (currCust != line.Substring(0, 6))
                {
                    // Added 10/26/2021
                    if (currCust != " ")
                    {
                        PrintTotals(shipCode, total, outputFile);
                    }

                    total = 0.0;
                    // end of Added 10/26/2021

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

                    // Needed to move declaration of shipCode above since we're using it in the if
                    // statement above
                    /*string*/
                    shipCode = line.Substring(112, 2);
                    outputFile.WriteLine(msg);

                    // Added 11/08/2021
                    //myListBox.Items.Add(msg);
                    // End of Added 11/08/2021

                    string header = string.Format("  SKU   Quantity Description Price\n");
                    outputFile.WriteLine(header);
                }

                string sku = line.Substring(114, 4);
                int quantity = Convert.ToInt32(line.Substring(118, 2));
                string descr = line.Substring(120, 10);
                double price = Convert.ToDouble(line.Substring(130));

                string item = string.Format($"{sku,10} {quantity,6} {descr,-15} {price,10:C2}");
                outputFile.WriteLine(item);

                // Added 10/25/2021
                // Adding current item to the total
                total = total + quantity * price;
                // End of Added 10/25/2021
            }
            // Added 10/26/2021 
            // added to print totals for the last customer in the file
            PrintTotals(shipCode, total, outputFile);
            // End of added 10/26/2021
            inputFile.Close();
            outputFile.Close();
        }


        public static void PrintTotals(string shipCode, double total, StreamWriter outputFile)
        {
            // Added 10/25/2021
            
            string totalLine = string.Format($"Subtotal: {total,-12:C2}");
            outputFile.WriteLine(totalLine);

            // Calculate shipping cost
            double shipCost = 0.0;      // Free shipping
            if (shipCode == "00")       // Flat rate $5.00
            {
                // shipping cost is fixed 5.00
                shipCost = 5.0;
            }
            else if (shipCode == "01")  // Shipping is 8% of total cost
            {
                // shipping cost is 8% of total cost
                shipCost = .08 * total;
            }

            // write the shipping cost to the file
            totalLine = string.Format($"Shipping: {shipCost,12:C2}");  //:C2 format as currency w/ 2 decimal places
            outputFile.WriteLine(totalLine);

            // add the ship cost to the total and print to the file
            total = total + shipCost;           // same as total += shipCost;
            totalLine = string.Format($"Order Total: {total,12:C2}");
            outputFile.WriteLine(totalLine);
        }

      
    }
}
