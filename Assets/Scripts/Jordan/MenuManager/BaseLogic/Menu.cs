using JMiles42.AdvancedVariables;
using JMiles42.Components;
using JMiles42.Extensions;
using UnityEngine;

namespace JMiles42.Systems.MenuManager
{
	public abstract class Menu<T>: Menu where T: Menu<T>
	{
		protected static Menu<T> instance;

		public static Menu<T> Instance
		{
			get
			{
				if(!instance.IsNull())
					return instance;
				instance = (T)FindObjectOfType(typeof(T));
				//if (!instance)
				//	Debug.LogError(typeof (T) + " is needed in the scene."); //Print error
				return instance;
			}
			private set { instance = value; }
		}

		protected virtual void Awake()
		{
			instance = this;
		}

		protected virtual void OnDestroy()
		{
			Instance = null;
		}

		protected static void Open()
		{
			if(Instance == null)
				MenuManager.Instance.CreateInstance<T>();

			MenuManager.Instance.OpenMenu(Instance);
		}

		public virtual void OnEnable()
		{
			Open();
		}

		public virtual void OnDisable()
		{ }

		protected static void Close()
		{
			if(Instance == null)
			{
				Debug.LogErrorFormat("Trying to close menu {0} but Instance is null", typeof(T));
				return;
			}

			MenuManager.Instance.CloseMenu(Instance);
		}

		public override void OnBackPressed()
		{
			Close();
		}
	}

	public abstract class Menu: JMilesBehavior
	{
		[Tooltip("Destroy the Game Object when menu is closed (reduces memory usage)")]
		public BoolReference DestroyWhenClosed;

		[Tooltip("Disable menus that are under this one in the stack")]
		public BoolReference DisableMenusUnderneath;

		[Tooltip("Ignore The Disable Menus Underneath option")]
		public BoolReference StayActiveUnderneath;

		public abstract void OnBackPressed();

		public static void MenuManagerGoBack()
		{
			MenuManager.OnBackPressed();
		}

		public void GoBack()
		{
			MenuManager.OnBackPressed();
		}

		public static bool HasInstance { get; protected set; }
	}
}