using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDataManager : MonoBehaviour {

	public List<TextMeshProUGUI> Diamonds = new List<TextMeshProUGUI>();
	public List<TextMeshProUGUI> PU = new List<TextMeshProUGUI>();
	public TextMeshProUGUI Energy;
	public Slider slider;
	
	// Use this for initialization
	void Start () {

		UpdateDiamonds();

		UpdateEnergy();

		UpdatePowerUP();
	}

	public void UpdateDiamonds(){
		foreach(TextMeshProUGUI text in Diamonds){
			text.text = GameManager.instance.gmCoins + "";
		}
	}

	public void UpdatePowerUP(){
		foreach(TextMeshProUGUI text in PU){
			text.text = GameManager.instance.gmPowerUps + "";
		}
	}
	
	public void UpdateEnergy(){
		if(Energy != null)
			Energy.text = GameManager.instance.gmEnergy + "/" + GameManager.instance.gmMaxEnergy;
		if(slider != null)
			slider.value = GameManager.instance.gmEnergy;
	}
}
