using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] PlayerConfig config;
    

    public void UpdateStaminaBar(float stamina)
    {
        slider.value = stamina/config.maxStamina;
    }

}
