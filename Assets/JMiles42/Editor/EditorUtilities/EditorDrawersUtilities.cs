using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
    public class EditorDrawersUtilities
    {
        public static void DrawProperty(Rect position, EditorEntry serializedProperty)
        {
            EditorGUI.SelectableLabel(new Rect(position.x, position.y, serializedProperty, position.height), (serializedProperty));
            EditorGUI.PropertyField(new Rect(position.x + serializedProperty, position.y, position.width - serializedProperty, position.height),
                                    serializedProperty,
                                    GUIContent.none);
        }

        public static void DrawPropertyWidthIndexed(
            Rect position,
            GUIContent label,
            SerializedProperty property,
            int totalAmount,
            int index,
            bool expandToWidth = false,
            bool onMultipuleLines = true)
        {
            var prop = new EditorEntry(label.text, property);
            prop.Draw(GetRectWidthIndexed(position, totalAmount, index, expandToWidth, onMultipuleLines));
        }

        public static Rect GetRectWidthIndexed(Rect position, int totalAmount, int index, bool expandToWidth = false, bool onMultipuleLines = true)
        {
            position.width = position.width / totalAmount;

            if (index == 0)
            {
                return position;
            }
            if (onMultipuleLines)
            {
                position.y -= (EditorGUIUtility.singleLineHeight + (2 * index));
            }
            position.height = EditorGUIUtility.singleLineHeight;
            position.x += (position.width) * index;
            if (expandToWidth)
            {
                position.width = (position.width * (index + 1));
            }
            return position;
        }

        public static void DrawSplitProgressBar(Rect pos, float value, string name = "", bool isPosativeLeft = true)
        {
            var leftPos = pos;
            leftPos.width *= 0.5f;
            var rightPos = leftPos;
            rightPos.x += leftPos.width;
            leftPos.x += leftPos.width;
            leftPos.width *= -1;

            float leftValue = 0;
            float rightValue = 0;

            if (isPosativeLeft)
            {
                if (value >= 0)
                {
                    leftValue = value;
                    rightValue = 0;
                }
                else
                {
                    leftValue = 0;
                    rightValue = -value;
                }
            }
            else
            {
                if (value <= 0)
                {
                    leftValue = -value;
                    rightValue = 0;
                }
                else
                {
                    leftValue = 0;
                    rightValue = value;
                }
            }

            EditorGUI.ProgressBar(leftPos, +leftValue, "");
            EditorGUI.ProgressBar(rightPos, +rightValue, "");
        }
    }
}