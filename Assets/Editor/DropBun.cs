using UnityEngine;

public class DropBun : MonoBehaviour
{
    public GameObject spritePrefab; 
    public Transform spawnParent; 

    public void SpawnSprite()
    {
        Debug.Log("Button pressed via script!");
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

        GameObject newSprite = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);

        if (spawnParent != null)
        {
            newSprite.transform.parent = spawnParent;
        }
    }
}