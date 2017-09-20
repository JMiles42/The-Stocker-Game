using JMiles42.Attributes;
using JMiles42.UI;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	public class ButtonClickEvent: ButtonClickEventBase
	{
		[NoFoldout(true)] public ButtonText myButton;

		public override Button Button
		{
			get { return myButton.Btn; }
		}

		public override string ButtonText
		{
			get { return myButton.Text.text; }
			set { myButton.Text.text = value; }
		}

		public override GameObject ButtonGO
		{
			get { return Button.gameObject; }
		}

		public override GameObject TextGO
		{
			get { return myButton.Text.gameObject; }
		}
	}
}