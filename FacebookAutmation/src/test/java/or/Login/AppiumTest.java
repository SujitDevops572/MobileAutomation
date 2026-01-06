package or.Login;
import io.appium.java_client.android.AndroidDriver;
import io.appium.java_client.android.options.UiAutomator2Options;

import org.openqa.selenium.By;
import org.testng.annotations.Test;
import java.net.URL;

public class AppiumTest {

    
    public static void main(String[] args) throws Exception {

    	UiAutomator2Options options = new UiAutomator2Options()
    	        .setDeviceName("RZCW8253AJR")
    	        .setPlatformName("Android")
    	        .setAutomationName("UiAutomator2")
    	        .setAppPackage("com.example.simplecalculator")
    	        .setAppActivity("com.example.simplecalculator.MainActivity");

    	AndroidDriver driver = new AndroidDriver(new URL("http://127.0.0.1:4723"), options);
Thread.sleep(500);

driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn6\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn16\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn14\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn20\"]")).click();

driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn1\"]")).click();
Thread.sleep(200);

driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn7\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn12\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn5\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn20\"]")).click();

driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn1\"]")).click();
Thread.sleep(200);

driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btnScientific\"]")).click();

driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btnBasic\"]")).click();

driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn7\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn12\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn5\"]")).click();
driver.findElement(By.xpath("//android.widget.Button[@resource-id=\"com.example.simplecalculator:id/btn20\"]")).click();
Thread.sleep(2000);
driver.quit();
    }
}
