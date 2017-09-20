using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.Localization
{
    public class LocalizationWindow: TabedWindow<LocalizationWindow>
    {
        private readonly Tab<LocalizationWindow>[] tabs =
        {
            new LocalizationWindowLanguages(),
            new LocalizationWindowStrings(),
            //new LocalizationWindowInfo(),
            //new LocalizationWindowTexturePage(),
            //new LocalizationWindowAudio(),
            new LocalizationWindowHelp()
        };

        public override Tab<LocalizationWindow>[] Tabs
        {
            get { return tabs; }
        }

        [MenuItem("Tools/Localization Window")]
        private static void Init()
        {
            GetWindow();
            window.titleContent = new GUIContent("Localization Window");
        }
    }
}