using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public string target;
    private Common c;

    private void Start()
    {
        c = GetComponent<Common>();
    }

    private void Update()
    {
        if (!c.IsOriginal)
        {
            if (!c.Get_aboutWall("Out", "all"))
            {
                c.Move(Vector2.right);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!c.IsOriginal)
        {
            if (collision.CompareTag(target))
            {
                Destroy(gameObject);
            }
        }
    }
}
