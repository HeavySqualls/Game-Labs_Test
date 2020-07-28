﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Kryz.CharacterStats;

public class StatsDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStat _stat;
    public CharacterStat Stat { 
        get { return _stat; }

        set
        {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            nameText.text = _name;
        }
    }

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI valueText;

    [SerializeField] StatTooltip tooltip;

    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        nameText = texts[0];
        valueText = texts[1];

        if (tooltip == null)
        {
            tooltip = FindObjectOfType<StatTooltip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
    }
}
