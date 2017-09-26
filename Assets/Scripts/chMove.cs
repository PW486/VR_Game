using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chMove : MonoBehaviour {
    public float moveSpeed = 5f;

    public float turnSpeed = 200f;

    public int myKey;

    public Canvas start_canvas;
    public Canvas end_canvas;
    public Canvas death_canvas;
    public Canvas play_canvas;
    public Canvas no_canvas;
    public Text myText;
    public Text keyText;

    public AudioSource get;
    public AudioSource move;
    public AudioSource heart;

    void Start()
    {
        play_canvas.gameObject.SetActive(true);
        StartCoroutine("startCanvas");
        StartCoroutine("heartBeat");
        move.Play();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Key")
        {
            get.Play();
            myKey--;
            myText.text = "Remain Key : " + myKey.ToString();
            StartCoroutine("getKey");
            Vector3 vector = new Vector3(0, 100f, 0);
            col.gameObject.transform.position = vector;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "door")
        {
            if(myKey == 0)
            {
                StartCoroutine("end_game");
            }
            else
            {
                StartCoroutine("noCanvas");
            }
        }
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(0f, 0f, v * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);

        if(Input.anyKey)
        {
            move.mute = false;
        }
        else
        {
            move.mute = true;
        }

		if (Input.touchCount > 0)
		{
            transform.position += new Vector3(Camera.main.transform.forward.x * moveSpeed * Time.deltaTime, 0, Camera.main.transform.forward.z * moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator heartBeat()
    {
        yield return new WaitForSeconds(3.0f);
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                heart.Play();
                yield return new WaitForSeconds(3.1f - j * 0.3f);
            }
        }
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                heart.Play();
                yield return new WaitForSeconds(0.8f - j * 0.3f);
            }
        }
        death_canvas.gameObject.SetActive(true);
        StartCoroutine("gameover");
    }

    IEnumerator gameover()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(2.5f);
        Application.Quit();
    }

    IEnumerator end_game()
    {
        end_canvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        Application.Quit();
    }

    IEnumerator startCanvas()
    {
        start_canvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        start_canvas.gameObject.SetActive(false);
    }

    IEnumerator getKey()
    {
        keyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        keyText.gameObject.SetActive(false);
    }

    IEnumerator noCanvas()
    {
        no_canvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        no_canvas.gameObject.SetActive(false);
    }

}
