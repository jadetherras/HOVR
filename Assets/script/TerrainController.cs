using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField]
    public static bool is_vertigo = false;

    protected bool prev_vertigo = is_vertigo;
    protected Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (is_vertigo){
            
            terrain.transform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (prev_vertigo != is_vertigo && is_vertigo){
            terrain.transform.gameObject.SetActive(true);
        }
    }
}
