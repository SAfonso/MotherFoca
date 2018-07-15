using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sfx : MonoBehaviour
{

    public static Sfx instance;
    public AudioSource StartMusic, IngameMusic;
    public AudioSource WinSceneSound, PanelSound;
    public AudioSource okbutton, cancelbutton, menubutton;
    public AudioSource swipe, fall, collectstar, teleport;

    [Header("Mute SFX")]
    public Toggle MuteMusic;
    public Toggle MuteSound;
    [SerializeField] private bool MusicOn, SfxOn;

    void Awake() {
        instance = this;
        //
        ToggleMusic();
        ToggleSFX();
    }

    private void OnLevelWasLoaded(int level) {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            instance.gameObject.SetActive(false);
        }
        else
        {
            if (!instance.gameObject.activeSelf)
            {
                instance.gameObject.SetActive(true);
            }
        }

    }

    public void ToggleSFX() {
        SfxOn = MuteSound.isOn ? true : false;
        if (SfxOn == false) MuteAllSFX();
    }
    public void ToggleMusic() {
        MusicOn = MuteMusic.isOn ? true: false;
        if (MusicOn == false) MuteAllMusic();
    }

    public void MuteAllMusic() {
        StopBackGroundMusic(StartMusic);
        StopBackGroundMusic(IngameMusic);
    }

    public void MuteAllSFX() {
        StopSound(WinSceneSound);
        StopSound(PanelSound);
        StopSound(okbutton);
        StopSound(cancelbutton);
        StopSound(menubutton);
        StopSound(swipe);
        StopSound(fall);
        StopSound(collectstar);
        StopSound(teleport);
    }
    
    private void PlayBackGroundMusic(AudioSource backgroundMusic) {
        if (!backgroundMusic.isPlaying && backgroundMusic!= null)
        {
            backgroundMusic.Play();
        }
    }
    private void StopBackGroundMusic(AudioSource backgroundMusic) {
        if (backgroundMusic!=null)  backgroundMusic.Stop();
    }

    public void PlayStartMusic() {
        if (MusicOn == true) PlayBackGroundMusic(StartMusic);
    }
    public void StopStartMusic() {
        if (MusicOn == true) StopBackGroundMusic(StartMusic);
    }
    public void PlayIngameMusic() {
        if (MusicOn == true) PlayBackGroundMusic(IngameMusic);
    }
    public void StopIngameMusic() {
        if (MusicOn == true) StopBackGroundMusic(IngameMusic);
    }


    private void PlaySound(AudioSource mySound) {
        if (!mySound.isPlaying && mySound != null) {
            mySound.Play();
        }
    }
    private void StopSound(AudioSource mySound) {
        if (mySound != null) mySound.Stop();
    }

    public void PlayWinSound() {
        if (SfxOn == true) PlaySound(WinSceneSound);
    }
    public void StopWinSound() {
        if (SfxOn == true) StopSound(WinSceneSound);
    }
    public void PlayPanelSound()
    {
        if (SfxOn == true) PlaySound(PanelSound);
    }
    public void StopPanelSound()
    {
        if (SfxOn == true) StopSound(PanelSound);
    }

    public void PlayokbuttonSound()
    {
        if (SfxOn == true) PlaySound(okbutton);
    }
    public void StopokbuttonSound()
    {
        if (SfxOn == true) StopSound(okbutton);
    }
    public void PlaycancelbuttonSound()
    {
        if (SfxOn == true) PlaySound(cancelbutton);
    }
    public void StopcancelbuttonSound()
    {
        if (SfxOn == true) StopSound(cancelbutton);
    }
    public void PlaymenubuttonSound()
    {
        if (SfxOn == true) PlaySound(menubutton);
    }
    public void StopmenubuttonSound()
    {
        if (SfxOn == true) StopSound(menubutton);
    }

    public void PlayswipeSound()
    {
        if (SfxOn == true) PlaySound(swipe);
    }
    public void StopswipeSound()
    {
        if (SfxOn == true) StopSound(swipe);
    }
    public void PlayfallSound()
    {
        if (SfxOn == true) PlaySound(fall);
    }
    public void StopfallSound()
    {
        if (SfxOn == true) StopSound(fall);
    }
    public void PlaycollectstarSound()
    {
        if (SfxOn == true) PlaySound(collectstar);
    }
    public void StopcollectstarSound()
    {
        if (SfxOn == true) StopSound(collectstar);
    }
    public void PlayteleportSound()
    {
        if (SfxOn == true) PlaySound(teleport);
    }
    public void StopteleportSound()
    {
        if (SfxOn == true) StopSound(teleport);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) PlaymenubuttonSound();
    }

}
