using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollList : MonoBehaviour
{
    [SerializeField]
    GameObject listContent;
    [SerializeField]
    GameObject itemListPrefab;
    [SerializeField]
    List<EntityData> items;
    [SerializeField]
    AutoTween autoTween;

    //populate the scrollable list at start
    void Start()
    {
        StartCoroutine(InitItems());
    }

    IEnumerator InitItems()
    {
        GameObject newItem;
        Text tmpTxt;
        //wait for model manager to be initialized
        while (ModelManager.instance == null)
        {
            yield return null;
        }
        //initialize list items
        for (int i = 0; i < items.Count; ++i)
        {
            //set new item with image
            newItem = Instantiate(itemListPrefab, listContent.transform);
            tmpTxt = newItem.GetComponentInChildren<Text>();
            tmpTxt.text = items[i].Name;
            newItem.name = items[i].Name;
            Addlisteners(i, newItem);
            //update Object Manager List
            ModelManager.instance.AddModel(items[i]);
        }
        //set first model in model manager
        ModelManager.instance.SelectModel(0);
        //disable gameobject
        gameObject.SetActive(false);
    }

    //add the listeners separately, with the index value copied
    void Addlisteners(int index, GameObject item)
    {
        int staticIndex = index;

        item.GetComponent<Button>().onClick.AddListener(() => {
            autoTween.Toggle();
            ModelManager.instance.SelectModel(staticIndex);
        });
    }

}
