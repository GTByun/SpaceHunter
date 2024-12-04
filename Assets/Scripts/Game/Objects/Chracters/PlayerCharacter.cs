using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    public Sprite[] spr_life = new Sprite[3], spr_character = new Sprite[3];
    public GameObject bullet;
    public Image img_life;
    private bool invincible;
    private int life, state_spr;
    private float bullet_plusY;
    private SpriteRenderer sprd;
    private Common c;

    private void Init()
    {
        invincible = false;
        life = 3;
        state_spr = 0;
    }

    private void Set_sprite(int state)
    {
        c.Set_sprite(spr_character[state]);
        state_spr = state;
    }
    private void Set_sprite_nearVincible() => Set_sprite((state_spr == 1) ? 2 : 1);

    private void Simple_Move(KeyCode presser, bool condition, Vector2 dir)
    {
        if (Input.GetKey(presser) && condition)
        {
            c.Move(dir);
        }
    }

    private void Load_GameOver() => SceneManager.LoadScene("GameOver");

    private void Start()
    {
        bullet_plusY = 0.31f;
        c = GetComponent<Common>();
        Init();
    }

    private void Update()
    {
        if (life != 0)
        {
            img_life.sprite = spr_life[life - 1];
            Simple_Move(KeyCode.UpArrow, !c.Get_aboutWall("Wall", "top"), Vector2.up);
            Simple_Move(KeyCode.DownArrow, !c.Get_aboutWall("Wall", "under"), Vector2.down);
            Simple_Move(KeyCode.LeftArrow, !c.Get_aboutWall("Wall", "left"), Vector2.left);
            Simple_Move(KeyCode.RightArrow, !c.Get_aboutWall("Wall", "right"), Vector2.right);
            if (c.Cooltime_Trigger(0, Input.GetKey(KeyCode.Z), true))
            {
                GameManager.instance.Get_sound_str("PlayerShot");
                c.Create(bullet, "me", 0, bullet_plusY, 0, 90);
            }
            if (c.Cooltime_Trigger(1, invincible, false))
            {
                invincible = false;
                CancelInvoke(nameof(Set_sprite_nearVincible));
                Set_sprite(0);
            }
        }
        else
        {
            Destroy(img_life);
            gameObject.SetActive(false);
            Invoke(nameof(Load_GameOver), 1.0f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("E_Bullet") && !invincible)
        {
            GameManager.instance.Get_sound_str("PlayerAttack");
            life--;
        }
        if (collision.CompareTag("Item1"))
        {
            invincible = true;
            c.Reset_cooltimeNow(1);
            Set_sprite(1);
            InvokeRepeating(nameof(Set_sprite_nearVincible), 4.0f, 0.1f);
            GameManager.instance.Get_sound_str("Item");
        }
    }
}
