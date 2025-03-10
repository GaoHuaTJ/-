// See https://aka.ms/new-console-template for more information
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System.Reflection;

Console.WriteLine("Hello, World!");

// 初始化 Chrome 驱动（确保 Chrome 浏览器已安装）
IWebDriver driver = new ChromeDriver();
driver.Manage().Window.Maximize();
try
{
    // 导航到目标网页
    driver.Navigate().GoToUrl("https://www.baidu.com");

    // 通过 CDP 执行完整页面截图
    var devTools = driver as IDevTools;
    if (devTools == null)
    {
        throw new Exception("当前驱动不支持 DevTools");
    }

    // 获取完整页面截图（base64 编码）
    var screenshotResponse = devTools.GetDevToolsSession()
        .SendCommand<OpenQA.Selenium.DevTools.V133.Page.CaptureScreenshotCommandSettings>(
            new OpenQA.Selenium.DevTools.V133.Page.CaptureScreenshotCommandSettings
            {
                Format = "png",
                CaptureBeyondViewport = true, // 关键参数：截取整个页面
                FromSurface = true
            }
        );

    // 保存截图到文件
    byte[] screenshotBytes = Convert.FromBase64String(screenshotResponse.Result.);
   
    
    File.WriteAllBytes("fullpage_screenshot.png", screenshotBytes);
    Console.WriteLine("已保存");
}
finally
{
    driver.Quit();
}
;


