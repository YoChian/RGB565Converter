# RGB565Converter

一个实现将JPG，PNG，BMP，GIF转换为RGB565格式的.h文件的简易工具。

内置简单的缩放和裁剪功能（可能的话请尽量使用其他图像编辑软件进行编辑）。

## 纯逻辑测试

项目中的 WinForms UI 现在只负责交互，RGB565 编码、头文件拼装、位图帧提取与裁剪/缩放逻辑已经拆分到 `RGB565Converter/Core/` 下的独立类中。

新增了一个不依赖 WinForms 的控制台测试项目 `RGB565Converter.LogicTests/`，可以单独验证纯逻辑代码。安装 .NET SDK 后可运行：

```bash
dotnet run --project RGB565Converter.LogicTests/RGB565Converter.LogicTests.csproj
```
