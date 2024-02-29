using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : Menu
{
    public GameObject soundMenuUI;
    public GameObject controlsMenuUI;
    public Slider[] sliders;
    
    void Start() {
        base.Start();
        foreach (Slider slider in sliders) {
            slider.onValueChanged.AddListener(delegate {SliderChange(slider);});
        }
    }

    public void SoundButton() {
        soundMenuUI.SetActive(true);
        controlsMenuUI.SetActive(false);
    }

    public void ControlsButton() {
        soundMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
    }

    public void BackButton() {
        soundMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void SliderChange(Slider slider) {
        Debug.Log(slider);
        Debug.Log(slider.gameObject.transform.parent.GetChild(2));
        slider.gameObject.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text 
            = GetSliderValuePretty(slider.value); // update the text of the slider
    }

    private string GetSliderValuePretty(float value) {
        return (Math.Round(value, 2) * 100).ToString();
    }

}
