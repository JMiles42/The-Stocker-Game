using ForestOfChaosLib.Editor.Utilities;
using ForestOfChaosLib.Editor.Windows;
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
		using(FoCsEditorDisposables.VerticalScope(GUI.skin.box))
			DrawLoadedScenes();
		using(FoCsEditorDisposables.VerticalScope(GUI.skin.box))
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
		using(FoCsEditorDisposables.VerticalScope(GUI.skin.box))
		{
			using(FoCsEditorDisposables.HorizontalScope())
			{
				EditorGUILayout.LabelField("Path:");
				EditorGUILayout.LabelField(scene.path);
			}
			using(FoCsEditorDisposables.HorizontalScope())
			{
				EditorGUILayout.LabelField("Name:");
				EditorGUILayout.LabelField(scene.name);
			}

			using(FoCsEditorDisposables.HorizontalScope())
			{
				EditorGUILayout.LabelField("Build Index");
				EditorGUILayout.LabelField(scene.buildIndex.ToString());
			}
			using(FoCsEditorDisposables.HorizontalScope())

			{
				EditorGUILayout.LabelField("Root Count");
				EditorGUILayout.LabelField(scene.rootCount.ToString());
			}
		}
	}
}