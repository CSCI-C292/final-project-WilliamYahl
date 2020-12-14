
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Randomizer : MonoBehaviour
{
    [SerializeField] GameObject foreground;
    [SerializeField] GameObject playspace;
    [SerializeField] GameObject background;

    [SerializeField] Tile wall;
    [SerializeField] Tile roof;
    [SerializeField] Tile floor;
    [SerializeField] Tile overhang;
    [SerializeField] Tile testing;


    // Start is called before the first frame update
    void Start()
    {
        generate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    private void generate()
    {

        int[,] gameMap = generateDungeon();
        
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (gameMap[i, j] == 0)
                {
                    playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), roof);
                }
                background.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), floor);
            }
        }
    }
    private int[,] generateDungeon()
    {
        int[,] map = new int[100, 100];
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                map[i, j] = 0;
            }
        }

        for (int k = 1; k <= 100; k++)
        {

            int x = Random.Range(3, 20);
            int y = Random.Range(3, 20);
            int posX = Random.Range(5, 95 - x);
            int posY = Random.Range(5, 95 - y);

            bool doContinue = true;
            for (int i = posX - 3; i < posX + x + 3; i++)
            {
                for (int j = posY - 3; j < posY + y + 3; j++)
                {
                    if(map[i,j] != 0)
                    {
                        doContinue = false;
                    }
                }
            }

            if(doContinue)
            {
                for (int i = 1; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        if (i >= posX && i <= posX + x && j >= posY && j <= posY + y)
                        {
                            map[i, j] = k;
                        }
                    }
                }
            }
        }
        return map;
    }// idea 2
    */

    private void generate()
    {
        //List<room> dungeon = new List<room>();
        List<room> dungeon = recursiveDungeon(0, 99, 0, 99);
        int[,] map = new int[100, 100];
        //Debug.Log(Random.Range(0,-10));

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                //playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), roof);
                //background.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), floor);

                map[i, j] = 1;
            }
        }

        for (int k = 0; k < dungeon.Count; k++)
        {
            //Debug.Log(dungeon[k].xPos + " " + dungeon[k].width + " " + dungeon[k].yPos + " " + dungeon[k].height);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (i > dungeon[k].xPos && i < dungeon[k].xPos + dungeon[k].width && j > dungeon[k].yPos && j < dungeon[k].yPos + dungeon[k].height)
                    {
                        //playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), testing);
                        map[i, j] = 0;
                    }
                }
            }

            for (int i = 0; i < dungeon[k].hallways.Count; i++)
            {
                room room1 = dungeon[k];
                room room2 = dungeon[k].hallways[i];

                //Debug.Log("---");
                //Debug.Log("1 " + room1.xPos + " " + room1.width + " " + room1.yPos + " " + room1.height);
                //Debug.Log("2 " + room2.xPos + " " + room2.width + " " + room2.yPos + " " + room2.height);

                int min;
                int max;
                if (room1.xPos == room2.xPos + room2.width)
                {
                    min = Mathf.Max(room1.yPos, room2.yPos) + 1;
                    max = Mathf.Min(room1.yPos + room1.height, room2.yPos + room2.height);
                    //if (min != max)
                    //{
                        //playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(room1.xPos, Random.Range(min, max), 0), testing);
                        map[room1.xPos, Random.Range(min, max)] = 0;
                    //}
                }
                else
                {
                    min = Mathf.Max(room1.xPos, room2.xPos) + 1;
                    max = Mathf.Min(room1.xPos + room1.width, room2.xPos + room2.width);
                    //if (min != max)
                    //{
                        //playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(Random.Range(min, max), room1.yPos, 0), testing);
                        map[Random.Range(min, max), room1.yPos] = 0;
                    //}
                }
            }
        }

        for (int i = 0; i < 100; i++)
        {
            for (int j = 1; j < 100; j++)
            {
                if (map[i, j] == 1)
                {
                    if (map[i, j - 1] == 1)
                    {
                        playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), roof);
                    }
                    else
                    {
                        playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), wall);
                    }
                }
                else
                {
                    if (map[i, j - 1] == 1)
                    {
                        foreground.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), overhang);
                    }

                    playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), testing);
                }
                background.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), floor);
            }
        }

        for (int i = 0; i < 100; i++)
        {
            //playspace.GetComponent<Tilemap>().SetTile(new Vector3Int(i, 0, 0), roof);
            background.GetComponent<Tilemap>().SetTile(new Vector3Int(i, 0, 0), floor);
            foreground.GetComponent<Tilemap>().SetTile(new Vector3Int(i, 100, 0), overhang);
        }
    }

    private List<room> recursiveDungeon(int xMin, int xMax, int yMin, int yMax)
    {
        List<room> dungeon = new List<room>();
        //bool isX;
        int split = -1;
        List<room> split1 = new List<room>();
        List<room> split2 = new List<room>();
        if (xMax - xMin >= yMax - yMin && xMax - xMin >= 20)
        {
            //isX = true;
            split = Random.Range(xMin + 7, xMax - 7);
            split1.AddRange(recursiveDungeon(xMin, split, yMin, yMax));
            split2.AddRange(recursiveDungeon(split, xMax, yMin, yMax));
        }
        else if (yMax - yMin >= 20) 
        {
            //isX = false;
            split = Random.Range(yMin + 7, yMax - 7);
            split1.AddRange(recursiveDungeon(xMin, xMax, split, yMax));
            split2.AddRange(recursiveDungeon(xMin, xMax, yMin, split));
        }
        else
        {
            dungeon.Add(createRoom(xMin, xMax, yMin, yMax));
            return dungeon;
        }

        List<hallway> possible = new List<hallway>();
        for(int i = 0; i < split1.Count; i++)
        {
            for(int j = 0; j < split2.Count; j++)
            {
                //check if they are adjacent and possibly make door? make sure there is always a door.
                //might need to check which direction it split (isX)
                if (split1[i].xPos == split2[j].xPos + split2[j].width)
                {
                    if(split1[i].yPos + 2 < split2[j].yPos + split2[j].height && split1[i].yPos + split1[i].height > split2[j].yPos + 2)
                    {
                        possible.Add(new hallway(i, j, true));
                        //split1[i].hallways.Add(split2[j]);
                    }
                }
                else if (split2[j].xPos == split1[i].xPos + split1[i].width)
                {
                    if (split2[j].yPos + 2 < split1[i].yPos + split1[i].height && split2[j].yPos + split2[j].height > split1[i].yPos + 2)
                    {
                        possible.Add(new hallway(i, j, false));
                        //split2[j].hallways.Add(split1[i]);
                        //make hallway in x
                        //add chance later
                    }
                }
                else if (split1[i].yPos == split2[j].yPos + split2[j].height)
                {
                    if (split1[i].xPos + 2 < split2[j].xPos + split2[j].width && split1[i].xPos + split1[i].width > split2[j].xPos + 2)
                    {
                        possible.Add(new hallway(i, j, true));
                        //split1[i].hallways.Add(split2[j]);
                    }
                }
                else if (split2[j].yPos == split1[i].yPos + split1[i].height)
                {
                    if (split2[j].xPos + 2 < split1[i].xPos + split1[i].width && split2[j].xPos + split2[j].width > split1[i].xPos + 2)
                    {
                        possible.Add(new hallway(i, j, false));
                        //split2[j].hallways.Add(split1[i]);
                        //make hallway in y
                        //add chance later
                    }
                }
                //possibly add everything to a list that will have one guaranteed to have a door, and the others will have a door by chance.

                //Debug.Log("lag");
            }
        }

        int door = Random.Range(0, possible.Count); 
        for(int i= 0; i < possible.Count; i++)
        {
            if(i == door)
            {
                if(possible[i].side == true)
                {
                    split1[possible[i].cat1].hallways.Add(split2[possible[i].cat2]);
                }
                else
                {
                    split2[possible[i].cat2].hallways.Add(split1[possible[i].cat1]);
                }
            }
            else
            {
                if (0 == Random.Range(0,8))
                {
                    if (possible[i].side == true)
                    {
                        split1[possible[i].cat1].hallways.Add(split2[possible[i].cat2]);
                    }
                    else
                    {
                        split2[possible[i].cat2].hallways.Add(split1[possible[i].cat1]);
                    }
                }
            }
        }
        
        //Debug.Log((xMax - xMin) + " " + (yMax - yMin));
        dungeon.AddRange(split1);
        dungeon.AddRange(split2);
        return dungeon;
    }

    private room createRoom(int xMin, int xMax, int yMin, int yMax)
    {
        //int height = Random.Range(0, yMax - yMin);
        //int width = Random.Range(0, xMax - xMin);
        //int yPos = Random.Range(yMin, yMax - height);
        //int xPos = Random.Range(xMin, xMax - width);

        int height = yMax - yMin;// - 2;
        int width = xMax - xMin;// - 2;
        int yPos = yMin;// + 1;
        int xPos = xMin;// + 1;


        //Debug.Log(xMin + " " + xMax + " " + yMin + " " + yMax);
        //Debug.Log(xPos + " " + width + " " + yPos + " " + height);
        return new room(xPos,yPos,height,width);
    }


    public class room
    {
        public int xPos;
        public int yPos;
        public int height;
        public int width;

        public List<room> hallways = new List<room>();

        public room(int x, int y, int h, int w)
        {
            xPos = x;
            yPos = y;
            height = h;
            width = w;
        }
    }

    public class hallway
    {
        public int cat1;
        public int cat2;
        public bool side;

        public hallway(int cat1, int cat2, bool side)
        {
            this.cat1 = cat1;
            this.cat2 = cat2;
            this.side = side;
        }

    }



}
