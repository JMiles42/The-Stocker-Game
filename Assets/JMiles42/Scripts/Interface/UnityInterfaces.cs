using UnityEngine;

namespace JMiles42.UnityInterfaces
{
	public interface IOnEnable
	{
		void OnEnable();
	}

	public interface IOnDisable
	{
		void OnDisable();
	}

	public interface IEventListening
	{
		void OnEnable();
		void OnDisable();
	}

	public interface IAwake
	{
		void Awake();
	}

	public interface IStart
	{
		void Start();
	}

	public interface IUpdate
	{
		void Update();
	}

	public interface IFixedUpdate
	{
		void FixedUpdate();
	}

	public interface ILateUpdate
	{
		void LateUpdate();
	}

	public interface IOnAnimatorIK
	{
		void OnAnimatorIK();
	}

	public interface IOnAnimatorMove
	{
		void OnAnimatorMove();
	}

	public interface IOnApplicationFocus
	{
		void OnApplicationFocus();
	}

	public interface IOnApplicationPause
	{
		void OnApplicationPause();
	}

	public interface IOnApplicationQuit
	{
		void OnApplicationQuit();
	}

	public interface IOnBecameInvisible
	{
		void OnBecameInvisible();
	}

	public interface IOnBecameVisible
	{
		void OnBecameVisible();
	}

	public interface IOnDrawGizmosSelected
	{
		void OnDrawGizmosSelected();
	}

	public interface IOnDrawGizmos
	{
		void OnDrawGizmos();
	}

	public interface IOnMouseEnter
	{
		void OnMouseEnter();
	}

	public interface IOnMouseDrag
	{
		void OnMouseDrag();
	}

	public interface IOnMouseDown
	{
		void OnMouseDown();
	}

	public interface IOnMouseExit
	{
		void OnMouseExit();
	}

	public interface IOnMouseOver
	{
		void OnMouseOver();
	}

	public interface IOnMouseUp
	{
		void OnMouseUp();
	}

	public interface IOnMouseUpAsButton
	{
		void OnMouseUpAsButton();
	}

	public interface IOnCollisionEnter
	{
		void OnCollisionEnter(Collision collision);
	}

	public interface IOnCollisionStay
	{
		void OnCollisionStay(Collision collision);
	}

	public interface IOnCollisionExit
	{
		void OnCollisionExit(Collision collision);
	}

	public interface IOnCollision: IOnCollisionEnter, IOnCollisionStay, IOnCollisionExit
	{}

	public interface IOnCollisionEnter2D
	{
		void OnCollisionEnter2D(Collision2D collision2D);
	}

	public interface IOnCollisionExit2D
	{
		void OnCollisionExit2D(Collision2D collision2D);
	}

	public interface IOnCollisionStay2D
	{
		void OnCollisionStay2D(Collision2D collision2D);
	}

	public interface IOnCollision2D: IOnCollisionEnter2D, IOnCollisionStay2D, IOnCollisionExit2D
	{}

	public interface IOnTriggerEnter
	{
		void OnTriggerEnter(Collider collision);
	}

	public interface IOnTriggerStay
	{
		void OnTriggerStay(Collider collision);
	}

	public interface IOnTriggerExit
	{
		void OnTriggerExit(Collider collision);
	}

	public interface IOnTrigger: IOnTriggerEnter, IOnTriggerStay, IOnTriggerExit
	{}

	public interface IOnTriggerEnter2D
	{
		void OnTriggerEnter2D(Collider2D collision2D);
	}

	public interface IOnTriggerStay2D
	{
		void OnTriggerStay2D(Collider2D collision2D);
	}

	public interface IOnTriggerExit2D
	{
		void OnTriggerExit2D(Collider2D collision2D);
	}

	public interface IOnTrigger2D: IOnTriggerEnter2D, IOnTriggerStay2D, IOnTriggerExit2D
	{}

	public interface IOnValidate
	{
		void OnValidate();
	}

	public interface IOnWillRenderObject
	{
		void OnWillRenderObject();
	}

	public interface IReset
	{
		void Reset();
	}

	public interface IOnPostRender
	{
		void OnPostRender();
	}

	public interface IOnPreCull
	{
		void OnPreCull();
	}

	public interface IOnPreRender
	{
		void OnPreRender();
	}

	public interface IOnRenderImage
	{
		void OnRenderImage();
	}

	public interface IOnRenderObject
	{
		void OnRenderObject();
	}
}