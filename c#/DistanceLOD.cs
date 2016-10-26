using UnityEngine;
using System.Collections;

public class DistanceLOD : MonoBehaviour {
	public float[] distanceRanges;
	public GameObject[] LODModels;
	public Camera cam;
	private Renderer[][] rends;
	private int current = -2;

	void Start () {
		collectRenderers();
		turnOffEverything();
	}

	void collectRenderers(){
		rends = new Renderer[LODModels.Length][];
		for(int i = 0; i < LODModels.Length; i++){
			rends[i] = LODModels[i].GetComponentsInChildren<Renderer>();
		}
	}

	void Update(){
		checkCameraPosition();
	}

	void checkCameraPosition(){
		float d = Vector3.Distance(cam.transform.position, transform.position);
		int level = -1;
		for(int i = 0; i < distanceRanges.Length; i++){
			if(d < distanceRanges[i]){
				level = i;
				break;
			}
		}

		if(level == -1){
			level = distanceRanges.Length;
		}

		if(current != level){
			changeLOD(level);
		}
	}

	void changeLOD(int level){
		turnOn(level);
		if(current >= 0){
			turnOff(current);
		}
		current = level;
	}

	void turnOffEverything(){
		for(int i = 0; i < LODModels.Length; i++){
			turnOff(i);
		}
	}

	void turnOn(int i){
		for(int j = 0; j < rends[i].Length; j++){
			rends[i][j].enabled = true;
		}
	}

	void turnOff(int i){
		for(int j = 0; j < rends[i].Length; j++){
			rends[i][j].enabled = false;
		}
	}


}
