using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bronto.API.Tests
{
    [TestClass]
    public abstract class BrontoBaseTestClass
    {
        public class Timer
        {
            public DateTime Start = DateTime.Now;
            public DateTime? End = null;
            public string Title = "Timer";
            public Timer()
            {
                
            }

            public Timer(string title)
            {
                this.Title = title;
            }

            public override string ToString()
            {
                DateTime end = End ?? DateTime.Now;
                TimeSpan ts = (end - Start);
                return string.Format("The timer {0} ran for {1} min, {2} sek, {3} ms", Title, ts.Minutes, ts.Seconds,
                    ts.Milliseconds);
            }
        }


        private string _ApiToken = null;

        private Queue<Timer> timers = new Queue<Timer>();

        public Timer StartTimer(string Title)
        {
            Timer t = new Timer(Title);
            timers.Enqueue(t);
            return t;
        }

        public Timer EndTimer()
        {
            return timers.Dequeue();
        }

        private Dictionary<string, string> _ApiFields = null;

        protected Dictionary<string, string> ApiFields
        {
            get
            {
                if (_ApiFields == null)
                {
                    const string brontofile = "brontoapifields.txt";
                    string fullpath = GetApiFilePath(new string[]
                        {
                    Path.Combine(Environment.CurrentDirectory, brontofile),
                    Path.Combine(Assembly.GetExecutingAssembly().Location, brontofile),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),brontofile),
                    Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()),brontofile)
                        });

                    _ApiFields = new Dictionary<string, string>();
                    if (!string.IsNullOrEmpty(fullpath))
                    {
                        string[] Lines = File.ReadAllLines(fullpath);
                        foreach (string line in Lines)
                        {
                            string[] s = line.Split(';');
                            _ApiFields.Add(s[0], s[1]);
                        }
                    }
                }
                return _ApiFields;
            }
        }



        protected string ApiToken
        {
            get
            {
                const string brontofile = "brontoapi.txt";
                if (string.IsNullOrEmpty(_ApiToken))
                {
                    string fullpath = GetApiFilePath(new string[]
                    {
                    Path.Combine(Environment.CurrentDirectory, brontofile),
                    Path.Combine(Assembly.GetExecutingAssembly().Location, brontofile),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),brontofile),
                    Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()),brontofile)
                    });

                    if (string.IsNullOrEmpty(fullpath))
                    {
                        throw new FileNotFoundException("Could not locate a file with the bronto Api Token", brontofile);
                    }
                    ApiToken = File.ReadAllText(fullpath);
                }
                return _ApiToken;
            }
            private set { _ApiToken = value; }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            
        }

        private static string GetApiFilePath(IEnumerable<string> paths) => paths.FirstOrDefault(File.Exists);
    }
}
