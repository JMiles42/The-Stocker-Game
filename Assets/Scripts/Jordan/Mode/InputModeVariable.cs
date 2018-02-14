using System;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.Base;
using UnityEngine;

[Serializable]
[AdvFolderName("Stocker")]
[CreateAssetMenu]
public class InputModeReference: AdvReference<InputMode>
{ }

[Serializable]
public class InputModeVariable: AdvVariable<InputMode, InputModeReference>
{
	public static implicit operator InputModeVariable(InputMode input)
	{
		var fR = new InputModeVariable
				 {
					 UseConstant = true
				 };
		fR.Value = input;

		return fR;
	}
}