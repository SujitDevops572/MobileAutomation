package org.nnew;

import com.aventstack.extentreports.ExtentReports;
import com.aventstack.extentreports.ExtentTest;
import com.aventstack.extentreports.reporter.ExtentSparkReporter;

import java.io.File;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.testng.annotations.Test;

public class Xtent {
	
	
	    
	@Test
	void apple() throws InterruptedException {
		
		System.out.println("APple");
		System.out.println("MAngo!!!");
		WebDriver driver = new ChromeDriver();
		driver.get("https://www.youtube.com/");
		 try {
		        Thread.sleep(5000);
		        System.out.println("HEllo!!!!!!!!!!!!!!!!!!!!!");
		    } catch (InterruptedException e) {
		        e.printStackTrace();
		    }
		 
		 ExtentSparkReporter spark = new ExtentSparkReporter("D:\\ExtentReport\\AutomationReport\\TestReport12.html");
		 ExtentReports extent = new ExtentReports();
		 extent.attachReporter(spark);

		 ExtentTest test = extent.createTest("Check the Credit Value Option in Client User List");

		 test.info("Test started");

		 System.out.println("Wait starts!!!!!!");
		 
		 Thread.sleep(5000);
		 
		 
		 
		 test.pass("Test passed successfully");

		 extent.flush();
	 
	

}
	}
