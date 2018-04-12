using System;
using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.Animation;
using ForestOfChaosLib.Grid;
using UnityEngine;

public class PlayerAnimation : FoCsBehavior
{
	public AnimatorKey SpeedKey;
	public AnimatorKey WalkingKey;
	[SerializeField] private Animator _animator;

	public Animator Animator
	{
		get { return _animator ?? (_animator = GetComponent<Animator>()); }
		set { _animator = value; }
	}
	[SerializeField] private Player _player;

	public Player Player
	{
		get { return _player ?? (_player = GetComponent<Player>()); }
		set { _player = value; }
	}

	void OnEnable()
	{
		Player.OnMoveToBlock += OnMoveToBlock;
		Player.OnStartMove += OnStartMove;
		Player.OnStopMove += OnStopMove;
	}

	private void OnStopMove()
	{
		WalkingKey.boolData = false;
		WalkingKey.CalculateAnimator(Animator);
	}

	private void OnStartMove()
	{
		WalkingKey.boolData = true;
		WalkingKey.CalculateAnimator(Animator);
	}

	private void OnMoveToBlock(GridPosition gridPosition)
	{
		Animator.gameObject.transform.rotation = Quaternion.LookRotation(gridPosition.WorldPosition - Player.GridPosition.WorldPosition, Vector3.up);
	}

	void OnDisable()
	{
		Player.OnMoveToBlock -= OnMoveToBlock;
		Player.OnStartMove -= OnStartMove;
		Player.OnStopMove -= OnStopMove;
	}
}
