using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChangeLayer
{
    public static void SetLayerRecursively(this Transform parent, string layer)
    {
        parent.SetLayerRecursively(LayerMask.NameToLayer(layer));
    }

    // Changes layer for child objects as well
    public static void SetLayerRecursively(this Transform parent, int layer)
    {
        parent.gameObject.layer = layer;

        for (int i = 0, count = parent.childCount; i < count; i++)
        {
            parent.GetChild(i).SetLayerRecursively(layer);
        }
    }
}

