using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    // For Health Bar that appears above the object

    public GameObject uiPrefab;
    public Transform target;

    Transform ui;
    Image healthSlider;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform; // references camera in scene

        foreach (Canvas C in FindObjectsOfType<Canvas>())
        {
            if (C.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, C.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>(); //Finds image child of prefab
                break;
            }
        }

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position; //Moves with player
            ui.forward = -cam.forward; //
        }
    }
}
