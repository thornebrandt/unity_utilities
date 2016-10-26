using UnityEngine;
using System.Collections;

public class DistanceLOD : MonoBehaviour {
	public float[] distanceRanges;
	//Tip: Length of distanceRanges should be one less than LODModels. ( they are edges in between )
	public GameObject[] LODModels;
	public GameObject mask;
	// mask is meant to be a group of colliders
	// in which the objects are only active within.
	// use mask if occlusion isn't behaving correctly.
	public Camera cam;
	private Renderer[][] rends;
	private Bounds[] bounds;
	private int current;
	private int level;
	private Vector3 camPosition;

	void Start () {
		collectRenderers();
		collectMaskBounds();
		turnOffEverything();
	}

	void collectRenderers(){
		rends = new Renderer[LODModels.Length][];
		for(int i = 0; i < LODModels.Length; i++){
			rends[i] = LODModels[i].GetComponentsInChildren<Renderer>();
		}
	}

	void collectMaskBounds(){
		if(mask != null){
			Collider[] colliders = mask.GetComponentsInChildren<Collider>();
			bounds = new Bounds[colliders.Length];
			for(int i = 0; i < bounds.Length; i++){
				bounds[i] = colliders[i].bounds;
			}
		}
	}

	void Update(){
		if(checkMask()){
			showLOD();
		} else {
			turnOffEverything();
		}
	}

	bool checkMask(){
		if(bounds != null && bounds.Length > 0){
			camPosition = cam.transform.position;
			bool found = false;
			for(int i = 0; i < bounds.Length; i++){
				if(bounds[i].Contains(camPosition)){
					print("found");
					found = true;
				}
			}
			return found;
		} else {
			return true;
		}
	}


	void showLOD(){
		float d = Vector3.Distance(cam.transform.position, transform.position);
		level = -1;
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
		current = -2;
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
