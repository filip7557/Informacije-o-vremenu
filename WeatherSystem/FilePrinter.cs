using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeatherSystemLogic
{
    public class FilePrinter : IPrinter
    {
        private string fileName;

        public FilePrinter(string fileName)
        {
            this.fileName = fileName;
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public void PrintWeather(Weather weather)
        {
            File.AppendAllText(fileName, weather.ToString() + "\n");
        }
    }
}
