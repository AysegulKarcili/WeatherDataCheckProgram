using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDataCheck
{
    class Program
    {
        private static DateTime Date(string dataRow)
        {
            string year = dataRow.Split('|')[2];
            string month = dataRow.Split('|')[3];
            if (month.Length != 2)
            {
                month = '0' + month;
            }
            string day = dataRow.Split('|')[4];
            if (day.Length != 2)
            {
                day = '0' + day;
            }
            string hour = dataRow.Split('|')[5] + ":00:00";
            if (hour.Length != 8)
            {
                hour = "0" + hour;
            }

            string dateStr = year + '-' + month + '-' + day + ' ' + hour;
            DateTime date = DateTime.Parse(dateStr);
            return date;
        }

        static void Main(string[] args)
        {
            String path = @"..\..\..\bitlis.txt";
            String[] data = File.ReadAllLines(path);

            String[] dataDaysL = new String[1827];
            dataDaysL[0] = data[0];
            int dayInd = 1;
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i].Split('|')[4] != data[i - 1].Split('|')[4]) //Date(data[i]).Day != Date(data[i - 1]).Day
                {
                    dataDaysL[dayInd] = data[i];
                    dayInd++;
                }
            }
            String[] dataDays = new String[dayInd];
            for (int i = 0; i < dataDays.Length; i++)
            {
                dataDays[i] = dataDaysL[i];
                //Console.WriteLine(dataDays[i]);
            }

            String[] missingDays = new String[dataDays.Length];
            int misInd = 0;
            DateTime nextDate = Date(dataDays[0]).AddDays(1);
            for (int i = 1; i < dataDays.Length; i++)
            {
                if (Date(dataDays[i]).Day != nextDate.Day)
                {
                    missingDays[misInd] = nextDate.ToString();
                    misInd++;
                    i--;
                }
                nextDate = nextDate.AddDays(1);
            }
            String[] missingDaysS = new String[misInd];
            for (int i = 0; i < missingDaysS.Length; i++)
            {
                missingDaysS[i] = missingDays[i];
                Console.WriteLine(missingDaysS[i]);
            }

            String pathMis = @"..\..\..\bitlisEksikGunler.txt";
            File.WriteAllLines(pathMis, missingDaysS);

            Console.WriteLine(dayInd);
            Console.WriteLine(misInd);
            Console.ReadLine();
        }
    }
}
