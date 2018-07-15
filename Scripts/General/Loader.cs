using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	public static Loader instance;

	UIManager uiComp;

	    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

  void Start() 
   { 
        //UIMANAGER
        uiComp = FindObjectOfType<UIManager>();
        uiComp.OpenStartScene();

  }

 	public void OnLevelWasLoaded(int i){

		 //UIMANAGER
        uiComp = FindObjectOfType<UIManager>();
        uiComp.OpenStartScene();

		if((SceneManager.GetActiveScene().buildIndex != 3) && (SceneManager.GetActiveScene().buildIndex != 0)){ 
    		uiComp.CloseOptions();
    	}
    	if((SceneManager.GetActiveScene().buildIndex == 1) /*|| (SceneManager.GetActiveScene().buildIndex == 2)*/){
			uiComp.CloseShop();
    	}
		if(SceneManager.GetActiveScene().buildIndex == 3){
			uiComp.ShowStars();
		}

        
 }
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Q) ){
			if(SceneManager.GetActiveScene().buildIndex == 0){
				GameManager.instance.firstRun = 0;
				Application.Quit();
			}else{
				GameManager.instance.isReloaded = true;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
			}
			
		}
		if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.A) ){
			Debug.Log("Escape");
			if(SceneManager.GetActiveScene().buildIndex == 0){
				Application.Quit();
			}else{
				GameManager.instance.isReloaded = true;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
			}
			
		}
		
	}

// Scene loaders
	public void LoadGame(){
		SceneManager.LoadScene(1);
	}

	public void LoadMap(){
		GameManager.instance.activeCheats = false;
		SceneManager.LoadScene(1);
	}

	public void ReLoadMap(){
		GameManager.instance.activeCheats = false;
		GameManager.instance.isReloaded = true;
		SceneManager.LoadScene(1);
	}
	
	public void LoadLevel(){
		Timer.instance.AddTime();
		SceneManager.LoadScene(2);
	}

	public void LoadWin(){
		GameManager.instance.activeCheats = false;
		SceneManager.LoadScene(3);
	}

	public void ReloadLevel(){
		if(GameManager.instance.gmEnergy > 0){
			Timer.instance.AddTime();
			SceneManager.LoadScene(2);
		}else{
			ReLoadMap();
		}
	}

// Reload

	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}


	public void ReloadWithCheats(){
		GameManager.instance.activeCheats = true;
		Reload();
	}

	bool quitting;
 	void OnApplicationQuit() {
    	quitting = true;
 	}
 	void OnDestroy() {
    	if (quitting) {
         	GameManager.instance.PreSave();
     	}
 	}

}
