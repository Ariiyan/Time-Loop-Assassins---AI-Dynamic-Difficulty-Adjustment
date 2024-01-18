/*using UnityEngine;
using System.Collections.Generic;

public class EnvironmentGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited;
        public bool[] doors = new bool[4]; 
    }

    public Vector2Int size = new Vector2Int(5, 5); 
    public GameObject roomPrefab;
    public Vector2 roomSize = new Vector2(10, 10);
    private List<Cell> cells;

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        InitializeCells();
        CreateMaze();
        InstantiateRooms();
    }

    void InitializeCells()
    {
        cells = new List<Cell>();
        for (int i = 0; i < size.x * size.y; i++)
        {
            cells.Add(new Cell());
        }
    }

    void CreateMaze()
    {
        Stack<int> stack = new Stack<int>();
        int current = 0;
        cells[current].visited = true;
        int visitedCells = 1;

        while (visitedCells < cells.Count)
        {
            List<int> neighbours = GetUnvisitedNeighbours(current);

            if (neighbours.Count > 0)
            {
                int chosen = neighbours[Random.Range(0, neighbours.Count)];
                RemoveWall(current, chosen);

                stack.Push(current);
                current = chosen;
                cells[current].visited = true;
                visitedCells++;
            }
            else if (stack.Count > 0)
            {
                current = stack.Pop();
            }
        }
    }

    void RemoveWall(int current, int next)
    {
        if (next == current + 1) // Right
        {
            cells[current].doors[0] = true;
            cells[next].doors[1] = true;
        }
        else if (next == current - 1) // Left
        {
            cells[current].doors[1] = true;
            cells[next].doors[0] = true;
        }
        else if (next == current + size.x) // Down
        {
            cells[current].doors[3] = true;
            cells[next].doors[2] = true;
        }
        else if (next == current - size.x) // Up
        {
            cells[current].doors[2] = true;
            cells[next].doors[3] = true;
        }
    }

    List<int> GetUnvisitedNeighbours(int index)
    {
        List<int> neighbours = new List<int>();

        int x = index % size.x;
        int y = index / size.x;

        if (x < size.x - 1 && !cells[index + 1].visited) // Right
            neighbours.Add(index + 1);
        if (x > 0 && !cells[index - 1].visited) // Left
            neighbours.Add(index - 1);
        if (y < size.y - 1 && !cells[index + size.x].visited) // Down
            neighbours.Add(index + size.x);
        if (y > 0 && !cells[index - size.x].visited) // Up
            neighbours.Add(index - size.x);

        return neighbours;
    }

    void InstantiateRooms()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector3 position = new Vector3(i * roomSize.x, 0, -j * roomSize.y);
                GameObject roomInstance = Instantiate(roomPrefab, position, Quaternion.identity);
                RoomBehaviour roomBehaviour = roomInstance.GetComponent<RoomBehaviour>();
                if (roomBehaviour != null)
                {
                    int index = i + j * size.x;
                    roomBehaviour.UpdateRoom(cells[index].doors);
                }
            }
        }
    }
}
*/