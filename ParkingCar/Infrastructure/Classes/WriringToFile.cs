using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ParkingCar.Infrastructure.Interface;
using ParkingCar.Model;

namespace ParkingCar.Infrastructure.Classes
{
    class WriringToFile : IWriringToFile
    {
        private readonly string path;
        private List<Car> list;
        public WriringToFile(string _path, List<Car> _list)
        {
            path = _path;
            list = _list;
        }
        public async void PushToFile(List<Transaction> list, string path, double balance)
        {
            byte[] info = GetInfo(list).Result;
            string targetPath = GetTargetPath(path).Result;
            Task tasks = Task.WhenAll(GetInfo(list), GetTargetPath(path));

            try
            {
                await tasks;
            }
            catch (AggregateException aex)
            {
                aex.Flatten().Handle(ex =>
                {
                    Console.WriteLine(ex.Message);
                    return true;
                });
            }

            if (File.Exists(targetPath))
            {
                using (FileStream fs = new FileStream(targetPath, FileMode.OpenOrCreate))
                {
                    fs.Seek(0, SeekOrigin.End);
                    Task f1 = Write(fs, info);
                    Task f2 = fs.FlushAsync();
                    await Task.WhenAll(f1, f2);
                }
            }
        }
        public Task<string> GetTargetPath(string path)
        {
            var task = Task<string>.Run(() =>
            {
                var dir = Directory.GetCurrentDirectory().TrimEnd("bin\\Debug".ToArray());
                string targetPath = Path.Combine(dir, Path.GetDirectoryName(path) + "\\" + Path.GetFileName(path));
                return targetPath;
            });
            return task;
        }

        public Task<byte[]> GetInfo(List<Transaction> list)
        {
            var task = Task<byte[]>.Run(() =>
            {
                string sum = list.Sum(i => i.WriteOffs).ToString();
                string data = String.Format("{0}{1}     {2}", "\r", DateTime.Now.ToString(), sum);
                byte[] info = new UTF8Encoding(true).GetBytes(data);
                return info;
            });

            return task;
        }
        public async Task Write(FileStream fs, byte[] info)
        {
            await fs.WriteAsync(info, 0, info.Length);
        }
    }

}
