using UnityEngine;
using System.Collections;

public class TriggerColor : TriggerClass {
	private Color startColor;
	private Color endColor;
	private Color targetColor;
	private Color emissionColor;
	private float emissionAmount;
	private float maxEmission = 1.5f;
	private Renderer r;
	private Shader shader;
	private float speed = 5f;
	private float dampening = 3f;
	private bool shrink = true;
	private float scale;
	public int materialIndex = 0;
	public string inputKey = "1";
	public Color color;

	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer>();
		startColor = new Color(.5f, .5f, .5f);
		endColor = color;
		color = startColor;
		if(r.materials.Length > materialIndex){
		} else {
			materialIndex = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		checkInput();
		updateColor();
		updateEmission();
		updateTargetColor();
	}

	void updateEmission(){
		emissionAmount = Mathf.Lerp(emissionAmount, 0, Time.deltaTime * dampening/2);
		emissionColor = targetColor * emissionAmount;
		r.materials[materialIndex].SetColor("_EmissionColor", emissionColor);
		DynamicGI.SetEmissive(r, emissionColor);
	}

	void updateColor(){
		color = Color.Lerp(color, targetColor, Time.deltaTime * speed);
		r.materials[materialIndex].color = color;
	}

	void updateTargetColor(){
		targetColor = Color.Lerp(targetColor, startColor, Time.deltaTime * dampening);
	}

	void checkInput(){
		if(Input.GetKeyDown(inputKey)){
			trigger(1f);
		}
	}

	public override void trigger(float vel){
		targetColor = endColor;
		emissionAmount = maxEmission;
	}

}
