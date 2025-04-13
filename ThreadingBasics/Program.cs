using System.Diagnostics;

namespace Threading;

internal class Program
{

    static void Main(string[] args)
    {

        #region without threading

        //Stopwatch sw = Stopwatch.StartNew();
        //Console.WriteLine("Main thread start!");
        //Console.WriteLine($"Thread Id in main: {Thread.CurrentThread.ManagedThreadId}");

        //fun1(); // uses the main thread
        //fun2(); // uses the main thread

        //Console.WriteLine("Main End!");
        //sw.Stop();

        //Console.WriteLine($"Total time consumed: {sw.ElapsedMilliseconds}" ); // will consume 5 seconds because it only use one thread which is the main thread to run all the functions

        #endregion

        #region Foreground Threads

        //Stopwatch sw = Stopwatch.StartNew();
        //Console.WriteLine("Main thread start!");
        //Console.WriteLine($"Thread Id in main: {Thread.CurrentThread.ManagedThreadId} <::::> Is a backgorund Thread: {Thread.CurrentThread.IsBackground}");
        //Thread th1 = new Thread(fun1);
        //Thread th2 = new Thread(fun2);

        //th1.Start(); // so here the main thread will start other two threads and make them run and continue his work like which is Console.WriteLine("Main End!"); and the rest of lines and won't wait for thread one or two to finish in order to continue his work
        //// but a slight note here since thread one and two are <foreground threads>
        //// the main thread won't end even after finishing his work till thread one and two finish their work too
        //th2.Start();

        //Console.WriteLine("Main End!");
        //sw.Stop();

        //Console.WriteLine($"Total time consumed: {sw.ElapsedMilliseconds}" ); // the time will be like <5 milliseconds> why because main thread won't wait for the other threads in order to continue his work

        #endregion

        #region Background Threads

        //Stopwatch sw = Stopwatch.StartNew();
        //Console.WriteLine("Main thread start!");
        //Console.WriteLine($"Thread Id in main: {Thread.CurrentThread.ManagedThreadId} <::::> Is a backgorund Thread: {Thread.CurrentThread.IsBackground}");
        //Thread th1 = new Thread(fun1);
        //Thread th2 = new Thread(fun2);
        //th1.IsBackground = true;
        //th2.IsBackground = true;

        //th1.Start(); // so here the main thread will start other two threads and make them run and continue his work like which is Console.WriteLine("Main End!"); and the rest of lines and won't wait for thread one or two to finish in order to continue his work
        //// but a slight note here since thread one and two are <background threads>
        //// the main thread will end end after finishing his work and the other two threads also will end accordingly even if thier work weren't finished yet
        //th2.Start();

        //Console.WriteLine("Main End!");
        //sw.Stop();

        //Console.WriteLine($"Total time consumed: {sw.ElapsedMilliseconds}"); // will only consume like 3 or 5 milliseconds because it won't wait for the other two threads

        #endregion

        #region join to wait for other threads to finish their work before the main thread continues his work
        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine("Main thread start!");
        Console.WriteLine($"Thread Id in main: {Thread.CurrentThread.ManagedThreadId}");
        Thread th1 = new Thread(fun1);
        Thread th2 = new Thread(fun2);
        th1.IsBackground = true;
        th2.IsBackground = true;

        th1.Start();
        th2.Start();
        Console.WriteLine("Main End!");
        th1.Join(); // will block the main thread --> Synchronous waiting (blocking) // if the main thread was doing anything else beside the flow of the lines of code in the main method here 
        // the main thread won't be able to continue it because it will be blocked 

        th2.Join();
        sw.Stop();

        Console.WriteLine($"Total time consumed: {sw.ElapsedMilliseconds}"); // will only consume 3 seconds (which is the maximum time of times consumed by anythread)
        #endregion

    }

    static void fun1()
    {
        Console.WriteLine("Fun1 starting ........");
        Console.WriteLine($"Thread Id in main: {Thread.CurrentThread.ManagedThreadId} <::::> Is a backgorund Thread: {Thread.CurrentThread.IsBackground}");
        Thread.Sleep(2000);
        Console.WriteLine("Fun1 end........");
    }

    static void fun2()
    {
        Console.WriteLine("Fun2 starting .....");
        Console.WriteLine($"Thread Id in main: {Thread.CurrentThread.ManagedThreadId} <::::> Is a backgorund Thread: {Thread.CurrentThread.IsBackground}");
        Thread.Sleep(3000);

        Console.WriteLine("Fun2 end... ");
    }
   
}
