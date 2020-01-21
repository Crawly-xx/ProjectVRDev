using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    //TODO: Material picker, each submesh in mesh can only exist in 1 material. Use this.
    
    Mesh mesh; //The to be rendered mesh
    private int verticecount;
    public float floor_size;
    public int floor_matrix_width;
    public int floor_matrix_height;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();

        int[,] matrix = RandomFloorMatrix(floor_matrix_width, floor_matrix_height);
        MatrixLogger(matrix); //preview in debugger
       // Vector3[] verts = MatrixToVertices(matrix);
       // int[] triangles = Trianglie();
    }

    //Hmm Random floor made from.. squares

    int[,] RandomFloorMatrix(int x, int y)
    {
       
        //TODO move this to another function
        //First fill a matrix with some algorhitm to see where the squares should be placed. :D 0=noting 1=square
        //Lets make it so the first 0,0 in our floor matrix is a floor so the value should be 1.
        
        int i = 0;
        int j = 0;
        int[,]thematrix = new int[x,y];
        thematrix[i, j] = 1;

        bool done = false;

        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;

        bool rightb = false;
        bool leftb = false;
        bool upb = false;
        bool downb = false;

        bool rightw = false;
        bool leftw = false;
        bool upw = false;
        bool downw = false;

        List<string> floptions = new List<string>();

        int neighbours = 0;

        float randof = 0;
        int randoi =0;

        //int testcounter = 0;
        while (!done) // replace with !done
        {
            //check if in bounds of the matrix e.g i,j>=0 i,j<=x,y
            if (MatrixBoundsCheck(thematrix, i, j) )
            {
                //Check floor tile connected to what others
                if (MatrixBoundsCheck(thematrix, i + 1, j)) {
                    if (thematrix[i + 1, j] == 1) {
                        right = true; neighbours++;
                    }
                    else if (thematrix[i + 1, j] == 2)
                    {
                        rightw = true;
                    }
                }
                else { rightb = true; } //out of bounds


                if (MatrixBoundsCheck(thematrix, i - 1, j))
                {
                    if (thematrix[i - 1, j] == 1)
                    {
                        left = true; neighbours++;
                    }
                    else if (thematrix[i - 1, j] == 2)
                    {
                        leftw = true;
                    }
                }
                else { leftb = true; }

                if (MatrixBoundsCheck(thematrix, i, j+1))
                {
                    if (thematrix[i, j + 1] == 1)
                    {
                        up = true; neighbours++;
                    }
                    else if (thematrix[i, j + 1] == 2)
                    {
                        upw = true;
                    }
                }
                else { upb = true; }


                if (MatrixBoundsCheck(thematrix, i, j-1))
                {
                    if (thematrix[i, j - 1] == 1)
                    {
                        down = true; neighbours++;
                    }
                    else if (thematrix[i, j - 1] == 2)
                    {
                        downw = true;
                    }
                }
                else { downb = true; }

                //if it has 2+ neighbours stop, something went wrong
                if (neighbours >= 2) { neighbours = 0; print("more than 2 neighb"); break; }

                //pick a inbound! floorless! non-wall!
                //neighbour tile and make it a floor tile and this will be our 
                //next "selected tile"


                if (!rightb && !right && !rightw) { floptions.Add("right");  }
                if (!leftb && !left && !leftw) { floptions.Add("left"); } //not out of bounds and not already neightbour/floor
                if (!upb && !up && !upw) { floptions.Add("up"); }
                if (!downb && !down && !downw) { floptions.Add("down"); }

                randof = Random.value;
                randoi = (int)(floptions.Count * randof);
                
                //no more options left quit the loop.
                if (floptions.Count == 0) { Debug.Log("no more floptions, matrix 100% complete"); break; }
                
                
                //make the tile a floor and the surrounding non floor tiles "walls" aka non-floorable area.
                switch (floptions[randoi])
                {
                    case "up":
                        thematrix[i, j+1] = 1;
                        if (!leftb && thematrix[i - 1, j] == 0) { thematrix[i - 1, j] = 2; }
                        if (!downb && thematrix[i,j-1]==0) { thematrix[i, j - 1] = 2; }
                        if (!rightb && thematrix[i + 1, j] == 0) { thematrix[i+1,j] = 2; }
                        j++;
                        break;
                    case "down":
                        thematrix[i, j-1] = 1;
                        if (!leftb && thematrix[i - 1, j] == 0) { thematrix[i - 1, j] = 2; }
                        if (!upb && thematrix[i, j + 1] == 0) { thematrix[i, j + 1] = 2; }
                        if (!rightb && thematrix[i + 1, j] == 0) { thematrix[i + 1, j] = 2; }
                        j--;
                        break;
                    case "left":
                        thematrix[i-1, j] = 1;
                        if (!upb && thematrix[i, j + 1] == 0) { thematrix[i, j + 1] = 2; }
                        if (!downb && thematrix[i, j - 1] == 0) { thematrix[i, j - 1] = 2; }
                        if (!rightb && thematrix[i + 1, j] == 0) { thematrix[i + 1, j] = 2; }
                        i--;
                        break;
                    case "right":
                        thematrix[i+1, j] = 1;
                        if (!leftb && thematrix[i - 1, j] == 0) { thematrix[i - 1, j] = 2; }
                        if (!downb && thematrix[i, j - 1] == 0) { thematrix[i, j - 1] = 2; }
                        if (!upb && thematrix[i, j+1] == 0) { thematrix[i , j+1] = 2; }
                        i++;                       
                        break;
                }

                thematrix[i, j] = 1; // make the *active tile a floor

                //cleanup
                floptions.Clear();

                rightb = false;
                right = false;
                rightw = false;

                leftb = false;
                left = false;
                leftw = false;

                upb = false;
                up = false;
                upw = false; 

                downb = false;
                down = false;
                downw = false;

                neighbours = 0;
            }

            //testcounter++;
        }
        
        return thematrix;
    }

    bool MatrixBoundsCheck(int[,] matrix, int row, int col)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);
        if (row >= 0 && col >= 0 && row < rowLength && col < colLength)
        {
            return true;
        }

        return false;

    }

    void MatrixLogger(int[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        string row= "";

        for (int i = 0; i< rowLength; i++)
        {
            for (int j = 0; j< colLength; j++)
            {
                row = row + ", " + matrix[i, j];
            }

            Debug.Log(row);
            row = "";
        }
    }

    Vector3[] MatrixToVertices(int[,] matrix)
    {
        Vector3[] verts= new Vector3[matrix.GetLength(0)*matrix.GetLength(1)]; //bad math!

        int k = 0; // vertice counter                                                                                                     
        for (int i=0; i < matrix.GetLength(0); i++) {
            for (int j=0; j< matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 1) {
                    verts[k] = new Vector3(floor_matrix_width * i, 0, floor_matrix_height * j); //bottom left
                    k++;
                    verts[k] = new Vector3(floor_matrix_width * (i + 1), 0, floor_matrix_height * j); //bottom right
                    k++;
                    verts[k] = new Vector3(floor_matrix_width * (i + 1), 0, floor_matrix_height * (j + 1)); //top right
                    k++;
                    new Vector3(floor_matrix_width * i, 0, floor_matrix_height * (j + 1)); //top left
                    k++;
                }
            }
        }

        verticecount = k; //ugly code;

        return verts;
    }

    int[] Trianglie()
    {
        int[] tria = new int[verticecount];

        for (int i=0; i < verticecount; i++)
        {
          //  tria[i] = new int(i, i + 1, i + 2);
        }

        return tria;
    }

    void UpdateMesh(Vector3[] vertices,int[] triangles, int submesh)
    {
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles,submesh);
    }

    void ClearMesh()
    {
        mesh.Clear();
    }

}
