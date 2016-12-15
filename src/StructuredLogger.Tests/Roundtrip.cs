﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Build.Logging.StructuredLogger;
using Microsoft.Build.Logging.StructuredLoggerHost;
using StructuredLogViewer;
using Xunit;

namespace StructuredLogger.Tests
{
    public class Roundtrip
    {
        //[Fact]
        public void RoundtripTest()
        {
            foreach (var file in Directory.GetFiles(@"D:\XmlBuildLogs", "*.xml", SearchOption.AllDirectories).ToArray())
            {
                var build = Serialization.Read(file);
                var newName = Path.ChangeExtension(file, ".new.xml");
                Serialization.Write(build, newName);
                if (Differ.AreDifferent(file, newName))
                {
                    break;
                }
                else
                {
                    File.Delete(newName);
                }

                newName = Path.ChangeExtension(file, ".buildlog");
                Serialization.Write(build, newName);
                build = Serialization.Read(newName);
                newName = Path.ChangeExtension(file, ".new2.xml");
                Serialization.Write(build, newName);
                if (Differ.AreDifferent(file, newName))
                {
                    break;
                }
                else
                {
                    File.Delete(newName);
                    //File.Delete(Path.ChangeExtension(file, ".buildlog"));
                }
            }
        }

        //[Fact]
        public void SearchPerf()
        {
            var file = @"D:\contentsync.xml";
            var build = Serialization.Read(file);
            var sw = Stopwatch.StartNew();
            var search = new Search(build);
            var results = search.FindNodes("test");
            var elapsed = sw.Elapsed;
            MessageBox.Show(elapsed.ToString());
            File.WriteAllLines(@"D:\2.txt", results.Select(r => r.Field).ToArray());
        }
    }
}
