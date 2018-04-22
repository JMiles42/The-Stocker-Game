namespace ForestOfChaosLib.MenuManaging
{
	public class OpenMenuFromPrefabRefStart: FoCsBehavior
	{
		public enum Mode
		{
			OnEnable,
			Start,
			Awake
		}

		public Menu MenuToOpen;
		public Mode OpenMode = Mode.OnEnable;

		public void Open()
		{
			MenuToOpen.OpenByRef();
		}

		private void OnEnable()
		{
			if(OpenMode == Mode.OnEnable)
				Open();
		}

		private void Awake()
		{
			if(OpenMode == Mode.Awake)
				Open();
		}

		private void Start()
		{
			if(OpenMode == Mode.Start)
				Open();
		}
	}
}