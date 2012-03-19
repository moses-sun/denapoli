using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.Prism.Logging;

namespace Denapoli
{
    public class CallbackLogger : ILoggerFacade
    {
        private ObservableCollection<Tuple<string, Category, Priority, DateTime>> Messages { get; set; }

        public CallbackLogger()
        {
            Messages = new ObservableCollection<Tuple<string, Category, Priority, DateTime>>();
        }
       
        public void Log(string message, Category category, Priority priority)
        {
           // Messages.Add(new Tuple<string, Category, Priority,DateTime>(message, category, priority, DateTime.Now));
        }

        public void DumpLog(string fileName)
        {
            /*var writer = new StreamWriter(fileName);
            Messages.ForEach(msg=> writer.WriteLine(msg.Item4 + ":" + msg.Item2 + ":" + msg.Item3 + ": " + msg.Item1));
            writer.Close();*/
        }
    }
}