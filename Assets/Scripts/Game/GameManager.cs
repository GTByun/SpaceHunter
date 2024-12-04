using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int score;
    public static GameManager instance;
    public GameObject sound_obj;
    public GameObject[] enemy_obj;
    public TextMeshProUGUI scoreText;
    private int count_eliteAppear;
    private Common c;
    private Common[] enemy_info;
    
    private void Init()
    {
        count_eliteAppear = 0;
        score = 0;
    }

    private float Get_enemy_xWallLimit(int id)
    {
        float limiter_range = enemy_info[id].Get_limit("Wall", "x");
        return Random.Range(-limiter_range, limiter_range);
    }
    private float Get_enemy_yOutLimit(int id) => enemy_info[id].Get_limit("Out", "y");
    public void Get_sound_str(string sound)
    {
        switch (sound)
        {
            case "PlayerAttack":
                Create_sound(0);
                break;
            case "PlayerShot":
                Create_sound(1);
                break;
            case "EnemyAttack":
                Create_sound(2);
                break;
            case "EnemyShot":
                Create_sound(3);
                break;
            case "Item":
                Create_sound(4);
                break;
            default:
                break;
        }
    }

    public void Plus_score(int num) => score += num;
    public void Plus_count_eliteAppear_one() => count_eliteAppear++;

    private void Create_Enemy(int id) => c.Create(enemy_obj[id], "center", Get_enemy_xWallLimit(id), Get_enemy_yOutLimit(id), 0, 0);
    private void Create_sound(int id) => c.Create(sound_obj, "center", 0, 0, id, 0);

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        c = GetComponent<Common>();
        c.Set_cooltime(0, Random.Range(2.0f, 3.0f));
        enemy_info = new Common[enemy_obj.Length];
        for (int i = 0; i < enemy_obj.Length; i++)
        {
            enemy_info[i] = enemy_obj[i].GetComponent<Common>();
        }
        Init();
    }

    private void Update()
    {
        if (c.Cooltime_Trigger(0, true, true))
        {
            Create_Enemy(0);
            c.Set_cooltime(0, Random.Range(2.0f, 3.0f));
        }
        if (count_eliteAppear >= 10)
        {
            Create_Enemy(1);
            count_eliteAppear -= 10;
        }
        scoreText.text = score.ToString();
    }
}
