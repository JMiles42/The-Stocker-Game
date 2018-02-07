using JMiles42.Editor;
using JMiles42.Editor.Utilities;
using JMiles42.Maths.Random;
using UnityEditor;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorEditor: JMilesEditorBase<MapCreator>
{
	public override void DrawGUI()
	{
		if(JMilesGUILayoutEvents.Button("Randomize Seed + Generate"))
		{
			RandomizeSeed();
			GenerateMap();
		}
		using(EditorDisposables.HorizontalScope())
		{
			if(JMilesGUILayoutEvents.Button("Randomize Seed"))
				RandomizeSeed();
			if(JMilesGUILayoutEvents.Button("Generate Map"))
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