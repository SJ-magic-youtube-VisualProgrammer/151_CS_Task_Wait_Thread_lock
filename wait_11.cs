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
			RunAsync();
			Console.WriteLine($"<-- return from RunAsync");
			
			Console.WriteLine($"\nfinish");
		}
		
		static void RunAsync(){
			Console.WriteLine($"--> Start:RunAsync");
			
			const int NUM = 3;
			
			var value = new List< List<int> >{
				new List<int> {1, 2, 3}, // sum =  6,
				new List<int> {4, 5, 6}, // sum = 15,
				new List<int> {7, 8, 9}, // sum = 24,
			};
			
			Task<int>		tsk_0 = Task<int>.Run( () => Heavy(0, value[0]) );
			Task<string>	tsk_1 = Task<string>.Run( () => Heavy_string(1, value[1]) );
			Task<int>		tsk_2 = Task<int>.Run( () => Heavy(2, value[2]) );
			
			Task.WaitAll(new Task[]{tsk_0, tsk_1, tsk_2});
			
			Console.WriteLine($"tsk_0.Result = {tsk_0.Result}");
			Console.WriteLine($"tsk_1.Result = {tsk_1.Result}");
			Console.WriteLine($"tsk_2.Result = {tsk_2.Result}");
			
			Console.WriteLine($"<-- End :RunAsync");
		}
		
		static bool IsAllTaskCompleted(Task[] tsks){
			foreach(var tsk in tsks){
				if(!tsk.IsCompleted) return false;
			}
			
			return true;
		}
		
		static int Heavy(int id, List<int> values){
			Console.WriteLine($"--> Start:Heavy");
			
			int result = 0;
			
			foreach(var v in values){
				Thread.Sleep(1000);
				result += v;
			}
			
			Console.WriteLine($"<-- End :Heavy");
			
			return result;
		}
		
		static string Heavy_string(int id, List<int> values){
			Console.WriteLine($"--> Start:Heavy");
			
			int result = 0;
			
			foreach(var v in values){
				Thread.Sleep(1000);
				result += v;
			}
			
			Console.WriteLine($"<-- End :Heavy");
			
			return result.ToString();
		}
	}
}
