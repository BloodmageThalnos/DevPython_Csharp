DevPython
============
一个轻量级的python开发工具。

Todo
--------
- 跳转函数定义、跳转变量定义、查找所有引用位置
- 批量注释 / 取消注释
- 上次编辑位置
- 在文件编辑器中打开
- 运行参数
- 代码格式化、区域代码格式化
- 代码混淆、其他花里胡哨
- 界面调整

Done
--------
- 代码编辑器(文件读写、显示行号、关键字高亮、查找替换、文件拖入打开）
- 输出编译过程
- 词法分析（关键字高亮）
- 语法分析（语法树输出）
- 代码运行
- 代码调试：加断点、单步运行、运行期间查看变量的值...

Background
--------
当前python语言最流行的IDE是Pycharm，它提供了项目管理、代码跳转、智能提示、自动完成、单元测试、版本控制等大量功能。但对于小型的Python项目开发来说，比如写一个单文件的算法实验，或写一个爬虫来下载一些图片时，Pycharm就会显得有些笨重和臃肿。而使用Notepad++、Atom等文本编辑器编写时，又会想要一些编译器中提供的简单功能，比如出错时的单步调试、代码格式化（自动对齐）等。正如Dev C++之于Visual Studio，一个轻量级的python开发工具是很有必要的。

Functions
----------
1. 简单文本编辑功能。
2. 基于词法分析、语法分析的Python（或其子集）的解释器。
3. 代码调试、变量重命名、代码格式化、代码混淆等工具（可能根据开发进度选择一部分实现）

Appendix
-----------
1. The form and editor function are cloned from  http://www.simplygoodcode.com/2012/04/notepad-clone-in-net-winforms/
2. The coding structure are studied and simplified from Cython.
3. 
