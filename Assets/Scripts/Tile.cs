using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Texture[] roadTextures;
    [SerializeField] private Texture[] terrainTextures;
    [SerializeField] private Transform[] buildingPartPrefabs;
    
    public Tile northNeighbour;
    public Tile westNeighbour;
    public Tile southNeighbour;
    public Tile eastNeighbour;

    public TerrainType terrainType;
    public RoadType roadType;

    private MeshRenderer meshRenderer;
    private int height;
    
    
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        terrainType = TerrainType.Ground;
    }

    private void Update()
    {
        //TODO: optymalizacja - robic zmiany tylko kiedy to potrzebne
        if (terrainType == TerrainType.Road)
        {
            UpdateRoadTexture();
        }
        else if (terrainType == TerrainType.Ground)
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            materialPropertyBlock.SetTexture("_MainTex", terrainTextures[0]);
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
        else if (terrainType == TerrainType.Building)
        {
            // TODO: do osobnej metody? 
            //MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            //materialPropertyBlock.SetTexture("_MainTex", terrainTextures[0]);
            //meshRenderer.SetPropertyBlock(materialPropertyBlock);
            

        }
        else
        {
            Debug.Assert(false, "Not supported Terrain Type!");
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && !IsOnBoarder())
        {
            if (Input.GetKey(KeyCode.R))
                terrainType = TerrainType.Road;
            else if (Input.GetKey(KeyCode.G))
                terrainType = TerrainType.Ground;
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetKey(KeyCode.B))
        {
            if (terrainType != TerrainType.Building)
            {
                terrainType = TerrainType.Building;
                Transform building = Instantiate(buildingPartPrefabs[0]);
                building.SetParent(transform, true);
                building.localPosition = Vector3.zero;
                building.localScale = Vector3.one;
                height = 0;
            }
            else
            {
                height += 1;
                Transform building = Instantiate(buildingPartPrefabs[1]);
                building.transform.SetParent(transform, true);
                building.localPosition = new Vector3(0f, 0f, -height);
                building.localScale = Vector3.one;
            }
            
        }
    }

    private bool IsOnBoarder()
    {
        return northNeighbour == null || westNeighbour == null || southNeighbour == null || eastNeighbour == null;
    }


    private void UpdateRoadTexture()
    {
        PickCorrectRoadType();
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        materialPropertyBlock.SetTexture("_MainTex", roadTextures[(int)roadType]);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
    
    private void PickCorrectRoadType()
    {
        if (northNeighbour.terrainType == TerrainType.Road && southNeighbour.terrainType == TerrainType.Road && 
            westNeighbour.terrainType != TerrainType.Road && eastNeighbour.terrainType != TerrainType.Road)
        {
            roadType = RoadType.Vertical;
        }
        
        if (northNeighbour.terrainType != TerrainType.Road && southNeighbour.terrainType != TerrainType.Road && 
            westNeighbour.terrainType == TerrainType.Road && eastNeighbour.terrainType == TerrainType.Road)
        {
            roadType = RoadType.Horizontal;
        }
        
        if (northNeighbour.terrainType != TerrainType.Road && southNeighbour.terrainType == TerrainType.Road && 
            westNeighbour.terrainType == TerrainType.Road && eastNeighbour.terrainType != TerrainType.Road)
        {
            roadType = RoadType.CornerLu;
        }
        
        if (northNeighbour.terrainType != TerrainType.Road && southNeighbour.terrainType == TerrainType.Road && 
            westNeighbour.terrainType != TerrainType.Road && eastNeighbour.terrainType == TerrainType.Road)
        {
            roadType = RoadType.CornerRu;
        }
        
        if (northNeighbour.terrainType == TerrainType.Road && southNeighbour.terrainType != TerrainType.Road && 
            westNeighbour.terrainType == TerrainType.Road && eastNeighbour.terrainType != TerrainType.Road)
        {
            roadType = RoadType.CornerLd;
        }
        
        if (northNeighbour.terrainType == TerrainType.Road && southNeighbour.terrainType != TerrainType.Road && 
            westNeighbour.terrainType != TerrainType.Road && eastNeighbour.terrainType == TerrainType.Road)
        {
            roadType = RoadType.CornerRd;
        }
        
        if (northNeighbour.terrainType != TerrainType.Road && southNeighbour.terrainType == TerrainType.Road && 
            westNeighbour.terrainType == TerrainType.Road && eastNeighbour.terrainType == TerrainType.Road)
        {
            roadType = RoadType.TjunctionU;
        }
        
        if (northNeighbour.terrainType == TerrainType.Road && southNeighbour.terrainType == TerrainType.Road && 
            westNeighbour.terrainType != TerrainType.Road && eastNeighbour.terrainType == TerrainType.Road)
        {
            roadType = RoadType.TjunctionL;
        }
        if (northNeighbour.terrainType == TerrainType.Road && southNeighbour.terrainType != TerrainType.Road && 
            westNeighbour.terrainType == TerrainType.Road && eastNeighbour.terrainType == TerrainType.Road)
        {
            roadType = RoadType.TjunctionD;
        }
        
        if (northNeighbour.terrainType == TerrainType.Road && southNeighbour.terrainType == TerrainType.Road && 
            westNeighbour.terrainType == TerrainType.Road && eastNeighbour.terrainType != TerrainType.Road)
        {
            roadType = RoadType.TjunctionR;
        }
        
        if (northNeighbour.terrainType == TerrainType.Road && southNeighbour.terrainType == TerrainType.Road && 
            westNeighbour.terrainType == TerrainType.Road && eastNeighbour.terrainType == TerrainType.Road)
        {
            roadType = RoadType.Intersection;
        }
        
        
    }


    
}