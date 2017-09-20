using System;
using System.IO;
using UnityEngine;

namespace JMiles42.Systems.Screenshot
{
    public static class Screenshot
    {
        public static void TakeScreenShot() { TakeScreenShot(ScreenShotArgs.GetMainCam()); }

        public static void TakeScreenShot(params ScreenShotArgs[] screenShotArgs)
        {
            foreach (var args in screenShotArgs)
            {
                TakeScreenShot(args);
            }
        }

        public static void TakeScreenShot(ScreenShotArgs screenShotArgs)
        {
            CreateFolder(screenShotArgs.Path);

            var rt = new RenderTexture(screenShotArgs.FrameWidth, screenShotArgs.FrameHeight, 24);
            screenShotArgs.Cam.targetTexture = rt;

            var tFormat = screenShotArgs.ClearBackground? TextureFormat.ARGB32 : TextureFormat.RGB24;

            var screenShot = new Texture2D(screenShotArgs.FrameWidth, screenShotArgs.FrameHeight, tFormat, false);
            screenShotArgs.Cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, screenShotArgs.FrameWidth, screenShotArgs.FrameHeight), 0, 0);
            screenShotArgs.Cam.targetTexture = null;
            RenderTexture.active = null;
            var bytes = screenShot.EncodeToPNG();
            var filename = screenShotArgs.GetFullFileName();

            File.WriteAllBytes(filename, bytes);
        }

        private static void CreateFolder(string path) { Directory.CreateDirectory(path); }
    }

    public class ScreenShotArgs
    {
        public Camera Cam = null;
        public int FrameHeight = 1080;
        public int FrameWidth = 1920;
        public bool ClearBackground = false;
        public string fileName = "";

#if UNITY_EDITOR
        public string Path = Application.streamingAssetsPath + @"/../../../Screenshots";
#else
            path
		public string Path = Application.persistentDataPath + @"/Screenshots";
#endif

        public ScreenShotArgs() {}

        public ScreenShotArgs(ScreenShotArgs other)
        {
            Cam = other.Cam;
            FrameHeight = other.FrameHeight;
            FrameWidth = other.FrameWidth;
            ClearBackground = other.ClearBackground;
            Path = other.Path;
        }

        public ScreenShotArgs(Camera cam, int frameHeight = 1080, int frameWidth = 1920, bool clearBackground = false)
        {
            Cam = cam;
            FrameHeight = frameHeight;
            FrameWidth = frameWidth;
            ClearBackground = clearBackground;
        }

        public static ScreenShotArgs GetMainCam()
        {
            var screenShotArgs = new ScreenShotArgs {Cam = Camera.main};
            return screenShotArgs;
        }

        public static ScreenShotArgs GetCurrentCam()
        {
            var screenShotArgs = new ScreenShotArgs {Cam = Camera.current};
            return screenShotArgs;
        }

        public ScreenShotArgs GetScaledArg(int amount)
        {
            var shotArgs = new ScreenShotArgs(this);
            shotArgs.FrameWidth *= amount;
            shotArgs.FrameHeight *= amount;
            return shotArgs;
        }

        public virtual string GetFullFileName()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                var strPath = "";

                strPath = string.Format("{0}/Screenshot[{3:yyyy-MM-dd(hh-mm-ss)}][{1}x{2}]px.png", Path, FrameWidth, FrameHeight, DateTime.Now);

                return strPath;
            }
            return fileName;
        }
    }
}