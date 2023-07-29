using System;

class Djikstra {		
		static int V = 9;
		int minDistance(int[] dist, bool[] shortestPathTree){
				
				// 👻	value							🌟 index of
				int min = int.MaxValue, min_index = -1;

				for (int v=0; v<V; v++){
						if (shortestPathTree[v] == false && dist[v] <= min){
								min = dist[v]; 
								min_index = v; 
						}
				}

				return min_index;
		}
		void printSolution(int[] dist, int n) {
				Console.Write("Vertex     Distance "
											+ "from Source\n");
				for (int i = 0; i < V; i++)
						Console.Write(i + " \t\t " + dist[i] + "\n");
		}

		public void DjikstraAlgorithm(int[, ] graph, int src){
				int[] dist = new int[V];
				bool[] shortestPathTreeSet = new bool[V];

				for (int i=0; i<V; i++){
						shortestPathTreeSet[i] = false;
						dist[i] = int.MaxValue;
				}

				dist[src] = 0;

				for (int count = 0; count < V; count++) { 
						int u = minDistance(dist, shortestPathTreeSet);

						shortestPathTreeSet[u] = true;

						for (int v=0; v < V; v++){
								if (!shortestPathTreeSet[v] 
														&& graph[u,v] != 0 && dist[v] == int.MaxValue // this one got me last time. had != instead of == 
														&& dist[u] + graph[u,v] < dist[v]) {
																dist[v] = dist[u] + graph[u,v];  
														}
						}
				}

				printSolution(dist, src);
		}
}

namespace AStar{
		public class Cell{
				public Cell parent;
				public int g, h, f, x, y;
				public bool isBlocked;
		}

		class AStar {
				// f(n) = g(n) + h(n)

				Cell[,] grid;
				List<Cell> openList, closedList;

				// initialize the grid, openlist and closedlist

				public void FindPath(Cell start, Cell end){

						openList.Add(start);

						// Keep checking until we reach our destino.
						while (openList.Count > 0){ 

								Cell currentCell = GetCellWithLowestFScore();

								closedList.Add(currentCell);
								openList.Remove(currentCell);

								// Are we there yet?
								if (currentCell == end){
										break;
								}

								// We're not there yet.
								foreach(Cell neighbor in GetWalkableNeighbors(currentCell)){
										if (!closedList.Contains(neighbor)){
												int tempGScore = currentCell.g + 1; // let's say each move costs 1.
												
												if (!openList.Contains(neighbor)){
														openList.Add(neighbor); 
												} else if (tempGScore >= neighbor.g) {
														// Move on. This isn't a better path.
														continue;
												}
												
												// This is the best path so far.
												neighbor.parent = currentCell; // link the best cell to current cell.
												neighbor.g = tempGScore;
												neighbor.h = CalculateHScore(neighbor, end); // Euclidean or Manhattan distance here.
												neighbor.f = neighbor.g + neighbor.h;
										}
								}
						}
				}

				private int CalculateHScore(Cell neighbor, Cell end) {
						throw new NotImplementedException();
				}

				private IEnumerable<Cell> GetWalkableNeighbors(Cell currentCell) {
						
						List<Cell> neighbors = new List<Cell>();
						
						int gridsize = 5; // Get Gridsize safely. Just so happens to be 5
						// deltas 
						int[] dx = {-1, 1, 0, 0};
						int[] dy = { 0, 0, -1, 1};

						for (int i=0; i< 4; i++) {
								int newX = currentCell.x + dx[i];
								int newY = currentCell.y + dy[i];
								
								// Check if this cell is inside the grid and not blocked.
								if (newX >= 0 && newX < gridsize && 
										newY >= 0 && newY < gridsize && 
										!grid[newX, newY].isBlocked)
										{		
												neighbors.Add(grid[newX, newY]);
										}
						}

						return neighbors;
				}

				private Cell GetCellWithLowestFScore() {
						Cell lowestFScoreCell = null;

						foreach (Cell cell in openList){
								if (lowestFScoreCell == null || cell.f < lowestFScoreCell.f) { 
										lowestFScoreCell = cell; // Finding similarities to djikstra's here.
								}
						}

						return lowestFScoreCell;
				}
		}
}


class Program{
		static int Main(string[] args) {
				int[,] graph = new int[,] {   { 0,  4, 0,  0,  0,  0, 0,  8, 0 }, // I made a really good graph to debug here.
																			{ 4,  0, 8,  0,  0,  0, 0, 11, 0 },
																			{ 0,  8, 0,  7,  0,  4, 0,  0, 2 },
																			{ 0,  0, 7,  0,  9, 14, 0,  0, 0 },
																			{ 0,  0, 0,  9,  0, 10, 0,  0, 0 },
																			{ 0,  0, 4, 14, 10,  0, 2,  0, 0 },
																			{ 0,  0, 0,  0,  0,  2, 0,  1, 6 },
																			{ 8, 11, 0,  0,  0,  0, 1,  0, 7 },
																			{ 0,  0, 2,  0,  0,  0, 6,  7, 0 }};

				
				Djikstra d = new Djikstra();

				d.DjikstraAlgorithm(graph, 1);

				return 0;
		}
}

