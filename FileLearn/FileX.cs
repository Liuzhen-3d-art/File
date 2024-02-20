using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;


namespace FileLearn//最重要的是using方法，因为如果不能及时清理缓冲区，而去执行下一个IO方法就会因为线程冲突导致程序异常，而勤加清理线程缓冲区则不会出现什么问题；总结：多用using（IO操作）
{
    class FileX
    {
        public void AppendllLines()//在文件中写入内容
        {
            //文件路径中的目录一定要存在，否则会报异常
           
            File.AppendAllLines("D://logs/log.txt", new List<string> { "第一行：今天学习的是文件写入类。" + DateTime.Now.ToString("D") ,"第二行：今天学的是写入类下的写入操作"});
            //如果发现写入的内容出现乱码，就需要用到字符编码
            File.AppendAllLines("D://logs/log2.txt", new List<string> { "第一行：今天学习的是文件写入类。" + DateTime.Now, "第二行：今天学的是写入类下的写入操作" }, Encoding.UTF8);
            //如果不需要换行就需要AppendAllText，当然使用\r\n也可以进行换行
            File.AppendAllText("D://logs/log2.txt", "AppendAllText" + DateTime.Now);

        }
        public void StreamWriter()//在文件流里写入内容
        {
           
            //此方法适用于所有的文件流写入工作。
            using (var _streamWriter = File.AppendText("D://logs/log.txt")) //方法原理是首先创建一个streamWriter流，然后再根据路径获取指定文件，再将这个文件写入到streamWriter（文件流）中，用完后要关闭
            {
                _streamWriter.WriteLine("我是通过streamWriter对象写入的");
                _streamWriter.Flush();//刷新缓冲区,必须在写入完后加入这个方法
                
            }



        }
        public void FileCopy()//复制文件，建议直接用第二个，第一个方法覆盖会报错
        {
            File.Copy("D://logs/log.txt", "D://logs/log3.txt");// 将一个文件复制到另一个地方的文件上，但要求复制的地址上没有要复制的文件（也就是只能复制到从没有复制过的地方）
            File.Copy("D://Logs/log.txt", "E://logs/log.txt",true);//功能同上但可以覆盖相同名称的文件了
        }
        public void FileCreate()//覆盖文件并返回文件流,并写入文件
        {
            using (var fileStream= File.Create("D://logs/log.txt"))
            {
                using (StreamWriter stringWriter = new StreamWriter(fileStream))
                {
                    stringWriter.WriteLine("这是Create的写入方式");
                }
                
            }
        }
        public void FileDelete()//删除文件
        {
            File.Delete("D://logs/log.txt");//删除
        }
        public void FileMove()//移动文件
        {
            File.Move("D://logs/log2.txt", "D://logs/log4.txt");
        }
        public void FileExists()//检测文件是否存在的方法，返回Bool类型
        {
            if (File.Exists("D://logs/log2.txt"))
            {
                Console.WriteLine("存在");
            }
            else
            {
                Console.WriteLine("不存在");
            }
        }
        public void FileOpen()
        {
            ///如果你要对它进行写入，我们需要设置一下文件写入权限。FileAccess就是设置读写权限的方法。
            using (var fs = File.Open("D://logs/log4.txt", FileMode.Open,FileAccess.ReadWrite))
            {
                StreamWriter writer = new StreamWriter(fs);//文件流写的方法
                
                    writer.WriteLine("TestOpen");
                
                using(StreamReader reader=new StreamReader(fs))//文件读取的方法
                {
                    string line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
            
        }
        public void FileOpenRead()//打开现有文件进行读取,没有读取的权限
        {
            using (var fss = File.OpenRead("D://logs/log4.txt"))
            {
                using (StreamReader reader = new StreamReader(fss))//文件读取的方法
                {
                    string line = reader.ReadLine();
                    Console.WriteLine(line);
                }

            }
        }
      
        public void FileInfoText()//FileInfo是可以重复对文件进行操作
        {
            FileInfo fileInfo = new FileInfo("D://logs/log4.txt");
            using (StreamWriter sw = fileInfo.AppendText())//写入
            {
                sw.WriteLine("山东");
            }
            using (StreamReader sr = fileInfo.OpenText())//打开
            {
                Console.WriteLine($"这是从FileInfo类型方法中读取的值：{sr.ReadLine()}");
            }


        }
        public void FileInfoCreate(string DiZhi,string NeiRong)
        {
            string dizhi = Path.GetDirectoryName(DiZhi);//返回字符串格式的路径信息
            if (!Directory.Exists(dizhi))
            {
                Directory.CreateDirectory(dizhi);//判断并创建文件夹
            }
            FileInfo fileInfo = new FileInfo(DiZhi);
            using (FileStream fs = fileInfo.Create())//创建文件
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(NeiRong);//由字符串填装到Byte的数组里
                fs.Write(info, 0, info.Length);//通过字节数组的方式通过第一个位置开始写入，一直到写入完毕为止
                
            }
            using(StreamReader sr = fileInfo.OpenText())//读取文件内文本信息
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {


                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{DiZhi}\t");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"|\t{ s}");
                    
                    return;
                }
                Console.WriteLine("空了");
            }

           
        }

        public void FileDresify()//实时5秒自动检索文件器，会自动返回用户提供的根目录下的所有文件
        {
            while (true)
            {
                Console.WriteLine("-----------S-------------T--------------O---------------P----------->>");
                DirectoryInfo df = new DirectoryInfo("H://C#");
                var fle = df.EnumerateDirectories("*", SearchOption.AllDirectories);
               
                try
                {
                    foreach (var item in fle)
                    {
                        Console.WriteLine(item.FullName); 
                      
                      
                    }
                    var File = df.EnumerateFiles("*", SearchOption.AllDirectories);
                    foreach (var item2 in File)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(item2.FullName);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                
                catch (Exception)
                {

                    continue;
                }
                Thread.Sleep(5000);



            }
        }
    


    }
}
