﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDown : MonoBehaviour
{
    public float CountDownTime;
    private float GameTime;
    private float timer = 0;
    public Text GameCountTimeText;
    // Start is called before the first frame update
    void Start()
    {
        GameTime = CountDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        int M = (int)(GameTime / 60);
        float S = GameTime % 60;
        //if ()//gamestart
        //{
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer = 0;
                GameTime--;
                GameCountTimeText.text = M + ":" + string.Format("{0:00}", S);
                if (S <= 0)
                {
                    
                    //Gameover
                }
            }

        //}

    }
}
