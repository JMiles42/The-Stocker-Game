using System;
using JMiles42.Extensions;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Editor
{
    public abstract class TabedWindow<T>: Window<T> where T: EditorWindow
    {
        public int activeTab = 0;

        public abstract Tab<T>[] Tabs{ get; }

        public TitleBarPos TitleBarPosition = TitleBarPos.Top;
        public bool TitleBarScrollable = false;

        private Vector2 scrolPos = Vector2.zero;

        public float TitleBarLabelWidth = 100;
        public float TitleBarLabelWidthTotal => TitleBarScrollable? TitleBarLabelWidth + 20 : TitleBarLabelWidth;

        protected override void DrawGUI()
        {
            switch (TitleBarPosition)
            {
                case TitleBarPos.Top:
                    DrawTop();
                    break;
                case TitleBarPos.Buttom:
                    DrawButtom();
                    break;
                case TitleBarPos.Left:
                    DrawLeft();
                    break;
                case TitleBarPos.Right:
                    DrawRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawActiveTab()
        {
            if (!Tabs.IsNullOrEmpty())
                Tabs[activeTab].DrawTab(this);
        }

        private void DrawHeaderGUI()
        {
            for (int i = 0; i < Tabs.Length; i++)
            {
                if (GUILayout.Toggle(i == activeTab, Tabs[i].TabName, EditorStyles.toolbarButton, GUILayout.MinWidth(TitleBarLabelWidth)))
                    activeTab = i;
            }
        }

        private void DrawTop()
        {
            if (TitleBarScrollable)
            {
                using (new GUILayout.VerticalScope())
                {
                    using (var scrollViewScope =
                            new EditorGUILayout.ScrollViewScope(scrolPos, GUILayout.ExpandWidth(true), GUILayout.Height(33)) {handleScrollWheel = true})
                    {
                        scrolPos = scrollViewScope.scrollPosition;
                        using (new GUILayout.HorizontalScope(EditorStyles.toolbar, GUILayout.ExpandWidth(true)))
                            DrawHeaderGUI();
                    }
                    using (new GUILayout.VerticalScope(GUILayout.ExpandHeight(true)))
                        DrawActiveTab();
                }
            }
            else
            {
                using (new GUILayout.VerticalScope())
                {
                    using (new GUILayout.HorizontalScope(EditorStyles.toolbar, GUILayout.ExpandWidth(true)))
                        DrawHeaderGUI();
                    using (new GUILayout.VerticalScope(GUILayout.ExpandHeight(true)))
                        DrawActiveTab();
                }
            }
        }

        private void DrawButtom()
        {
            if (TitleBarScrollable)
            {
                using (new GUILayout.VerticalScope(GUILayout.ExpandHeight(true)))
                    DrawActiveTab();
                using (new GUILayout.VerticalScope())
                {
                    using (var scrollViewScope =
                            new EditorGUILayout.ScrollViewScope(scrolPos, GUILayout.ExpandWidth(true), GUILayout.Height(33)) {handleScrollWheel = true})
                    {
                        scrolPos = scrollViewScope.scrollPosition;
                        using (new GUILayout.HorizontalScope(EditorStyles.toolbar, GUILayout.ExpandWidth(true)))
                            DrawHeaderGUI();
                    }
                }
            }
            else
            {
                using (new GUILayout.VerticalScope())
                {
                    using (new GUILayout.VerticalScope(GUILayout.ExpandHeight(true)))
                        DrawActiveTab();
                    using (new GUILayout.HorizontalScope(EditorStyles.toolbar, GUILayout.ExpandWidth(true)))
                        DrawHeaderGUI();
                }
            }
        }

        private void DrawLeft()
        {
            if (TitleBarScrollable)
            {
                using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
                {
                    using (var scrollViewScope =
                            new EditorGUILayout.ScrollViewScope(scrolPos,
                                                                GUILayout.ExpandHeight(true),
                                                                GUILayout.Width(TitleBarLabelWidthTotal)) {handleScrollWheel = true})
                    {
                        using (new GUILayout.VerticalScope(GUILayout.Width(TitleBarLabelWidth)))
                        {
                            scrolPos = scrollViewScope.scrollPosition;
                            DrawHeaderGUI();
                        }
                    }
                    DrawActiveTab();
                }
            }
            else
            {
                using (new GUILayout.HorizontalScope())
                {
                    using (new GUILayout.VerticalScope(EditorStyles.toolbar, GUILayout.Width(TitleBarLabelWidth)))
                        DrawHeaderGUI();
                    using (new GUILayout.VerticalScope(GUILayout.ExpandWidth(true)))
                        DrawActiveTab();
                }
            }
        }

        private void DrawRight()
        {
            if (TitleBarScrollable)
            {
                using (new GUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
                {
                    DrawActiveTab();

                    using (var scrollViewScope =
                            new EditorGUILayout.ScrollViewScope(scrolPos,
                                                                GUILayout.ExpandHeight(true),
                                                                GUILayout.Width(TitleBarLabelWidthTotal)) {handleScrollWheel = true})
                    {
                        using (new GUILayout.VerticalScope(GUILayout.Width(TitleBarLabelWidth)))
                        {
                            scrolPos = scrollViewScope.scrollPosition;
                            DrawHeaderGUI();
                        }
                    }
                }
            }
            else
            {
                using (new GUILayout.HorizontalScope())
                {
                    using (new GUILayout.VerticalScope(GUILayout.ExpandWidth(true)))
                        DrawActiveTab();
                    using (new GUILayout.VerticalScope(EditorStyles.toolbar, GUILayout.Width(TitleBarLabelWidth)))
                        DrawHeaderGUI();
                }
            }
        }

        public enum TitleBarPos
        {
            Top,
            Buttom,
            Left,
            Right
        }
    }
}