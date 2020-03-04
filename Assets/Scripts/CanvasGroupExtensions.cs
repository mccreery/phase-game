using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasGroupExtensions
{
    public static void SetVisible(this CanvasGroup canvasGroup, bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.blocksRaycasts = visible;
        canvasGroup.interactable = visible;
    }

    public static bool IsVisible(this CanvasGroup canvasGroup)
    {
        return canvasGroup.alpha > 0;
    }
}
