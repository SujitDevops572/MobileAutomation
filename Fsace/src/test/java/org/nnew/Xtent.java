package org.nnew;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.testng.annotations.Test;

public class Xtent {

	@Test
	void apple() {
		
		System.out.println("APple");
		System.out.println("MAngo!!!");
		WebDriver driver = new ChromeDriver();
		driver.get("https://www.youtube.com/");
		
	}
	
	
	
}
