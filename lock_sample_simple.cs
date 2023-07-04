/************************************************************
■ThreadPool.GetMaxThreads(Int32, Int32) メソッド
	https://learn.microsoft.com/ja-jp/dotnet/api/system.threading.threadpool.getmaxthreads?view=net-7.0#system-threading-threadpool-getmaxthreads(system-int32@-system-int32@)
************************************************************/
namespace VisuapProgrammer_sj{
	/****************************************
	****************************************/
	internal class LockTest{
		// public int Count{ get; private set; } = 0;
		volatile private int Count = 0;
		
		/********************
		ロック対象のオブジェクトには、参照型の任意のオブジェクトを指定できます。
		通常は、object型でOKです。
		static method内で使用する場合、こちらも、staticにしてください。
		********************/
		object lockobj = new object();
		
		/******************************
		******************************/
		static void Main(string[] args){
			/********************
			********************/
			const int NUM = 50000;
			
			var tsks = new Task[NUM];
			var lockTest = new LockTest();
			
			for(int i = 0; i < NUM; i++){
				tsks[i] = Task.Run( () => lockTest.Increment() );
			}
			
			Task.WaitAll(tsks);
			
			Console.WriteLine(lockTest.Count);
		}
		
		/******************************
		******************************/
		void Increment(){
			// Console.WriteLine($"ThreadId = {Thread.CurrentThread.ManagedThreadId}");
			// Thread.Sleep(1000);
			
			Count++;
			// lock(lockobj) { Count++; }
		}
	}
}


