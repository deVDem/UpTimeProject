using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Classes
{
    public class DataController
    {
        public static DataController sDataController;

        public DataController()
        {

        }


        public static DataController Get()
        {
            if (sDataController == null)
            {
                sDataController = new DataController();
            }
            return sDataController;
        }
    }
}
