using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using UnityEngine.EventSystems;

public class ToolTipWriter : FoCsBehavior, IPointerEnterHandler, IPointerExitHandler
{
	public StringReference Tooltip;
	public Placer Info;

	public void OnPointerEnter(PointerEventData eventData)
	{
		Tooltip.Value = Info.Info;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Tooltip.Value = "";
	}
}
