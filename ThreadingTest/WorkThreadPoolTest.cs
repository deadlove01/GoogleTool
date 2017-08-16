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
using System.Threading;

namespace BlackHen.Threading.Tests
{
   [TestFixture] public class WorkThreadPoolTests : NUnit.Framework.Assertion
   {
      [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void BadConstruct1()
      {
         WorkThreadPool threadPool = new WorkThreadPool(0, 0);
      }

      [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void BadConstruct2()
      {
         WorkThreadPool threadPool = new WorkThreadPool(4, 3);
      }

      [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void BadConstruct3()
      {
         WorkThreadPool threadPool = new WorkThreadPool(-1, 3);
      }

      [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void BadConstruct4()
      {
         WorkThreadPool threadPool = new WorkThreadPool(4, -1);
      }

      [Test] public void DefaultThreadPool()
      {
         WorkThreadPool threadPool = WorkThreadPool.Default;
         AssertNotNull (threadPool);
      }

      [Test] public void Disposing()
      {
         WorkThreadPool threadPool = new WorkThreadPool();
         threadPool.Dispose();
      }

      private ThreadPriority runningPriority;
      [Test] public void RunningPriority()
      {
         runningPriority = (ThreadPriority) (-1);
         WorkQueue worklist = new WorkQueue();
         worklist.RunningWorkItem += new WorkItemEventHandler(worklist_RunningWorkItem);
         worklist.Add(new WorkQueueTest.SimpleWork(1, ThreadPriority.AboveNormal));
         worklist.WaitAll();

         AssertEquals ("running priority", ThreadPriority.AboveNormal, runningPriority);
      }

      private void worklist_RunningWorkItem(object sender, WorkItemEventArgs e)
      {
         runningPriority = Thread.CurrentThread.Priority;
      }

      private volatile ResourceExceptionEventArgs wteArgs;
      [Test] public void WorkThreadException()
      {
         wteArgs = null;
         IWorkItem work = new FailedWork();
         work.State = WorkItemState.Scheduled;

         WorkThreadPool.Default.ThreadException += new ResourceExceptionEventHandler(OnWorkThreadException);
         WorkThreadPool.Default.BeginWork(work);

         while (wteArgs == null)
         {
            Thread.Sleep(TimeSpan.FromMilliseconds(50));
         }

         AssertNotNull ("WorkThreadException event", wteArgs);
         AssertNotNull (wteArgs.Exception);
         AssertEquals ("WorkThreadException testing.", wteArgs.Exception.Message);
      }
      private void OnWorkThreadException(object sender, ResourceExceptionEventArgs e)
      {
         wteArgs = e;
      }

      private class FailedWork : WorkItem
      {
         public override void Perform()
         {
            // Do nothing
         }

         protected override void ValidateStateTransition(WorkItemState current, WorkItemState next)
         {
            base.ValidateStateTransition (current, next);

            if (next == WorkItemState.Completed)
               throw new Exception("WorkThreadException testing.");
         }


      }

   }
}
