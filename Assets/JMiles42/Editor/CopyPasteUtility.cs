using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JMiles42.Editor.Utilities
{
    public static class CopyPasteUtility
    {
        public static string CopyBuffer
        {
            get { return EditorGUIUtility.systemCopyBuffer; }
            set { EditorGUIUtility.systemCopyBuffer = value; }
        }

        public static string CopyBufferNoTypeName
        {
            get
            {
                var copyBufferSplit = CopyBuffer.Split(new [] {COPY_SPLIT}, StringSplitOptions.None);
                if (copyBufferSplit.Length > 1)
                {
                    var list = copyBufferSplit.ToList();
                    list.RemoveAt(0);
                    return String.Join(String.Empty, list.ToArray());
                }

                return CopyBuffer;
            }
        }
#pragma warning disable 168

        public static bool CanCopy<T>(T value)
        {
            string s;
            try
            {
                s = JsonUtility.ToJson(value, true);
            }
            catch (Exception e)
            {
                return false;
            }
            return s != "{}";
        }

        public static bool CanEditorCopy<T>(T value)
        {
            string s;
            try
            {
                s = EditorJsonUtility.ToJson(value, true);
            }
            catch (Exception e)
            {
                return false;
            }
            if (IsEditorCopyNoEntries(GetFullJSONMatchStringBuilder(s).ToString()))
            {
                return false;
            }
            return s != "{}";
        }

        private const string COPY_SPLIT = ">||>";
        private const string COPY_SPLIT_S = COPY_SPLIT + "\n";



        public static bool EditorCopy<T>(T value)
        {
            try
            {
                CopyBuffer = value.GetType() + COPY_SPLIT_S + EditorJsonUtility.ToJson(value, true);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static bool Copy<T>(T value)
        {
            try
            {
                CopyBuffer = value.GetType() + COPY_SPLIT_S + JsonUtility.ToJson(value, true);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static T Paste<T>()
        {


            var value = JsonUtility.FromJson<T>(CopyBufferNoTypeName);
            return value;
        }

        public static void Paste<T>(ref T obj) { JsonUtility.FromJsonOverwrite(CopyBufferNoTypeName, obj); }
        public static void EditorPaste<T>(ref T obj) { EditorJsonUtility.FromJsonOverwrite(CopyBufferNoTypeName, obj); }

        public static void EditorPaste<T>(T obj) { EditorJsonUtility.FromJsonOverwrite(CopyBufferNoTypeName, obj); }

        private const string NEEDLE = "\".*\":";

        static bool IsValidObjectInBuffer() { return CopyBuffer.Contains(COPY_SPLIT_S); }

        public static bool IsTypeInBuffer(Object obj)
        {
            var bufferContainsAType = IsValidObjectInBuffer();

            if (!bufferContainsAType)
                return false;
            var t = obj.GetType();

            var copyBufferSplit = CopyBuffer.Split(new string[] {COPY_SPLIT}, StringSplitOptions.None);
            //Debug.Log("Main Type: " + t + " | Copy Type: " + copyBufferSplit[0]);


            return t.ToString() == copyBufferSplit[0];
        }
#pragma warning restore 168

        private static string RemoveJSONArrayArea(string str)
        {
            while (str.Contains("[") && str.Contains("]"))
            {
                if (str.Contains("[") && str.Contains("]"))
                {
                    var start = str.IndexOf('[');
                    var end = str.IndexOf(']');
                    var count = end - start;
                    str = str.Remove(start, count + 1);
                }
            }

            return str;
        }

        private static StringBuilder GetFullJSONMatchStringBuilder(MatchCollection otherMatches)
        {
            var otherStringBuilder = new StringBuilder(1024);
            for (var index = 0; index < otherMatches.Count; index++)
            {
                otherStringBuilder.Append(otherMatches[index].Value);
            }

            return otherStringBuilder;
        }

        private static MatchCollection GetJSONFieldNames(string other)
        {
            other = RemoveJSONArrayArea(other);
            var otherMatches = Regex.Matches(other, NEEDLE);
            return otherMatches;
        }

        private static StringBuilder GetFullJSONMatchStringBuilder(string other) { return GetFullJSONMatchStringBuilder(GetJSONFieldNames(other)); }

        public static bool IsEditorCopyNoEntries(string str)
        {
            const string defualtString = "\"MonoBehaviour\":\"m_Enabled\":\"m_EditorHideFlags\":\"m_Name\":\"m_EditorClassIdentifier\":";
            return str == defualtString;
        }
    }
}