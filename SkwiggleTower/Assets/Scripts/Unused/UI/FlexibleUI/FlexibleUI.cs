using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Val Script 7/1/19
/// </summary>
[ExecuteInEditMode()]

public class FlexibleUI : MonoBehaviour {

    public FlexibleUIData flexibleUIData;

    protected virtual void OnSkinUI(){}

    public void Initialize()
    {
        OnSkinUI();
    }

    public virtual void Update()
    {
        if (Application.isEditor)
        {
            OnSkinUI();
        }
    }
}
