using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {

	[SerializeField]
	Canvas ShopCanvas;

    [Header("START SCENE")]
    [SerializeField]
    CanvasGroup StartScene_FadeInOut;
    [SerializeField]
    [Range(0.2f, 2f)]
    private float StartScene_FadeInTime = 0.75f;

    [Header("Options Panel")]
    [SerializeField]
    CanvasGroup OptionsPanel;
    [SerializeField]
    [Range(0.2f, 2f)]
    private float OptionsPanel_Time = 0.4f;

    [Header("NoPU Panel")]
    [SerializeField]
    CanvasGroup NoPUPanel;
    [SerializeField]
    [Range(0.2f, 2f)]
    private float NoPUPanel_Time = 0.4f;

    [Header("NoEnergy Panel")]
    [SerializeField]
    CanvasGroup NoEnergyPanel;
    [SerializeField]
    [Range(0.2f, 2f)]
    private float NoEnergyPanel_Time = 0.4f;

    [Header("NoDiamonds Panel")]
    [SerializeField]
    CanvasGroup NoDiamondsPanel;
    [SerializeField]
    [Range(0.2f, 2f)]
    private float NoDiamondsPanel_Time = 0.4f;

    [Header("ReloadLevel Panel")]
    [SerializeField]
    CanvasGroup ReloadLevelPanel;
    [SerializeField]
    [Range(0.2f, 2f)]
    private float ReloadLevelPanel_Time = 0.4f;

    [Header("LevelPreview Panel")]
    [SerializeField]
    CanvasGroup LevelPreviewPanel;
    [SerializeField]
    [Range(0.2f, 2f)]
    private float LevelPreviewPanel_Time = 0.4f;

    public GameObject starsGroup;


    //------------------------------------

    //Open and close

    //StartScene

    public void OpenStartScene() {
        if (StartScene_FadeInOut != null) 
            StartScene_FadeInOut.DOFade(0f, StartScene_FadeInTime);
    }

    //Options
	public void OpenOptions(){
        Debug.Log("Hola" + (OptionsPanel != null));
        if (OptionsPanel != null)  
            SetPanelVisibility( OptionsPanel, true, OptionsPanel_Time);
	}

	public void CloseOptions(){
       if (OptionsPanel != null) 
        SetPanelVisibility( OptionsPanel, false, OptionsPanel_Time);
	}

    //NoPu

	public void OpenNoPU(){
        //SetPanelVisibility( NoPUPanel.GetComponent<CanvasGroup>(), true);
        if (NoPUPanel != null) SetPanelVisibilityScale( NoPUPanel, true, NoPUPanel_Time);
	}

	public void CloseNoPU(){
        // SetPanelVisibility( NoPUPanel.GetComponent<CanvasGroup>(), false);
        if (NoPUPanel != null) 
            SetPanelVisibilityScale( NoPUPanel, false, NoPUPanel_Time);
	}

    //NoEnergy

	public void OpenNoEnergy(){
        //SetPanelVisibility( NoEnergyPanel.GetComponent<CanvasGroup>(), true);
        if (NoEnergyPanel != null) 
            SetPanelVisibilityScale( NoEnergyPanel, true, NoEnergyPanel_Time);
	}

	public void CloseNoEnergy(){
        //SetPanelVisibility( NoEnergyPanel.GetComponent<CanvasGroup>(), false);
        if (NoEnergyPanel != null) 
            SetPanelVisibilityScale( NoEnergyPanel, false, NoEnergyPanel_Time);
	}

    //ReloadLevel

	public void OpenReloadLevel(){
        //SetPanelVisibility( ReloadLevelPanel.GetComponent<CanvasGroup>(), true);
        if (ReloadLevelPanel != null) 
            SetPanelVisibilityScale( ReloadLevelPanel, true, ReloadLevelPanel_Time);
	}

	public void CloseReloadLevel(){
        //SetPanelVisibility( ReloadLevelPanel.GetComponent<CanvasGroup>(), false);
        if (ReloadLevelPanel != null) 
            SetPanelVisibilityScale( ReloadLevelPanel, false, ReloadLevelPanel_Time);
	}

    //NoDiamonds

    public void OpenNoDiamonds(){
        //SetPanelVisibility( NoDiamondsPanel.GetComponent<CanvasGroup>(), true);
        if (NoDiamondsPanel != null) SetPanelVisibilityScale( NoDiamondsPanel, true, NoDiamondsPanel_Time);
	}

	public void CloseNoDiamonds(){
        //SetPanelVisibility( NoDiamondsPanel.GetComponent<CanvasGroup>(), false);
        if (NoDiamondsPanel != null) SetPanelVisibilityScale( NoDiamondsPanel, false, NoDiamondsPanel_Time);
	}

    //LevelPreview

    public void OpenLevelPreview(){
        //SetPanelVisibility( NoDiamondsPanel.GetComponent<CanvasGroup>(), true);
        if (LevelPreviewPanel != null) SetPanelVisibilityScale( LevelPreviewPanel, true, LevelPreviewPanel_Time);
	}

	public void CloseLevelPreview(){
        //SetPanelVisibility( NoDiamondsPanel.GetComponent<CanvasGroup>(), false);
        if (LevelPreviewPanel != null) SetPanelVisibilityScale(LevelPreviewPanel, false, LevelPreviewPanel_Time);
	}

    //SelectPU

	public void SelectPU(){
		if(GameManager.instance.gmPowerUps > 0){
			//seleccionar power up
		}
		else
			OpenNoPU();
	}

    public void SelectPUInGame(){
		if(GameManager.instance.gmPowerUps > 0){
			OpenReloadLevel();
		}
		else
			OpenNoPU();
	}

    // ---------------------------

    //Change level

	public void StarLevel(){
		if(GameManager.instance.gmEnergy > 0){
			FindObjectOfType<LevelPreview>().LoadLevel();
		}else
			OpenNoEnergy();
	}

    public void ReloadLevel(){
        if(GameManager.instance.gmEnergy > 0){
			Loader.instance.LoadLevel();
		}else
			OpenNoEnergy();
    }

    public void NextLevel(){
        Loader.instance.LoadMap();
    }

    public void GoToMap(){
        Loader.instance.ReLoadMap();
    }

    public void ExitApp(){
        //Cerrar app
    }

    //----------------------------------------------

    //Shop
    //Open and close

	public void OpenShop(){
		ShopCanvas.enabled = true;
		this.gameObject.GetComponent<ShopManager>().PrepareEscene(Constants.actualPage);
	}

	public void OpenShop(int page){
		ShopCanvas.enabled = true;
		this.gameObject.GetComponent<ShopManager>().PrepareEscene(page);
	}
	
	public void CloseShop(){
		ShopCanvas.enabled = false;
	}

    //

	public void PayEnergy(){
		if(GameManager.instance.gmCoins >= 20){
			GameManager.instance.gmCoins -= 20;
            GameManager.instance.gmEnergy++;
		    Loader.instance.LoadLevel();
		}else{
			OpenNoDiamonds();
		}
	}

    //----------------------------------------------------------

    //Change panel visibility

	public void SetPanelVisibility(CanvasGroup canvasGroup, bool visible){
		if(visible){
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
			canvasGroup.alpha = 1;
		}else{
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.alpha = 0;
		}
	}

    //version con FadeInout y parm de duracion
    public void SetPanelVisibility(CanvasGroup canvasGroup, bool visible, float duration)
    {
        Sequence mySeq = DOTween.Sequence();

        if (visible)
        {
            mySeq.AppendCallback(() => { canvasGroup.interactable = true; canvasGroup.blocksRaycasts = true; });
            mySeq.Append(canvasGroup.DOFade(1f, duration));
        }
        else {
            mySeq.AppendCallback(() => { canvasGroup.interactable = false; canvasGroup.blocksRaycasts = false; });
            mySeq.Append(canvasGroup.DOFade(0f, duration));

        }
    }

    //versión con scale+bounce y parm de duracion
    public void SetPanelVisibilityScale(CanvasGroup canvasGroup, bool visible, float duration) {
        Sequence mySeq = DOTween.Sequence();

        if (visible)
        {
            mySeq.AppendCallback(() => SetPanelVisibility(canvasGroup, true));
            mySeq.Append(canvasGroup.transform.DOScale(Vector3.zero, duration).From());
        }
        else {
            mySeq.Append(canvasGroup.transform.DOScale(Vector3.zero, duration));
            mySeq.AppendCallback(() => SetPanelVisibility(canvasGroup, false));
            mySeq.Append(canvasGroup.transform.DOScale(Vector3.one, 0f));
        }
    }

    public void ShowStars(){
        starsGroup = GameObject.Find("Stars");
        int index = 0;
        for(int i = 0; i <= GameManager.instance.actualLevel.allStars.Length-1; i++){
			if((GameManager.instance.actualLevel.allStars[i].wasCatched) && (index < 3)){
                Debug.Log("Index >> " + index);
                starsGroup.transform.GetChild(index).GetChild(0).GetComponent<Animator>().SetBool("IsStart", true);
                index++;
                StartCoroutine("WhaitToShow");
			}
		}
    }

    IEnumerator WhaitToShow() {
        yield return new WaitForSeconds(1.5f);
    }


  
}
