using UnityEngine;
using UnityEngine.Tilemaps;

public enum BC_TileToughness
{
    SOFT, HARD, UNBREAKABLE
}

public class BC_MapTile : Tile
{
    public BC_TileToughness toughness;

    #if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a Dynamic Tile Asset
    [UnityEditor.MenuItem("Assets/Create/BC_MapTile")]
    public static void CreateBC_MapTile()
    {
        string path = UnityEditor.EditorUtility.SaveFilePanelInProject(
            "Save Map Tile", "New Map Tile", "Asset", "Save Map Tile", "Assets"
        );
        if (path == "")
            return;
        UnityEditor.AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BC_MapTile>(), path);
    }
#endif
}