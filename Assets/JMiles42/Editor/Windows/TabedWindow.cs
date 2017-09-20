using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
    public abstract class TabedWindow<T>: Window<T> where T: EditorWindow
    {
        public int activeTab = 0;

        public abstract Tab<T>[] Tabs{ get; }


        protected override void DrawGUI()
        {
            DrawMainHeaderGUI();
            Tabs[activeTab].DrawTab(this);
        }

        private void DrawMainHeaderGUI()
        {
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                for (int i = 0; i < Tabs.Length; i++)
                {
                    if (GUILayout.Toggle(i == activeTab, Tabs[i].TabName, EditorStyles.toolbarButton))
                        activeTab = i;
                }
            }
        }
    }
}