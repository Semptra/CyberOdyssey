using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacesheep : MonoBehaviour
{
    public int Health;

    public void Damage(int damage)
    {
        Health -= damage;
        Debug.Log($"Spacesheep damaged by: {damage}. Health left: {Health}");
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
