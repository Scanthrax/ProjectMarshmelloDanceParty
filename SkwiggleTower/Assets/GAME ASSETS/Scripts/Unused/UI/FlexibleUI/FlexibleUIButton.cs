using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
///  Val Script 7/1/19
/// </summary>
[RequireComponent (typeof(Button))]
public class FlexibleUIButton : FlexibleUI

{
    private Button button;


    void Awake()
    {
        button = GetComponent<Button>();
        base.Initialize();
    }

    protected override void OnSkinUI()
    {
        base.OnSkinUI();
        button.colors = flexibleUIData.buttonColorBlock;
    }

    
	
}
