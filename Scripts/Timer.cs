using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

	public static Timer instance;
	TextMeshProUGUI TimerText;
	//public Button myButton;

	public float timeToCount;

	float timer;
	bool starter;
	bool canWrite;

	void Awake()
    {
		Constants.firstTimePlay = true;
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		starter = false;
		canWrite = false;
	}

 	public void OnLevelWasLoaded(int i){
    	if(SceneManager.GetActiveScene().buildIndex == 1){ 
			TimerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<TextMeshProUGUI>();
			if(!starter){
				TimerText.text = "00:00";
			}
			canWrite = true;
    	}else{
			canWrite = false;
		}
 	}

	public void SetMinutes(float value){
		timer = value;
		starter = true;
	}
	
	// Update is called once per frame
	void Update(){
		if (starter){
			float minutes = Mathf.Floor(timer / 60);
			float seconds = (timer % 60);
			if ((minutes >= 0) || (seconds >= 0)){
				timer -= Time.deltaTime;
				minutes = Mathf.Floor(timer / 60);
				seconds = (timer % 60);
				if(canWrite){
					TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
				}
				
			}else{
				starter = false;
				GameManager.instance.gmEnergy++;
				if(canWrite){
					TimerText.text = "00:00";
					FindObjectOfType<UIDataManager>().UpdateEnergy();
				}				
				if (GameManager.instance.gmEnergy < GameManager.instance.gmMaxEnergy){
					SetMinutes(timeToCount);
				}
				
				// Add GameManager.time + 1
				// Si GameManager.time != 0 -> SetMinutes()
				
			}
			
		}
	}

	public void AddTime(){
		if (starter){
			GameManager.instance.gmEnergy--;
		}else{
			GameManager.instance.gmEnergy--;
			SetMinutes(timeToCount);
		}
		
	}

	public void RestartTimer(){
		GameManager.instance.gmEnergy = GameManager.instance.gmMaxEnergy;
		starter = false;
		if(canWrite){
			TimerText.text = "00:00";
			FindObjectOfType<UIDataManager>().UpdateEnergy();
		}
	}

	public void AddEnergy(){
		GameManager.instance.gmEnergy++;
		if (GameManager.instance.gmEnergy >= GameManager.instance.gmMaxEnergy){
			RestartTimer();
		}
	}
}
