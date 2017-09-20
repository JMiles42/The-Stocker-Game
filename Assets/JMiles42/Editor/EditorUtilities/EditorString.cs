namespace JMiles42.Editor
{
	public class EditorString
	{
		public readonly string String;
		public readonly float Length;

		public EditorString(string str)
		{
			String = str;
			Length = EditorHelpers.GetStringLengthinPix(String);
		}

		public static implicit operator float(EditorString editorString) { return editorString.Length; }

		public static implicit operator string(EditorString editorString) { return editorString.String; }
	}
}