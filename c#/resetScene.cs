using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetScene : MonoBehaviour {
	void Update () {
		checkKeyboardInput();
	}
  void checkKeyboardInput(){
      if(Input.GetKeyDown("space")){
          SceneManager.LoadScene(0);
      }
  }
}