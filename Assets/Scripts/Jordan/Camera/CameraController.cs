using System.Linq;
using JMiles42;
using JMiles42.AdvVar;
using JMiles42.Attributes;
using JMiles42.CSharpExtensions;
using JMiles42.Curves.Components;
using JMiles42.Interfaces;
using JMiles42.UnityScriptsExtensions;
using UnityEngine;

public class CameraController: JMilesBehavior, IEventListening
{
	public float Speed = 5;

	[SerializeField] private Camera _camera;

	public Camera Camera
	{
		get { return _camera ?? (_camera = GetComponentInChildren<Camera>()); }
		set { _camera = value; }
	}

	public Transform CameraHolder;

	[SerializeField] [DisableEditing] private Vector3 startPos = Vector3.zero;

	public float zoomRate = 1;

	[SerializeField] private float zoomLevel = 0.5f;

	public float zoomMovement = 0.1f;

	public BezierCurveV3DBehaviour ZoomCurve;

	public MapSO MapReference;
	public GridBlockListVariable GridBlockReference;

	public Vector3 minPos;
	public Vector3 maxPos;
	public BoolReference GameActive;

	public void OnEnable()
	{
		GameplayInputManager.OnScreenStartMove += OnScreenStartMove;
		GameplayInputManager.OnScreenMoved += OnScreenMoved;
		GameplayInputManager.OnScreenEndMove += OnScreenEndMove;
		GameplayInputManager.OnScreenZoom += OnScreenZoom;
		MapReference.OnValueChange += UpdateCameraLimits;
		GridBlockReference.OnMapFinishSpawning += CalculateLimits;
	}

	public void OnDisable()
	{
		GameplayInputManager.OnScreenStartMove -= OnScreenStartMove;
		GameplayInputManager.OnScreenMoved -= OnScreenMoved;
		GameplayInputManager.OnScreenEndMove -= OnScreenEndMove;
		GameplayInputManager.OnScreenZoom -= OnScreenZoom;
		MapReference.OnValueChange -= UpdateCameraLimits;
		GridBlockReference.OnMapFinishSpawning -= CalculateLimits;
	}

	private void Start()
	{
		if(Camera == null)
			Camera = GetComponentInChildren<Camera>();
		if(Camera == null)
			Camera = Camera.main;
		if(CameraHolder == null)
			CameraHolder = Transform;
		OnScreenZoom(0f);
	}

	private void UpdateCameraLimits()
	{
		Position = MapReference.Value.SpawnPosition.WorldPosition;
	}


	private void OnScreenZoom(float amount)
	{
		if(!GameActive.Value)
			return;
		zoomLevel = (zoomLevel + (amount * (zoomRate))).Clamp();

		Camera.transform.position = ZoomCurve.Position + ZoomCurve.Lerp(zoomLevel);
		Camera.transform.LookAt(ZoomCurve.Transform);
	}

	private void OnScreenStartMove(Vector2 mousePos)
	{
		startPos = CameraHolder.position;
	}

	private void OnScreenMoved(Vector2 mouseDelta)
	{
		if(!GameActive.Value)
			return;

		var pos = startPos + (Camera.transform.TransformDirection(mouseDelta).FromX_Y2Z() * Speed).SetY(0);
		CameraHolder.transform.position = new Vector3(pos.x.Clamp(minPos.x, maxPos.x), pos.y, pos.z.Clamp(minPos.z, maxPos.z));
	}

	private void OnScreenEndMove(Vector2 mousePos)
	{
		startPos = Vector3.zero;
	}

	public void CalculateLimits()
	{
		var f = GridBlockReference.Value.First();

		minPos = f.Position;
		maxPos = f.Position;
		foreach(var gridBlock in GridBlockReference.Value)
		{
			minPos = Vector3.Min(minPos,gridBlock.Position);
			maxPos = Vector3.Max(maxPos,gridBlock.Position);
		}
	}
}