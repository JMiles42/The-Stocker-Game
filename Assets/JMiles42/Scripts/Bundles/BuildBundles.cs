#if UNITY_EDITOR
using UnityEditor;

namespace JMiles42.Utilities
{
	public class BuildBundles
	{
		[MenuItem("Assets/Build Assest Bundles")]
		private static void BuildAssestsBundles()
		{
			BuildPipeline.BuildAssetBundles("Assests/Asset Bundles", BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.StandaloneWindows64);
		}
	}
}
#endif