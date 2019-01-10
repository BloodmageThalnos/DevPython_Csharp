using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ScintillaNET;

namespace DevPython {
    partial class Main {
        void InitContent()
        {
            TextArea.Text = "# Test example\ndef calc(s,low,high):\n    if low == high:\n        return s[low],s[low],s[low],s[low]\n    a,b,c,d = calc(s, low, (low + high)//2)\n    e, f, g, h = calc(s, (low + high)//2+1,high)\n    return a + e,max(b, a + f),max(g, c + e),max(d, h, c + f)\n\nprint('请输入元素个数：')\nn = int(input())\nprint('请输入每个元素的值：')\ns = list(map(int, input().split()))\nprint('最大连续子段和：')\nprint(calc(s, 0, len(s) - 1)[3])";
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.controlContentTextBox = new System.Windows.Forms.TextBox();
            this.menubarMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemEditReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemFormatFont = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变量替换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.代码格式化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行命令行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebuggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调试命令行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.检查ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检查当前行语法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.运行参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.继续运行CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下一步NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单步进入SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止调试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.添加查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.DonateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlStatusBar = new System.Windows.Forms.StatusStrip();
            this.controlCaretPositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.sciTextArea = new ScintillaNET.Scintilla();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.sciOutputArea = new ScintillaNET.Scintilla();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.sciLogArea = new ScintillaNET.Scintilla();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.跳转到上个编辑位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.跳转到下个编辑位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.在文件夹中显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.menubarMain.SuspendLayout();
            this.controlStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlContentTextBox
            // 
            this.controlContentTextBox.AcceptsReturn = true;
            this.controlContentTextBox.AcceptsTab = true;
            this.controlContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlContentTextBox.HideSelection = false;
            this.controlContentTextBox.Location = new System.Drawing.Point(0, 24);
            this.controlContentTextBox.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.controlContentTextBox.MaxLength = 0;
            this.controlContentTextBox.Multiline = true;
            this.controlContentTextBox.Name = "controlContentTextBox";
            this.controlContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.controlContentTextBox.Size = new System.Drawing.Size(843, 523);
            this.controlContentTextBox.TabIndex = 0;
            this.controlContentTextBox.Visible = false;
            this.controlContentTextBox.WordWrap = false;
            this.controlContentTextBox.TextChanged += new System.EventHandler(this.controlContentTextBox_TextChanged);
            this.controlContentTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.controlContentTextBox_KeyDown);
            this.controlContentTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.controlContentTextBox_KeyUp);
            this.controlContentTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.controlContentTextBox_MouseDown);
            // 
            // menubarMain
            // 
            this.menubarMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.menubarMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menubarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuitemEdit,
            this.工具TToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.DebugDToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menubarMain.Location = new System.Drawing.Point(0, 0);
            this.menubarMain.Name = "menubarMain";
            this.menubarMain.Padding = new System.Windows.Forms.Padding(0);
            this.menubarMain.Size = new System.Drawing.Size(843, 24);
            this.menubarMain.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemFileNew,
            this.toolStripSeparator7,
            this.menuitemFileOpen,
            this.menuitemFileSave,
            this.menuitemFileSaveAs,
            this.toolStripSeparator1,
            this.menuitemFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.fileToolStripMenuItem.Text = "文件(&F)";
            // 
            // menuitemFileNew
            // 
            this.menuitemFileNew.Name = "menuitemFileNew";
            this.menuitemFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuitemFileNew.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileNew.Text = "新建(&N)";
            this.menuitemFileNew.Click += new System.EventHandler(this.menuitemFileNew_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemFileOpen
            // 
            this.menuitemFileOpen.Name = "menuitemFileOpen";
            this.menuitemFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuitemFileOpen.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileOpen.Text = "打开(&O)";
            this.menuitemFileOpen.Click += new System.EventHandler(this.menuitemFileOpen_Click);
            // 
            // menuitemFileSave
            // 
            this.menuitemFileSave.Name = "menuitemFileSave";
            this.menuitemFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuitemFileSave.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileSave.Text = "保存(&S)";
            this.menuitemFileSave.Click += new System.EventHandler(this.menuitemFileSave_Click);
            // 
            // menuitemFileSaveAs
            // 
            this.menuitemFileSaveAs.Name = "menuitemFileSaveAs";
            this.menuitemFileSaveAs.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileSaveAs.Text = "另存为...";
            this.menuitemFileSaveAs.Click += new System.EventHandler(this.menuitemFileSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemFileExit
            // 
            this.menuitemFileExit.Name = "menuitemFileExit";
            this.menuitemFileExit.Size = new System.Drawing.Size(190, 26);
            this.menuitemFileExit.Text = "退出(&X)";
            this.menuitemFileExit.Click += new System.EventHandler(this.menuitemFileExit_Click);
            // 
            // menuitemEdit
            // 
            this.menuitemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemEditUndo,
            this.toolStripSeparator5,
            this.menuitemEditCut,
            this.menuitemEditCopy,
            this.menuitemEditPaste,
            this.menuitemEditSelectAll,
            this.toolStripSeparator6,
            this.menuitemEditFind,
            this.menuitemEditFindNext,
            this.menuitemEditReplace,
            this.toolStripSeparator3,
            this.menuitemFormatFont});
            this.menuitemEdit.Name = "menuitemEdit";
            this.menuitemEdit.Size = new System.Drawing.Size(71, 24);
            this.menuitemEdit.Text = "编辑(&E)";
            this.menuitemEdit.DropDownOpening += new System.EventHandler(this.menuitemEdit_DropDownOpening);
            // 
            // menuitemEditUndo
            // 
            this.menuitemEditUndo.Name = "menuitemEditUndo";
            this.menuitemEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuitemEditUndo.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditUndo.Text = "撤销(&Z)";
            this.menuitemEditUndo.Click += new System.EventHandler(this.menuitemEditUndo_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemEditCut
            // 
            this.menuitemEditCut.Name = "menuitemEditCut";
            this.menuitemEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuitemEditCut.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditCut.Text = "剪切(&X)";
            this.menuitemEditCut.Click += new System.EventHandler(this.menuitemEditCut_Click);
            // 
            // menuitemEditCopy
            // 
            this.menuitemEditCopy.Name = "menuitemEditCopy";
            this.menuitemEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuitemEditCopy.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditCopy.Text = "复制(&C)";
            this.menuitemEditCopy.Click += new System.EventHandler(this.menuitemEditCopy_Click);
            // 
            // menuitemEditPaste
            // 
            this.menuitemEditPaste.Name = "menuitemEditPaste";
            this.menuitemEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuitemEditPaste.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditPaste.Text = "粘贴(&V)";
            this.menuitemEditPaste.Click += new System.EventHandler(this.menuitemEditPaste_Click);
            // 
            // menuitemEditSelectAll
            // 
            this.menuitemEditSelectAll.Name = "menuitemEditSelectAll";
            this.menuitemEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuitemEditSelectAll.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditSelectAll.Text = "全选(&A)";
            this.menuitemEditSelectAll.Click += new System.EventHandler(this.menuitemEditSelectAll_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemEditFind
            // 
            this.menuitemEditFind.Name = "menuitemEditFind";
            this.menuitemEditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuitemEditFind.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditFind.Text = "查找(&F)";
            this.menuitemEditFind.Click += new System.EventHandler(this.menuitemEditFind_Click);
            // 
            // menuitemEditFindNext
            // 
            this.menuitemEditFindNext.Name = "menuitemEditFindNext";
            this.menuitemEditFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.menuitemEditFindNext.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditFindNext.Text = "查找下一个";
            this.menuitemEditFindNext.Click += new System.EventHandler(this.menuitemEditFindNext_Click);
            // 
            // menuitemEditReplace
            // 
            this.menuitemEditReplace.Name = "menuitemEditReplace";
            this.menuitemEditReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuitemEditReplace.Size = new System.Drawing.Size(190, 26);
            this.menuitemEditReplace.Text = "替换(&H)";
            this.menuitemEditReplace.Click += new System.EventHandler(this.menuitemEditReplace_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
            // 
            // menuitemFormatFont
            // 
            this.menuitemFormatFont.Name = "menuitemFormatFont";
            this.menuitemFormatFont.Size = new System.Drawing.Size(190, 26);
            this.menuitemFormatFont.Text = "字体(&F)";
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.变量替换ToolStripMenuItem,
            this.代码格式化ToolStripMenuItem,
            this.toolStripSeparator15,
            this.跳转到上个编辑位置ToolStripMenuItem,
            this.跳转到下个编辑位置ToolStripMenuItem,
            this.toolStripSeparator16,
            this.在文件夹中显示ToolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.工具TToolStripMenuItem.Text = "工具(&T)";
            // 
            // 变量替换ToolStripMenuItem
            // 
            this.变量替换ToolStripMenuItem.Name = "变量替换ToolStripMenuItem";
            this.变量替换ToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.变量替换ToolStripMenuItem.Text = "变量替换...";
            this.变量替换ToolStripMenuItem.Click += new System.EventHandler(this.变量替换ToolStripMenuItem_Click);
            // 
            // 代码格式化ToolStripMenuItem
            // 
            this.代码格式化ToolStripMenuItem.Name = "代码格式化ToolStripMenuItem";
            this.代码格式化ToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.代码格式化ToolStripMenuItem.Text = "代码格式化...";
            this.代码格式化ToolStripMenuItem.Click += new System.EventHandler(this.代码格式化ToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.运行命令行ToolStripMenuItem,
            this.DebuggerToolStripMenuItem,
            this.调试命令行ToolStripMenuItem,
            this.toolStripSeparator9,
            this.检查ToolStripMenuItem,
            this.检查当前行语法ToolStripMenuItem,
            this.toolStripSeparator2,
            this.运行参数ToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.debugToolStripMenuItem.Text = "运行(&R)";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.runToolStripMenuItem.Text = "运行";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // 运行命令行ToolStripMenuItem
            // 
            this.运行命令行ToolStripMenuItem.Name = "运行命令行ToolStripMenuItem";
            this.运行命令行ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.运行命令行ToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.运行命令行ToolStripMenuItem.Text = "运行（命令行）";
            this.运行命令行ToolStripMenuItem.Click += new System.EventHandler(this.运行命令行ToolStripMenuItem_Click);
            // 
            // DebuggerToolStripMenuItem
            // 
            this.DebuggerToolStripMenuItem.Name = "DebuggerToolStripMenuItem";
            this.DebuggerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.DebuggerToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.DebuggerToolStripMenuItem.Text = "调试";
            this.DebuggerToolStripMenuItem.Click += new System.EventHandler(this.DebuggerToolStripMenuItem_Click);
            // 
            // 调试命令行ToolStripMenuItem
            // 
            this.调试命令行ToolStripMenuItem.Name = "调试命令行ToolStripMenuItem";
            this.调试命令行ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6)));
            this.调试命令行ToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.调试命令行ToolStripMenuItem.Text = "调试（命令行）";
            this.调试命令行ToolStripMenuItem.Click += new System.EventHandler(this.调试命令行ToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(250, 6);
            // 
            // 检查ToolStripMenuItem
            // 
            this.检查ToolStripMenuItem.Name = "检查ToolStripMenuItem";
            this.检查ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.检查ToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.检查ToolStripMenuItem.Text = "检查";
            this.检查ToolStripMenuItem.Click += new System.EventHandler(this.检查ToolStripMenuItem_Click);
            // 
            // 检查当前行语法ToolStripMenuItem
            // 
            this.检查当前行语法ToolStripMenuItem.Name = "检查当前行语法ToolStripMenuItem";
            this.检查当前行语法ToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.检查当前行语法ToolStripMenuItem.Text = "检查当前行语法...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(250, 6);
            // 
            // 运行参数ToolStripMenuItem
            // 
            this.运行参数ToolStripMenuItem.Name = "运行参数ToolStripMenuItem";
            this.运行参数ToolStripMenuItem.Size = new System.Drawing.Size(253, 26);
            this.运行参数ToolStripMenuItem.Text = "运行参数...";
            this.运行参数ToolStripMenuItem.Click += new System.EventHandler(this.运行参数ToolStripMenuItem_Click);
            // 
            // DebugDToolStripMenuItem
            // 
            this.DebugDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.继续运行CToolStripMenuItem,
            this.下一步NToolStripMenuItem,
            this.单步进入SToolStripMenuItem,
            this.停止调试ToolStripMenuItem,
            this.toolStripSeparator8,
            this.添加查看ToolStripMenuItem});
            this.DebugDToolStripMenuItem.Enabled = false;
            this.DebugDToolStripMenuItem.Name = "DebugDToolStripMenuItem";
            this.DebugDToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.DebugDToolStripMenuItem.Text = "调试(&D)";
            // 
            // 继续运行CToolStripMenuItem
            // 
            this.继续运行CToolStripMenuItem.Name = "继续运行CToolStripMenuItem";
            this.继续运行CToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.继续运行CToolStripMenuItem.Text = "继续(&C)";
            this.继续运行CToolStripMenuItem.Click += new System.EventHandler(this.继续运行CToolStripMenuItem_Click);
            // 
            // 下一步NToolStripMenuItem
            // 
            this.下一步NToolStripMenuItem.Name = "下一步NToolStripMenuItem";
            this.下一步NToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.下一步NToolStripMenuItem.Text = "下一步(&N)";
            this.下一步NToolStripMenuItem.Click += new System.EventHandler(this.下一步NToolStripMenuItem_Click);
            // 
            // 单步进入SToolStripMenuItem
            // 
            this.单步进入SToolStripMenuItem.Name = "单步进入SToolStripMenuItem";
            this.单步进入SToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.单步进入SToolStripMenuItem.Text = "单步进入(&S)";
            this.单步进入SToolStripMenuItem.Click += new System.EventHandler(this.单步进入SToolStripMenuItem_Click);
            // 
            // 停止调试ToolStripMenuItem
            // 
            this.停止调试ToolStripMenuItem.Name = "停止调试ToolStripMenuItem";
            this.停止调试ToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.停止调试ToolStripMenuItem.Text = "停止调试(&E)";
            this.停止调试ToolStripMenuItem.Click += new System.EventHandler(this.停止调试ToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(169, 6);
            // 
            // 添加查看ToolStripMenuItem
            // 
            this.添加查看ToolStripMenuItem.Name = "添加查看ToolStripMenuItem";
            this.添加查看ToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.添加查看ToolStripMenuItem.Text = "添加查看(&W)";
            this.添加查看ToolStripMenuItem.Click += new System.EventHandler(this.添加查看ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemHelp,
            this.toolStripSeparator4,
            this.menuitemAbout,
            this.DonateToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.helpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // menuitemHelp
            // 
            this.menuitemHelp.Name = "menuitemHelp";
            this.menuitemHelp.Size = new System.Drawing.Size(216, 26);
            this.menuitemHelp.Text = "Dev Python 帮助(&H)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(213, 6);
            // 
            // menuitemAbout
            // 
            this.menuitemAbout.Name = "menuitemAbout";
            this.menuitemAbout.Size = new System.Drawing.Size(216, 26);
            this.menuitemAbout.Text = "关于 Dev Python(&A)";
            this.menuitemAbout.Click += new System.EventHandler(this.menuitemAbout_Click);
            // 
            // DonateToolStripMenuItem
            // 
            this.DonateToolStripMenuItem.Name = "DonateToolStripMenuItem";
            this.DonateToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.DonateToolStripMenuItem.Text = "赞助...";
            // 
            // controlStatusBar
            // 
            this.controlStatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.controlStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlCaretPositionLabel});
            this.controlStatusBar.Location = new System.Drawing.Point(0, 523);
            this.controlStatusBar.Name = "controlStatusBar";
            this.controlStatusBar.Padding = new System.Windows.Forms.Padding(19, 0, 1, 0);
            this.controlStatusBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.controlStatusBar.Size = new System.Drawing.Size(843, 24);
            this.controlStatusBar.SizingGrip = false;
            this.controlStatusBar.TabIndex = 2;
            // 
            // controlCaretPositionLabel
            // 
            this.controlCaretPositionLabel.AutoSize = false;
            this.controlCaretPositionLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.controlCaretPositionLabel.Name = "controlCaretPositionLabel";
            this.controlCaretPositionLabel.Size = new System.Drawing.Size(219, 19);
            this.controlCaretPositionLabel.Text = "Ln {LineNumber}, Col {ColumnNumber}";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.sciTextArea);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(843, 499);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripSeparator10,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator11,
            this.toolStripButton9,
            this.toolStripButton10,
            this.toolStripSeparator12,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripSeparator13,
            this.toolStripButton15,
            this.toolStripButton18,
            this.toolStripButton16,
            this.toolStripButton17,
            this.toolStripSeparator14,
            this.toolStripComboBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(843, 28);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::DevPython.Properties.Resources._5_02;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton3.Text = "新建";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::DevPython.Properties.Resources._5_03;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton4.Text = "打开";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::DevPython.Properties.Resources._5_04;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton5.Text = "保存";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::DevPython.Properties.Resources._5_05;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton6.Text = "另存为";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::DevPython.Properties.Resources._5_06;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton7.Text = "退出";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::DevPython.Properties.Resources._5_08;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton8.Text = "打印";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::DevPython.Properties.Resources._5_10;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::DevPython.Properties.Resources._5_11;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::DevPython.Properties.Resources._5_13;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton9.Text = "查找";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = global::DevPython.Properties.Resources._5_14;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton10.Text = "替换";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = global::DevPython.Properties.Resources._5_24;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton11.Text = "检查";
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = global::DevPython.Properties.Resources._5_25;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton12.Text = "运行（命令行）";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = global::DevPython.Properties.Resources._5_26;
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton13.Text = "运行";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Image = global::DevPython.Properties.Resources._5_27;
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton14.Text = "调试";
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Image = global::DevPython.Properties.Resources._5_29;
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton15.Text = "toolStripButton15";
            this.toolStripButton15.ToolTipText = "继续";
            this.toolStripButton15.Click += new System.EventHandler(this.继续_Click);
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.Image = global::DevPython.Properties.Resources.sss171;
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton18.Text = "停止";
            this.toolStripButton18.ToolTipText = "停止调试";
            this.toolStripButton18.Click += new System.EventHandler(this.toolStripButton18_Click);
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton16.Image = global::DevPython.Properties.Resources._5_17;
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton16.Text = "下一步";
            this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click);
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton17.Image = global::DevPython.Properties.Resources._5_16;
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton17.Text = "toolStripButton17";
            this.toolStripButton17.ToolTipText = "单步进入";
            this.toolStripButton17.Click += new System.EventHandler(this.toolStripButton17_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.AutoSize = false;
            this.toolStripComboBox1.DropDownWidth = 150;
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox1.Text = "Python 3.6.1 64-bit Debug";
            // 
            // sciTextArea
            // 
            this.sciTextArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sciTextArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sciTextArea.FontQuality = ScintillaNET.FontQuality.LcdOptimized;
            this.sciTextArea.HScrollBar = false;
            this.sciTextArea.Lexer = ScintillaNET.Lexer.Python;
            this.sciTextArea.Location = new System.Drawing.Point(0, 31);
            this.sciTextArea.Name = "sciTextArea";
            this.sciTextArea.Size = new System.Drawing.Size(843, 250);
            this.sciTextArea.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(843, 214);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sciOutputArea);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(835, 185);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "输出";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // sciOutputArea
            // 
            this.sciOutputArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sciOutputArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sciOutputArea.Location = new System.Drawing.Point(3, 3);
            this.sciOutputArea.Name = "sciOutputArea";
            this.sciOutputArea.Size = new System.Drawing.Size(829, 179);
            this.sciOutputArea.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(835, 185);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "变量监视";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(829, 179);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "变量";
            this.columnHeader1.Width = 230;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "值";
            this.columnHeader2.Width = 572;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(835, 185);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "变量监视（自动）";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView2.LabelEdit = true;
            this.listView2.Location = new System.Drawing.Point(3, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(829, 179);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "变量";
            this.columnHeader3.Width = 230;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "值";
            this.columnHeader4.Width = 572;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.sciLogArea);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(835, 185);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "编译日志";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // sciLogArea
            // 
            this.sciLogArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sciLogArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sciLogArea.Location = new System.Drawing.Point(3, 3);
            this.sciLogArea.Name = "sciLogArea";
            this.sciLogArea.Size = new System.Drawing.Size(829, 179);
            this.sciLogArea.TabIndex = 1;
            // 
            // 跳转到上个编辑位置ToolStripMenuItem
            // 
            this.跳转到上个编辑位置ToolStripMenuItem.Name = "跳转到上个编辑位置ToolStripMenuItem";
            this.跳转到上个编辑位置ToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.跳转到上个编辑位置ToolStripMenuItem.Text = "跳转到上个编辑位置";
            this.跳转到上个编辑位置ToolStripMenuItem.Click += new System.EventHandler(this.跳转到上个编辑位置ToolStripMenuItem_Click);
            // 
            // 跳转到下个编辑位置ToolStripMenuItem
            // 
            this.跳转到下个编辑位置ToolStripMenuItem.Name = "跳转到下个编辑位置ToolStripMenuItem";
            this.跳转到下个编辑位置ToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.跳转到下个编辑位置ToolStripMenuItem.Text = "跳转到下个编辑位置";
            this.跳转到下个编辑位置ToolStripMenuItem.Click += new System.EventHandler(this.跳转到下个编辑位置ToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(225, 6);
            // 
            // 在文件夹中显示ToolStripMenuItem
            // 
            this.在文件夹中显示ToolStripMenuItem.Name = "在文件夹中显示ToolStripMenuItem";
            this.在文件夹中显示ToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.在文件夹中显示ToolStripMenuItem.Text = "在文件夹中显示";
            this.在文件夹中显示ToolStripMenuItem.Click += new System.EventHandler(this.在文件夹中显示ToolStripMenuItem_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(225, 6);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 547);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.controlStatusBar);
            this.Controls.Add(this.controlContentTextBox);
            this.Controls.Add(this.menubarMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menubarMain;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Main";
            this.Text = "{DocumentName} - Dev Python";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menubarMain.ResumeLayout(false);
            this.menubarMain.PerformLayout();
            this.controlStatusBar.ResumeLayout(false);
            this.controlStatusBar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        Scintilla TextArea
        {
            get { return sciTextArea; }
            set { sciTextArea = value; }
        }

        private void InitializeTextarea()
        {
            // INITIAL VIEW CONFIG
            TextArea.WrapMode = WrapMode.Word;
            TextArea.IndentationGuides = IndentView.LookBoth;
            TextArea.WrapIndentMode = WrapIndentMode.Indent;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();

            InitContent();
        }

        public void InitDragDropFile()
        {

            TextArea.AllowDrop = true;
            TextArea.DragEnter += delegate (object sender, DragEventArgs e) {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            TextArea.DragDrop += delegate (object sender, DragEventArgs e)
            {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        Open(path);

                    }
                }
            };
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {
                //FileName.Text = Path.GetFileName(path);
                TextArea.Text = File.ReadAllText(path);
            }
        }

        private void InitColors()
        {

            TextArea.SetSelectionBackColor(true, IntToColor(0x114D9C));
            
        }

        private void InitSyntaxColoring()
        {

            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[Style.Default].Font = "Consolas";
            TextArea.Styles[Style.Default].Size = 12;
            TextArea.Styles[Style.Default].BackColor = IntToColor(0x212121);
            TextArea.Styles[Style.Default].ForeColor = IntToColor(0x000000);
            TextArea.StyleClearAll();
            
            // Configure the CPP (C#) lexer styles
            TextArea.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            TextArea.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            TextArea.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            TextArea.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            TextArea.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            TextArea.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            TextArea.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            TextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            TextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            TextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            TextArea.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            // Python lexer styles
            TextArea.Styles[Style.Python.Default].ForeColor = IntToColor(0xFFFFFF);
            TextArea.Styles[Style.Python.Number].ForeColor = IntToColor(0xFFFF00);
            TextArea.Styles[Style.Python.Word].ForeColor = IntToColor(0x48A8EE);
            TextArea.Styles[Style.Python.Word2].ForeColor = IntToColor(0x5EC9A9);
            TextArea.Styles[Style.Python.String].ForeColor = IntToColor(0xC079C0);
            TextArea.Styles[Style.Python.Character].ForeColor = IntToColor(0xC079C0);

            TextArea.Lexer = Lexer.Python;

            TextArea.SetKeywords(0, "class false true finally is return not none continue for lambda try def from nonlocal while and del global with as elif if or yield assert else import pass break except raise");
            // https://www.cnblogs.com/PastimeRr/p/8305022.html
            TextArea.SetKeywords(1, "abs all any ascii bin bool bytearray bytes callable chr classmethod compile complex delattr dict dir divmod enumerate eval exec filter format float frozenset getattr globals hasattr hash help hex id input int isinstance issubclass iter len list locals map max memoryview min next object oct open ord pow print property range repr reversed round set setattr slice sorted staticmethod str sum super tuple type vars zip __import__");
            // https://www.cnblogs.com/xiao1/p/5856890.html

        }

        private void OnTextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin()
        {
            TextArea.CaretForeColor = IntToColor(0xFFFFFF);
            TextArea.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            TextArea.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            TextArea.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = TextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;
        }

        private void scintilla_MarginClick(object sender,MarginClickEventArgs e)
        {
            if(e.Margin==BOOKMARK_MARGIN||e.Margin==NUMBER_MARGIN)
            {
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if((line.MarkerGet()&mask)>0)
                {
                    line.MarkerDelete(BOOKMARK_MARKER);
                    breakpoint[TextArea.LineFromPosition(e.Position) + 1] = false;
                }
                else
                {
                    line.MarkerAdd(BOOKMARK_MARKER);
                    breakpoint[TextArea.LineFromPosition(e.Position) + 1] = true;
                }
            }
        }

        private void InitBookmarkMargin()
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER) | (1 << 3) | (1<<4);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

            var marker2 = TextArea.Markers[3];
            marker2.Symbol = MarkerSymbol.Background;
            marker2.SetBackColor(IntToColor(0x3575FF));
            marker2.SetForeColor(IntToColor(0x000000));
            marker2.SetAlpha(100);

            var marker3 = TextArea.Markers[4];
            marker3.Symbol = MarkerSymbol.Background;
            marker3.SetBackColor(IntToColor(0xFF003B));
            marker3.SetForeColor(IntToColor(0x000000));
            marker3.SetAlpha(100);

            TextArea.MarginClick += scintilla_MarginClick;

        }

        private void InitCodeFolding()
        {

            TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            TextArea.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            TextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                TextArea.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                TextArea.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            TextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            TextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            TextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private System.Windows.Forms.TextBox controlContentTextBox;
        private System.Windows.Forms.MenuStrip menubarMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuitemFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuitemFileOpen;
        private System.Windows.Forms.ToolStripMenuItem menuitemFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuitemFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuitemEdit;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditFind;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditFindNext;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditReplace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditSelectAll;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuitemHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuitemAbout;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditCut;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditCopy;
        private System.Windows.Forms.ToolStripMenuItem menuitemEditPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.StatusStrip controlStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel controlCaretPositionLabel;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ScintillaNET.Scintilla sciTextArea;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ScintillaNET.Scintilla sciOutputArea;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private ToolStripMenuItem menuitemFormatFont;
        private ToolStripMenuItem DonateToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem menuitemFileExit;
        private ToolStripMenuItem DebuggerToolStripMenuItem;
        private ToolStripMenuItem DebugDToolStripMenuItem;
        private ToolStripMenuItem 下一步NToolStripMenuItem;
        private ToolStripMenuItem 单步进入SToolStripMenuItem;
        private ToolStripMenuItem 继续运行CToolStripMenuItem;
        private ToolStripMenuItem 工具TToolStripMenuItem;
        private ToolStripMenuItem 变量替换ToolStripMenuItem;
        private ToolStripMenuItem 代码格式化ToolStripMenuItem;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ToolStripMenuItem 添加查看ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator8;
        private ListView listView2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private TabPage tabPage4;
        private Scintilla sciLogArea;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem 检查ToolStripMenuItem;
        private ToolStripMenuItem 检查当前行语法ToolStripMenuItem;
        private ToolStripMenuItem 运行命令行ToolStripMenuItem;
        private ToolStripMenuItem 停止调试ToolStripMenuItem;
        private ToolStripMenuItem 调试命令行ToolStripMenuItem;
        private ToolTip toolTip1;
        private ToolTip toolTip2;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
        private ToolStripButton toolStripButton9;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripButton toolStripButton10;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripButton toolStripButton11;
        private ToolStripButton toolStripButton12;
        private ToolStripButton toolStripButton13;
        private ToolStripButton toolStripButton14;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripButton toolStripButton15;
        private ToolStripButton toolStripButton16;
        private ToolStripButton toolStripButton17;
        private ToolStripButton toolStripButton18;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem 运行参数ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripMenuItem 跳转到上个编辑位置ToolStripMenuItem;
        private ToolStripMenuItem 跳转到下个编辑位置ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripMenuItem 在文件夹中显示ToolStripMenuItem;
    }
}