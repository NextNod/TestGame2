using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movment_eng : MonoBehaviour
{
    enum Way { Up, Down, Right, Left };

    int width = Screen.width,
        height = Screen.height;

    public Sprite right;
    public Sprite left;
    public Sprite up;
    public Sprite down;

    public GameObject bomb;
    public Transform SpavnBomb;

    SpriteRenderer sprite;
    Way personWay;
    bool lastToach = false;

    void Start() 
    {
        personWay = Way.Right;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && !Farmer_eng.isGameOver) {
            var touch = Input.GetTouch(Input.touchCount - 1);

            if (touch.position.x > width - (width / 3)) {
                if (personWay != Way.Right) {
                    personWay = Way.Right;
                    sprite.sprite = right;
                    SpavnBomb.localPosition = new Vector3(6f, 0f);
                }
                transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, 2f * Time.deltaTime);
            }
            else if (touch.position.x < width / 3) {
                if (personWay != Way.Left) {
                    personWay = Way.Left;
                    sprite.sprite = left;
                    SpavnBomb.localPosition = new Vector3(-6f, 0f);
                }
                transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right * -1, 2f * Time.deltaTime);
            }
            else if (touch.position.y > height - (height / 3)) {
                if (personWay != Way.Up) {
                    personWay = Way.Up;
                    sprite.sprite = up;
                    SpavnBomb.localPosition = new Vector3(0f, 6f);
                }
                transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, 2f * Time.deltaTime);
            }
            else if (touch.position.y < height / 3) {
                if (personWay != Way.Down) {
                    personWay = Way.Down;
                    sprite.sprite = down;
                    SpavnBomb.localPosition = new Vector3(0f, -6f);
                }
                transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up * -1, 2f * Time.deltaTime);
            } else {
                lastToach = true;
            }
        } else if (lastToach) {
            float x = SpavnBomb.position.x, y = SpavnBomb.position.y;
            if (y > -4.8f && y < 3.9f && x > -9.9f && x < 9.3f) {
                lastToach = false;
                var tmp = Instantiate(bomb);
                tmp.transform.localPosition = SpavnBomb.position;
                tmp.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
