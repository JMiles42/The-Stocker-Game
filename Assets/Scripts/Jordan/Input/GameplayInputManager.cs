using System;
using System.Collections.Generic;
using System.Linq;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.InputSystem;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.Generics;
using ForestOfChaosLib.Interfaces;
using ForestOfChaosLib.UnityScriptsExtensions;
using UnityEngine;

public class GameplayInputManager: Singleton<GameplayInputManager>, IEventListening, IUpdate
{
	public AdvInputAxisReference PrimaryClick;
	public AdvInputAxisReference MiddleClick;
	public AdvInputAxisReference SecondaryClick;
	public AdvInputAxisReference ScrollWheel;
	public AdvInputAxisReference CameraMode;
	public AdvInputAxisReference PlacementMode;
	public AdvInputAxisReference EditMode;

	public List<SavedTouchData> TouchList = new List<SavedTouchData>(2);
	public List<int> TouchIndexesToRemove = new List<int>(0);
	public float TimeForAlternateTouch = 0.2f;
	public float MovementForCancelTouch = 0.1f;

	[DisableEditing] public int touchCount;

	public Vector2Reference MousePosition;
	public InputModeReference InputModeReference;
	public BoolReference GameActive;

	public static event Action<Vector2> OnPrimaryClick = (a) =>
												  {
													  //Debug.Log("Primary" + a);
												  };

	public static event Action<Vector2> OnSecondaryClick = (a) =>
													{
														//Debug.Log("Secondary" + a);
													};

	public static event Action<Vector2> OnScreenStartMove = a =>
													 {
														 //Debug.Log("Screen Moved" + a);
													 };

	public static event Action<Vector2> OnScreenMoved = a =>
												 {
													 //Debug.Log("Screen Moved" + a);
												 };

	public static event Action<Vector2> OnScreenEndMove = a =>
												   {
													   //Debug.Log("Screen Moved" + a);
												   };

	public static event Action<float> OnScreenZoom = a =>
	{
		//Debug.Log("Screen Moved" + a);
	};

	public void OnEnable()
	{
		Input.simulateMouseWithTouches = false;
		Input.backButtonLeavesApp = false;
		MovementForCancelTouch = Screen.dpi * MovementForCancelTouch;

		PrimaryClick.OnKeyDown += OnPrimaryKeyDown;
		SecondaryClick.OnKeyDown += OnSecondaryKeyDown;
		MiddleClick.OnKeyDown += OnKeyMiddleDown;
		MiddleClick.OnKeyUp += OnKeyMiddleUp;
		MiddleClick.OnKey += OnKeyMiddle;

		ScrollWheel.OnKey += OnScroll;

		CameraMode.OnKeyDown += CameraModeSwitch;
		PlacementMode.OnKeyDown += PlacementModeSwitch;
		EditMode.OnKeyDown += EditModeSwitch;

	}

	public void OnDisable()
	{
		PrimaryClick.OnKeyDown -= OnPrimaryKeyDown;
		SecondaryClick.OnKeyDown -= OnSecondaryKeyDown;
		MiddleClick.OnKeyDown -= OnKeyMiddleDown;
		MiddleClick.OnKeyUp -= OnKeyMiddleUp;
		MiddleClick.OnKey -= OnKeyMiddle;

		ScrollWheel.OnKey -= OnScroll;

		CameraMode.OnKeyDown -= CameraModeSwitch;
		PlacementMode.OnKeyDown -= PlacementModeSwitch;
		EditMode.OnKeyDown -= EditModeSwitch;
	}

	private void OnScroll(float amount) { OnScreenZoom.Trigger(amount); }

	public void Update()
	{
		MousePosition.Value = Input.mousePosition;

		if(!GameActive.Value)
			return;
		PrimaryClick.UpdateDataAndCallEvents();
		MiddleClick.UpdateDataAndCallEvents();
		SecondaryClick.UpdateDataAndCallEvents();
		ScrollWheel.UpdateDataAndCallEvents(0f);

		if ((touchCount = Input.touchCount) == 0)
		{
			TouchList.Clear();
			TouchIndexesToRemove.Clear();
			return;
		}

		for (var i = 0; i < Input.touchCount; i++)
		{
			var touch = Input.GetTouch(i);
			CheckTouches(touch);
		}
		RemoveTouches();
	}

	private void RemoveTouches()
	{
		var fingerIds = new List<int>(Input.touches.Select(t => t.fingerId));
		for (int t = Input.touchCount - 1; t >= 0; t--)
		{
			if (TouchList.InRange(t))
				if (!fingerIds.Contains(TouchList[t].FingerID) || TouchIndexesToRemove.Contains(TouchList[t].FingerID))
					TouchList.RemoveAt(t);
		}
		TouchIndexesToRemove.Clear();
	}

	private void OnPrimaryKeyDown()
	{
		if(!GameActive.Value)
			return;
		OnPrimaryClick.Trigger(Input.mousePosition.ToVector2());
	}

	private void OnSecondaryKeyDown() {
		if(!GameActive.Value)
			return;
		OnSecondaryClick.Trigger(Input.mousePosition.ToVector2()); }


	private void EditModeSwitch()
	{
		if(!GameActive.Value)
			return;
		InputModeReference.Value = InputMode.Edit;
	}

	private void PlacementModeSwitch()
	{
		if(!GameActive.Value)
			return;
		InputModeReference.Value = InputMode.Placement;
	}

	private void CameraModeSwitch()
	{
		if(!GameActive.Value)
			return;
		InputModeReference.Value = InputMode.Camera;
	}

	private Vector2 MouseStartPos;

	private Vector2 MouseDelta
	{
		get { return MousePosition.Value - MouseStartPos; }
	}

	private void OnKeyMiddleDown()
	{
		if(!GameActive.Value)
			return;
		MouseStartPos = MousePosition.Value;
		OnScreenStartMove.Trigger(MousePosition.Value);
	}

	private void OnKeyMiddleUp()
	{
		if(!GameActive.Value)
			return;
		MouseStartPos = Vector2.zero;
		OnScreenEndMove.Trigger(MousePosition.Value);
	}

	private void OnKeyMiddle(float amount)
	{
		if(!GameActive.Value)
			return;
		OnScreenMoved.Trigger(MouseDelta);
	}

	private void DoTouchPanCamera(SavedTouchData touch) {}

	private void CheckTouches(Touch touch)
	{
		switch (touch.phase)
		{
			case TouchPhase.Began:
				TouchList.Add(new SavedTouchData(touch));
				break;
			case TouchPhase.Moved:
				break;
			case TouchPhase.Stationary:
				break;
			case TouchPhase.Ended:
				for (var i = 0; i < TouchList.Count; i++)
				{
					if (TouchList[i].FingerID == touch.fingerId)
					{
						CalculateTouch(TouchList[i], touch);
						TouchIndexesToRemove.Add(i);
					}
				}
				break;
			case TouchPhase.Canceled:
				break;
		}
	}

	private void CalculateTouch(SavedTouchData data, Touch newTouch)
	{
		var resualts = data.GetTouchEndData();

		var touchLength = CalculateTouchLength(resualts.HeldTime);

		//Debug.Log("Held Time: " + resualts.HeldTime + ":" + touchLength);
		switch (touchLength)
		{
			case TouchLength.Short:
				if (TouchHasNotMoved(data, newTouch, MovementForCancelTouch))
					OnPrimaryClick.Trigger(resualts.Data.StartPos);
				break;
			case TouchLength.Long:
				if (TouchHasNotMoved(data, newTouch, MovementForCancelTouch))
					OnSecondaryClick.Trigger(resualts.Data.StartPos);
				break;
		}
	}

	private static bool TouchHasNotMoved(SavedTouchData data, Touch newTouch, float movementForCancelTouch)
	{
		var dist = Vector3.Distance(data.StartPos, newTouch.position);
		if (dist >= movementForCancelTouch)
		{
			//Debug.Log("Cancel Distance: " + dist);
			return false;
		}
		return true;
	}

	public TouchLength CalculateTouchLength(float time)
	{
		if (time >= TimeForAlternateTouch)
			return TouchLength.Long;
		return TouchLength.Short;
	}

	[Serializable]
	public class SavedTouchData: IEqualityComparer<SavedTouchData>
	{
		public int FingerID;
		public float StartTime;
		public Vector2 StartPos;

		public SavedTouchData(Touch touch)
		{
			FingerID = touch.fingerId;
			StartPos = touch.position;
			StartTime = Time.time;
		}

		public TouchEndData GetTouchEndData()
		{
			var touch = new TouchEndData {Data = this, HeldTime = Time.time - StartTime};
			return touch;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (obj is Touch)
				return Equals((Touch) obj);
			var other = obj as SavedTouchData;
			return other != null && Equals(other);
		}

		public bool Equals(SavedTouchData other) { return other.FingerID == FingerID; }
		public bool Equals(Touch other) { return other.fingerId == FingerID; }

		public override int GetHashCode() { return -2035406951 + FingerID.GetHashCode(); }

		public struct TouchEndData
		{
			public bool Over;
			public SavedTouchData Data;
			public float HeldTime;
		}

		public bool Equals(SavedTouchData x, SavedTouchData y) { return (x != null) && x.Equals(y); }
		public int GetHashCode(SavedTouchData obj) { return obj.GetHashCode(); }
	}

	[Serializable]
	public enum TouchLength
	{
		Short,
		Long
	}

	public class ScreenMoving
	{
		public Vector2 StartPos;
	}
}