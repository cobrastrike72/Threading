using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace ThreadingWinFormsApp
{
    public partial class Form1 : Form
    {
        int x = 0;
        int z = 100000; // i used global variables because thread doesn't return value or take a value as parameter unlike Task
        public Form1()
        {
            InitializeComponent();
        }

        int GetNumberOfPrimes(int x)
        {
            MessageBox.Show($"GetNumberOfPrimes function will be in thread no:{Thread.CurrentThread.ManagedThreadId.ToString()}");
            int c = 0;
            for (int i = 3; i < x; i++)
            {
                bool flag = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0) flag = false;
                }
                if (flag) c++;
            }
            return c;
        }

        #region Blocking (Synchronous waiting)  (.join)
        //void fun1()
        //{
        //    x = GetNumberOfPrimes(z);

        //}
        //public void button1_Click(object sender, EventArgs e)
        //{

        //    Thread th = new Thread(fun1);
        //    th.IsBackground = true;
        //    th.Start();
        //    th.Join(); // wait for the result --> this will block the main thread the ui won't be responsive because the main thread will be block because this is (Synchronous waiting)

        //    textBox1.Text = x.ToString();
        //    MessageBox.Show($"button 1 works in thread no: {Thread.CurrentThread.ManagedThreadId.ToString()}");

        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show($"button 2 works in thread no: {Thread.CurrentThread.ManagedThreadId.ToString()}");
        //}

        #endregion


        #region Non-Blocking (Asynchronous waiting) (await)
        async Task<int> fun1(int z)
        {
            Task<int> tq = new Task<int>(() => GetNumberOfPrimes(z)); // you can pass value as a parameter using call back function like that and make the call of the function you wanna pass a parameter in the body of the callback function
            tq.Start();
            int x = await tq;
            return x;
        }
        public async void button1_Click(object sender, EventArgs e)
        {
            int x = await fun1(100000); // the main thread will wait here and won't continue its work only in this function untill the other thread finishs its work --> but the main thread won't be blocked and will be free to do other work like keeping the ui responsive (moving the form or croping it in runtime) or if you click on the button two while the other thread calculting the primes the main thread will be able respond to you because it works asynchronously
            // not like the join will make the main thread blocked and everything will be frozen untill the result appear of the calculation

            textBox1.Text = x.ToString();
            MessageBox.Show($"button 1 works in thread no: {Thread.CurrentThread.ManagedThreadId.ToString()}");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"button 2 works in thread no: {Thread.CurrentThread.ManagedThreadId.ToString()}");
        }

        #endregion

        #region Difference between Await and Join
        /*
         Blocking (Synchronous waiting): (join)

            Meaning: You stop the current thread and wait for something to finish.

            Thread Usage: The thread is stuck doing nothing — just waiting.

            Efficiency: Wasteful — thread is idle (just standing there).

            Effect on UI apps: Freezes the app — users can’t click, move window, etc.


        Non-Blocking (Asynchronous waiting): (await)

            Meaning: You let the current thread do other work while waiting for something to finish.

            Thread Usage: The thread is free to continue doing other tasks (like keeping the UI responsive).

            Efficiency: Efficient — thread can serve other requests or update the UI.

            Effect on UI apps: Keeps app smooth — users can interact while waiting.


        Real Life Example 🛒

            Blocking (Join):
            Imagine you are at a coffee shop.
            You order a coffee, and you just stand there doing nothing until the coffee is ready. (Wasting time.)

            Non-Blocking (Await):
            You order a coffee, they give you a buzzer.
            You go sit, chat, work, relax.
            When the coffee is ready, the buzzer calls you back. (Much better.)

        */
        #endregion


        #region Diff Between Thread VS Task

        /*
        Can return a value?

        Task: ✅ Yes, using Task<T>

        Thread: ❌ No, thread method must be void

        --------------------------

        🧠 Passing Parameters

        Task:
        ✅ Directly pass parameters via lambda:
        Task.Run(() => Myfun(value));

        Thread (lambda):
        ✅ Also direct via lambda:
        new Thread(() => Myfun(value)).Start();

        --------------------------

        Can be awaited?

        Task: ✅ Yes (await Task) or .Result() --> async waiting (non-blocking)

        Thread: ❌ No (use join) --> sync (blocking)

        --------------------------

        Exception handling

        Task: ✅ Easy — exceptions are captured and re-thrown when you await

        Thread: ❌ Hard — must catch manually inside the thread

        --------------------------

        Efficiency

        Task: ✅ Reuses threads from the ThreadPool — lightweight

        Thread: ❌ Creates a new OS thread — heavier on resources


        --------------------------

        Cancellation

        Task: ✅ Supports cancellation with CancellationToken

        Thread: ❌ No built-in cancellation — you have to code it yourself

        --------------------------

        Scheduling

        Task: ✅ Managed by the .net runtime

        Thread: ❌ You manually manage starting, sleeping, and stopping

        --------------------------

        Parallelism

        Task: ✅ Easily parallelize tasks with Task.Run, Task.WhenAll, etc.

        Thread: ❌ More complicated to coordinate multiple threads

        --------------------------

        Best for

        Task: Short-running or asynchronous operations

        Thread: Long-running or low-level control (timing, thread affinity, etc.)


        */
        #endregion
    }
}
