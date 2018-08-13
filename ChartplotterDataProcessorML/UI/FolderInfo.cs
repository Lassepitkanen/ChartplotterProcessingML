using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartplotterDataProcessorML.UI
{
    public class FolderInfo
    {
        public void ShowFolderInfo(string[] folders)
        {
            Console.WriteLine("ID  FOLDER             SIZE");
            for (int i = 0; i < folders.Length; i++)
            {
                Console.Write(String.Format("{0, 2}", i + 1) + "  " + Path.GetFileName(folders[i]));
                Console.SetCursorPosition(20, i + 1);
                Console.WriteLine(String.Format("{0, 7}", GetFolderSize(folders[i])));
            }
        }

        private long GetFolderSize(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length);
        }
    }
}
