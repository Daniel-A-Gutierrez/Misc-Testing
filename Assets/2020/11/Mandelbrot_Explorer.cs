using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mandelbrot_Explorer : MonoBehaviour
{
    public new GameObject camera ;
    public GameObject canvas;
    public GameObject visuals;
    public float zoom_exponent = 1;
    public float scrollSpeed;
    public float zoomSpeed;
    public int iterations = 100;
    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.U))
        {
            canvas.SetActive(!canvas.activeSelf);
        }

        iterations += ( Input.GetKey(KeyCode.UpArrow) ? 1 : 0 ) - ( Input.GetKey(KeyCode.DownArrow) ? 1 : 0 );
        visuals.GetComponent<MeshRenderer>().material.SetInt("_Iterations" , iterations );

        float viewportHeight = camera.GetComponent<Camera>().orthographicSize*2;
        float viewportWidth = viewportHeight*16.0f/9.0f;
        Vector2 moveVec = (Vector2.zero + Vector2.left * (Input.GetKey(KeyCode.A) ? 1 : 0)
                                       + Vector2.right * (Input.GetKey(KeyCode.D) ? 1 : 0)
                                       + Vector2.down * (Input.GetKey(KeyCode.S) ? 1 : 0)
                                       + Vector2.up * (Input.GetKey(KeyCode.W) ? 1 : 0)).normalized;
        moveVec *= new Vector2(viewportWidth,viewportHeight).magnitude * Time.deltaTime;
        
        camera.transform.Translate(moveVec.x,moveVec.y,0);

        if(zoom_exponent < 19)
        {
            zoom_exponent += Time.deltaTime * zoomSpeed * (Input.GetKey(KeyCode.Space) ? 1 : 0);
        }
        if(zoom_exponent > -19)
        {
            zoom_exponent -= Time.deltaTime * zoomSpeed * (Input.GetKey(KeyCode.LeftShift) ? 1 : 0);
        }
        camera.GetComponent<Camera>().orthographicSize = (Mathf.Pow(2,-zoom_exponent));

        canvas.transform.Find("iterations").GetComponent<TextMeshProUGUI>().SetText(iterations.ToString());
        canvas.transform.Find("zoom").GetComponent<TextMeshProUGUI>().SetText("2 ^" + zoom_exponent);
        canvas.transform.Find("center").GetComponent<TextMeshProUGUI>().SetText("" + camera.transform.position.x + "\n" + camera.transform.position.y + " i");

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
