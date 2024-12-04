using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private bool canBear;
    private Common c;

    private void Init() => canBear = true;

    private void Start()
    {
        c = GetComponent<Common>();
        Init();
        if (c.IsOriginal)
        {
            c.Create(gameObject, "center", 0, -c.Get_limit("Wall", "y"), 1, 0);
        }
    }

    private void Update()
    {
        if (!c.IsOriginal)
        {
            if (!c.Get_aboutWall("Out", "under"))
            {
                c.Move(Vector2.down);
            }
            else
            {
                Destroy(gameObject);
            }
            if (canBear && !c.Get_aboutWall("Wall", "top"))
            {
                c.Create(gameObject, "center", 0, c.Get_limit("Out", "y"), 1, 0);
                canBear = false;
            }
        }
    }
}
