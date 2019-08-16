using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Valarie Script: Not FOr Prototype, Multi input device scripts
/// </summary>
public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    int pointerID;
    bool buttonHeld;
    bool buttonPressed;

    void Awake()
    {
        pointerID = -999;
    }

    //When the screen is first touched
    public void OnPointerDown(PointerEventData data)
    {
        if (pointerID != -999)
            return;

        pointerID = data.pointerId;

        buttonHeld = true;
        buttonPressed = true;
    }

    //When the finger leaves the screen
    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId != pointerID)
            return;

        pointerID = -999;
        buttonHeld = false;
        buttonPressed = false;
    }

    //Functions like Input.GetButtonDown()
    public bool GetButtonDown()
    {
        if (buttonPressed)
        {
            buttonPressed = false;
            return true;
        }

        return false;
    }

    //Functions like Input.GetButton()
    public bool GetButton()
    {
        return buttonHeld;
    }
}
