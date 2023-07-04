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
		static CancellationTokenSource tokenSource = new ();
		
		static void Main(string[] args)
		{
			Console.WriteLine($"start");
			
			Console.WriteLine($"--> call RunAsync");
			Task t = RunAsync(tokenSource.Token);
			Console.WriteLine($"<-- return from RunAsync");
			
			try{
				while(!t.IsCompleted){
					ConsoleKey key;
					if(IsKeyPressed(out key)){
						if(key == ConsoleKey.Spacebar)	tokenSource.Cancel();
					}
					
					if(t.Wait(200)) break;
					Console.Write(".");
				}
			}catch (AggregateException ex){
				Console.WriteLine($"---> {ex.GetType()}");
				foreach (var inner in ex.Flatten().InnerExceptions){
					Console.WriteLine($"---{inner.GetType()}");
					Console.WriteLine($"---{inner.Message}");
					
					if(inner.GetType() == typeof(OperationCanceledException)){
						Console.WriteLine("---sj:OperationCanceledException.");
					}else if(inner.GetType() == typeof(TaskCanceledException)){
						Console.WriteLine("---sj:TaskCanceledException.");
					}
				}
			}
			
			if(t.Status == TaskStatus.Canceled){
				Console.WriteLine("---write your own process when canceled.");
			}
			
			Console.WriteLine($"\nfinish");
		}
		
		static async Task RunAsync(CancellationToken token){
			Console.WriteLine($"--> Start:RunAsync");
			
			var value = new List<int>{1, 2, 3};
			
			Task t =  Task.Run( () => Heavy(value, token), token );
			
			try{
				await t;
			}catch (OperationCanceledException ex){
				Console.WriteLine($"--{ex.GetType()}");
				Console.WriteLine($"--{ex.Message}");
				throw;
			}
			
			Console.WriteLine($"<-- End :RunAsync");
		}
		
		static void Heavy(List<int> values, CancellationToken token){
			Console.WriteLine($"--> Start:Heavy");
			
			int result = 0;
			
			foreach(var v in values){
				Thread.Sleep(1000);
				
				token.ThrowIfCancellationRequested();
				result += v;
			}
			
			Console.WriteLine($"\nsum = {result}");
			Console.WriteLine($"<-- End :Heavy");
		}
		
		static bool IsKeyPressed(out ConsoleKey key){
			key = ConsoleKey.A; // temp;
			
			bool ret = false;
			
			if (Console.KeyAvailable == true){
				ConsoleKeyInfo cki = new ConsoleKeyInfo();
				cki = Console.ReadKey(true);
				
				key = cki.Key;
				ret = true;
			}
			
			return ret;
		}
	}
}
