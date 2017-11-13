﻿using JMiles42.Attributes;
using JMiles42.Components;
using JMiles42.Extensions;
using JMiles42.UnityInterfaces;
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

	[SerializeField, DisableEditing] private Vector3 startPos = Vector3.zero;
	public float zoomRate = 1;
	[SerializeField] private float zoomLevel = 0.5f;
	public float zoomMovement = 0.1f;
	[SerializeField] private Transform zoomMin;
	[SerializeField] private Transform zoomMax;

	void Start()
	{
		if (Camera.IsNull())
		{
			Camera = GetComponentInChildren<Camera>();
		}
		if (Camera.IsNull())
		{
			Camera = Camera.main;
		}
		if (CameraHolder.IsNull())
		{
			CameraHolder = Transform;
		}
	}

	public void OnEnable()
	{
		GameplayInputManager.Instance.OnScreenStartMove += OnScreenStartMove;
		GameplayInputManager.Instance.OnScreenMoved += OnScreenMoved;
		GameplayInputManager.Instance.OnScreenEndMove += OnScreenEndMove;
		GameplayInputManager.Instance.OnScreenZoom += OnScreenZoom;
	}

	public void OnDisable()
	{
		GameplayInputManager.Instance.OnScreenStartMove -= OnScreenStartMove;
		GameplayInputManager.Instance.OnScreenMoved -= OnScreenMoved;
		GameplayInputManager.Instance.OnScreenEndMove -= OnScreenEndMove;
		GameplayInputManager.Instance.OnScreenZoom -= OnScreenZoom;
	}

	private void OnScreenZoom(float amount)
	{
		zoomLevel = (zoomLevel + (amount * (zoomRate))).Clamp();

		Camera.transform.position = Vector3.Lerp(zoomMin.position, zoomMax.position, zoomLevel);
	}

	private void OnScreenStartMove(Vector2 mousePos) { startPos = CameraHolder.position; }

	private void OnScreenMoved(Vector2 mouseDelta)
	{
		var pos = startPos + (Camera.transform.TransformDirection(mouseDelta).FromX_Y2Z() * Speed).SetY(0);
		CameraHolder.transform.position = new Vector3(pos.x.Clamp(CameraRangeLimiter.MinPos.x, CameraRangeLimiter.MaxPos.x),
													  pos.y,
													  pos.z.Clamp(CameraRangeLimiter.MinPos.z, CameraRangeLimiter.MaxPos.z));
	}

	private void OnScreenEndMove(Vector2 mousePos) { startPos = Vector3.zero; }
}