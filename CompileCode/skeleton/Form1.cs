using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace skeleton
{
    public partial class Form1 : Form
    {
        string fileName = string.Empty;

        public Form1()
        {
            InitializeComponent();
            lblError.Text = "";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (validate())
            { 
                lblError.Text = ""; 
            }
           
        }
        private bool validate()
        {
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Paswword can't be Empty";
                return false;
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                lblError.Text = "FileName can't be Empty";
                return false;
            }
            return true;
        }
        private void btnCancle_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            saveFileCCD.ShowDialog();
             fileName = saveFileCCD.FileName;
            txtFileName.Text = fileName;
            lblError.Text = "";
        }
    }
}
