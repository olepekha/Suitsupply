﻿using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using log4net.Core;
using log4net;



namespace ShopNet
{
    public class TestBase
    {
        public IWebDriver driver;
        public WebDriverWait waitf;
        TimeSpan t = new TimeSpan(0, 1, 0);
        public ILog logger ;

        public TestBase()
        {
             XmlConfigurator.Configure();
            logger = log4net.LogManager.GetLogger(typeof(TestBase));
         }

        public void Initialize(String BrowserName) 
        {
            logger.Info("Init method has been called");
            if (BrowserName.Equals("firefox"))
            {
                driver = new FirefoxDriver();
            }
            else 
                driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Manage().Window.Maximize();
            waitf = new WebDriverWait(driver, t);
        }
     

        [TearDown]
        public void CloseBrowser() 
        {
            driver.Quit();
        }

        public static IEnumerable<String> BrowsersToRunWith() 
        {
            String[] browsers = { "chrome" };//, "firefox"};

            foreach (String b in browsers)
            {
                yield return b;
            }
         }
    }


    
}
