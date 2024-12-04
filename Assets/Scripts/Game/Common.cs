using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{
    public float speed;
    public int type;
    public bool isHaveSprite;
    public bool[] isCooltimeFull;
    public float[] cooltime;
    private float width, height, wallWidth, wallHeight;
    private float[] cooltimeNow;
    private SpriteRenderer sprd;

    public bool IsOriginal => transform.position.x == type + 10;

    public void Reset_cooltimeNow(int i) => cooltimeNow[i] = 0;

    private void Reset_size()
    {
        width = sprd.sprite.bounds.size.x / 2;
        height = sprd.sprite.bounds.size.y / 2;
        width *= transform.lossyScale.x;
        height *= transform.lossyScale.x;
    }

    private void Init()
    {
        if (isHaveSprite)
        {
            Reset_size();
        }
        cooltimeNow = new float[cooltime.Length];
        for (int i = 0; i < cooltime.Length; i++)
        {
            cooltimeNow[i] = isCooltimeFull[i] ? cooltime[i] : 0;
        }
    }

    public void Set_cooltime(int num, float cooltime) => this.cooltime[num] = cooltime;
    public void Set_sprite(Sprite spr) => sprd.sprite = spr;

    public float Get_height => height;
    public float Get_limit(string type, string axis)
    {
        float plus_width = 0, plus_height = 0;
        switch (type)
        {

            case "Wall":
                plus_width = -width;
                plus_height = -height;
                break;
            case "Out":
                plus_width = width;
                plus_height = height;
                break;
        }

        switch (axis)
        {
            case "x":
                return (plus_width != 0) ? wallWidth + plus_width : wallWidth;
            case "y":
                return (plus_width != 0) ? wallHeight + plus_height : wallHeight;
            default:
                return 0;
        }
    }
    public bool Get_aboutWall(string type, string wall_type)
    {
        switch (wall_type)
        {
            case "right":
                return transform.position.x >= Get_limit(type, "x");
            case "left":
                return transform.position.x <= -Get_limit(type, "x");
            case "top":
                return transform.position.y >= Get_limit(type, "y");
            case "under":
                return transform.position.y <= -Get_limit(type, "y");
            case "xSide":
                return Mathf.Abs(transform.position.x) >= Get_limit(type, "x");
            case "ySide":
                return Mathf.Abs(transform.position.y) >= Get_limit(type, "y");
            case "all":
                return Mathf.Abs(transform.position.x) >= Get_limit(type, "x") || Mathf.Abs(transform.position.y) >= Get_limit(type, "y");
            default:
                return false;
        }
    }

    public void Move(Vector2 dir) => transform.Translate(speed * Time.deltaTime * dir);
    public void Create(GameObject obj, string type, float plus_x, float plus_y, float plus_z, float dir)
    {
        Vector3 vector = new Vector3(plus_x, plus_y, plus_z);
        if (type.Equals("me"))
        {
            vector += new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, dir));
        Instantiate(obj, vector, quaternion);
    }
    public bool Cooltime_Trigger(int type, bool condition, bool isReset)
    {
        if (cooltimeNow[type] >= cooltime[type] && condition)
        {
            cooltimeNow[type] = isReset ? 0 : cooltime[type];
            return true;
        }
        else if (cooltimeNow[type] < cooltime[type])
        {
            cooltimeNow[type] += Time.deltaTime;
        }
        return false;
    }

    private void Start()
    {
        if (isHaveSprite)
        {
            sprd = GetComponent<SpriteRenderer>();
            wallHeight = Camera.main.orthographicSize;
            wallWidth = wallHeight * Camera.main.aspect;
        }
        Init();

    }
}
