using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JMiles42.Attributes;
using JMiles42.Extensions;
using JMiles42.Generics;
using JMiles42.Systems.InputManager;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class GameplayInputManager: Singleton<GameplayInputManager>, IEventListening, IUpdate {
	public InputAxis PrimaryClick = "MouseL";
	public InputAxis MiddleClick = "MouseM";
	public InputAxis SecondaryClick = "MouseR";

	public List<SavedTouchData> TouchList = new List<SavedTouchData>(2);
	public List<int> TouchIndexesToRemove = new List<int>(0);
	public float TimeForAlternateTouch = 0.2f;
	public float MovementForCancelTouch = 0.1f;

	[DisableEditing] public int touchCount;

	public event Action<Vector2> OnPrimaryClick = (a) => {
													  //Debug.Log("Primary" + a);
												  };

	public event Action<Vector2> OnSecondaryClick = (a) => {
														//Debug.Log("Secondary" + a);
													};

	public event Action<Vector2> OnScreenMoved = a => {
													 //Debug.Log("Screen Moved" + a);
												 };

	public void OnEnable() {
		Input.simulateMouseWithTouches = false;
		Input.backButtonLeavesApp = false;
		MovementForCancelTouch = Screen.dpi * MovementForCancelTouch;

		PrimaryClick.onKeyDown += OnPrimaryKeyDown;
		SecondaryClick.onKeyDown += OnSecondaryKeyDown;
		MiddleClick.onKeyDown += OnKeyMiddleDown;
		MiddleClick.onKeyUp += OnKeyMiddleUp;
		MiddleClick.onKey += OnKeyMiddle;
	}

	public void Update() {
		PrimaryClick.DoInput();
		SecondaryClick.DoInput();
		if ((touchCount = Input.touchCount) == 0) {
			TouchList.Clear();
			TouchIndexesToRemove.Clear();
			return;
		}

		for (var i = 0; i < Input.touchCount; i++) {
			var touch = Input.GetTouch(i);
			CheckTouches(touch);
		}
		RemoveTouches();
	}

	private void RemoveTouches() {
		var fingerIds = new List<int>(Input.touches.Select(t => t.fingerId));
		for (int t = Input.touchCount - 1; t >= 0; t--) {
			if (!fingerIds.Contains(TouchList[t].FingerID) || TouchIndexesToRemove.Contains(TouchList[t].FingerID))
				TouchList.RemoveAt(t);
		}
		TouchIndexesToRemove.Clear();
	}

	private void OnPrimaryKeyDown() { OnPrimaryClick.Trigger(Input.mousePosition); }

	private void OnSecondaryKeyDown() { OnSecondaryClick.Trigger(Input.mousePosition); }

	private void OnKeyMiddleDown() {}

	private void OnKeyMiddleUp() {}

	private void OnKeyMiddle() {}

	private void DoTouchPanCamera(SavedTouchData touch) {}

	public void OnDisable() {
		PrimaryClick.onKeyDown -= OnPrimaryKeyDown;
		SecondaryClick.onKeyDown -= OnSecondaryKeyDown;
		MiddleClick.onKeyDown -= OnKeyMiddleDown;
	}

	private void CheckTouches(Touch touch) {
		switch (touch.phase) {
			case TouchPhase.Began:
				TouchList.Add(new SavedTouchData(touch));
				break;
			case TouchPhase.Moved:
				//TouchList.Contains()
				//DoTouchPanCamera(touch);
				break;
			case TouchPhase.Stationary:
				break;
			case TouchPhase.Ended:
				for (var i = 0; i < TouchList.Count; i++) {
					if (TouchList[i].FingerID == touch.fingerId) {
						CalculateTouch(TouchList[i], touch);
						TouchIndexesToRemove.Add(i);
					}
				}
				break;
			case TouchPhase.Canceled:
				break;
		}
	}

	private void CalculateTouch(SavedTouchData data, Touch newTouch) {
		var resualts = data.GetTouchEndData();

		var touchLength = CalculateTouchLength(resualts.HeldTime);

		//Debug.Log("Held Time: " + resualts.HeldTime + ":" + touchLength);
		switch (touchLength) {
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

	private static bool TouchHasNotMoved(SavedTouchData data, Touch newTouch, float movementForCancelTouch) {
		var dist = Vector3.Distance(data.StartPos, newTouch.position);
		if (dist >= movementForCancelTouch) {
			Debug.Log("Cancel Distance: " + dist);
			return false;
		}
		return true;
	}

	public TouchLength CalculateTouchLength(float time) {
		if (time >= TimeForAlternateTouch)
			return TouchLength.Long;
		return TouchLength.Short;
	}

	[Serializable]
	public class SavedTouchData: IEqualityComparer<SavedTouchData> {
		public int FingerID;
		public float StartTime;
		public Vector2 StartPos;

		public SavedTouchData(Touch touch) {
			FingerID = touch.fingerId;
			StartPos = touch.position;
			StartTime = Time.time;
		}

		public TouchEndData GetTouchEndData() {
			var touch = new TouchEndData {Data = this, HeldTime = Time.time - StartTime};
			return touch;
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj))
				return false;
			if (obj is Touch)
				return Equals((Touch) obj);
			return obj is SavedTouchData && Equals((SavedTouchData) obj);
		}

		public bool Equals(SavedTouchData other) { return other.FingerID == FingerID; }
		public bool Equals(Touch other) { return other.fingerId == FingerID; }

		public override int GetHashCode() { return -2035406951 + FingerID.GetHashCode(); }

		public struct TouchEndData {
			public bool Over;
			public SavedTouchData Data;
			public float HeldTime;
		}

		public bool Equals(SavedTouchData x, SavedTouchData y) { return (x != null) && x.Equals(y); }
		public int GetHashCode(SavedTouchData obj) { return obj.GetHashCode(); }
	}

	[Serializable]
	public enum TouchLength {
		Short,
		Long
	}

	public class ScreenMoving {
		public Vector2 StartPos;
	}
}