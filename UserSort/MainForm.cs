﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserSort.Classes;

namespace UserSort
{
    public partial class MainForm : Form
    {

        private string[] selectedFiles;
        private List<User> users = new List<User>();
        private string saveLocation;

        public MainForm()
        {
            InitializeComponent();
        }

        // Display Open File Dialog box to allow users to select CVS, JSON and XML files
        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Files";
                ofd.Filter = "Data Files (*.csv, *.json, *.xml)| *.csv; *.json; *.xml|All Files (*.*)|*.*";
                ofd.RestoreDirectory = true;
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedFiles = ofd.FileNames;
                    btnSelect.Enabled = false;
                    btnImport.Enabled = true;
                }
            }
        }

        // Import selected files
        private void btnImport_Click(object sender, EventArgs e)
        {
            foreach (string file in selectedFiles)
            {

                string fileExtension = Path.GetExtension(file);

                if (fileExtension == ".json")
                {
                    users.AddRange(ImportFiles.LoadJSON(file));
                }
                else if (fileExtension == ".xml")
                {
                    users.AddRange(ImportFiles.LoadXML(file));
                }
                else if (fileExtension == ".csv")
                {
                    users.AddRange(ImportFiles.LoadCSV(file));
                }
            }

            btnImport.Enabled = false;
            btnDisplay.Enabled = true;
            MessageBox.Show("Files have been imported!", "Success", MessageBoxButtons.OK);
        }

        // Display List of Users on Rich Text box
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sort users by User ID?", "Sort Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Sort data
                users = SortData.SortByUserID(users);
            }

            // Convert "Last Login Time" to ISO 8601
            SortData.ConvertLoginTime(users);

            var list = new BindingList<User>(users);
            dataGridView1.DataSource = list;
            btnExport.Enabled = true;
        }



        // Export Data to CSV, JSON and XML
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    saveLocation = folderBrowser.SelectedPath;
                    //ExportData.ExportXML(users, saveLocation + @"\users");
                    ExportData.ExportJSON(users, saveLocation + @"\users");
                }


            }
        }

    }
}
