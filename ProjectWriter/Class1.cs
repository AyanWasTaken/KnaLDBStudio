using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorHelper;

namespace ProjectWriter
{
    public class Writer
    {
        public static void write()
        {
            if (Directory.Exists(Environment.CurrentDirectory) == false)
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\repos\");
            }
            else if (Directory.Exists(Environment.CurrentDirectory) == true)
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\repos\");
            }
            else
            {
                ErrorHelper.Show show = new ErrorHelper.Show();
                show.Error("Please contact a support representative.", "Error");
            }
        }
    }
}
