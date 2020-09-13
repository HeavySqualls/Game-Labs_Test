using System.Text;
using UnityEngine;
using TMPro;
using Kryz.CharacterStats;


public class StatTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statModifiersLabelText;
    [SerializeField] TextMeshProUGUI modifiersText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(CharacterStat stat, string statName)
    {
        statNameText.text = GetStatTopText(stat, statName);
        modifiersText.text = GetStatModifiersText(stat);
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private string GetStatTopText(CharacterStat stat, string statName)
    {
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stat.Value);
        sb.Append(" (");
        sb.Append(stat.BaseValue);

        if (stat.Value > stat.BaseValue)
        {
            sb.Append(" + ");
        }

        sb.Append(stat.Value - stat.BaseValue);
        sb.Append(")");

        return sb.ToString();
    }

    private string GetStatModifiersText(CharacterStat stat)
    {
        sb.Length = 0;

        foreach (StatModifier mod in stat.StatModifiers)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (mod.Value > 0)
            {
                sb.Append("+");
            }

            sb.Append(mod.Value);

            sEquipment item = mod.Source as sEquipment;

            if (item != null)
            {
                sb.Append(" ");
                sb.Append(item.itemName);
            }
            else
            {
                Debug.LogError("Modifier is not an Equippable item!");
            }
        }

        return sb.ToString();
    }
    
}
