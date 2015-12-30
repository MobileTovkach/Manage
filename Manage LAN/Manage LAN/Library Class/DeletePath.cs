using System;
using System.IO;
using System.Threading;

namespace Manage_LAN.Library_Class
{
    internal class DeletePath
    {
        public static Tuple<int, string> ErrDic;

        public static void DelPath(string topPath)
        {
            if (!Directory.Exists(topPath))
            {
                ErrDic = new Tuple<int, string>(1,"Direcyory was not found!");
            }
            else
            {
                Directory.Delete(topPath,true);
                Thread.Sleep(2000);
                ErrDic = new Tuple<int, string>(0,"Delete commplit");
            }
        }
    }
}
