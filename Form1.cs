using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            try
            {
                var dirs = Directory.EnumerateDirectories(folderPath)
                    .Select(p => new DirectoryInfo(p))
                    .OrderBy(d => d.Name);

                foreach (var d in dirs)
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(d.LastWriteTime.ToString("g"));
                    lv.Items.Add(item);
                }

                // 파일 추가
                var files = Directory.EnumerateFiles(folderPath)
                    .Select(p => new FileInfo(p))
                    .OrderBy(f => f.Name);

                foreach (var f in files)
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(f.Length.ToString("N0") + " 바이트");
                    item.SubItems.Add(f.LastWriteTime.ToString("g"));
                    lv.Items.Add(item);
                }

                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    lv.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(this, "폴더를 찾을 수 없습니다.",
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, "입출력 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        private void CompareAndPopulate(string leftFolder, string rightFolder)
        {
            lvwLeftDir.BeginUpdate();
            lvwRightDir.BeginUpdate();
            lvwLeftDir.Items.Clear();
            lvwRightDir.Items.Clear();

            try
            {
                // Include both files and directories in the comparison
                var leftEntries = Directory.Exists(leftFolder)
                    ? Directory.EnumerateFileSystemEntries(leftFolder).Select(p => Directory.Exists(p) ? (FileSystemInfo)new DirectoryInfo(p) : new FileInfo(p)).ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase)
                    : new Dictionary<string, FileSystemInfo>(StringComparer.OrdinalIgnoreCase);

                var rightEntries = Directory.Exists(rightFolder)
                    ? Directory.EnumerateFileSystemEntries(rightFolder).Select(p => Directory.Exists(p) ? (FileSystemInfo)new DirectoryInfo(p) : new FileInfo(p)).ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase)
                    : new Dictionary<string, FileSystemInfo>(StringComparer.OrdinalIgnoreCase);

                var allNames = leftEntries.Keys.Union(rightEntries.Keys, StringComparer.OrdinalIgnoreCase)
                    .OrderBy(n => n, StringComparer.OrdinalIgnoreCase);

                foreach (var name in allNames)
                {
                    leftEntries.TryGetValue(name, out var lf);
                    rightEntries.TryGetValue(name, out var rf);

                    // Left item
                    var leftItem = lf != null ? new ListViewItem(lf.Name) : new ListViewItem(string.Empty);
                    if (lf != null)
                    {
                        if (lf is FileInfo lfi)
                        {
                            leftItem.SubItems.Add(lfi.Length.ToString("N0") + " 바이트");
                            leftItem.SubItems.Add(lfi.LastWriteTime.ToString("g"));
                        }
                        else // DirectoryInfo
                        {
                            leftItem.SubItems.Add("<DIR>");
                            leftItem.SubItems.Add(lf.LastWriteTime.ToString("g"));
                        }
                    }
                    else
                    {
                        leftItem.SubItems.Add(string.Empty);
                        leftItem.SubItems.Add(string.Empty);
                    }

                    // Right item
                    var rightItem = rf != null ? new ListViewItem(rf.Name) : new ListViewItem(string.Empty);
                    if (rf != null)
                    {
                        if (rf is FileInfo rfi)
                        {
                            rightItem.SubItems.Add(rfi.Length.ToString("N0") + " 바이트");
                            rightItem.SubItems.Add(rfi.LastWriteTime.ToString("g"));
                        }
                        else // DirectoryInfo
                        {
                            rightItem.SubItems.Add("<DIR>");
                            rightItem.SubItems.Add(rf.LastWriteTime.ToString("g"));
                        }
                    }
                    else
                    {
                        rightItem.SubItems.Add(string.Empty);
                        rightItem.SubItems.Add(string.Empty);
                    }

                    // Coloring rules
                    if (lf != null && rf != null)
                    {
                        // if same type
                        if (lf.GetType() == rf.GetType())
                        {
                            if (lf is FileInfo lfFile && rf is FileInfo rfFile)
                            {
                                // same file
                                if (lfFile.Length == rfFile.Length && lfFile.LastWriteTime == rfFile.LastWriteTime)
                                {
                                    leftItem.ForeColor = Color.Black;
                                    rightItem.ForeColor = Color.Black;
                                }
                                else
                                {
                                    if (lfFile.LastWriteTime > rfFile.LastWriteTime)
                                    {
                                        leftItem.ForeColor = Color.Red;
                                        rightItem.ForeColor = Color.Gray;
                                    }
                                    else if (lfFile.LastWriteTime < rfFile.LastWriteTime)
                                    {
                                        leftItem.ForeColor = Color.Gray;
                                        rightItem.ForeColor = Color.Red;
                                    }
                                    else
                                    {
                                        leftItem.ForeColor = Color.Orange;
                                        rightItem.ForeColor = Color.Orange;
                                    }
                                }
                            }
                            else // both directories -> compare timestamps
                            {
                                var ldt = lf.LastWriteTime;
                                var rdt = rf.LastWriteTime;
                                if (ldt == rdt)
                                {
                                    leftItem.ForeColor = Color.Black;
                                    rightItem.ForeColor = Color.Black;
                                }
                                else if (ldt > rdt)
                                {
                                    leftItem.ForeColor = Color.Red;
                                    rightItem.ForeColor = Color.Gray;
                                }
                                else
                                {
                                    leftItem.ForeColor = Color.Gray;
                                    rightItem.ForeColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            // different types (file vs dir)
                            leftItem.ForeColor = Color.Orange;
                            rightItem.ForeColor = Color.Orange;
                        }
                    }
                    else if (lf != null)
                    {
                        leftItem.ForeColor = Color.Purple;
                    }
                    else if (rf != null)
                    {
                        rightItem.ForeColor = Color.Purple;
                    }

                    lvwLeftDir.Items.Add(leftItem);
                    lvwRightDir.Items.Add(rightItem);
                }

                for (int i = 0; i < lvwLeftDir.Columns.Count; i++)
                    lvwLeftDir.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                for (int i = 0; i < lvwRightDir.Columns.Count; i++)
                    lvwRightDir.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(this, "폴더를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, "입출력 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lvwLeftDir.EndUpdate();
                lvwRightDir.EndUpdate();
            }
        }





        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) &&
                        Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    if (!string.IsNullOrWhiteSpace(txtRightDir.Text) && Directory.Exists(txtRightDir.Text))
                        CompareAndPopulate(dlg.SelectedPath, txtRightDir.Text);
                    else
                        PopulateListView(lvwLeftDir, dlg.SelectedPath);

                }

            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) &&
                        Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) && Directory.Exists(txtLeftDir.Text))
                        CompareAndPopulate(txtLeftDir.Text, dlg.SelectedPath);
                    else
                        PopulateListView(lvwRightDir, dlg.SelectedPath);

                }

            }
        }

        private void CopyFileWithConfirm(string source, string dest)
        {
            if (File.Exists(dest))
            {
                var result = MessageBox.Show(
                    $"이미 존재하는 파일입니다.\n덮어쓰시겠습니까?\n{Path.GetFileName(dest)}",
                    "확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            File.Copy(source, dest, true); // true = 덮어쓰기 허용
        }

        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwLeftDir.SelectedItems)
            {
                string name = item.Text;

                string sourcePath = Path.Combine(txtLeftDir.Text, name);
                string destPath = Path.Combine(txtRightDir.Text, name);

                // 폴더면 폴더 전체 복사
                if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, destPath);
                }
                // 파일이면 파일 복사
                else if (File.Exists(sourcePath))
                {
                    CopyFileWithConfirm(sourcePath, destPath);
                }
                // 존재하지 않으면 무시
            }

            // 우측 목록 갱신
            PopulateListView(lvwRightDir, txtRightDir.Text);
        }


        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            
            foreach (ListViewItem item in lvwRightDir.SelectedItems)
            {
                string name = item.Text;

                string sourcePath = Path.Combine(txtRightDir.Text, name);
                string destPath = Path.Combine(txtLeftDir.Text, name);

                // 📁 폴더면 → 통째로 복사
                if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, destPath);
                }
                // 📄 파일이면 → 파일 복사
                else if (File.Exists(sourcePath))
                {
                    CopyFileWithConfirm(sourcePath, destPath);
                }
            }

            // 🔥 갱신 + 색상 다시 적용
            PopulateListView(lvwLeftDir, txtLeftDir.Text);
        }


        private void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destDir, fileName);

                CopyFileWithConfirm(file, destFile);
            }

            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                string dirName = Path.GetFileName(dir);
                string destSubDir = Path.Combine(destDir, dirName);

                CopyDirectory(dir, destSubDir);
            }
        }


    }
}
