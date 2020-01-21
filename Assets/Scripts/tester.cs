using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    int[,] mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = new int[5, 5];
        mat[0, 0] = 1;
        mat[4, 4] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
        //    Debug.Log(mat[4, 4] == 1);
        }
    }
}
