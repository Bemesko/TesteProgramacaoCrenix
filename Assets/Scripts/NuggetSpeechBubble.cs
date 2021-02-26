using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NuggetSpeechBubble : MonoBehaviour
{
    private TextMeshProUGUI _speechText;

    private void Awake()
    {
        _speechText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GearSlotManager.OnAllSlotsFilled += ChangeText;
    }

    private void OnDisable()
    {
        GearSlotManager.OnAllSlotsFilled -= ChangeText;
    }

    void ChangeText(bool isSlotsFilled)
    {
        Debug.Log("Change Text");
        if (isSlotsFilled)
        {
            _speechText.SetText("Yay, parabéns. Task concluída!");
        }
        else
        {
            _speechText.SetText("Encaixe as engrenagens em qualquer ordem");
        }
    }
}
