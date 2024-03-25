using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiveStep : MonoBehaviour, IStep
{
    public string TitleName => "иои╚";


    public Paint Paint;
    public ColorPicker Picker;
    public ColorPreview ColorPreview;
    private void Awake()
    {
        Picker.onColorChanged += (x) =>
        {
            Paint.ColorValue = x;
        };


    }

    public void SetShowState()
    {
        gameObject.SetActive(true);
        Picker.gameObject.SetActive(false);
        ColorPreview.gameObject.SetActive(false);
    }

}
