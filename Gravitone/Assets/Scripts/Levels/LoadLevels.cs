using UnityEngine;
using System.Collections;

public class LoadLevels : MonoBehaviour {

	public void NextLevelButton(string levelName)
     {
         Application.LoadLevel(levelName);
     }
		 
}
