/*******************************
Version: 1.0
Project Boon
*******************************/

//Originally created by Saad Khawaja
//https://www.assetstore.unity3d.com/en/#!/content/23706
//Altered by Jordan Miles for his own useability

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

namespace JMiles42.OthersSystems {
    public class INIParser {
        #region "Declarations"
        // *** Error: In case there're errors, this will changed to some value other than 1 ***
        // Error codes:
        // 1: Null TextAsset
        public int error;

        // *** Lock for thread-safe access to file and local cache ***
        private readonly object m_Lock = new object();

        // *** File name ***

        public string FileName { get; private set; }

        // ** String represent Ini

        public string IniString { get; private set; }

        // *** Automatic flushing flag ***
        private bool m_AutoFlush;

        // *** Local cache ***
        private readonly Dictionary<string, Dictionary<string, string>> m_Sections = new Dictionary<string, Dictionary<string, string>>();

        private readonly Dictionary<string, Dictionary<string, string>> m_Modified = new Dictionary<string, Dictionary<string, string>>();

        // *** Local cache modified flag ***
        private bool m_CacheModified;
        #endregion

        public INIParser() {}
        public INIParser(string path) { Open(path); }
        public INIParser(TextAsset name) { Open(name); }

        ~INIParser() {
            if (FileName == null && IniString == null)
                return;
            Close();
        }

        #region "Methods"
        // *** Open ini file by path ***
        public void Open(string path) {
            FileName = path;

            if (File.Exists(FileName))
            {
                IniString = File.ReadAllText(FileName);
            }
            else
            {
                //If file does not exist, create one
                var temp = File.Create(FileName);
                temp.Close();
                IniString = "";
            }

            Initialize(IniString, false);
        }

        // *** Open ini file by TextAsset: All changes is saved to local storage ***
        public void Open(TextAsset name) {
            if (name == null)
            {
                // In case null asset, treat as opened an empty file
                error = 1;
                IniString = "";
                FileName = null;
                Initialize(IniString, false);
            }
            else
            {
                FileName = Application.persistentDataPath + name.name;

                //*** Find the TextAsset in the local storage first ***
                IniString = File.Exists(FileName)? File.ReadAllText(FileName) : name.text;
                Initialize(IniString, false);
            }
        }

        // *** Open ini file from string ***
        public void OpenFromString(string str) {
            FileName = null;
            Initialize(str, false);
        }

        // *** Get the string content of ini file ***
        public override string ToString() { return IniString; }

        private void Initialize(string iniString, bool AutoFlush) {
            IniString = iniString;
            m_AutoFlush = AutoFlush;
            Refresh();
        }

        // *** Close, save all changes to ini file ***
        public void Close() {
            lock (m_Lock)
            {
                PerformFlush();

                //Clean up memory
                FileName = null;
                IniString = null;
            }
        }

        // *** Parse section name ***
        private static string ParseSectionName(string Line) {
            if (!Line.StartsWith("["))
                return null;
            if (!Line.EndsWith("]"))
                return null;
            return Line.Length < 3? null : Line.Substring(1, Line.Length - 2);
        }

        // *** Parse key+value pair ***
        private static bool ParseKeyValuePair(string Line, ref string Key, ref string Value) {
            // *** Check for key+value pair ***
            int i;
            if ((i = Line.IndexOf('=')) <= 0)
                return false;

            int j = Line.Length - i - 1;
            Key = Line.Substring(0, i).Trim();
            if (Key.Length <= 0)
                return false;

            Value = (j > 0)? (Line.Substring(i + 1, j).Trim()) : ("");
            return true;
        }

        // *** If a line is neither SectionName nor key+value pair, it's a comment ***
        private static bool IsComment(string Line) {
            string tmpKey = null, tmpValue = null;
            if (ParseSectionName(Line) != null)
                return false;
            return !ParseKeyValuePair(Line, ref tmpKey, ref tmpValue);
        }

        // *** Read file contents into local cache ***
        private void Refresh() {
            lock (m_Lock)
            {
                StringReader sr = null;
                try
                {
                    // *** Clear local cache ***
                    m_Sections.Clear();
                    m_Modified.Clear();

                    // *** String Reader ***
                    sr = new StringReader(IniString);

                    // *** Read up the file content ***
                    Dictionary<string, string> CurrentSection = null;
                    string s;
                    string Key = null;
                    string Value = null;
                    while ((s = sr.ReadLine()) != null)
                    {
                        s = s.Trim();

                        // *** Check for section names ***
                        string SectionName = ParseSectionName(s);
                        if (SectionName != null)
                        {
                            // *** Only first occurrence of a section is loaded ***
                            if (m_Sections.ContainsKey(SectionName))
                            {
                                CurrentSection = null;
                            }
                            else
                            {
                                CurrentSection = new Dictionary<string, string>();
                                m_Sections.Add(SectionName, CurrentSection);
                            }
                        }
                        else if (CurrentSection != null)
                        {
                            // *** Check for key+value pair ***
                            if (!ParseKeyValuePair(s, ref Key, ref Value))
                                continue;
                            // *** Only first occurrence of a key is loaded ***
                            if (!CurrentSection.ContainsKey(Key))
                            {
                                CurrentSection.Add(Key, Value);
                            }
                        }
                    }
                }
                finally
                {
                    // *** Cleanup: close file ***
                    if (sr != null)
                        sr.Close();
                }
            }
        }

        private void PerformFlush() {
            // *** If local cache was not modified, exit ***
            if (!m_CacheModified)
                return;
            m_CacheModified = false;

            // *** Copy content of original iniString to temporary string, replace modified values ***
            var sw = new StringWriter();

            try
            {
                Dictionary<string, string> CurrentSection = null;
                Dictionary<string, string> CurrentSection2 = null;
                StringReader sr = null;
                try
                {
                    // *** Open the original file ***
                    sr = new StringReader(IniString);

                    // *** Read the file original content, replace changes with local cache values ***
                    string Key = null;
                    string Value = null;
                    var Reading = true;

                    var Deleted = false;
                    string Key2 = null;
                    string Value2 = null;

                    while (Reading)
                    {
                        string s = sr.ReadLine();
                        Reading = (s != null);

                        // *** Check for end of iniString ***
                        bool Unmodified;
                        string SectionName;
                        if (Reading)
                        {
                            Unmodified = true;
                            s = s.Trim();
                            SectionName = ParseSectionName(s);
                        }
                        else
                        {
                            Unmodified = false;
                            SectionName = null;
                        }

                        // *** Check for section names ***
                        if ((SectionName != null) || (!Reading))
                        {
                            if (CurrentSection != null)
                            {
                                // *** Write all remaining modified values before leaving a section ****
                                if (CurrentSection.Count > 0)
                                {
                                    // *** Optional: All blank lines before new values and sections are removed ****
                                    var sb_temp = sw.GetStringBuilder();
                                    while ((sb_temp[sb_temp.Length - 1] == '\n') || (sb_temp[sb_temp.Length - 1] == '\r'))
                                    {
                                        sb_temp.Length = sb_temp.Length - 1;
                                    }
                                    sw.WriteLine();

                                    foreach (string fkey in CurrentSection.Keys)
                                    {
                                        if (!CurrentSection.TryGetValue(fkey, out Value))
                                            continue;
                                        sw.Write(fkey);
                                        sw.Write('=');
                                        sw.WriteLine(Value);
                                    }
                                    sw.WriteLine();
                                    CurrentSection.Clear();
                                }
                            }

                            if (Reading)
                            {
                                // *** Check if current section is in local modified cache ***
                                if (!m_Modified.TryGetValue(SectionName, out CurrentSection))
                                {
                                    CurrentSection = null;
                                }
                            }
                        }
                        else if (CurrentSection != null)
                        {
                            // *** Check for key+value pair ***
                            if (ParseKeyValuePair(s, ref Key, ref Value))
                            {
                                if (CurrentSection.TryGetValue(Key, out Value))
                                {
                                    // *** Write modified value to temporary file ***
                                    Unmodified = false;
                                    CurrentSection.Remove(Key);

                                    sw.Write(Key);
                                    sw.Write('=');
                                    sw.WriteLine(Value);
                                }
                            }
                        }

                        // ** Check if the section/key in current line has been deleted ***
                        if (Unmodified)
                        {
                            if (SectionName != null)
                            {
                                if (!m_Sections.ContainsKey(SectionName))
                                {
                                    Deleted = true;
                                    CurrentSection2 = null;
                                }
                                else
                                {
                                    Deleted = false;
                                    m_Sections.TryGetValue(SectionName, out CurrentSection2);
                                }
                            }
                            else if (CurrentSection2 != null)
                            {
                                if (ParseKeyValuePair(s, ref Key2, ref Value2))
                                {
                                    Deleted = !CurrentSection2.ContainsKey(Key2);
                                }
                            }
                        }

                        // *** Write unmodified lines from the original iniString ***
                        if (!Unmodified)
                            continue;
                        if (IsComment(s))
                            sw.WriteLine(s);
                        else if (!Deleted)
                            sw.WriteLine(s);
                    }

                    // *** Close string reader ***
                    sr.Close();
                    sr = null;
                }
                finally
                {
                    // *** Cleanup: close string reader ***
                    if (sr != null)
                        sr.Close();
                }

                // *** Cycle on all remaining modified values ***
                foreach (var SectionPair in m_Modified)
                {
                    CurrentSection = SectionPair.Value;
                    if (CurrentSection.Count <= 0)
                        continue;
                    sw.WriteLine();

                    // *** Write the section name ***
                    sw.Write('[');
                    sw.Write(SectionPair.Key);
                    sw.WriteLine(']');

                    // *** Cycle on all key+value pairs in the section ***
                    foreach (var ValuePair in CurrentSection)
                    {
                        // *** Write the key+value pair ***
                        sw.Write(ValuePair.Key);
                        sw.Write('=');
                        sw.WriteLine(ValuePair.Value);
                    }
                    CurrentSection.Clear();
                }
                m_Modified.Clear();

                // *** Get result to iniString ***
                IniString = sw.ToString();
                sw.Close();
                sw = null;

                // ** Write iniString to file ***
                if (FileName != null)
                {
                    File.WriteAllText(FileName, IniString);
                }
            }
            finally
            {
                // *** Cleanup: close string writer ***
                if (sw != null)
                    sw.Close();
            }
        }

        // *** Check if the section exists ***
        public bool IsSectionExists(string SectionName) { return m_Sections.ContainsKey(SectionName); }

        // *** Check if the key exists ***
        public bool IsKeyExists(string SectionName, string Key) {
            // *** Check if the section exists ***
            if (!m_Sections.ContainsKey(SectionName))
                return false;

            Dictionary<string, string> Section;
            m_Sections.TryGetValue(SectionName, out Section);

            // If the key exists
            return Section != null && Section.ContainsKey(Key);
        }

        // *** Delete a section in local cache ***
        public void SectionDelete(string SectionName) {
            // *** Delete section if exists ***
            if (!IsSectionExists(SectionName))
                return;
            lock (m_Lock)
            {
                m_CacheModified = true;
                m_Sections.Remove(SectionName);

                //Also delete in modified cache if exist
                m_Modified.Remove(SectionName);

                // *** Automatic flushing : immediately write any modification to the file ***
                if (m_AutoFlush)
                    PerformFlush();
            }
        }

        // *** Delete a key in local cache ***
        public void KeyDelete(string SectionName, string Key) {
            //Delete key if exists
            if (!IsKeyExists(SectionName, Key))
                return;

            lock (m_Lock)
            {
                m_CacheModified = true;
                Dictionary<string, string> Section;
                m_Sections.TryGetValue(SectionName, out Section);
                if (Section != null)
                {
                    Section.Remove(Key);

                    //Also delete in modified cache if exist
                    if (m_Modified.TryGetValue(SectionName, out Section))
                        Section.Remove(SectionName);
                }

                // *** Automatic flushing : immediately write any modification to the file ***
                if (m_AutoFlush)
                    PerformFlush();
            }
        }

        // *** Read a value from local cache ***
        public string ReadValue(string SectionName, string Key, string DefaultValue) {
            lock (m_Lock)
            {
                // *** Check if the section exists ***
                Dictionary<string, string> Section;
                if (!m_Sections.TryGetValue(SectionName, out Section))
                    return DefaultValue;

                // *** Check if the key exists ***
                string Value;
                return !Section.TryGetValue(Key, out Value)? DefaultValue : Value;

                // *** Return the found value ***
            }
        }

        // *** Insert or modify a value in local cache ***
        public void WriteValue(string SectionName, string Key, string Value) {
            lock (m_Lock)
            {
                // *** Flag local cache modification ***
                m_CacheModified = true;

                // *** Check if the section exists ***
                Dictionary<string, string> Section;
                if (!m_Sections.TryGetValue(SectionName, out Section))
                {
                    // *** If it doesn't, add it ***
                    Section = new Dictionary<string, string>();
                    m_Sections.Add(SectionName, Section);
                }

                // *** Modify the value ***
                if (Section.ContainsKey(Key))
                    Section.Remove(Key);
                Section.Add(Key, Value);

                // *** Add the modified value to local modified values cache ***
                if (!m_Modified.TryGetValue(SectionName, out Section))
                {
                    Section = new Dictionary<string, string>();
                    m_Modified.Add(SectionName, Section);
                }

                if (Section.ContainsKey(Key))
                    Section.Remove(Key);
                Section.Add(Key, Value);

                // *** Automatic flushing : immediately write any modification to the file ***
                if (m_AutoFlush)
                    PerformFlush();
            }
        }

        // *** Encode byte array ***
        private static string EncodeByteArray(byte[] Value) {
            if (Value == null)
                return null;

            var sb = new StringBuilder();
            foreach (byte b in Value)
            {
                string hex = Convert.ToString(b, 16);
                int l = hex.Length;
                if (l > 2)
                {
                    sb.Append(hex.Substring(l - 2, 2));
                }
                else
                {
                    if (l < 2)
                        sb.Append("0");
                    sb.Append(hex);
                }
            }
            return sb.ToString();
        }

        // *** Decode byte array ***
        private static byte[] DecodeByteArray(string Value) {
            if (Value == null)
                return null;

            int l = Value.Length;
            if (l < 2)
                return new byte[] {};

            l /= 2;
            var Result = new byte[l];
            for (var i = 0; i < l; i++)
                Result[i] = Convert.ToByte(Value.Substring(i * 2, 2), 16);
            return Result;
        }

        // *** Getters for various types ***
        public bool ReadValue(string SectionName, string Key, bool DefaultValue) {
            string StringValue = ReadValue(SectionName, Key, DefaultValue.ToString(CultureInfo.InvariantCulture));
            int Value;
            if (int.TryParse(StringValue, out Value))
                return (Value != 0);
            return DefaultValue;
        }

        public int ReadValue(string SectionName, string Key, int DefaultValue) {
            string StringValue = ReadValue(SectionName, Key, DefaultValue.ToString(CultureInfo.InvariantCulture));
            int Value;
            return int.TryParse(StringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out Value)? Value : DefaultValue;
        }

        public long ReadValue(string SectionName, string Key, long DefaultValue) {
            string StringValue = ReadValue(SectionName, Key, DefaultValue.ToString(CultureInfo.InvariantCulture));
            long Value;
            return long.TryParse(StringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out Value)? Value : DefaultValue;
        }

        public float ReadValue(string SectionName, string Key, float DefaultValue) {
            string StringValue = ReadValue(SectionName, Key, DefaultValue.ToString(CultureInfo.InvariantCulture));
            float Value;
            return float.TryParse(StringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out Value)? Value : DefaultValue;
        }

        public double ReadValue(string SectionName, string Key, double DefaultValue) {
            string StringValue = ReadValue(SectionName, Key, DefaultValue.ToString(CultureInfo.InvariantCulture));
            double Value;
            return double.TryParse(StringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out Value)? Value : DefaultValue;
        }

        public byte[] ReadValue(string SectionName, string Key, byte[] DefaultValue) {
            string StringValue = ReadValue(SectionName, Key, EncodeByteArray(DefaultValue));
            try
            {
                return DecodeByteArray(StringValue);
            }
            catch (FormatException)
            {
                return DefaultValue;
            }
        }

        public DateTime ReadValue(string SectionName, string Key, DateTime DefaultValue) {
            string StringValue = ReadValue(SectionName, Key, DefaultValue.ToString(CultureInfo.InvariantCulture));
            DateTime Value;
            return DateTime.TryParse(
                                     StringValue, CultureInfo.InvariantCulture,
                                     DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AssumeLocal, out Value)? Value :
                DefaultValue;
        }

        // *** Setters for various types ***
        public void WriteValue(string SectionName, string Key, bool Value) { WriteValue(SectionName, Key, (Value)? ("1") : ("0")); }

        public void WriteValue(string SectionName, string Key, int Value) { WriteValue(SectionName, Key, Value.ToString(CultureInfo.InvariantCulture)); }

        public void WriteValue(string SectionName, string Key, long Value) { WriteValue(SectionName, Key, Value.ToString(CultureInfo.InvariantCulture)); }

        public void WriteValue(string SectionName, string Key, float Value) { WriteValue(SectionName, Key, Value.ToString(CultureInfo.InvariantCulture)); }

        public void WriteValue(string SectionName, string Key, double Value) { WriteValue(SectionName, Key, Value.ToString(CultureInfo.InvariantCulture)); }

        public void WriteValue(string SectionName, string Key, byte[] Value) { WriteValue(SectionName, Key, EncodeByteArray(Value)); }

        public void WriteValue(string SectionName, string Key, DateTime Value) { WriteValue(SectionName, Key, Value.ToString(CultureInfo.InvariantCulture)); }
        #endregion
    }
}