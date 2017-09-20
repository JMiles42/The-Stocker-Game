using System;
using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

//TODO: Move this to usable in gameplay, by making a non editor script, that this calls
namespace JMiles42.Systems.Screenshot
{
    [ExecuteInEditMode]
    public class ScreenshotTaker: Tab<ScreenCapWindow>
    {
        private int HighResWidth = 1920;
        private int HighResHeight = 1080;
        private int scale = 1;

        public Camera myCamera;
#if UNITY_EDITOR
        private static string defaultPath = "";
#else
        private static readonly string defaultPath = "";
#endif
        private static string path = "";

        private bool isTransparent;

        public override string TabName
        {
            get { return "Screenshot"; }
        }

        public override void DrawTab(Window<ScreenCapWindow> owner)
        {
#if UNITY_EDITOR
            path = defaultPath = Application.streamingAssetsPath + @"/../../Screenshots";
#else
            path = defaultPath = Application.persistentDataPath + @"/Screenshots";
#endif

            if (path == "")
            {
                path = defaultPath = Application.streamingAssetsPath + @"/../../Screenshots";
            }
            //Horizontal Scope
            ////An Indented way of using Unitys Scopes
            using (new GUILayout.HorizontalScope())
            {
                //Vertical Scope
                ////An Indented way of using Unitys Scopes
                using (new GUILayout.VerticalScope(GUI.skin.box, GUILayout.Width(owner.position.width * 0.49f)))
                {
                    DrawResolutionGUI();
                    DrawFilePathGUI();

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField(string.Format("Screenshot will be taken at {0} x {1} px", HighResWidth * scale, HighResHeight * scale),
                                               EditorStyles.boldLabel);
                    //Horizontal Scope
                    ////An Indented way of using Unitys Scopes
                    using (new GUILayout.HorizontalScope())
                    {
                        DrawTakeImageGUI();
                    }
                }
                //Vertical Scope
                ////An Indented way of using Unitys Scopes
                using (new GUILayout.VerticalScope(GUI.skin.box))
                {
                    DrawCameraOptionsGUI();
                }
            }
        }

        private static void DrawFilePathGUI()
        {
            //Horizontal Scope
            ////An Indented way of using Unitys Scopes
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Save Path", EditorStyles.boldLabel);
                GUILayout.Label("By Defualt the \"../../\" will put it at the projects root folder next to the Assests folder");
            }

            EditorGUILayout.TextField(path, GUILayout.ExpandWidth(true));
            //Horizontal Scope
            ////An Indented way of using Unitys Scopes
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Browse"))
                    path = EditorUtility.SaveFolderPanel("Path to Save Images", path, Application.dataPath);
                if (GUILayout.Button("Default", GUILayout.Width(40)))
                    path = defaultPath;
            }
        }

        private void DrawCameraOptionsGUI()
        {
            GUILayout.Label("Select Camera", EditorStyles.boldLabel);

            myCamera = EditorGUILayout.ObjectField(myCamera, typeof (Camera), true, null) as Camera ?? Camera.main;

            isTransparent = EditorGUILayout.Toggle("Transparent Background", isTransparent);
        }

        private void DrawResolutionGUI()
        {
            EditorGUILayout.LabelField("Resolution", EditorStyles.boldLabel);
            HighResWidth = EditorGUILayout.IntField("Width", HighResWidth);
            HighResHeight = EditorGUILayout.IntField("Height", HighResHeight);
            DrawImageSizeGUI();
            scale = EditorGUILayout.IntSlider("Scale", scale, 1, 16);
        }

        private void DrawImageSizeGUI()
        {
            //Horizontal Scope
            ////An Indented way of using Unitys Scopes
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Set To Screen Size"))
                {
                    HighResHeight = (int) Handles.GetMainGameViewSize().y;
                    HighResWidth = (int) Handles.GetMainGameViewSize().x;
                }

                if (GUILayout.Button("Default Size"))
                {
                    HighResHeight = 1080;
                    HighResWidth = 1920;
                    scale = 1;
                }
            }
        }

        private void DrawTakeImageGUI()
        {
            if (GUILayout.Button("Take Screenshot", GUILayout.MinHeight(40)))
            {
                if (path == "")
                {
                    path = EditorUtility.SaveFolderPanel("Path to Save Images", path, Application.persistentDataPath);
                    Debug.Log("Path Set");
                    TakeScreenShot();
                }
                else
                {
                    TakeScreenShot();
                }
            }

            if (GUILayout.Button("Open Folder", GUILayout.MaxWidth(100), GUILayout.MinHeight(40)))
            {
                Application.OpenURL("file://" + path);
            }
        }

        private void TakeScreenShot()
        {
            var args = new ScreenShotArgs {FrameWidth = HighResWidth, FrameHeight = HighResHeight, Path = path, ClearBackground = isTransparent};
            if (myCamera == null)
                myCamera = Camera.main;
            if (myCamera == null)
                myCamera = UnityEngine.Object.FindObjectOfType<Camera>();

            args.Cam = myCamera;

            if (scale > 1)
            {
                Screenshot.TakeScreenShot(args, args.GetScaledArg(scale));
            }
            else
            {
                Screenshot.TakeScreenShot(args);
            }
        }
    }

    [Serializable]
    public class ScreenShotSettings
    {
        private readonly int resWidth;
        private readonly int resHeight;

        public Camera myCamera;
        private readonly int scale;

        public ScreenShotSettings()
        {
            resWidth = 1920;
            resHeight = 1080;
            scale = 2;
            myCamera = Camera.main;
        }

        public ScreenShotSettings(int _resWidth, int _resHeight, int _scale, Camera _cam)
        {
            resWidth = _resWidth;
            resHeight = _resHeight;
            scale = _scale;
            myCamera = _cam;
        }

        public int GetWidth(bool withScale = false)
        {
            if (withScale)
                return resWidth * scale;
            return resWidth;
        }

        public int GetHeight(bool withScale = false)
        {
            if (withScale)
                return resHeight * scale;
            return resHeight;
        }
    }
}