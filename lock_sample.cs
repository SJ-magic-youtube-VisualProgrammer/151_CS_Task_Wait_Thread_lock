/************************************************************
■ThreadPool.GetMaxThreads(Int32, Int32) メソッド
	https://learn.microsoft.com/ja-jp/dotnet/api/system.threading.threadpool.getmaxthreads?view=net-7.0#system-threading-threadpool-getmaxthreads(system-int32@-system-int32@)
************************************************************/
namespace VisuapProgrammer_sj{
	/****************************************
	****************************************/
	internal class LockTest{
		public int Count{ get; private set; } = 0;
		// volatile private int Count = 0;
		
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
			// const int NUM = 50000;
			const int NUM = 100;
			
			var tsks = new Task[NUM];
			var lockTest = new LockTest();
			
			// Console.WriteLine(">---------- max threads");
			// lockTest.PrintNumWorkerThread_max();
			
			for(int i = 0; i < NUM; i++){
				tsks[i] = Task.Run( () => lockTest.Increment() );
			}
			
			/*
			Console.WriteLine(">---------- available therads");
			while(!IsAllTaskCompleted(ts)){
				Task.WaitAll(ts, 200);
				Console.Write(".");
				// lockTest.PrintNumWorkerThread_available();
			}
			*/
			Task.WaitAll(tsks);
			
			Console.WriteLine(lockTest.Count);
			
			// Console.WriteLine(">---------- available therads");
			// lockTest.PrintNumWorkerThread_available();
		}
		
		/******************************
		******************************/
		static bool IsAllTaskCompleted(Task[] tsks){
			foreach(var tsk in tsks){
				if(!tsk.IsCompleted) return false;
			}
			
			return true;
		}
		
		/******************************
		******************************/
		void PrintNumWorkerThread_max(){
			int num_workerThreads;			// スレッド プール内のワーカー スレッド
			int num_completionPortThreads;	// スレッド プール内の非同期 I/O スレッド
			
			ThreadPool.GetMaxThreads(out num_workerThreads, out num_completionPortThreads);
			
			Console.WriteLine($"num_workerThreads = {num_workerThreads}, num_completionPortThreads = {num_completionPortThreads}");
		}
		
		/******************************
		******************************/
		void PrintNumWorkerThread_available(){
			int num_workerThreads;			// スレッド プール内のワーカー スレッド
			int num_completionPortThreads;	// スレッド プール内の非同期 I/O スレッド
			
			ThreadPool.GetAvailableThreads(out num_workerThreads, out num_completionPortThreads);
			Console.WriteLine($"num_workerThreads = {num_workerThreads}, num_completionPortThreads = {num_completionPortThreads}");
		}
		
		/******************************
		******************************/
		void Increment(){
			Console.WriteLine($"ThreadId = {Thread.CurrentThread.ManagedThreadId}");
			// Thread.Sleep(1000);
			
			// Count++;
			lock(lockobj) { Count++; }
		}
	}
}


