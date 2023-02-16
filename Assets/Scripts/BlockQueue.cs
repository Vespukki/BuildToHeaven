using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BlockQueue : MonoBehaviour
{
    //front is 0, back is blocks.length
    public List<Block> blocks;

    public Block uBlock;
    private void Start()
    {
        //StartCoroutine(StartBlockPlacement());
        AddToFront(uBlock);
    }

    IEnumerator StartBlockPlacement()
    {
        while(blocks.Count > 0)
        {
            PlaceNext();
            yield return new WaitForSeconds(5);
        }
    }

    public void PlaceNext()
    {
        Instantiate(blocks[0], new Vector3(0, 5, 0), Quaternion.identity);
        blocks.RemoveAt(0);
    }

    public void AddToFront(Block block)
    {
        blocks.Insert(0, block);
    }
}
