using System.Diagnostics;
using System.Threading;

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

        //Background threads mean:
        //→ They don't keep the application alive.
        //→ If the main(foreground) thread exits, background threads are automatically killed.

        //Stopwatch sw = Stopwatch.StartNew();
        //Console.WriteLine("Main thread start!");
        //Console.WriteLine($"Thread Id in main: {Thread.CurrentThread.ManagedThreadId} <::::> Is a backgorund Thread: {Thread.CurrentThread.IsBackground}");
        //Thread th1 = new Thread(fun1);
        //Thread th2 = new Thread(fun2);
        //th1.IsBackground = true;
        //th2.IsBackground = true;

        //th1.Start(); // so here the main thread will start other two threads and make them run and continue his work like which is Console.WriteLine("Main End!"); and the rest of lines and won't wait for thread one or two to finish in order to continue his work
        //// but a slight note here since thread one and two are <background threads>
        //// the main thread will end after finishing his work and the other two threads also will end accordingly even if thier work weren't finished yet
        //// if you don't want them to end unless they finish thier work too or if the main thread depend on them in its work use <.join()> in the example below you will understand
        //th2.Start();

        //Console.WriteLine("Main End!");
        //sw.Stop();

        //Console.WriteLine($"Total time consumed: {sw.ElapsedMilliseconds}"); // will only consume like 3 or 5 milliseconds because it won't wait for the other two threads

        #endregion

        #region join to wait for other threads to finish their work before the main thread continues its work

        // use .join() if the current thread(main thread) depeneds on them in its work or (if the thread is a background thread and you want thier work to be completed even if the main thread finishs his work before them

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
        // the main thread won't be able to continue it because it will be blocked till th1 finishes its work

        th2.Join(); // same as th1

        //after th1 and th2 finish thier work main thread will continue its work below
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
