
using UnityEngine;
//source code: https://www.minapecheux.com/articles/2022/06/30_making-a-hacknslash-14-creating-an-in-game-menu-adding-item-details/
public static class Graphics {
    public static Color BrightenColor(Color c, float amount = 0.35f){
        float r = Mathf.Clamp01(c.r + amount);
        float g = Mathf.Clamp01(c.g + amount);
        float b = Mathf.Clamp01(c.b + amount);

        return new Color(r,g,b, c.a);
    }

    public static uint ToValue(this Color32 c32, bool includeAlpha = false){
        if(!includeAlpha) return ((uint)c32.r << 16) | ((uint)c32.g << 8) | (uint)c32.b;
        return ((uint)c32.r << 24) | ((uint)c32.g << 16) | ((uint)c32.b << 8) | (uint)c32.a;
    }
}