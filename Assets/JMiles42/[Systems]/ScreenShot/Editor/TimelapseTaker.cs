using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

//TODO: Move this to usable in gameplay, by making a non editor script, that this calls
namespace JMiles42.Systems.Screenshot
{
    [ExecuteInEditMode]
    public class TimelapseTaker: Tab<ScreenCapWindow>
    {
        private int HighResWidth = 1920;
        private int HighResHeight = 1080;
        private int scale = 1;
        private float waitTime = 1f;
        private int times = 5;

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
            get { return "Timelapse"; }
        }

        public override void DrawTab(Window<ScreenCapWindow> owner)
        {
#if UNITY_EDITOR
            path = defaultPath = Application.streamingAssetsPath + @"/../../Screenshots";
#else
            path = defaultPath = Application.persistentDataPath + @"/Screenshots";
#endif

            EditorGUILayout.
                    LabelField("Please note: This window will spawn a Gameobject in the scene, with the name \"Timelapse_OBJ\", if this object is removed the Timelapse will stop.",
                               EditorStyles.boldLabel);

            if (path == "")
            {
                path = defaultPath = Application.streamingAssetsPath + @"/../../../Screenshots";
            }
            //Horizontal Scope
            ////An Indented way of using Unitys Scopes
            using (new GUILayout.HorizontalScope())
            {
                //Vertical Scope
                ////An Indented way of using Unitys Scopes
                using (new GUILayout.VerticalScope(GUI.skin.box, GUILayout.Width(owner.position.width * 0.5f)))
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
                    EditorGUILayout.Space();
                    DrawTimelapesOptionsGUI();
                    EditorGUILayout.Space();
                    DisplayTimelapseData(Timelapse);
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
                if (GUILayout.Button("Default", GUILayout.Width(30)))
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
            //Disabled Scope
            ////An Indented way of using Unitys Scopes
            using (new EditorGUI.DisabledGroupScope(!Application.isPlaying))
            {
                if (GUILayout.Button("Start Timelapse", GUILayout.MinHeight(40)))
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
            }

            if (GUILayout.Button("Open Folder", GUILayout.MaxWidth(100), GUILayout.MinHeight(40)))
            {
                Application.OpenURL("file://" + path);
            }
        }

        private void DrawTimelapesOptionsGUI()
        {
            EditorGUILayout.LabelField("Timelapse Options", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("Time Between Pictures (In seconds)");
            waitTime = EditorGUILayout.FloatField("", waitTime);

            EditorGUILayout.LabelField("Total Amount of pictures (Set to 0 for untill you exit playmode)");
            times = EditorGUILayout.IntField("", times);
        }

        private static void DisplayTimelapseData(Timelapse timelapse)
        {
            if (timelapse == null)
            {
                EditorGUILayout.LabelField(new GUIContent("No Timelapse Active"), GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.Height(32));
                return;
            }
            if (timelapse.TimeRemaining.Second == 0)
            {
                EditorGUILayout.LabelField(new GUIContent("Timelapse Has Finished"), GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.Height(32));

                EditorGUILayout.LabelField(new GUIContent(timelapse.ShootsTaken.ToString("Total Pictures Taken : 0")),
                                           GUI.skin.box,
                                           GUILayout.ExpandWidth(true),
                                           GUILayout.Height(32));

                return;
            }

            EditorGUILayout.LabelField(new GUIContent(string.Format("Seconds Left: {0}", timelapse.TimeRemaining.Second)),
                                       GUI.skin.box,
                                       GUILayout.ExpandWidth(true),
                                       GUILayout.Height(32));
            EditorGUILayout.LabelField(new GUIContent(timelapse.ShootsTaken.ToString("Pictures Taken : 0")),
                                       GUI.skin.box,
                                       GUILayout.ExpandWidth(true),
                                       GUILayout.Height(32));
        }

        private Timelapse Timelapse;

        private void TakeScreenShot()
        {
            var args = new ScreenShotArgs {FrameWidth = HighResWidth, FrameHeight = HighResHeight, Path = path, ClearBackground = isTransparent};
            if (myCamera == null)
                myCamera = Camera.main;
            if (myCamera == null)
                myCamera = Object.FindObjectOfType<Camera>();

            args.Cam = myCamera;

            Timelapse = new Timelapse(waitTime, times, args.GetScaledArg(scale));
        }
    }
}