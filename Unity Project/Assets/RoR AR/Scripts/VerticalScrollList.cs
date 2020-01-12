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
    List<ListItem> items;
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
            tmpTxt.text = items[i].name;
            newItem.name = items[i].name;
            Addlisteners(i, newItem);
            //update Object Manager List
            ModelManager.instance.AddModel(items[i].item);
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
            UIManager.instance.ToggleBlockClick();
            ModelManager.instance.SelectModel(staticIndex);
        });
    }

}
