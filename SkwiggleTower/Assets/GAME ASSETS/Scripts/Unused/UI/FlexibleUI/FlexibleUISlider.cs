using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///  Val Script 7/1/19
/// </summary>
[RequireComponent(typeof(Slider))]
public class FlexibleUISlider : FlexibleUI

{
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        base.Initialize();
    }

    protected override void OnSkinUI()
    {
        base.OnSkinUI();
        slider.colors = flexibleUIData.sliderColorBlock;
    }



}
