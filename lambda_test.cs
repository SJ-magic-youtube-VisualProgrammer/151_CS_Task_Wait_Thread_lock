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
			Case_2();
		}
		
		static void Case_0(){
			string str_org = "Hello";
			
			string str_temp = str_org;
			Action action = () => Console.WriteLine(str_temp);
			
			str_org += " World!";
			
			action();
			
			Console.WriteLine(str_org);
		}
		
		static void Case_1(){
			const int NUM = 5;
			
			Action[] actions = new Action[NUM];
			
			for(int i = 0; i < NUM; i++){
				int _i = i;
				actions[i] = () => Func(_i);
			}
			
			for(int i = 0; i < NUM; i++){
				actions[i]();
			}
		}
		
		static void Case_2(){
			const int NUM = 5;
			
			var tsks = new Task[NUM];
			
			for(int i = 0; i < NUM; i++){
				int _i = i;
				tsks[i] = Task.Run( () => Func(_i) );
			}
			
			Task.WaitAll(tsks);
		}
		
		static void Func(int i){
			Console.WriteLine($"param = {i}");
		}
		
		
	}
}
