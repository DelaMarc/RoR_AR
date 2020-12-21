using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollList : MonoBehaviour
{
    [SerializeField] GameObject listContent;
    [SerializeField] GameObject itemListPrefab;
    [SerializeField] AutoTween autoTween;

    public void Init(EntityData[] a_entities)
    {
        //populate the scrollable list at start
        GameObject newItem;
        Text tmpTxt;
        
        //initialize list items
        for (int i = 0; i < a_entities.Length; ++i)
        {
            //set new item with image
            newItem = Instantiate(itemListPrefab, listContent.transform);
            tmpTxt = newItem.GetComponentInChildren<Text>();
            tmpTxt.text = a_entities[i].Name;
            newItem.name = a_entities[i].Name;
            Addlisteners(i, newItem);
            //update Object Manager List
            //ModelManager.instance.AddModel(items[i]);
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
