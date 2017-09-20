using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JMiles42.Extensions;
using UnityEditor;

namespace JMiles42.Editor
{
    public static class DefineManager
    {
        public const string DefineManagerPath = FileStrings.ASSETS_GENERATED_RAWDATA + "/DefineManager" + FileStrings.JMDataFILEEXT;
        private const string mcs_rspPath = FileStrings.ASSETS + "/mcs.rsp";

        private static List<string> s_defines = new List<string> {"JMiles42"};
        public static Action OnDataChanged;

        public static List<string> Defines
        {
            get { return s_defines; }
            private set { s_defines = value; }
        }

        static DefineManager() { Init(); }

        public static void AddDefine(string defineName, bool writeFile = true)
        {
            if (Defines.Contains(defineName))
                return;

            Defines.Add(defineName);

            if (!writeFile)
                return;
            WriteFiles();
        }

        public static void Init()
        {
            if (File.Exists(DefineManagerPath))
            {
                using (var stream = new StreamReader(DefineManagerPath))
                {
                    Defines = new List<string>();
                    while (!stream.EndOfStream)
                    {
                        Defines.Add(stream.ReadLine());
                    }
                }
            }

            WriteFiles();
            AssetDatabase.Refresh();
        }

        private static void WriteFiles()
        {
            WriteDataFile();
            OnDataChanged.Trigger();
            WriteDefineFile();
        }

        private static void WriteDataFile() { ScriptGenerators.WriteFile(DefineManagerPath, Defines); }

        private static void WriteDefineFile()
        {
            if (Defines.Count == 0)
                return;

            var data = new[] {Defines.Aggregate("-define:", (current, str) => current + (str + ";"))};

            ScriptGenerators.WriteFile(mcs_rspPath, data);
        }
    }
}