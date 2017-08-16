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
   [TestFixture] public class WorkItemTests : NUnit.Framework.Assertion
   {
      public class SampleWorkItem : WorkItem
      {
         public override void Perform()
         {
            // do nothing.
         }

      }

      [Test] public void Defaults()
      {
         SampleWorkItem a = new SampleWorkItem();

         AssertEquals ("priority", ThreadPriority.Normal, a.Priority);
         AssertEquals ("state", WorkItemState.Created, a.State);
      }

      [Test] public void Comparing()
      {
         SampleWorkItem a = new SampleWorkItem();
         SampleWorkItem b = new SampleWorkItem();
         a.Priority = ThreadPriority.BelowNormal;
         b.Priority = ThreadPriority.AboveNormal;

         Assert(a.CompareTo(a) == 0);
         Assert(a.CompareTo(b) < 0);
         Assert(b.CompareTo(a) > 0);
      }

      [Test] public void ValidTransitions()
      {
         SampleWorkItem work = new SampleWorkItem();

         AssertEquals (WorkItemState.Created, work.State);
         work.State = WorkItemState.Queued;
         work.State = WorkItemState.Scheduled;
         work.State = WorkItemState.Running;
         work.State = WorkItemState.Failing;
         work.State = WorkItemState.Completed;

         work = new SampleWorkItem();
         work.State = WorkItemState.Scheduled;
         work.State = WorkItemState.Running;
         work.State = WorkItemState.Completed;
      }

      [Test, ExpectedException(typeof(InvalidTransitionException))]
      public void InvalidTransition1()
      {
         SampleWorkItem work = new SampleWorkItem();
         work.State = WorkItemState.Completed;
      }

      [Test] public void Timing()
      {
         DateTime start = DateTime.Now;
         
         SampleWorkItem work = new SampleWorkItem();
         work.State = WorkItemState.Queued;
         work.State = WorkItemState.Scheduled;
         Thread.Sleep(TimeSpan.FromSeconds(1.5));

         work.State = WorkItemState.Running;
         Thread.Sleep(TimeSpan.FromSeconds(1.5));

         work.State = WorkItemState.Failing;
         work.State = WorkItemState.Completed;

         DateTime end = DateTime.Now;
         Assert ("creating time", work.CreatedTime >= start);
         Assert ("start time", work.StartedTime > work.CreatedTime);
         Assert ("completed time", work.CompletedTime > work.StartedTime);
         Assert ("completed time", work.CompletedTime <= end);

      }
   }
}
