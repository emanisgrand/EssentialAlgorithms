using System;

class Djikstra{
		// Readonly. Lookup what I called this.
		static int V = 9;

		/// <summary>
		/// Helper function
		/// </summary>
		/// <param name="dist">distance values</param>
		/// <param name="shortestPathTree">searched vertices</param>
		/// <returns>index of vertex with minimum distance</returns>
		int minDistance(int[] dist, bool[] shortestPathTree){
				
				// 👻	value							🌟 index of
				int min = int.MaxValue, min_index = -1;

				for (int v=0; v<V; v++){
						if (shortestPathTree[v] == false && dist[v] <= min){
								min = dist[v]; // distance to vertex?
								min_index = v; // index of vertex.
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

		void DjikstraAlgorithm(int[, ] graph, int src){
				// 🗄️ will hold shortest distance from src -> i
				int[] dist = new int[V]; 

				// 🗄️ [i]=T if v@i is included in spt or shortest distance from src -> i is "finalized"
				bool[] sptSet = new bool[V];

				// 0. Initialize all distances as ♾️ and stpSet[] as false
				for (int i=0; i<V; i++){
						dist[i] = int.MaxValue;
						sptSet[i] = false;
				}

				// 1. Distance of src vertex to itself is always 0
				dist[src] = 0;

				// 2. Loop thru verts to find shortest path
				for (int count =0; count < V; count++){
						// 0. Pick the min distance vert from the unprocessed group. * u == src in first iteration
						int u = minDistance(dist, sptSet);

						// 1. Mark the picked vertex as processed.
						sptSet[u] = true;

						// 2. Update the dist value of the adjacent verts of the picked vert
						for (int v=0; v< V; v++) {
								// 0. check the following:
								// ☑️ dist[v] is !in shortestPath set,
								// ☑️ there's an edge from u->v, 
								// ☑️ total weight of path from (src->v) < current value of dist[v]
								if (!sptSet[v] && graph[u, v] != 0
															 && dist[u] != int.MaxValue 
															 && dist[u] + graph[u, v] < dist[v]){
																		dist[v] = dist[u] + graph[u,v]; 		
															 }
						}
				}
				printSolution(dist, V);

		}

		static int Main(string[] args){
				int[,] graph = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
																			{ 4, 0, 8, 0, 0, 0, 0, 11, 0 },
																			{ 0, 8, 0, 7, 0, 4, 0, 0, 2 },
																			{ 0, 0, 7, 0, 9, 14, 0, 0, 0 },
																			{ 0, 0, 0, 9, 0, 10, 0, 0, 0 },
																			{ 0, 0, 4, 14, 10, 0, 2, 0, 0 },
																			{ 0, 0, 0, 0, 0, 2, 0, 1, 6 },
																			{ 8, 11, 0, 0, 0, 0, 1, 0, 7 },
																			{ 0, 0, 2, 0, 0, 0, 6, 7, 0 } };

				Djikstra d = new Djikstra();
				
				d.DjikstraAlgorithm(graph, 0);
				
				return 0;
		}
}

