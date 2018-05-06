using ForestOfChaosLib.Editor;
using ForestOfChaosLib.Editor.ImGUI;
using ForestOfChaosLib.Editor.Utilities;
using ForestOfChaosLib.Maths.Random;
using UnityEditor;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorEditor: FoCsEditor<MapCreator>
{
	public override void DrawGUI()
	{
		if(FoCsGUILayout.Button("Randomize Seed + Generate"))
		{
			RandomizeSeed();
			GenerateMap();
		}
		using(FoCsEditorDisposables.HorizontalScope())
		{
			if(FoCsGUILayout.Button("Randomize Seed"))
				RandomizeSeed();
			if(FoCsGUILayout.Button("Generate Map"))
				GenerateMap();
		}
	}

	private void RandomizeSeed()
	{
		Target.MapSettings.Data.Seed = RandomStrings.GetRandomString(8);
		EditorUtility.SetDirty(Target.MapSettings);
	}

	private void GenerateMap()
	{
		Target.GenerateMap();
		EditorUtility.SetDirty(Target.Map);
	}
}