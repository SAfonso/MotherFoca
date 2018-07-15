using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public List<GameObject> levelList = new List<GameObject>();
	
    public DataLevel actualLevel;
	public bool wasplayed;


	public int gmCoins = 0;
	public int gmEnergy = 30;
	public int gmMaxEnergy = 30;
	public float gmTime = 0;
	public int gmPowerUps = 0;
	public int gmCurrentLevel = 1;
	public int gmActualPage = 0;
	public bool gmCanMove = true;
	public float time;


	public bool activeCheats = false;


	public System.DateTime actualTime;
	public float tCount = 0;
	public bool isReloaded;
	public int firstRun = 0;
	Timer energyTimer;
	


	void Awake()
    {
		Constants.firstTimePlay = true;
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

/*		firstRun = PlayerPrefs.GetInt("savedFirstRun") ;
		firstRun = 0;
		PlayerPrefs.SetInt("savedFirstRun", firstRun);*/

		if(!Constants.reloaded){
			GetAll();
		}else{
			Constants.reloaded = false;
		}
		

    }

	public void GetAll(){
		
		tCount = 300;

		//Important note: place your prefabs folder(or levels or whatever) 
        //in a folder called "Resources" like this "Assets/Resources/Prefabs"
        Object[] subListObjects = Resources.LoadAll("Prefabs/Levels", typeof(GameObject));
		//Rellenamos elarray deprefabs de niveles
		foreach (GameObject subListObject in subListObjects){    
        	GameObject lo = (GameObject)subListObject;
			levelList.Add(lo);
     	}

		firstRun = PlayerPrefs.GetInt("savedFirstRun") ;
		if (firstRun == 0){
			firstRun = 1;
      		for (int i = 0; i <= levelList.Count-1; i++){
        		levelList[i].GetComponent<DataLevel>().Default();
      		}
		}else{
			Debug.Log("Entran en load");
			LoadDATA();
		}

		if(firstRun != 0){

		}else{

		}
		gmCoins = Constants.coins;
		gmEnergy = Constants.energy;
		gmMaxEnergy = Constants.maxEnergy;
		gmTime = Constants.time;
		gmPowerUps = Constants.powerUps;
		gmActualPage = Constants.actualPage;
		gmCanMove = true;

		time = 0f;
				
	

		if(SceneManager.GetActiveScene().buildIndex == 1){
			if (gmEnergy < gmMaxEnergy){
				Timer.instance.AddTime();
				Constants.acumulatedTime -= (tCount*60);
			}else{
				Constants.acumulatedTime = 0;
			}
    	}

		SetValues(1);
		wasplayed= actualLevel.wasPlayed;
	}

	public void LoadDATA(){
		Save dataToLoad = new Save(){save= new Save.SaveGame()};
		dataToLoad.LoadData();

		Constants.coins = dataToLoad.save.coins;
		Constants.energy = dataToLoad.save.energy;
		Constants.maxEnergy = dataToLoad.save.maxEnergy;
		Constants.time = dataToLoad.save.time;
		Constants.powerUps = dataToLoad.save.powerUps;
		Constants.actualPage = 0;
		Constants.firstTimePlay = dataToLoad.save.firstTimePlay;

		Constants.acumulatedTime = dataToLoad.save.acumulatedTime;

/*		Cojemos la hora actual*/

		actualTime = System.DateTime.Now;
/*		Recogemos la hora que había sido guardada*/

		long temp = System.Convert.ToInt64(dataToLoad.save.datetime);
		System.DateTime oldDate = System.DateTime.FromBinary(temp);
		
/*		Calculamos la diferencia*/

		System.TimeSpan difference = actualTime.Subtract(oldDate);
/*		Debug.Log("Diff : " + difference);*/

		CheckTimer(difference);


		for (int i = 0; i <= dataToLoad.save.dataLevel.Count-1; i++){

        	DataSeria lo = dataToLoad.save.dataLevel[i];

			DataLevel aux = DStoDL(lo);
				

			levelList[i].GetComponent<DataLevel>().SaveData(aux);
		}

/*		Debug.Log("Muertos : " + instance.lifeMinions);*/

		Constants.currentLevel = dataToLoad.save.currentLevel;
		
	}

	public void SaveDataGM(){
		Save dataToSave = new Save(){save= new Save.SaveGame()};
		Debug.Log("Hola");

		dataToSave.save.coins = Constants.coins;
		dataToSave.save.energy = Constants.energy;
		dataToSave.save.maxEnergy = Constants.maxEnergy;
		//dataToSave.save.time = Constants.time;
		dataToSave.save.powerUps = Constants.powerUps;
		dataToSave.save.firstTimePlay = Constants.firstTimePlay;

		dataToSave.save.acumulatedTime = GetActualTotalTime();

		dataToSave.save.currentLevel = Constants.currentLevel;

		string timeNow = System.DateTime.Now.ToBinary().ToString();
		//Debug.Log(timeNow);
		dataToSave.save.datetime = timeNow;

		dataToSave.save.dataLevel = new List<DataSeria>();
		for (int i = 0; i <= levelList.Count-1; i++){
			DataLevel lo = levelList[i].GetComponent<DataLevel>();
			DataSeria aux = new DataSeria();
			aux = DLtoDS(lo);
			dataToSave.save.dataLevel.Add(aux);
		}

		Debug.Log("Guardamos el dataSave");
		dataToSave.SaveData(dataToSave);

		PlayerPrefs.SetInt("savedFirstRun", firstRun);
		Debug.Log("Hola Back");

	}

	public void PreSave(){
		Constants.coins = gmCoins;
		Constants.energy = gmEnergy;
		Constants.maxEnergy = gmMaxEnergy;
		Constants.time = gmTime;
		Constants.powerUps = gmPowerUps;
		Constants.actualPage = gmActualPage;

		Constants.dataLevel.Clear();
		for (int i = 0; i <= levelList.Count-1; i++){
			GameObject lo = levelList[i].GetComponent<GameObject>();
			Constants.dataLevel.Add(lo);
		}
		Debug.Log("Terminamos el Presave");
		SaveDataGM();
	}


	public DataLevel DStoDL(DataSeria element){
		DataLevel aux =  new DataLevel();

		aux.level = element.level;
		aux.wasPlayed = element.wasPlayed;
		aux.beforeWasPlayed = element.beforeWasPlayed;
		for(int i=0; i<=element.allStars.Length-1; i++){
			aux.allStars[i].wasCatched = element.allStars[i].wasCatched;
			aux.allStars[i].wasCatchedBefore = element.allStars[i].wasCatchedBefore;
		}

		return aux;		
	}

	public DataSeria DLtoDS(DataLevel element){
		DataSeria aux =  new DataSeria();

		aux.level = element.level;
		aux.wasPlayed = element.wasPlayed;
		aux.beforeWasPlayed = element.beforeWasPlayed;
		for(int i=0; i<=element.allStars.Length-1; i++){
			aux.allStars[i].wasCatched = element.allStars[i].wasCatched;
			aux.allStars[i].wasCatchedBefore = element.allStars[i].wasCatchedBefore;
		}

		return aux;
	}


	public void CheckTimer(System.TimeSpan totalTime){
		float acum = Constants.acumulatedTime;
		double totalSeconds = totalTime.TotalSeconds;

/*		Debug.Log("TotalSeconds >>> " + totalSeconds);
		Debug.Log("Acumulates >>> " + acum);*/

		if (totalSeconds >= acum){
			Constants.energy = 30;
		}else{
			while((totalSeconds > 0) && (totalSeconds >= (tCount*60))){
				totalSeconds -= (tCount*60);
				acum -= (tCount*60);
				Constants.energy++;
/*				Debug.Log("Entra en counter " + counter);*/
			}
		}
		if (totalSeconds > 0){
			Constants.acumulatedTime = acum;
		}
	}

	public float GetActualTotalTime(){
		float value = 0;
		int auxQue = gmMaxEnergy - gmEnergy;
		int counter = 0;
		if (auxQue > 0){
			counter++;
			auxQue--;			
		}
		value = counter*tCount;

		return value;
		
	}

	public void FinishLevel(){
		//Save the data
		gmCoins += 20;
		for(int i = 0; i <= actualLevel.allStars.Length-1; i++){
			if(actualLevel.allStars[i].wasCatched){
				if(!actualLevel.allStars[i].wasCatchedBefore){
					gmCoins += 10;
					actualLevel.allStars[i].wasCatchedBefore = true;
				}
			}
		}
		levelList[actualLevel.level-1].GetComponent<DataLevel>().SaveData(actualLevel);
		Constants.currentLevel = actualLevel.level;
		//ActualiceMoney();

	}
	public void SetValues(int index){
		actualLevel = levelList[index-1].GetComponent<DataLevel>();
	}

	public bool LevelWasPlayed(int value){
		return levelList[value-1].GetComponent<DataLevel>().wasPlayed;
	}

/*	public void CleanValues(){
		actualLevel.level = 0;
		actualLevel.wasPlayed = false;
		actualLevel.beforeWasPlayed = false;
	}*/

	public GameObject GetLevel(){
		return levelList[actualLevel.level-1];
	}

	public DataLevel GetValues(){
		return actualLevel;
	}

	public void StartCDEnergy(){
		Timer.instance.AddTime();
	}
	// Update is called once per frame
	void Update () {
		
	}
}