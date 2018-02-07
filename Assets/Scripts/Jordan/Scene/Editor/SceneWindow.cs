using JMiles42.Editor.Windows;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWindow: Window<SceneWindow>
{
	private const string Title = "Scene Window";

	[MenuItem("Window/" + Title)]
	private static void Init()
	{
		GetWindow();
		window.titleContent.text = Title;
	}

	protected override void DrawGUI()
	{
		using(new GUILayout.VerticalScope(GUI.skin.box))
			DrawLoadedScenes();
		using(new GUILayout.VerticalScope(GUI.skin.box))
			DrawBuildScenes();
	}

	private void DrawLoadedScenes()
	{
		EditorGUILayout.LabelField("Loaded Scenes");
		var scenes = SceneManager.sceneCount;
		for(var i = 0; i < scenes; i++)
		{
			var scene = SceneManager.GetSceneAt(i);
			DrawScene(scene);
		}
	}

	private void DrawBuildScenes()
	{
		EditorGUILayout.LabelField("Build Settings Scenes");
		var Scenes = EditorBuildSettings.scenes;

		foreach(var scene in Scenes)
		{
			DrawScene(scene.path);
		}
	}

	private void DrawScene(string path)
	{
		DrawScene(SceneManager.GetSceneByPath(path));
	}

	private void DrawScene(Scene scene)
	{
		using(new GUILayout.VerticalScope(GUI.skin.box))
		{
			using(new GUILayout.HorizontalScope())
			{
				EditorGUILayout.LabelField("Path:");
				EditorGUILayout.LabelField(scene.path);
			}
			using(new GUILayout.HorizontalScope())
			{
				EditorGUILayout.LabelField("Name:");
				EditorGUILayout.LabelField(scene.name);
			}

			using(new GUILayout.HorizontalScope())
			{
				EditorGUILayout.LabelField("Build Index");
				EditorGUILayout.LabelField(scene.buildIndex.ToString());
			}
			using(new GUILayout.HorizontalScope())

			{
				EditorGUILayout.LabelField("Root Count");
				EditorGUILayout.LabelField(scene.rootCount.ToString());
			}
		}
	}
}