using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityCoolDown : MonoBehaviour
{
    public string abilityButtonAxisName = "Fire1";
    public Image darkMask;
    public TextMeshProUGUI coolDownTextDisplay; //UI display

    [SerializeField]
    private Ability ability;
    [SerializeField]
    private GameObject player;

    private Image myButtonImage;
    private AudioSource abilitySource;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    void Start()
    {
        Initialize(ability, player);
    }

    public void Initialize(Ability selectedAbility, GameObject player)
    {
        ability = selectedAbility; //sets ability in scriptable object
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability.aSprite; //sets sprites in scriptable object
        darkMask.sprite = ability.aSprite;
        coolDownDuration = ability.aBaseCoolDown; //sets value in scriptable object

        ability.Initialize(player);
    }

    // Update is called once per frame
    void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete) //if ability off cooldown
        {
            AbilityReady();
            if (Input.GetButtonDown(abilityButtonAxisName)) //both ready and ability button is pressed
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown(); //increment cooldown if not pressed
        }
    }
    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false; //if ready, disable dark mask
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime; //subtracting time from total time left
        float roundedCd = Mathf.Round(coolDownTimeLeft); //rounded cooldown time (avoid weird number displays)
        coolDownTextDisplay.text = roundedCd.ToString(); 
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration); //calculates fill of ability mask
    }

   

    private void ButtonTriggered() //when they press the fire button
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;

        abilitySource.clip = ability.aSound; //sets sound for the ability triggered
        abilitySource.Play(); 
        ability.TriggerAbility(); 
    }
}
