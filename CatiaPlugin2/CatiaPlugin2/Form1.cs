using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CatiaPlugin2
{
    public partial class folderSelection : Form
    {
        private List<Macro> macros;
        public folderSelection()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            button2.Enabled = false;
            button3.Enabled = false;
            macrosPanel.AutoScroll = true;
            paramPanel.AutoScroll = true;
            tabControl1.TabPages.Clear();
            if (!CatiaService.InitializeCatia())
            {
                ShowErrorBox("Catia is not started.");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    macros = MacroUtilities.GetMacrosInFolder(fbd.SelectedPath);
                    if(macros==null || !macros.Any())
                    {
                        ShowErrorBox("There were no good files inside that folder.");
                    }else
                    {
                        PopulateMacrosPanel();
                    }
                }
            }
        }

        private void PopulateMacrosPanel()
        {
            var lastHeight = 10;
            foreach(var macro in macros)
            {
                Button button = new Button();
                button.Text = macro.FileName.Substring(0, macro.FileName.Length - 10);
                button.Height = 30;
                button.Width = macrosPanel.Width - 25;
                button.Location = new Point(5, lastHeight);
                lastHeight += button.Height + 5;
                button.Click += (sender, e) => MacroButtonClicked(sender, e, macro);
                macrosPanel.Controls.Add(button);
            }

            if (macros.Count > 0)
            {
                button3.Enabled = true;
            }
        }

        private void RunMacro(object sender, EventArgs e, Macro macro, IEnumerable<KeyValuePair<string, string>> list)
        {
            List<Object> parameters = new List<Object>();
            foreach(var dEntry in list)
            {
                if (dEntry.Value.Equals("string", StringComparison.InvariantCultureIgnoreCase))
                {
                    string text = paramPanel.Controls.Find(dEntry.Key, true).FirstOrDefault().Text;
                    parameters.Add(text);
                }
                else if (dEntry.Value.Equals("integer", StringComparison.InvariantCultureIgnoreCase))
                {
                    string text = paramPanel.Controls.Find(dEntry.Key, true).FirstOrDefault().Text;
                    int value; 
                    if(Int32.TryParse(text, out value))
                    {
                        parameters.Add(value);
                    }else
                    {
                        ShowErrorBox("One of the variables is of wrong type");
                        return;
                    }
                }
                else if (dEntry.Value.Equals("double", StringComparison.InvariantCultureIgnoreCase))
                {
                    string text = paramPanel.Controls.Find(dEntry.Key, true).FirstOrDefault().Text;
                    text = text.Replace(".", ",");
                    double value;
                    if (Double.TryParse(text, out value))
                    {
                        parameters.Add(value);
                    }
                    else
                    {
                        text = text.Replace(",", ".");
                        if (Double.TryParse(text, out value))
                        {
                            parameters.Add(value);
                        }
                        else
                        {
                            ShowErrorBox("One of the variables is of wrong type");
                            return;
                        }
                    }
                }
            }
            CatiaService.RunMacro(macro.DirectoryName, macro.FileName, parameters);
        }

        private void MacroButtonClicked(object sender, EventArgs e, Macro macro)
        {
            tabControl1.TabPages.Clear();
            List<KeyValuePair<string, string>> textBoxToType = new List<KeyValuePair<string, string>>();
            paramPanel.Controls.Clear();
            var lastHeight = 10;

            Label label1 = new Label();
            label1.Text = "Current macro: " + macro.FileName.Substring(0, macro.FileName.Length - 10);
            label1.Location = new Point(5, lastHeight);
            label1.Width = paramPanel.Width + 50;
            label1.Font = new Font(label1.Font, FontStyle.Bold);
            lastHeight += label1.Height;
            paramPanel.Controls.Add(label1);

            foreach (var imagePair in macro.Images)
            {
                TabPage tabPage;
                if (!tabControl1.TabPages.ContainsKey(imagePair.Key))
                {
                    tabControl1.TabPages.Add(imagePair.Key, imagePair.Key);
                }
                tabPage = tabControl1.TabPages[imagePair.Key];
                try
                {
                    var image = Image.FromFile(imagePair.Value);
                    SetupImage(ref tabPage, image);

                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            foreach(var param in macro.Parameters)
            {
                Label label = new Label();
                label.Text = (string.IsNullOrWhiteSpace(param.DisplayName)) ? param.ParameterName+":" : param.DisplayName+":";
                label.Location = new Point(5, lastHeight);
                label.Width= paramPanel.Width - 10;
                lastHeight += label.Height;
                TextBox tb = new TextBox();
                tb.Name = param.ParameterName + "TextBox";
                textBoxToType.Add(new KeyValuePair<string, string>(tb.Name, param.Type));
                tb.Text = param.DefaultValue;
                tb.Height = 30;
                tb.Width = paramPanel.Width - 10;
                tb.Location = new Point(5, lastHeight);
                lastHeight += tb.Height + 5;
                paramPanel.Controls.Add(tb);
                paramPanel.Controls.Add(label);
            }
            button2.Enabled = true;
            RemoveClickEvent(button2);
            button2.Click += (s, e2) => RunMacro(s, e2, macro, textBoxToType);
        }

        private void RemoveClickEvent(Button b)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }

        private void ShowErrorBox(string message)
        {
            MessageBox.Show(message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void folderSelection_Load(object sender, EventArgs e)
        {

        }

        private void folderSelection_Resize(object sender, EventArgs e)
        {
            var height = this.Height;
            var width = this.Width;
            macrosPanel.Height = height  - macrosPanel.Location.Y * 4;
            paramPanel.Height = height - paramPanel.Location.Y * 4;
            button2.Location = new Point(macrosPanel.Location.X + 5, macrosPanel.Height + macrosPanel.Location.Y + 5);
            button3.Location = new Point(macrosPanel.Location.X + 5, macrosPanel.Height + macrosPanel.Location.Y + 10 + button2.Height);
            tabControl1.Location = new Point(paramPanel.Location.X + paramPanel.Width + 10, button1.Location.Y);
            tabControl1.Width = width - tabControl1.Location.X - 20;
            tabControl1.Height = button2.Location.Y + button2.Height;
        }

        public static void SetupImage(ref TabPage tabPage, Image image)
        {
            var ratioX = (double)tabPage.Width / image.Width;
            var ratioY = (double)tabPage.Height / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            tabPage.Height = newHeight;
            tabPage.Width = newWidth;

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            tabPage.BackgroundImage = newImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach(var macro in macros)
            {
                List<Object> parameters = new List<Object>();
                foreach(var parameter in macro.Parameters)
                {
                    if (parameter.Type.Equals("string", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(parameter.DefaultValue))
                        parameters.Add(parameter.DefaultValue);
                    else if (parameter.Type.Equals("integer", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(parameter.DefaultValue))
                    {
                        if (string.IsNullOrWhiteSpace(parameter.DefaultValue))
                            parameters.Add(0);
                        else
                        {
                            int val;
                            if (Int32.TryParse(parameter.DefaultValue, out val))
                            {
                                parameters.Add(val);
                            }
                            else {
                                ShowErrorBox(string.Format("There is an error in macro: {0}\nThe parameter {1} is of incorrect type.\nShoudl be {3}", macro.FileName, parameter.DisplayName, parameter.Type));
                                return;
                            }
                        }
                    }
                    else if (parameter.Type.Equals("double", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(parameter.DefaultValue))
                    {
                        if (string.IsNullOrWhiteSpace(parameter.DefaultValue))
                            parameters.Add(0);
                        else
                        {
                            double val;
                            var text = parameter.DefaultValue.Replace(".", ",");
                            if (double.TryParse(text, out val))
                            {
                                parameters.Add(val);
                            }
                            else {
                                text = text.Replace(",", ".");
                                if (double.TryParse(text, out val))
                                {
                                    parameters.Add(val);
                                }
                                else {
                                    ShowErrorBox(string.Format("There is an error in macro: {0}\nThe parameter {1} is of incorrect type.\nShoudl be {3}", macro.FileName, parameter.DisplayName, parameter.Type));
                                    return;
                                }
                            }
                        }
                    }
                }
                CatiaService.RunMacro(macro.DirectoryName, macro.FileName, parameters);
            }
        }
    }
}
