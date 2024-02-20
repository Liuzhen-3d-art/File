using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            FileX fileX = new FileX();

            //fileX.FileCopy();
            //fileX.FileCreate();
            // fileX.FileDelete();
            //fileX.FileMove();
            //fileX.FileExists();
            //fileX.FileOpen();
            //fileX.FileOpenRead();
            //fileX.FileInfoText();
            //fileX.FileInfoCreate("D://ShengMing/shengming.txt", "其实我做这一切都是为你将来变得更好");
            fileX.FileDresify();
        }
    }
}
