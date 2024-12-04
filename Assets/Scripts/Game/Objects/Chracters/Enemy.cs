using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp_, bullet_angle_start, bullet_angle_plus, bullet_angle_end, type;
    public GameObject bullet, item1;
    private int hp;
    private Common c;

    private void Init() => hp = hp_;

    private void Start()
    {
        c = GetComponent<Common>();
        Init();
    }

    private void Update()
    {
        if (!c.IsOriginal)
        {
            if (hp > 0 && !c.Get_aboutWall("Out", "under"))
            {
                c.Move(Vector2.down);
                if (c.Cooltime_Trigger(0, true, true))
                {
                    GameManager.instance.Get_sound_str("EnemyShot");
                    for (int dir = bullet_angle_start; dir < bullet_angle_end; dir += bullet_angle_plus)
                    {
                        c.Create(bullet, "me", 0, 0, 0, dir);
                    }
                }
            }
            else
            {
                if (hp <= 0)
                {
                    if (type == 0)
                    {
                        GameManager.instance.Plus_count_eliteAppear_one();
                    }
                    GameManager.instance.Plus_score((type == 0) ? 1 : 5);
                    if (Random.Range(0, 4) == 0)
                    {
                        c.Create(item1, "me", 0, 0, 0, 270);
                    }
                }
                Destroy(gameObject);
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!c.IsOriginal)
        {
            if (collision.CompareTag("P_Bullet"))
            {
                GameManager.instance.Get_sound_str("EnemyAttack");
                hp--;
            }
        }
    }
}
