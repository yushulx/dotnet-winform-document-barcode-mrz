using System.Drawing.Imaging;
using Dynamsoft;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;
using static Dynamsoft.MrzScanner;
using static Dynamsoft.DocumentScanner;
using static Dynamsoft.BarcodeQRCodeReader;
using System.Text.Json.Nodes;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

using MrzResult = Dynamsoft.MrzScanner.Result;
using DocResult = Dynamsoft.DocumentScanner.Result;
using BarcodeResult = Dynamsoft.BarcodeQRCodeReader.Result;

namespace Test
{
    public partial class Form1 : Form
    {
        private MrzScanner mrzScanner;
        private DocumentScanner documentScanner;
        private BarcodeQRCodeReader barcodeScanner;
        private VideoCapture capture;
        private bool isCapturing;
        private Thread? thread;
        private Mat _mat = new Mat();
        private DocResult[]? _docResults;
        private MrzResult[]? _mrzResults;
        private string? _currentFilename = "";

        public Form1()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Form1_Closing);
            string license = "DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ==";

            ActivateLicense(license);

            // Initialize camera
            capture = new VideoCapture(0);
            isCapturing = false;

            // Initialize MRZ scanner
            mrzScanner = MrzScanner.Create();
            mrzScanner.LoadModel();

            // Initialize document scanner
            documentScanner = DocumentScanner.Create();
            documentScanner.SetParameters(DocumentScanner.Templates.color);

            // Initialize barcode scanner
            barcodeScanner = BarcodeQRCodeReader.Create();
        }

        private void ActivateLicense(string license)
        {
            int ret = MrzScanner.InitLicense(license); // Get a license key from https://www.dynamsoft.com/customer/license/trialLicense
            ret = DocumentScanner.InitLicense(license);
            BarcodeQRCodeReader.InitLicense(license);
            if (ret != 0)
            {
                toolStripStatusLabel1.Text = "License is invalid.";
            }
            else
            {
                toolStripStatusLabel1.Text = "License is activated successfully.";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Code
            mrzScanner.Destroy();
            documentScanner.Destroy();
            barcodeScanner.Destroy();
        }

        // private void ShowResults(Result[] results)
        // {
        //     if (results == null)
        //         return;
        // }
        private Mat DetectMrz(Mat mat)
        {
            int length = mat.Cols * mat.Rows * mat.ElemSize();
            byte[] bytes = new byte[length];
            Marshal.Copy(mat.Data, bytes, 0, length);
            _mrzResults = mrzScanner.DetectBuffer(bytes, mat.Cols, mat.Rows, (int)mat.Step(), MrzScanner.ImagePixelFormat.IPF_RGB_888);
            if (_mrzResults != null)
            {
                string[] lines = new string[_mrzResults.Length];
                var index = 0;
                foreach (MrzResult result in _mrzResults)
                {
                    lines[index++] = result.Text;
                    richTextBox1.Text += result.Text + Environment.NewLine;
                    if (result.Points != null)
                    {
                        Point[] points = new Point[4];
                        for (int i = 0; i < 4; i++)
                        {
                            points[i] = new Point(result.Points[i * 2], result.Points[i * 2 + 1]);
                        }
                        Cv2.DrawContours(mat, new Point[][] { points }, 0, Scalar.Red, 2);
                    }
                }

                JsonNode? info = Parse(lines);
                if (info != null) richTextBox1.Text = info.ToString();
            }

            return mat;
        }

        private void DetectFile(string filename)
        {
            richTextBox1.Text = "";
            try
            {
                _mat = Cv2.ImRead(filename, ImreadModes.Color);
                Mat copy = new Mat(_mat.Rows, _mat.Cols, MatType.CV_8UC3);
                _mat.CopyTo(copy);
                if (checkBox1.Checked)
                {
                    pictureBox1.Image = DetectDocumentEdges(copy);
                    PreviewNormalizedImage();
                }
                else
                {
                    pictureBox1.Image = BitmapConverter.ToBitmap(copy);

                    if (checkBox2.Checked)
                    {
                        copy = DetectBarcode(copy);
                    }

                    if (checkBox3.Checked) {
                        copy = DetectMrz(copy);
                    }
                    pictureBox2.Image = BitmapConverter.ToBitmap(copy);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Mat DetectBarcode(Mat mat)
        {
            int length = mat.Cols * mat.Rows * mat.ElemSize();
            byte[] bytes = new byte[length];
            Marshal.Copy(mat.Data, bytes, 0, length);

            BarcodeResult[]? results = barcodeScanner.DecodeBuffer(bytes, mat.Cols, mat.Rows, (int)mat.Step(), BarcodeQRCodeReader.ImagePixelFormat.IPF_RGB_888);
            if (results != null)
            {
                foreach (BarcodeResult result in results)
                {
                    string output = "Text: " + result.Text + Environment.NewLine + "Format: " + result.Format1 + Environment.NewLine;
                    richTextBox1.AppendText(output);
                    richTextBox1.AppendText(Environment.NewLine);
                    int[]? points = result.Points;
                    if (points != null)
                    {
                        OpenCvSharp.Point[] all = new OpenCvSharp.Point[4];
                        int xMin = points[0], yMax = points[1];
                        all[0] = new OpenCvSharp.Point(xMin, yMax);
                        for (int i = 2; i < 7; i += 2)
                        {
                            int x = points[i];
                            int y = points[i + 1];
                            OpenCvSharp.Point p = new OpenCvSharp.Point(x, y);
                            xMin = x < xMin ? x : xMin;
                            yMax = y > yMax ? y : yMax;
                            all[i / 2] = p;
                        }
                        OpenCvSharp.Point[][] contours = new OpenCvSharp.Point[][] { all };
                        Cv2.DrawContours(mat, contours, 0, new Scalar(0, 0, 255), 2);
                        if (result.Text != null) Cv2.PutText(mat, result.Text, new OpenCvSharp.Point(xMin, yMax), HersheyFonts.HersheySimplex, 1, new Scalar(0, 0, 255), 2);
                    }
                }
            }
            else
            {
                richTextBox1.AppendText("No barcode detected!" + Environment.NewLine);
            }

            return mat;
        }

        private Bitmap DetectDocumentEdges(Mat mat)
        {
            int length = mat.Cols * mat.Rows * mat.ElemSize();
            byte[] bytes = new byte[length];
            Marshal.Copy(mat.Data, bytes, 0, length);

            _docResults = documentScanner.DetectBuffer(bytes, mat.Cols, mat.Rows, (int)mat.Step(), DocumentScanner.ImagePixelFormat.IPF_RGB_888);
            if (_docResults != null)
            {
                DocResult result = _docResults[0];
                if (result.Points != null)
                {
                    Point[] points = new Point[4];
                    for (int i = 0; i < 4; i++)
                    {
                        points[i] = new Point(result.Points[i * 2], result.Points[i * 2 + 1]);
                    }
                    Cv2.DrawContours(mat, new Point[][] { points }, 0, Scalar.Red, 2);
                }
            }

            Bitmap bitmap = BitmapConverter.ToBitmap(mat);
            return bitmap;
        }

        private void PreviewNormalizedImage()
        {
            if (_docResults != null)
            {
                DocResult result = _docResults[0];
                int length = _mat.Cols * _mat.Rows * _mat.ElemSize();
                byte[] bytes = new byte[length];
                Marshal.Copy(_mat.Data, bytes, 0, length);

                NormalizedImage image = documentScanner.NormalizeBuffer(bytes, _mat.Cols, _mat.Rows, (int)_mat.Step(), DocumentScanner.ImagePixelFormat.IPF_RGB_888, result.Points);
                if (image != null && image.Data != null)
                {
                    Mat newMat;
                    if (image.Stride < image.Width)
                    {
                        // binary
                        byte[] data = image.Binary2Grayscale();
                        newMat = new Mat(image.Height, image.Width, MatType.CV_8UC1, data);
                    }
                    else if (image.Stride >= image.Width * 3)
                    {
                        // color
                        newMat = new Mat(image.Height, image.Width, MatType.CV_8UC3, image.Data);
                    }
                    else
                    {
                        // grayscale
                        newMat = new Mat(image.Height, image.Stride, MatType.CV_8UC1, image.Data);
                    }

                    if (checkBox2.Checked)
                    {
                        newMat = DetectBarcode(newMat);
                    }

                    if (checkBox3.Checked) {
                        newMat = DetectMrz(newMat);
                    }
                    pictureBox2.Image = BitmapConverter.ToBitmap(newMat);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopScan();
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image files (*.bmp, *.jpg, *.png) | *.bmp; *.jpg; *.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    listBox1.Items.Add(dlg.FileName);
                    _currentFilename = dlg.FileName;
                    DetectFile(dlg.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!capture.IsOpened())
            {
                MessageBox.Show("Failed to open video stream or file");
                return;
            }

            if (button2.Text == "Camera Scan")
            {
                StartScan();
            }
            else
            {
                StopScan();
            }
        }

        private void StartScan()
        {
            button2.Text = "Stop";
            isCapturing = true;
            thread = new Thread(new ThreadStart(FrameCallback));
            thread.Start();
        }

        private void StopScan()
        {
            button2.Text = "Camera Scan";
            isCapturing = false;
            if (thread != null) thread.Join();
        }

        private void FrameCallback()
        {
            while (isCapturing)
            {
                capture.Read(_mat);
                Mat copy = new Mat(_mat.Rows, _mat.Cols, MatType.CV_8UC3);
                _mat.CopyTo(copy);
                pictureBox1.Image = BitmapConverter.ToBitmap(DetectMrz(copy));
            }
        }

        private void Form1_Closing(object? sender, FormClosingEventArgs e)
        {
            StopScan();
        }

        private void enterLicenseKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string license = InputBox("Enter License Key", "", "");
            if (license != null && license != "")
            {
                ActivateLicense(license);
            }
        }

        public static string InputBox(string title, string promptText, string value)
        {
            Form form = new Form();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(60, 72, 80, 30);
            buttonCancel.SetBounds(260, 72, 80, 30);

            form.ClientSize = new System.Drawing.Size(400, 120);
            form.Controls.AddRange(new Control[] { textBox, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            return textBox.Text;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentFilename = listBox1.SelectedItem.ToString();
            if (_currentFilename != null && _currentFilename != "")
            {
                DetectFile(_currentFilename);
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    listBox1.Items.Add(file);
                }
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_currentFilename != null && _currentFilename != "")
            {
                DetectFile(_currentFilename);
            }
        }
    }
}
