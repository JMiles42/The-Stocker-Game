using System;
using UnityEngine;

namespace JMiles42.Editor
{
    public class EditorColorChanger: IDisposable
    {
        private readonly Color color;
        private readonly ColourType colourType;

        public EditorColorChanger(Color _color, ColourType _colourType = ColourType.Background)
        {
            colourType = _colourType;
            switch (colourType)
            {
                case ColourType.Background:
                    color = GUI.backgroundColor;
                    GUI.backgroundColor = _color;
                    break;

                case ColourType.Content:
                    color = GUI.contentColor;
                    GUI.contentColor = _color;
                    break;

                case ColourType.Other:
                    color = GUI.color;
                    GUI.color = _color;
                    break;
            }
        }

        public void Dispose()
        {
            switch (colourType)
            {
                case ColourType.Background:
                    GUI.backgroundColor = color;
                    break;
                case ColourType.Content:
                    GUI.contentColor = color;
                    break;
                case ColourType.Other:
                    GUI.color = color;
                    break;
            }
        }

        public enum ColourType
        {
            Background,
            Content,
            Other
        }
    }
}