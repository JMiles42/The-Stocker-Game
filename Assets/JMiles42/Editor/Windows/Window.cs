using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace JMiles42.Editor
{
    /// <summary>
    /// [MenuItem("Tools/JMiles42/Example Window")]
    /// private static void Init(){
    /// GetWindow();
    /// }
    /// </summary>
    /// <typeparam name="T">Class name of type that inherets directly from this class, for a static ref to its self</typeparam>
    public abstract class Window<T>: EditorWindow where T: EditorWindow
    {
        protected static T window;

        protected static EditorWindow GetWindow()
        {
            // Get existing open window or if none, make a new one:
            return window = GetWindow<T>();
        }

        protected virtual void OnGUI()
        {
            if (window == null)
                GetWindow();

            DrawGUI();
        }

        protected static void DrawReorderableList(ReorderableList list)
        {
            list.DoLayoutList();
        }

        protected abstract void DrawGUI();

        protected virtual void Update()
        {
            //Repaint();
        }

        public static void DrawSpace() { EditorGUILayout.Space(); }

        public static void DrawSpace(int count)
        {
            for (int i = 0; i < count; i++)
            {
                EditorGUILayout.Space();
            }
        }

        public static void DrawSpace(float size) { GUILayout.Space(size); }
    }
}