using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Manager : MonoBehaviour {

    private int[][] map;
    public int maxX, maxY;
    public GameObject floorA;
    public GameObject floorB;
    public GameObject floorC;
    public int keyCount;

    public GameObject celing;

    public GameObject wallA;
    public GameObject wallB;
    public GameObject wallC;

    public GameObject key;

    // Use this for initialization
    void Start () {
        int[][] outCheck;
        outCheck = new int[maxX][];
        map = new int[maxX][];
        for(int i = 0; i < maxX; i ++)
        {
            map[i] = new int[maxY];
            outCheck[i] = new int[maxY];
        }

        for (int i = 0; i < maxX; i ++)
        {
            for(int j= 0; j < maxY; j++)
            {
                map[i][j] = 15;
                outCheck[i][j] = 0;
            }
        }

        int x = 0, y = 0;
        int oriX = 0, oriY = 0;
        map[x][y] = 7;
        bool findNewWay;

        for(int count = 0; count < maxX * maxY - 1; count ++)//맵 생성중..
        {
            int way = Random.Range(0, 4);
            findNewWay = false;

            oriX = x;
            oriY = y;

            for (int a = 0; a < 4; a++)
            {
                if(way%4 == 0)//왼쪽으로
                {
                    if(x!=0)
                    {
                        if(map[x-1][y] == 15)
                        {
                            x--;
                            findNewWay = true;
                            break;
                        }
                    }
                    way++;
                }
                else if (way % 4 == 1)//위로
                {
                    if (y != maxY-1)
                    {
                        if (map[x][y+1] == 15)
                        {
                            y++;
                            findNewWay = true;
                            break;
                        }
                    }
                    way++;
                }
                else if (way % 4 == 2)//오른쪽
                {
                    if (x != maxX -1)
                    {
                        if (map[x + 1][y] == 15)
                        {
                            x++;
                            findNewWay = true;
                            break;
                        }
                    }
                    way++;
                }
                else if (way % 4 == 3)//아래쪽으로
                {
                    if (y != 0)
                    {
                        if (map[x][y - 1] == 15)
                        {
                            y--;
                            findNewWay = true;
                            break;
                        }
                    }
                    way++;
                }
            }//way가 방향 값이고 x,y값이 추출

            bool find;
            find = false;

            if(!findNewWay)//갈길이 없을경우 즉 새로운 루트를 찾아야하는경우 우선 새로운거 찾고 끝내자
            {
                for(int a = 0; a < maxX; a ++ )
                {
                    for(int b= 0; b < maxY; b++)
                    {
                        if(map[a][b] != 15)
                        {
                            for( way = 0; way < 4; way++)
                            {
                                if(a!=0 && way == 0)//왼쪽
                                {
                                    if(map[a-1][b] == 15)
                                    {
                                        oriX = a;
                                        oriY = b;
                                        x = a - 1;
                                        y = b;
                                        find = true;
                                        break;
                                    }
                                }
                                else if(b!= maxY -1 && way == 1)//위쪽
                                {
                                    if (map[a][b+1] == 15)
                                    {
                                        oriX = a;
                                        oriY = b;
                                        x = a;
                                        y = b + 1;
                                        find = true;
                                        break;
                                    }
                                }
                                else if(a != maxX -1 && way == 2)//오른쪽
                                {
                                    if (map[a + 1][b] == 15)
                                    {
                                        oriX = a;
                                        oriY = b;
                                        x = a + 1;
                                        y = b;
                                        find = true;
                                        break;
                                    }
                                }
                                else if (b != 0 && way == 3)//아래쪽
                                {
                                    if (map[a][b - 1] == 15)
                                    {
                                        oriX = a;
                                        oriY = b;
                                        x = a;
                                        y = b - 1;
                                        find = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (find)
                            break;
                    }
                    if (find)
                        break;
                }
            }
            //여기서 새로운루트 즉 x,y를 찾으면 xy값 바꾸고 종료 그러면 미로 완성

           
            if (way%4 == 0)
            {
                map[oriX][oriY] -= 1;
                map[x][y] -= 4;
            }
            else if (way % 4 == 1)
            {
                map[oriX][oriY] -= 2;
                map[x][y] -= 8;
            }
            else if (way % 4 == 2)
            {
                map[oriX][oriY] -= 4;
                map[x][y] -= 1;
            }
            else if (way % 4 == 3)
            {
                map[oriX][oriY] -= 8;
                map[x][y] -= 2;
            }

            outCheck[x][y] = outCheck[oriX][oriY] + 1;
        }

        int max = outCheck[0][0];
        int tempX = 0, tempY = 0;

        for(int i = 0; i < maxX; i++)
        {
            for(int j = 0; j < maxY; j ++)
            {
                if(outCheck[i][j] > max && ( i == 0 || i ==maxX-1) && (j==0 || j == maxY -1))
                {
                    max = outCheck[i][j];
                    tempX = i;
                    tempY = j;
                }
            }
        }

        //----------------------------------여기서부터는 미로 생성코드

        for (int i = 0; i < maxX; i ++)
        {
            for(int j = 0; j < maxY; j ++)
            {
               Vector3 position = new Vector3(i * (float)5.86, 0, j * (float)5.86);
               Instantiate(floorA, position, Quaternion.identity);
                position = new Vector3(i * (float)5.86, 5.5f, j * (float)5.86);
                Instantiate(celing, position, Quaternion.identity);
            }
        }

        int[] random = new int[maxX * maxY];

        for(int i = 0; i < maxX; i ++)
        {
            for(int j = 0; j < maxY; j ++)
            {
                random[i * maxY + j] = 0;
            }
        }

        for (int i = 0; i < keyCount; i ++)
        {
            int randX;
            int randZ;
            while (true)
            {
                randX = Random.Range(1, maxX);
                randZ = Random.Range(1, maxY);
                if(random[randX*maxY + randZ] == 0)
                {
                    random[randX * maxY + randZ] = 1;
                    break;
                }
            }

            Vector3 position = new Vector3(randX * 5.86f + 2.3f, 0.06016f, randZ * 5.86f + 2.8f);
            Instantiate(key, position, Quaternion.identity);
        }

        bool[] sero = new bool[maxY];

        for(int i = 0; i < maxX +1; i ++)//세로 벽 
        {
            if (i != maxX)
            {
                for (int k = 0; k < maxY; k++)
                {
                    if (map[i][k] == 1 || map[i][k] == 3 || map[i][k] == 9 || map[i][k] == 5 || map[i][k] == 7 || map[i][k] == 13 || map[i][k] == 11)
                        sero[k] = true;
                    else
                        sero[k] = false;
                }
            }
            else
            {
                for (int k = 0; k < maxY; k++)
                {
                    if (map[i-1][k] == 4 || map[i-1][k] == 6 || map[i-1][k] == 12 || map[i - 1][k] == 5 || map[i - 1][k] == 7 || map[i - 1][k] == 13 || map[i - 1][k] == 14)
                        sero[k] = true;
                    else
                        sero[k] = false;
                }
            }
            for (int j = 0; j < maxY; j ++)
            {
                if(sero[j])
                { 
                    Vector3 position = new Vector3(i * 5.86f - 2.54f, 0, j * 5.86f + 0.01f);
                    Instantiate(wallA, position, Quaternion.identity);
                }
            }
        }

        bool[] garo = new bool[maxY+1];

        for (int i = 0; i < maxX; i++)//가로
        {
            for(int k = 0; k < maxY +1; k ++)
            {
                if(k!= maxY)
                {
                    if (map[i][k] == 8 || map[i][k] == 12 || map[i][k] == 9 || map[i][k] == 10 || map[i][k] == 11 || map[i][k] == 13 || map[i][k] == 14 || map[i][k] == 15)
                        garo[k] = true;
                    else
                        garo[k] = false;
                }
                else
                {
                    if (map[i][k-1] == 2 || map[i][k - 1] == 3 || map[i][k - 1] == 6 || map[i][k - 1] == 10 || map[i][k - 1] == 7 || map[i][k - 1] == 11 || map[i][k - 1] == 14 || map[i][k - 1] == 15)
                        garo[k] = true;
                    else
                        garo[k] = false;
                }
            }

            for (int j = 0; j < maxY +1; j++)
            {
                if (garo[j])
                {
                    Vector3 position = new Vector3(i * 5.86f + 0.68f, 0, j * 5.86f - 2.62f);
                    Instantiate(wallA, position, Quaternion.Euler(0, 90f, 0));
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
