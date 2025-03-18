using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KNA_Studio
{
    public partial class Builder: Form
    {
        public Builder()
        {
            InitializeComponent();
            MessageBox.Show("KNA LDB Studio 1.1 PTB: This our first public test build! Report bugs on our GitHub page.", "What's New", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Builder_Load(object sender, EventArgs e)
        {

        }
    }
}
