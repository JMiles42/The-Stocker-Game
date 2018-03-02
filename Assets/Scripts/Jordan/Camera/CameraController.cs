using System.Linq;
using ForestOfChaosLib;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.CSharpExtensions;
using ForestOfChaosLib.Curves.Components;
using ForestOfChaosLib.Interfaces;
using ForestOfChaosLib.UnityScriptsExtensions;
using UnityEngine;

public class CameraController: FoCsBehavior, IEventListening
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
	public GridBlockListReference GridBlockReference;

	public Vector3 minPos;
	public Vector3 maxPos;

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
		OnScreenZoom(0.4f);
	}

	private void UpdateCameraLimits()
	{
		Position = MapReference.Value.SpawnPosition.WorldPosition;
	}


	private void OnScreenZoom(float amount)
	{
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
		var pos = startPos + (Camera.transform.TransformDirection(mouseDelta).FromX_Y2Z() * Speed).SetY(0);
		CameraHolder.transform.position = new Vector3(pos.x.Clamp(minPos.x, maxPos.x), pos.y, pos.z.Clamp(minPos.z, maxPos.z));
	}

	private void OnScreenEndMove(Vector2 mousePos)
	{
		startPos = Vector3.zero;
	}

	public void CalculateLimits()
	{
		var f = GridBlockReference.Items.First();

		minPos = f.Position;
		maxPos = f.Position;
		foreach(var gridBlock in GridBlockReference.Items)
		{
			minPos = Vector3.Min(minPos,gridBlock.Position);
			maxPos = Vector3.Max(maxPos,gridBlock.Position);
		}
	}
}