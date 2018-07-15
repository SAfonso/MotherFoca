using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour {

	public Sprite defaultSprite, pressedSprite;
	Image thisSprite;

	//TextMeshProUGUI counterPu;
	bool pressed = false;
	// Use this for initialization
	void Start () {
		//counterPu = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		thisSprite = transform.GetComponent<Image>();
		pressed = false;

		//counterPu.text = (GameManager.instance.gmPowerUps).ToString();
	}

	public void Action(){
		if(!pressed){
			thisSprite.sprite = pressedSprite;
			pressed = true;
			//counterPu.text = (GameManager.instance.gmPowerUps-1).ToString();
		}else{
			thisSprite.sprite = defaultSprite;
			pressed = false;
			//counterPu.text = (GameManager.instance.gmPowerUps).ToString();
		}
	}
	
}
