using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Timeline;

public class Farmer_eng : MonoBehaviour
{
    enum Action { Up, Down, Right, Left, Wait }
    
    float lenght = 0;
    Action nowAction;
    new SpriteRenderer renderer;

    public Sprite Up;
    public Sprite Down;
    public Sprite Right;
    public Sprite Left;
    
    public Sprite Up_dirty;
    public Sprite Down_dirty;
    public Sprite Right_dirty;
    public Sprite Left_dirty;
    
    public GameObject Camera;
    public GameObject Label;
    public Collider2D bomb;
    public static bool isGameOver = false;

    void Start() 
    {
        renderer = GetComponent<SpriteRenderer>();
        if (isGameOver) GameOver();
    }

    [System.Obsolete]
    void Update()
    {
        if (!isGameOver)
        {
            if (lenght <= 0)
            {
                nowAction = (Action)Random.RandomRange(0, 5);
                lenght = Random.RandomRange(1f, 5f);

                switch (nowAction)
                {
                    case Action.Up:
                        renderer.sprite = Up;
                        break;

                    case Action.Down:
                        renderer.sprite = Down;
                        break;

                    case Action.Right:
                        renderer.sprite = Right;
                        break;

                    case Action.Left:
                        renderer.sprite = Left;
                        break;
                }
            }

            switch (nowAction)
            {
                case Action.Up:
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, Time.deltaTime);
                    break;

                case Action.Down:
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up * -1, Time.deltaTime);
                    break;

                case Action.Right:
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, Time.deltaTime);
                    break;

                case Action.Left:
                    transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right * -1, Time.deltaTime);
                    break;
            }

            lenght -= Time.deltaTime;
        }
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Im in!");
        var tag = other.gameObject.tag;

        if (tag == "bomb")
            GameOver();
        else if (tag == "Player") {
            isGameOver = true;
            Label.SetActive(true);
        }
    }

    public void GameOver() 
    {
        isGameOver = true;
        switch (nowAction)
        {
            case Action.Up:
                renderer.sprite = Up_dirty;
                break;

            case Action.Down:
                renderer.sprite = Down_dirty;
                break;

            case Action.Right:
                renderer.sprite = Right_dirty;
                break;

            case Action.Left:
                renderer.sprite = Left_dirty;
                break;
        }
        var pos = transform.position;
        pos.z = -10f;
        Camera.transform.DOMove(pos, 2f);
    }
}
