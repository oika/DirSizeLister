using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirSizeLister
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                return;
            }

            try
            {
                args.SelectMany(rt => Directory.GetDirectories(rt))
                    .Select(d => new { Path = d, Size = _GetDirSize(d), Count = _GetFileCount(d) })
                    .OrderByDescending(a => a.Size)
                    .ToList()
                    .ForEach(a => Console.WriteLine("{0, 9:#,0}kb\t{1, 5} files\t{2}", a.Size / 1000, a.Count, a.Path));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        private static long _GetFileCount(string dir)
        {
            return Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories).LongCount();
        }

        private static long _GetDirSize(string dir)
        {
            return Directory.GetFiles(dir).Select(f => new FileInfo(f).Length)
                            .Concat(Directory.GetDirectories(dir).Select(d => _GetDirSize(d)))
                            .Sum();
        }
    }
}
