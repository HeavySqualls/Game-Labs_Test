using System.Text;
using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemSlotText;
    [SerializeField] TextMeshProUGUI itemStatsText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(sEquipment item)
    {
        itemNameText.text = item.itemName;
        itemSlotText.text = item.equipmentType.ToString();

        sb.Length = 0;
        AddStat(item.HPBoost, " HP");
        AddStat(item.ShieldBoost, " Shield");
        AddStat(item.ShieldRegenBonus, " Regen", isPercent: true);
        AddStat(item.WeaponReloadBonus, " Reload", isPercent: true);
        AddStat(item.damage, " Damage");
        AddStat(item.reloadTime, " Seconds Reload Time");
        itemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (value > 0)
            {
                sb.Append(" + ");
            }

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }

            sb.Append(statName);
        }
    }
}
