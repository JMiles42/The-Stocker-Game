using JMiles42.Editor;

namespace JMiles42.Systems.Localization
{
    public class LocalizationWindowAudio: Tab<LocalizationWindow>
    {
        public override string TabName
        {
            get { return "Audio"; }
        }

        public override void DrawTab(Window<LocalizationWindow> owner) {}
    }
}