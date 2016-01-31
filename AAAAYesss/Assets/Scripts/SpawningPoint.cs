using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class SpawningPoint : MonoBehaviour {
    
    public const string BASE_TAG = "spawnPoint";
    
    private const string ITEM_TAG = "item";
    private const string ITEM_POOL_TAG = "itemPool";
    
    public int objectsToCollectCount = 3;

	public GameObject gameObjectToSpawn;
    private string[] objectsToCollect = new string[0];
    private bool[] isItemCollectedArray = new bool[0];
    
    private Collider2D colliderReference;
    
    public void Start(){
        colliderReference = GetComponent<Collider2D>();
        
        var poolGameObject = GameObject.FindGameObjectWithTag(ITEM_POOL_TAG);
        if(poolGameObject != null){
            var itemPool = poolGameObject.GetComponent<ItemPool>();
            
            List<GameObject> gameItems = new List<GameObject>(itemPool.gameObjectsToSpawn);
            
            objectsToCollect = new string[objectsToCollectCount];
            isItemCollectedArray = new bool[objectsToCollectCount];
            
            for(int i=0; i< objectsToCollectCount; ++i){
                int index = UnityEngine.Random.Range(0, gameItems.Count);
                objectsToCollect[i] = gameItems[index].name;
                gameItems.RemoveAt(index);
            }
        }
        
        if(gameObjectToSpawn != null){
            Instantiate(gameObjectToSpawn, transform.position + new Vector3(0, 0, Player.PLAYER_SPAWNING_DEPTH), Quaternion.identity);
        }
    }

	public void DroppedItem(GameObject item) 
	{
		if (item.CompareTag (ITEM_TAG) && IsWithinBounds(item))
		{
			int index = FindIndexOf(objectsToCollect, item.name);
            if(index >= 0){
                isItemCollectedArray[index] = true;
                CheckIfCanSummonDemon();
            }
		}
	}
    
    public void PickedUpItem(GameObject item){
        if (item.CompareTag (ITEM_TAG))
		{
            int index = FindIndexOf(objectsToCollect, item.name);
            if(index >= 0){
                isItemCollectedArray[index] = false;
            }
		}
    }

    private int FindIndexOf(string[] objectsToCollect, string name)
    {
        int index = -1; 
        for(int i = 0; i < objectsToCollect.Length && index == -1; ++i){
            if(name.Contains(objectsToCollect[i])){
                index = i;
            }
        }
        
        return index;
    }

    private bool IsWithinBounds(GameObject item)
    {
        return colliderReference.OverlapPoint(item.transform.position);
    }

    private void CheckIfCanSummonDemon()
    {
        bool areAllItemsCollected = true;
        for (int i=0; i< isItemCollectedArray.Length && areAllItemsCollected; ++i){
            areAllItemsCollected &= isItemCollectedArray[i];
        }
        
        if(areAllItemsCollected){
            SceneManager.LoadScene("MainScene");
        }
    }
}
