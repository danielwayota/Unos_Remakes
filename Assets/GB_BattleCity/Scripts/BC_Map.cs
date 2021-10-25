using UnityEngine;
using UnityEngine.Tilemaps;

public class BC_Map : MonoBehaviour, BC_IBulletTarget
{
    private Tilemap destructibleMap;

    void Start()
    {
        this.destructibleMap = this.GetComponent<Tilemap>();
    }

    public void PerdoneUstedLeHeGolpeado(Vector3 position)
    {
        // Esquina y tamaño de la explosión. Se usa luego para destruir los Tiles
        Vector3Int corner = Vector3Int.RoundToInt(position - Vector3.one);
        Vector3Int size = Vector3Int.RoundToInt(Vector3.one * 2f);

        bool done = false;

        for (int x = corner.x; x < (corner.x + size.x) && !done; x++)
        {
            for (int y = corner.y; y < (corner.y + size.y) && !done; y++)
            {
                var localTilePosition = new Vector3Int(x, y, 0);

                var tile = this.destructibleMap.GetTile<BC_MapTile>(localTilePosition);
                if (tile == null)
                    continue;

                if (tile.toughness == BC_TileToughness.SOFT)
                {
                    this.destructibleMap.SetTile(localTilePosition, null);

                    // Destruir solo un tile y terminar.
                    done = true;
                }
            }
        }
    }
}