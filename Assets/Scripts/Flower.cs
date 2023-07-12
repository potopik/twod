using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flower : Entity
{
    [SerializeField] private int lives = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.Instance.gameObject) 
        {
            player.Instance.GetDamage();
            lives--;
            Debug.Log("У растения " + lives);
        }

        if (lives < 1)
        Die();
    }

}
