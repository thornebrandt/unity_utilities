using UnityEngine;
using System.Collections;

public class playSprite : MonoBehaviour {
	public int colCount;
	public int rowCount;
	public int totalCells;
	public int stops = 3;
	public bool autoPlay = false;
	public bool loop = false;
	public string inputKey = "";
	private int rowNumber = 0;
	private int colNumber = 0;
	private Vector2 offset;
	private int index;
	private bool playing = true;



	void Start(){
		if(autoPlay == true){
			playing = true;
		}
	}

	void Update () {
		checkKeyboardInput();
		if(playing){
			animateSprite();
		}
	}

	void checkKeyboardInput(){
		if(inputKey != ""){
			if(Input.GetKeyDown(inputKey)){
				trigger();
			}
		}
	}


	void animateSprite(){
		if(index >= (totalCells - 1)){
			index = 0;
		}
		if(Time.frameCount % stops == 0){
			index = getNextInLoop(index);
		}
		if(loop == false && index == 0){
			playing = false;
		}
		float sizeX = 1.0f / colCount;
		float sizeY = 1.0f / rowCount;
		Vector2 size =  new Vector2(sizeX,sizeY);
		var uIndex = index % colCount;
		var vIndex = index / colCount;
		float offsetX = (uIndex+colNumber) * size.x;
		float offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
		Vector2 offset = new Vector2(offsetX,offsetY);
		GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
		GetComponent<Renderer>().material.SetTextureScale  ("_MainTex", size);
	}

	int getNextInLoop(int i){
	    int next = i;
	    next++;
	    if(next >= totalCells){
	        next = 0;
	    }
	    return next;
	}

	public void trigger(){
		playing = true;
		index = 0;
	}

}
