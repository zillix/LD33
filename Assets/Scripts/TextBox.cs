using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBox : MonoBehaviour, ITextBox{

	public Text textField;
	public GameObject pointer;

	private Camera camera;

	GameObject pointerTarget;
	
	SpriteRenderer sprite;
	SpriteRenderer pointerRenderer;

	float pointerLength;

	private Canvas canvas;


	// Use this for initialization
	void Awake () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
		pointerRenderer = pointer.GetComponent<SpriteRenderer>();
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		canvas = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<Canvas> ();
	}

	void Start()
	{
		pointerLength = pointerRenderer.bounds.size.x;

	}
	
	// Update is called once per frame
	void Update () {
		if (pointerTarget != null) {
			// Yeah, this is a mess

			Vector2 viewportTarget = pointerTarget.transform.position;//camera.WorldToViewportPoint(pointerTarget.transform.position);
			Vector2 myPosition = (Vector2)pointer.transform.position;
			RectTransform canvasRect = canvas.GetComponent<RectTransform>();
			viewportTarget.x += .5f;//canvasRect.sizeDelta.x / 2f;
			viewportTarget.y += .5f;//canvasRect.sizeDelta.y / 2f;

			Vector2 distVec = viewportTarget - myPosition;

			float targetAngle = Vector2.Angle (new Vector2(1, 0), distVec);
			Debug.Log (myPosition + " " + viewportTarget + " " + distVec);
			Debug.Log(targetAngle);
			pointer.transform.rotation = Quaternion.Lerp(pointer.transform.rotation, Quaternion.Euler(0, 0, targetAngle), .5f);
			pointer.SetActive(true);

			float dist = distVec.magnitude;
			float desiredXScale = dist / pointerLength * 3/5;

			pointerRenderer.transform.localScale = new Vector3(desiredXScale, 1, 1);
		}
		else
		{
			pointer.SetActive(false);
		}
	}

	public void hide()
	{
		setAlpha (0);
	}
	public void show()
	{
		setAlpha (1);
	}

	private void setAlpha(float value)
	{
		Color textColor = textField.color;
		textColor.a = value;
		textField.color = textColor;

		Color color = sprite.color;
		color.a = value;
		sprite.color = color;

		Color pointerColor = pointerRenderer.color;
		pointerColor.a = value;
		pointerRenderer.color = pointerColor;
	}

	public string text
	{
		get {
			return textField.text;
		}

		set{
			textField.text = value;
		}
	}

	public GameObject target
	{
		set{
			pointerTarget = value;
		}
	}
}
