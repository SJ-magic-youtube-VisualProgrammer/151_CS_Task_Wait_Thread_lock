/************************************************************
■async/awaitで複数のTaskを同時に実行する
	https://todosoft.net/blog/?p=344
	
■複数のTaskのすべての終了を待つ (C#プログラミング)
	https://www.ipentec.com/document/csharp-wait-task-complete-multiple

■ラムダ式の遅延実行に関する問題
	https://atmarkit.itmedia.co.jp/fdotnet/extremecs/extremecs_07/extremecs_07_10.html
************************************************************/
// using System;
// using System.Diagnostics; // need this to use StopWatch

/************************************************************
************************************************************/
namespace VisuapProgrammer_sj
{
	internal class AsyncTest
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"start");
			
			Console.WriteLine($"--> call RunAsync");
			Task t = RunAsync();
			Console.WriteLine($"<-- return from RunAsync");
			
			while(!t.IsCompleted){
				t.Wait(200);
				Console.Write(".");
			}
			Console.WriteLine();
			
			Console.WriteLine($"\nfinish");
		}
		
		static async Task RunAsync(){
			Console.WriteLine($"--> Start:RunAsync");
			
			const int NUM = 3;
			var tsks = new Task[NUM];
			
			var value = new List< List<int> >{
				new List<int> {1, 2, 3}, // sum =  6,
				new List<int> {4, 5, 6}, // sum = 15,
				new List<int> {7, 8, 9}, // sum = 24,
			};
			
			for(int i = 0; i < NUM; i++){
				int _i = i;
				tsks[i] = Task.Run( () => Heavy(_i, value[_i]) );
			}
			
			await Task.WhenAll(tsks);
			
			Console.WriteLine($"<-- End :RunAsync");
		}
		
		static bool IsAllTaskCompleted(Task[] tsks){
			foreach(var tsk in tsks){
				if(!tsk.IsCompleted) return false;
			}
			
			return true;
		}
		
		static void Heavy(int id, List<int> values){
			Console.WriteLine($"--> Start:Heavy");
			
			int result = 0;
			
			foreach(var v in values){
				Thread.Sleep(1000);
				result += v;
			}
			
			Console.WriteLine($"\n[{id}] sum = {result}");
			Console.WriteLine($"<-- End :Heavy");
		}
	}
}
