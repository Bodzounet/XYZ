using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemData : MonoBehaviour
{
    public enum e_dir
    {
        Left = -1,
        Right = 1
    }

    public Data[] items;

    public GameObject holder;
    public Text description;
    public SpriteRenderer illustration;

    private int currentItem;

    void Start()
    {
        currentItem = 0;
    }

    public void switchToNextItem(e_dir dir)
    {
        description.text = items[currentItem].description;
        illustration.sprite = items[currentItem].illustration;

        if (dir == e_dir.Left)
        {
            currentItem--;
            if (currentItem < 0)
            {
                currentItem = items.Length - 1;
            }
        }
        else
        {
            currentItem++;
            if (currentItem >= items.Length)
            {
                currentItem = 0;
            }
        }
    }
    
    [System.Serializable]
    public struct Data
    {
        public string description;
        public Sprite illustration;

        public Data(string description_, Sprite illustration_)
        {
            description = description_;
            illustration = illustration_;
        }
    }
}
