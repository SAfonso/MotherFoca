using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPreview : MonoBehaviour {

	int thisLevel = 0;
	TextMeshProUGUI levelText;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
			levelText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
	}
    public void SetData(int level){
		Debug.Log("este " + level);
		thisLevel = level;
		levelText.text = "Level " + level;
    }

	public void Cancel(){
		//Limpiamos los valores
		this.gameObject.SetActive(false);

	}

	public void LoadLevel(){
		GameManager.instance.SetValues(thisLevel);
		GameObject.FindObjectOfType<UIManager>().CloseLevelPreview();
		
		Loader.instance.LoadLevel();
	}

	public void PressCheatButton(){
		
	}
}