using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,IPointerDownHandler,IPointerUpHandler
{
	private Animation anim;
	public LayerMask rayLayer;

	public Scrollbar bar;

	private void Start()
	{
		anim = GetComponent<Animation>();
	
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			anim.Play("idle");
		}

		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			anim.Play("move");
		}

		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			anim.Play("attack01");
		}

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000f, rayLayer))
			{
				Vector3 explosionPos = hit.point;
				float radius = 5.0f;
				float power = 40.0f;
				Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

				foreach (Collider obj in colliders)
				{
					Rigidbody rb = obj.GetComponent<Rigidbody>();

					if (rb != null)
						rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
				}
			}
		}

	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("Drag");

		//throw new System.NotImplementedException();
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("Begin Drag");
		//throw new System.NotImplementedException();
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("End Drag");
		//throw new System.NotImplementedException();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Pointer Down");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("Pointer Up");
	}
}
