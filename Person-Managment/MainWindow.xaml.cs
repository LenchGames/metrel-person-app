using Microsoft.Win32;
using Person_Managment.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Person_Managment {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            if(sfd.ShowDialog() == true) {
                SerializeDataSet(sfd.FileName);
            }
        }

        private void SerializeDataSet(string filename) {
            XmlSerializer ser = new XmlSerializer(typeof(Person));

            // Creates a DataSet; adds a table, column, and ten rows.
            Person p = new Person() {
                GivenName = "Miha",
                LastName = "Lenko"
            };

            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, p);
            writer.Close();
        }
    }
}
