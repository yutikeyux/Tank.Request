using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Tank.Request.Illegalcharacters
{
	// Token: 0x02000086 RID: 134
	public class FileSystem
	{
		// Token: 0x0600025E RID: 606 RVA: 0x00011620 File Offset: 0x0000F820
		public FileSystem(string Path, string Directory, string Type)
		{
			this.initContent(Path);
			this.initFileWatcher(Directory, Type);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00011680 File Offset: 0x0000F880
		private void initContent(string Path)
		{
			bool flag = File.Exists(Path);
			if (flag)
			{
				this.filePath = Path;
				StreamReader sr = new StreamReader(Path, Encoding.GetEncoding("GB2312"));
				string str = "";
				bool flag2 = this.contentList.Count > 0;
				if (flag2)
				{
					this.contentList.Clear();
				}
				while (str != null)
				{
					str = sr.ReadLine();
					bool flag3 = !string.IsNullOrEmpty(str);
					if (flag3)
					{
						this.contentList.Add(str);
					}
				}
				bool flag4 = str == null;
				if (flag4)
				{
					sr.Close();
				}
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00011724 File Offset: 0x0000F924
		private void initFileWatcher(string directory, string type)
		{
			bool flag = Directory.Exists(directory);
			if (flag)
			{
				this.fileDirectory = directory;
				this.fileType = type;
				this.fileWatcher.Path = directory;
				this.fileWatcher.Filter = type;
				this.fileWatcher.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess);
				this.fileWatcher.EnableRaisingEvents = true;
				this.fileWatcher.Changed += this.OnChanged;
				this.fileWatcher.Renamed += FileSystem.OnRenamed;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000117B4 File Offset: 0x0000F9B4
		public bool checkIllegalChar(string strRegName)
		{
			bool flag = false;
			bool flag2 = !string.IsNullOrEmpty(strRegName);
			if (flag2)
			{
				flag = this.checkChar(strRegName);
			}
			return flag;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000117E0 File Offset: 0x0000F9E0
		private bool checkChar(string strRegName)
		{
			bool flag = false;
			foreach (object obj in this.contentList)
			{
				string strLine = (string)obj;
				bool flag2 = !strLine.StartsWith("GM");
				if (flag2)
				{
					foreach (char charl in strLine)
					{
						bool flag3 = strRegName.Contains(charl.ToString()) && charl.ToString() != " ";
						if (flag3)
						{
							flag = true;
							break;
						}
					}
					bool flag4 = flag;
					if (flag4)
					{
						break;
					}
				}
				else
				{
					string[] keyword = strLine.Split(new char[]
					{
						'|'
					});
					foreach (string key in keyword)
					{
						bool flag5 = strRegName.Contains(key);
						if (flag5)
						{
							flag = true;
							break;
						}
					}
					bool flag6 = flag;
					if (flag6)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00002CE8 File Offset: 0x00000EE8
		private void OnChanged(object source, FileSystemEventArgs e)
		{
			this.UpdataContent();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00002CF2 File Offset: 0x00000EF2
		private void UpdataContent()
		{
			this.initContent(this.filePath);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00002670 File Offset: 0x00000870
		private static void OnRenamed(object source, RenamedEventArgs e)
		{
		}

		// Token: 0x04000097 RID: 151
		public ArrayList contentList = new ArrayList();

		// Token: 0x04000098 RID: 152
		private FileSystemWatcher fileWatcher = new FileSystemWatcher();

		// Token: 0x04000099 RID: 153
		private string filePath = string.Empty;

		// Token: 0x0400009A RID: 154
		private string fileDirectory = string.Empty;

		// Token: 0x0400009B RID: 155
		private string fileType = string.Empty;
	}
}
