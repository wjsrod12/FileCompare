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
                var leftFiles = Directory.Exists(leftFolder)
                    ? Directory.EnumerateFiles(leftFolder).Select(p => new FileInfo(p)).ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase)
                    : new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);

                var rightFiles = Directory.Exists(rightFolder)
                    ? Directory.EnumerateFiles(rightFolder).Select(p => new FileInfo(p)).ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase)
                    : new Dictionary<string, FileInfo>(StringComparer.OrdinalIgnoreCase);

                var allNames = leftFiles.Keys.Union(rightFiles.Keys, StringComparer.OrdinalIgnoreCase)
                    .OrderBy(n => n, StringComparer.OrdinalIgnoreCase);

                foreach (var name in allNames)
                {
                    leftFiles.TryGetValue(name, out var lf);
                    rightFiles.TryGetValue(name, out var rf);

                    // Left item
                    var leftItem = lf != null ? new ListViewItem(lf.Name) : new ListViewItem(string.Empty);
                    if (lf != null)
                    {
                        leftItem.SubItems.Add(lf.Length.ToString("N0") + " 바이트");
                        leftItem.SubItems.Add(lf.LastWriteTime.ToString("g"));
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
                        rightItem.SubItems.Add(rf.Length.ToString("N0") + " 바이트");
                        rightItem.SubItems.Add(rf.LastWriteTime.ToString("g"));
                    }
                    else
                    {
                        rightItem.SubItems.Add(string.Empty);
                        rightItem.SubItems.Add(string.Empty);
                    }

                    // Coloring rules
                    if (lf != null && rf != null)
                    {
                        // same file
                        if (lf.Length == rf.Length && lf.LastWriteTime == rf.LastWriteTime)
                        {
                            leftItem.ForeColor = Color.Black;
                            rightItem.ForeColor = Color.Black;
                        }
                        else
                        {
                            // newer = red, older = gray
                            if (lf.LastWriteTime > rf.LastWriteTime)
                            {
                                leftItem.ForeColor = Color.Red;
                                rightItem.ForeColor = Color.Gray;
                            }
                            else if (lf.LastWriteTime < rf.LastWriteTime)
                            {
                                leftItem.ForeColor = Color.Gray;
                                rightItem.ForeColor = Color.Red;
                            }
                            else
                            {
                                // dates equal but sizes differ -> mark both orange
                                leftItem.ForeColor = Color.Orange;
                                rightItem.ForeColor = Color.Orange;
                            }
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
                string fileName = item.Text;

                string sourcePath = Path.Combine(txtLeftDir.Text, fileName);
                string destPath = Path.Combine(txtRightDir.Text, fileName);

                if (!File.Exists(sourcePath))
                    continue;

                CopyFileWithConfirm(sourcePath, destPath);
            }

            PopulateListView(lvwRightDir, txtRightDir.Text);
        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwRightDir.SelectedItems)
            {
                string fileName = item.Text;

                string sourcePath = Path.Combine(txtRightDir.Text, fileName);
                string destPath = Path.Combine(txtLeftDir.Text, fileName);

                if (!File.Exists(sourcePath))
                    continue;

                CopyFileWithConfirm(sourcePath, destPath);
            }

            PopulateListView(lvwLeftDir, txtLeftDir.Text);
        }


    }
}
