using System;
using JMiles42.AdvVar;
using JMiles42.AdvVar.Base;
using UnityEngine;

[Serializable]
[AdvFolderName("Stocker", 0)]
[CreateAssetMenu]
public class InputModeVariable: AdvVariable<InputMode>
{ }

[Serializable]
public class InputModeReference: AdvReference<InputMode, InputModeVariable>
{
	public static implicit operator InputModeReference(InputMode input)
	{
		var fR = new InputModeReference
				 {
					 UseConstant = true
				 };
		fR.Value = input;

		return fR;
	}
}