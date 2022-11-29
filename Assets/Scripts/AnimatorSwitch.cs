using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSwitch : MonoBehaviour
{

    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    public RuntimeAnimatorController anim3;
    public RuntimeAnimatorController anim4;
    public RuntimeAnimatorController anim5;

    public Animator anim;

    public SpriteRenderer spriteRenderer2;
    public ChangeColor changeColor;

    void Start()
    {
        
    }

    void Update()
    {
        spriteRenderer2 = GetComponent<SpriteRenderer>();
        spriteRenderer2.sprite = changeColor.spriteRenderer.sprite;

        

        ChangeAnimation();
    }

    public void ChangeAnimation()
    {
        if (spriteRenderer2.sprite == changeColor.sprite1)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim1 as RuntimeAnimatorController;
        }
        else if (spriteRenderer2.sprite == changeColor.sprite2)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim4 as RuntimeAnimatorController;
        }
        else if (spriteRenderer2.sprite == changeColor.sprite3)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim5 as RuntimeAnimatorController;
        }
        else if (spriteRenderer2.sprite == changeColor.sprite4)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
        }
        else if (spriteRenderer2.sprite == changeColor.sprite5)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim3 as RuntimeAnimatorController;

        }

    }
}
