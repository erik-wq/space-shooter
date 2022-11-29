using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        if (spriteRenderer.sprite == null) 
            spriteRenderer.sprite = sprite1;
    }

    public void ChangeSprite()
    {
        if (spriteRenderer.sprite == sprite1) 
        {
            spriteRenderer.sprite = sprite2;
        }
        else if (spriteRenderer.sprite == sprite2)
        {
            spriteRenderer.sprite = sprite3;
        }
        else if(spriteRenderer.sprite == sprite3)
        {
            spriteRenderer.sprite = sprite4;
        }
        else if(spriteRenderer.sprite == sprite4)
        {
            spriteRenderer.sprite = sprite5;
        }
        else if(spriteRenderer.sprite == sprite5)
        {
            spriteRenderer.sprite = sprite1;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }
    }
}
