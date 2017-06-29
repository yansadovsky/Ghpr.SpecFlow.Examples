﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Ghpr.SpecFlowPlugin;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Ghpr.SpecFlow.Examples.Steps
{
    [Binding]
    public class BasicsSteps
    {
        private int _firstNumber;
        private int _sum;

        public static byte[] TakeScreen()
        {
            var b = Screen.PrimaryScreen.Bounds;
            var ic = new ImageConverter();
            using (var btm = new Bitmap(b.Width, b.Height))
            using (var g = Graphics.FromImage(btm))
            {
                g.CopyFromScreen(b.X, b.Y, 0, 0, btm.Size, CopyPixelOperation.SourceCopy);
                return (byte[])ic.ConvertTo(btm, typeof(byte[]));
            }
        }

        [Given(@"I have number (.*)")]
        public void GivenIHaveNumber(int p0)
        {
            Trace.WriteLine("TRACE: I have some number...");
            Console.WriteLine("CONSOLE: I have some number...");
            Debug.WriteLine("DEBUG: I have some number...");
            _firstNumber = p0;
            _sum = p0;
        }

        [When(@"I add (.*)")]
        public void WhenIAdd(int p0)
        {
            _sum += p0;
        }

        [Then(@"the result sum should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            Assert.AreEqual(p0, _sum);
        }

        [When(@"I take screenshot")]
        public void WhenITakeScreenshot()
        {
            var bytes = TakeScreen();
            ScreenHelper.SaveScreenshot(bytes);
        }

        [AfterScenario]
        public static void WrapUpReport()
        {
            switch (TestContext.CurrentContext.Result.Outcome.Status.ToString().ToLower())
            {
                case "passed":
                    var bytes = TakeScreen();
                    ScreenHelper.SaveScreenshot(bytes);
                    break;

                default:
                    break;
            }
        }
    }
}
