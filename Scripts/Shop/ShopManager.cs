using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

  public List<GameObject> Page = new List<GameObject>();
	List<CanvasGroup> Canvases = new List<CanvasGroup>();
	[System.Serializable]
	public enum Paginas {PU,energy,diamond}; 
  public Paginas startShop; 
	int totalPages;

  public SO_ShopPrices Prices;

  public List<Button> PUButtons = new List<Button>();
  public List<Button> EnergyButtons = new List<Button>();
  public List<Button> DiamondButtons = new List<Button>();

  public List<Button> SelectionButtons = new List<Button>();

	// Use this for initialization
	void Start () {
		totalPages = Page.Count-1;

		for(int i = 0; i <= totalPages; i++){
			Canvases.Add(Page[i].GetComponent<CanvasGroup>());
		}

		PrepareEscene((int)startShop);

    RefreshButtons();
	}

	public void SetStartPage(int value){
    if(value == 0){ 
      startShop = Paginas.PU; 
    }else if(value == 1){
      startShop = Paginas.energy; 
    }else if(value == 2){
      startShop = Paginas.diamond;
    }
	}
	
	// Update is called once per frame
	public void PrepareEscene(int index){
		for(int i=0; i<=totalPages; i++){
			if (i != index){
				Canvases[i].interactable = false;
				Canvases[i].blocksRaycasts = false;
				Canvases[i].alpha = 0;
        SelectionButtons[i].interactable = true;
			}else{
        Canvases[i].interactable = true;
		    Canvases[i].blocksRaycasts = true;
		    Canvases[i].alpha = 1;

        startShop = (Paginas)i;

        SelectionButtons[i].interactable = false;
      }
		}

    RefreshButtons();
		
	}

  public void PowerUpBtn(){ 
    if((int)startShop != 0){ 
      PrepareEscene((int)Paginas.PU); 
    } 
    Debug.Log((int)startShop); 
  } 
 
  public void EnergyBtn(){ 
    if((int)startShop != 1){ 
      PrepareEscene((int)Paginas.energy); 
    } 

    Debug.Log((int)startShop); 
  } 
 
  public void DiamonBtn(){ 
    if((int)startShop != 2){ 
      PrepareEscene((int)Paginas.diamond);
    } 
    Debug.Log((int)startShop); 
  }

  public void BuyPU(int index){
    if(GameManager.instance.gmCoins - Prices.PU[index].price >= 0){
      GameManager.instance.gmPowerUps += Prices.PU[index].amount;
      GameManager.instance.gmCoins -= Prices.PU[index].price;
    }

    RefreshButtons();
    GetComponent<UIDataManager>().UpdatePowerUP();
    GetComponent<UIDataManager>().UpdateDiamonds();
  }

  public void BuyEnergy(int index){
    if(GameManager.instance.gmCoins - Prices.PU[index].price >= 0){
      if(index == 0){
        Timer.instance.RestartTimer();
      }else{
        GameManager.instance.gmMaxEnergy += Prices.PU[index].amount;
        GetComponent<UIDataManager>().slider.maxValue = GameManager.instance.gmMaxEnergy;
      }
      
      GameManager.instance.gmCoins -= Prices.PU[index].price;
    }

    RefreshButtons();
    GetComponent<UIDataManager>().UpdateEnergy();
    GetComponent<UIDataManager>().UpdateDiamonds();
  }

  public void BuyDiamonds(int index){
    //In-App purchase
    RefreshButtons();
    GetComponent<UIDataManager>().UpdateDiamonds();
  }

  public void RefreshButtons(){
    for(int i = 0; i < PUButtons.Count; i++){
			if(GameManager.instance.gmCoins - Prices.PU[i].price >= 0){
        PUButtons[i].interactable = true;
      }else
        PUButtons[i].interactable = false;
		}

    for(int i = 0; i < EnergyButtons.Count; i++){
			if(GameManager.instance.gmCoins - Prices.Energy[i].price >= 0){
        EnergyButtons[i].interactable = true;
      }else
        EnergyButtons[i].interactable = false;
		}
  }
 
/*    public void Next(){ 
    if(actualPage < (totalPages-2)){ 
      if (!nextBtn.interactable){ 
        nextBtn.interactable = true; 
      } 
      actualPage++; 
    }else{ 
      nextBtn.interactable = false; 
    } 
  } 
 
  public void Prev(){ 
    if(actualPage > 0){ 
      if(!prevBtn.interactable){ 
        prevBtn.interactable = true;   
      } 
      actualPage--; 
    }else{ 
      prevBtn.interactable = false; 
    } 
  }*/ 
}
