using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData : MonoBehaviour {

    public int level = 0;

	public List<GameObject> stGroup = new List<GameObject>();
	int listCount = 0;
	
	public bool canPLay = false;

	public GameObject levelPreviewCanvas;
	GameObject vfx;


	void Start(){
	canPLay = false;


	vfx = this.transform.GetChild(0).gameObject;
	vfx.SetActive(false);
/*	listCount = this.transform.GetChild(0).transform.GetChild(2).childCount;

	for(int i = 0; i <= (listCount-1); i++){
		stGroup.Add(this.transform.GetChild(0).GetChild(2).GetChild(i).gameObject);
	}
Debug.Log(">>>>>> " + (GameManager.instance.LevelWasPlayed(Constants.currentLevel)) + " " +  (level == (GameManager.instance.actualLevel.level)));
		if((!GameManager.instance.LevelWasPlayed(Constants.currentLevel)) && (level == (GameManager.instance.actualLevel.level-1))){
			for(int i = 0; i <= (stGroup.Count-1); i++){
				stGroup[i].transform.GetChild(0).gameObject.SetActive(false);
			}
		}else{
			for(int i = 0; i <= (GameManager.instance.actualLevel.allStars.Length-1); i++){
				if (!GameManager.instance.actualLevel.allStars[i].wasCatched){
					stGroup[i].transform.GetChild(0).gameObject.SetActive(false);
				}
			}

		}*/
	    if(level == 1){
			Debug.Log("estamos en lvl 1");
			canPLay = true;
	    }else{
			DataLevel afterLevel = GameManager.instance.levelList[level-2].GetComponent<DataLevel>();
			if(afterLevel.beforeWasPlayed){
				canPLay = true;
			}
	    }
	    if (canPLay){
		   vfx.SetActive(true);
	    }
	}

	public void SelectLevel(){
		if (canPLay){
/*			if(GameManager.instance.gmEnergy != 0){*/
				GameObject.FindObjectOfType<UIManager>().OpenLevelPreview();
				levelPreviewCanvas.GetComponent<LevelPreview>().SetData(level);

/*			}else{
				//Mostrar el de "No tiene PU"
			}*/

		//Aqui rellenamos los valores del canvas segun nuestras variables
		}
	}

}
