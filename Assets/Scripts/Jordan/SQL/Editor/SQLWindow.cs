using System.Collections;
using System.Collections.Generic;
using JMiles42.Editor;
using UnityEditor;
using UnityEngine;




//pass: fCsZs4f~Ak~V
public class SQLWindow: Window<SQLWindow>
{
	private const string Title = "SQLWindow";

	[MenuItem("JMiles42/" + Title)]
	private static void Init()
	{
		GetWindow();
		window.titleContent.text = Title;
	}
	protected override void DrawGUI()
	{

	}
}