using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light light2D;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
    
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on ItemWorld GameObject!");
        }

        light2D = GetComponent<Light>();
        if (light2D == null)
        {
            Debug.LogError("Light2D component not found on ItemWorld GameObject!");
        }

        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component not found on ItemWorld GameObject!");
        }
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        light2D.color = item.GetColor();

        if (item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

 
    private static ItemWorld CreateItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pFItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        return itemWorld;
    }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        ItemWorld itemWorld = CreateItemWorld(position, item);
        itemWorld.SetItem(item);
        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = Random.insideUnitCircle.normalized;
        ItemWorld itemworld = CreateItemWorld(dropPosition + randomDir * 5f, item);
        itemworld.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
        return itemworld;
    }
}
