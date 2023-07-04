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
			
			Console.WriteLine($"\nfinish");
		}
		
		static async Task RunAsync(){
			Console.WriteLine($"--> Start:RunAsync");
			
			var value = new List<int>{1, 2, 3};
			
			Task<int> t =  Task<int>.Run( () => Heavy(value) );
			
			var result = await t;
			
			Console.WriteLine($"result = {result}");
			Console.WriteLine($"<-- End :RunAsync");
		}
		
		static int Heavy(List<int> values){
			Console.WriteLine($"--> Start:Heavy");
			
			int result = 0;
			
			foreach(var v in values){
				Thread.Sleep(1000);
				result += v;
			}
			
			Console.WriteLine($"<-- End :Heavy");
			return result;
		}
	}
}
