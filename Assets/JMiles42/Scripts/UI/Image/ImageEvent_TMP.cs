#if TextMeshPro_DEFINE 
using JMiles42.Attributes;
using JMiles42.UI;
using UnityEngine;
using UnityEngine.UI;

namespace JMiles42.Events.UI
{
	public class ImageEvent_TMP: ImageEventBase
	{
		[NoFoldout(true)] public ImageText_TMP myImage;

		public override Image Image
		{
			get { return myImage.Image; }
		}

		public override string ImageText
		{
			get { return myImage.Text.text; }
			set { myImage.Text.text = value; }
		}

		public override GameObject ImageGO
		{
			get { return Image.gameObject; }
		}

		public override GameObject TextGO
		{
			get { return myImage.Text.gameObject; }
		}
	}
}
#endif