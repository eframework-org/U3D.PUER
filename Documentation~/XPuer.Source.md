# XPuer.Source

[![Version](https://img.shields.io/npm/v/org.eframework.u3d.puer)](https://www.npmjs.com/package/org.eframework.u3d.puer)
[![Downloads](https://img.shields.io/npm/dm/org.eframework.u3d.puer)](https://www.npmjs.com/package/org.eframework.u3d.puer)

XPuer.Source 实现了源文件管理系统，用于 TypeScript 源文件的解析和多编辑器集成。

## 功能特性

- 源文件解析：自动解析 TypeScript 源文件，分析类继承关系和字段信息
- 多编辑器支持：提供可视化配置选项，允许用户设置代码编辑器工具

## 使用手册

### 1. 首选项配置

源文件管理工具提供了以下配置选项：

#### 源文件路径
- 配置键：`Puer/Source/Path@Editor`
- 默认值：`Assets/TypeScripts`
- 功能说明：
  - 设置 TypeScript 源文件的根目录路径
  - 所有的源文件都将从此目录下解析
  - 自动监控目录下的文件变化

#### 编辑器选择
- 配置键：`Puer/Source/Tool@Editor`
- 默认值：`Auto`
- 功能说明：
  - 设置用于打开和编辑源文件的编辑器工具
  - 支持以下编辑器类型：
    - `Auto`：自动选择系统中可用的编辑器
    - `Code`：使用 Visual Studio Code
    - `Cursor`：使用 Cursor 编辑器
    - `IDEA`：使用 IntelliJ IDEA
  - 根据选择的编辑器自动配置打开方式

以上配置项均可在 `Tools/EFramework/Preferences/Puer/Source` 首选项编辑器中进行可视化配置。

### 2. 文件管理

#### 2.1 自动解析
系统会自动监控以下情况并触发源文件解析：
- 编辑器启动时
- 导入新的 `.ts` 或 `.mts` 文件时
- 修改现有源文件时

解析过程包括：
- 分析类定义和继承关系
- 提取公共字段信息
- 缓存解析结果供运行时使用

#### 2.2 快速访问
提供多种方式打开源文件：
- 菜单项：
  - `Tools/PuerTS/Open Project`
  - `Assets/PuerTS/Open Project`
  - 快捷键：`#m`

打开文件支持：
- 打开整个项目目录
- 打开单个文件并定位到指定行
- 根据配置的编辑器类型自动选择合适的打开方式

### 3. 编辑器集成

#### 3.1 Visual Studio Code
- Windows 路径：`C:/Users/<用户名>/AppData/Local/Programs/Microsoft VS Code/bin`
- macOS 路径：`/Applications/Visual Studio Code.app/Contents/Resources/app/bin/code`
- 支持功能：
  - 新窗口打开项目
  - 定位到指定文件和行号

#### 3.2 Cursor
- Windows 路径：`C:/Users/<用户名>/AppData/Local/Programs/cursor`
- macOS 路径：`/Applications/Cursor.app/Contents/MacOS/cursor`
- 支持功能：
  - 新窗口打开项目
  - 定位到指定文件和行号

#### 3.3 IntelliJ IDEA
- Windows 路径：`idea64`
- macOS 路径：`/Applications/IntelliJ IDEA.app/Contents/MacOS/idea`
- 支持功能：
  - 打开项目目录
  - 打开并定位文件

## 常见问题

更多问题，请查阅[问题反馈](../CONTRIBUTING.md#问题反馈)。

## 项目信息

- [更新记录](../CHANGELOG.md)
- [贡献指南](../CONTRIBUTING.md)
- [许可证](../LICENSE.md)