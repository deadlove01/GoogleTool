#region Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
/*
   Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
   All rights are reserved.

   Permission to use, copy, modify, and distribute this software 
   for any purpose and without any fee is hereby granted, 
   provided this notice is included in its entirety in the 
   documentation and in the source files.
  
   This software and any related documentation is provided "as is" 
   without any warranty of any kind, either express or implied, 
   including, without limitation, the implied warranties of 
   merchantibility or fitness for a particular purpose. The entire 
   risk arising out of use or performance of the software remains 
   with you. 
   
   In no event shall Richard Schneider, Black Hen Limited, or their agents 
   be liable for any cost, loss, or damage incurred due to the 
   use, malfunction, or misuse of the software or the inaccuracy 
   of the documentation associated with the software. 
*/
#endregion

using NUnit.Framework;
using System;
using System.Globalization;
using System.Threading;

namespace BlackHen.Threading.Tests
{
   [TestFixture] public class WorkQueueTest : NUnit.Framework.Assertion
   {
      private WorkQueue worklist;

      private int started;
      private int completed;
      private int failed;
      private bool workcompleted;
      [Test] public void WorkItemEvents()
      {
         started = 0;
         completed = 0;
         failed = 0;
         workcompleted = false;

         worklist = new WorkQueue();
         worklist.RunningWorkItem +=new WorkItemEventHandler(worklist_StartedWorkItem);
         worklist.FailedWorkItem +=new WorkItemEventHandler(worklist_FailedWorkItem);
         worklist.CompletedWorkItem +=new WorkItemEventHandler(worklist_CompletedWorkItem);
         worklist.AllWorkCompleted +=new EventHandler(worklist_WorkCompleted);
         worklist.Add(new SimpleWork(2, ThreadPriority.Normal));
         worklist.Add(new SimpleWork(1, ThreadPriority.AboveNormal));
         worklist.Add(new AutomatedWork(3, ThreadPriority.BelowNormal));
         worklist.Add(new AutomatedWork(4, ThreadPriority.BelowNormal));
         worklist.Add(new FailedWork());
         worklist.WaitAll();

         AssertEquals ("started", 5, started);
         AssertEquals ("failed", 1, failed);
         AssertEquals ("completed", 5, completed);
         Assert ("workcompleted", workcompleted);
      }

      private void worklist_StartedWorkItem(object sender, WorkItemEventArgs e)
      {
         AssertEquals ("worklist", worklist, sender);
         ++started;
      }
      private void worklist_FailedWorkItem(object sender, WorkItemEventArgs e)
      {
         AssertEquals ("failed test", e.WorkItem.FailedException.Message);
         AssertEquals ("worklist", worklist, sender);
         ++failed;
      }
      private void worklist_CompletedWorkItem(object sender, WorkItemEventArgs e)
      {
         AssertEquals ("worklist", worklist, sender);
         ++completed;
      }
      private void worklist_WorkCompleted(object sender, EventArgs e)
      {
         workcompleted = true;
      }

      private int scheduled;
      [Test] public void StateTransitions()
      {
         scheduled = 0;
         completed = 0;
         failed = 0;
         running = 0;

         worklist = new WorkQueue();
         worklist.ChangedWorkItemState += new ChangedWorkItemStateEventHandler(worklist_ChangedWorkItemState);
         worklist.Add(new SimpleWork(2, ThreadPriority.Normal));
         worklist.Add(new SimpleWork(1, ThreadPriority.AboveNormal));
         worklist.Add(new AutomatedWork(3, ThreadPriority.BelowNormal));
         worklist.Add(new AutomatedWork(4, ThreadPriority.BelowNormal));
         worklist.Add(new FailedWork());
         worklist.WaitAll();

         AssertEquals ("scheduled", 5, scheduled);
         AssertEquals ("running", 5, running);
         AssertEquals ("failed", 1, failed);
         AssertEquals ("completed", 5, completed);
      }

      private void worklist_ChangedWorkItemState(object sender, ChangedWorkItemStateEventArgs e)
      {
         switch (e.WorkItem.State)
         {
            case WorkItemState.Completed: ++completed; break;
            case WorkItemState.Failing: ++failed; break;
            case WorkItemState.Running: ++running; break;
            case WorkItemState.Scheduled: ++scheduled; break;
         }
      }

      private int running;
      [Test] public void Concurrency()
      {
         running = 0;
         completed = 0;
         worklist = new WorkQueue();
         worklist.RunningWorkItem += new WorkItemEventHandler(concurrent_StartedWorkItem);
         worklist.CompletedWorkItem +=new WorkItemEventHandler(concurrent_CompletedWorkItem);
         for (int i = 1; i <= 50; ++i)
         {
            worklist.Add(new AutomatedWork(i));
         }
         worklist.WaitAll();
         AssertEquals ("completed", 50, completed);
      }
      private void concurrent_StartedWorkItem(object sender, WorkItemEventArgs e)
      {
         AssertEquals ("worklist", worklist, sender);
         ++running;
         Assert ("concurrent limit exceeded", running <= worklist.ConcurrentLimit);
      }
      private void concurrent_CompletedWorkItem(object sender, WorkItemEventArgs e)
      {
         AssertEquals ("worklist", worklist, sender);
         --running;
         ++completed;
      }

      [Test] public void WaitTimeout()
      {
         // Check for a timeout.
         started = 0;
         completed = 0;
         worklist = new WorkQueue();
         worklist.RunningWorkItem += new WorkItemEventHandler(worklist_StartedWorkItem);
         worklist.CompletedWorkItem +=new WorkItemEventHandler(concurrent_CompletedWorkItem);
         worklist.Add(new SimpleWork(1));
         worklist.Add(new SimpleWork(2));
         AutomatedWork aw = new AutomatedWork(3, ThreadPriority.Lowest);
         aw.millisecondsTimeout = 5000;
         worklist.Add(aw);
         Assert ("timeout", !worklist.WaitAll(TimeSpan.FromSeconds(2)));
         AssertEquals ("started", 3, started);
         AssertEquals ("completed", 2, completed);
      }

      [Test] public void Pausing()
      {
         started = 0;
         completed = 0;
         worklist = new WorkQueue();
         worklist.RunningWorkItem += new WorkItemEventHandler(worklist_StartedWorkItem);
         worklist.CompletedWorkItem +=new WorkItemEventHandler(worklist_CompletedWorkItem);
         worklist.Pause();
         for (int i = 1; i <= 10; ++i)
         {
            worklist.Add(new AutomatedWork(i));
         }
         AssertEquals ("started", 0, started);
         AssertEquals ("completed", 0, completed);
         AssertEquals ("queued", 10, worklist.Count);
         worklist.Resume();
         worklist.WaitAll(TimeSpan.FromSeconds(60));
         AssertEquals ("started", 10, started);
         AssertEquals ("completed", 10, completed);
      }

      [Test, ExpectedException(typeof(InvalidOperationException))]
      public void IllegalPause()
      {
         worklist = new WorkQueue();
         worklist.Pause();
         worklist.WaitAll();
      }

      [Test] public void Priority()
      {
         AutomatedWork a = new AutomatedWork(1, ThreadPriority.Lowest, 100);
         AutomatedWork b = new AutomatedWork(2, ThreadPriority.BelowNormal, 100);
         AutomatedWork c = new AutomatedWork(3, ThreadPriority.Normal, 100);
         worklist = new WorkQueue();
         worklist.ConcurrentLimit = 1;
         worklist.Pause();
         worklist.Add(a);
         worklist.Add(b);
         worklist.Add(c);
         worklist.Resume();
         worklist.WaitAll();

         Assert("c is first", c.StartedTime < b.StartedTime);
         Assert("b is second", b.StartedTime < a.StartedTime);
      }

      [Test] public void UICulture()
      {
         string expectedText;

         CultureInfo previousUICulture = CultureInfo.CurrentUICulture;
         if (previousUICulture.TwoLetterISOLanguageName == "en")
         {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            expectedText = "bonjour";
         }
         else
         {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            expectedText = "hello";
         }
         Hello hello = new Hello();
         Thread.CurrentThread.CurrentUICulture = previousUICulture;

         worklist = new WorkQueue();
         worklist.Add(hello);
         worklist.WaitAll();

         AssertEquals (expectedText, hello.text);
      }

      [Test, ExpectedException(typeof(Exception), "testing exceptions on another thread.")]
      public void WaitRethrowsThreadExceptions()
      {
         worklist = new WorkQueue();
         worklist.CompletedWorkItem += new WorkItemEventHandler(te_CompletedWorkItem);
         worklist.Add(new SimpleWork(1));
         worklist.WaitAll();
      }
      private void te_CompletedWorkItem(object sender, WorkItemEventArgs e)
      {
         throw new Exception("testing exceptions on another thread.");
      }

      private ResourceExceptionEventArgs wteArgs;
      [Test] public void WorkThreadException()
      {
         wteArgs = null;
         worklist = new WorkQueue();
         worklist.WorkerException += new ResourceExceptionEventHandler(worklist_WorkThreadException);
         worklist.CompletedWorkItem += new WorkItemEventHandler(te_CompletedWorkItem);
         worklist.Add(new SimpleWork(1));
         try
         {
            worklist.WaitAll();
         }
         catch (Exception)
         {
         }
         AssertNotNull ("WorkThreadException event", wteArgs);
         AssertNotNull (wteArgs.Exception);
         AssertEquals ("testing exceptions on another thread.", wteArgs.Exception.Message);
      }
      private void worklist_WorkThreadException(object sender, ResourceExceptionEventArgs e)
      {
         wteArgs = e;
      }

      private int acalls;
      private int bcalls;
      [Test] public void Multicasting()
      {
         acalls = 0;
         bcalls = 0;
         worklist = new WorkQueue();
         worklist.CompletedWorkItem += new WorkItemEventHandler(ACompletedWorkItem);
         worklist.CompletedWorkItem += new WorkItemEventHandler(BCompletedWorkItem);
         worklist.AllWorkCompleted += new EventHandler(multicasting_AllWorkCompleted);
         for (int i = 1; i <= 10; ++i)
         {
            worklist.Add(new AutomatedWork(i));
         }
         worklist.WaitAll();

         AssertEquals ("acalls", 10, acalls);
         AssertEquals ("bcalls", 10, bcalls);

         worklist.CompletedWorkItem -= new WorkItemEventHandler(ACompletedWorkItem);
      }
      private void ACompletedWorkItem(object sender, WorkItemEventArgs e)
      {
         ++acalls;
      }
      private void BCompletedWorkItem(object sender, WorkItemEventArgs e)
      {
         ++bcalls;
      }
      private void multicasting_AllWorkCompleted(object sender, EventArgs e)
      {
         worklist.CompletedWorkItem -= new WorkItemEventHandler(BCompletedWorkItem);
      }

      #region Helper Classes
      private class Hello : WorkItem
      {
         public string text;

         public override void Perform()
         {
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
               text = "hello";
            else
               text = "bonjour";
         }

      }

      public class FailedWork : WorkItem
      {
         public override void Perform()
         {
            throw new Exception("failed test");
         }

      }

      public class SimpleWork : WorkItem
      {
         public int id;

         public SimpleWork(int id) : base()
         {
            this.id = id;
         }

         public SimpleWork(int id, ThreadPriority priority) : base()
         {
            this.id = id;
            this.Priority = priority;
         }

         public override void Perform()
         {
            // Do nothing.
         }

      }

      public class AutomatedWork : WorkItem
      {
         public int id;
         public int millisecondsTimeout = 10;

         public AutomatedWork(int id) : base()
         {
            this.id = id;
         }

         public AutomatedWork(int id, ThreadPriority priority) : base()
         {
            this.id = id;
            this.Priority = priority;
         }

         public AutomatedWork(int id, ThreadPriority priority, int sleepTime) : base()
         {
            this.id = id;
            this.Priority = priority;
            this.millisecondsTimeout = sleepTime;
         }

         public override void Perform()
         {
            //Console.WriteLine("AutomatedWork #{0}: on thread {1}.", this.id, AppDomain.GetCurrentThreadId());
            // Spend some time doing the work.
            Thread.Sleep(millisecondsTimeout);
         }

         public override string ToString()
         {
            return id.ToString();
         }

      }
      #endregion

   }
}
