using KNA_Studio.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KNA_Studio
{
    public partial class HomePage: Form
    {
        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        public HomePage()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Agreement agreement = new Agreement();
            //agreement.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Builder builder = new Builder();
            builder.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            CreateNewProject createNewProject = new CreateNewProject();
            createNewProject.ShowDialog();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            ExistingTypeSelecter existingTypeSelecter = new ExistingTypeSelecter();
            existingTypeSelecter.ShowDialog();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            try
            {
                // URL to open
                string url = "https://github.com/AyanWasTaken/KnaLDBStudio";

                // Use the operating system's shell to open the URL
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open browser: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
