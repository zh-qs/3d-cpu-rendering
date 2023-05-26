using GKProject.Drawing;
using GKProject.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using GKProject.IO;
using System.Threading;
using GKProject.Drawing.Shading;
using GKProject.Drawing.CameraModes;

namespace GKProject
{
    public partial class Form1 : Form
    {
        Panel panel;
        DirectBufferedBitmap bitmap;

        Graphics g;
        Model model;
        ChessBoard board;
        SphereParameters parameters;

        Scene scene;

        Thread thread;

        Color fogColor;

        bool canRunAnimation = true;

        public Form1()
        {
            InitializeComponent();
            panel = splitContainer1.Panel1;
            panel.Paint += Panel_Paint;
            panel.SizeChanged += Panel_SizeChanged;
            bitmap = new DirectBufferedBitmap(panel.Width, panel.Height);

            g = panel.CreateGraphics();

            scene = InitialParameters.GetScene(panel);
            InitialParameters.AddLightsToScene(scene);
            fogColor = scene.FogColor.ToColor();

            fogDensityLabel.Text = scene.FogDensity.ToString();
            fogDensityTrackBar.Value = (int)(scene.FogDensity * 1000);

            model = new Model(scene, new FlatShadingMethod(scene));

            parameters = InitialParameters.GetSphereParameters();
            model.AddSolid("sphere", new Sphere(InitialParameters.GetSphereRadius(), InitialParameters.GetSphereMaterial(), parameters));

            board = ChessBoard.BuildChessBoardAndFillModel(model, Render, scene);


            thread = GetGameAnimationThread();
        }

        private void Panel_SizeChanged(object sender, EventArgs e)
        {
            g.Dispose();
            bitmap.Dispose();

            bitmap = new DirectBufferedBitmap(panel.Width, panel.Height);
            g = panel.CreateGraphics();

            scene.ScreenWidth = panel.Width;
            scene.ScreenHeight = panel.Height;

            RenderIfNotAnimated();
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        void Render()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            bitmap.Clear(fogColor);
            model.RenderTo(bitmap);
            g.DrawImage(bitmap.Bitmap, Point.Empty);
            sw.Stop();

            fpsLabel.Invoke((MethodInvoker) delegate { fpsLabel.Text = $"FPS: {MathF.Round(1000 / sw.ElapsedMilliseconds, 2)}"; });
        }

        private void flatShadingRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (flatShadingRadioButton.Checked) model.ShadingMethod = new FlatShadingMethod(scene);
            RenderIfNotAnimated();
        }

        private void goraudShadingRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (goraudShadingRadioButton.Checked) model.ShadingMethod = new GoraudShadingMethod(scene);
            RenderIfNotAnimated();
        }

        private void phongShadingRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (phongShadingRadioButton.Checked) model.ShadingMethod = new PhongShadingMethod(scene);
            RenderIfNotAnimated();
        }

        private void lngStepsTrackBar_Scroll(object sender, EventArgs e)
        {
            parameters.LngSteps = lngStepsTrackBar.Value;
            lngStepsTextBox.Text = lngStepsTrackBar.Value.ToString();
            RenderIfNotAnimated();
        }

        private void latStepsTrackBar_Scroll(object sender, EventArgs e)
        {
            parameters.LatSteps = latStepsTrackBar.Value;
            latStepsTextBox.Text = latStepsTrackBar.Value.ToString();
            RenderIfNotAnimated();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (canRunAnimation)
            {
                runButton.Enabled = false;
                canRunAnimation = false;
                thread.Start();
            }
            else
            {
                thread = GetGameAnimationThread();
                board.ResetBoard();
                runButton.Text = "Run!";
                canRunAnimation = true;
                runButton.Enabled = gameSelectionComboBox.SelectedIndex >= 0;
            }
        }

        void RenderIfNotAnimated()
        {
            if (thread == null || thread.ThreadState != ThreadState.Background) Render();
        }

        private void gameSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            runButton.Enabled = true;
            thread = GetGameAnimationThread();
        }

        private void staticCameraRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            board.CameraMode = new StaticCameraMode();
        }

        private void followingCameraRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            board.CameraMode = new FollowingCameraMode();
        }

        private void attachedCameraRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            board.CameraMode = new AttachedCameraMode();
        }

        private void fogDensityTrackBar_Scroll(object sender, EventArgs e)
        {
            scene.FogDensity = (float)fogDensityTrackBar.Value / 1000;
            fogDensityLabel.Text = scene.FogDensity.ToString();
            RenderIfNotAnimated();
        }

        Thread GetGameAnimationThread()
        {
            if (gameSelectionComboBox.SelectedIndex < 0) return null;
            
            if (gameSelectionComboBox.SelectedIndex == 0)
            {
                Thread thread = new Thread(() =>
                {
                    board.MovePiece(new Point(1, 6), new Point(3, 6));
                    board.MovePiece(new Point(6, 4), new Point(4, 4));
                    board.MovePiece(new Point(1, 5), new Point(2, 5));
                    board.MovePiece(new Point(7, 3), new Point(3, 7));

                    runButton.Invoke((MethodInvoker)delegate { runButton.Text = "Reset"; runButton.Enabled = true; });
                });
                thread.IsBackground = true;
                return thread;
            }
            else if (gameSelectionComboBox.SelectedIndex == 1)
            {
                Thread thread = new Thread(() =>
                {
                    board.MovePiece(new Point(0, 1), new Point(2, 2));
                    board.MovePiece(new Point(7, 6), new Point(5, 5));
                    board.MovePiece(new Point(2, 2), new Point(0, 1));
                    board.MovePiece(new Point(5, 5), new Point(4, 7));
                    board.MovePiece(new Point(0, 6), new Point(2, 5));
                    board.MovePiece(new Point(4, 7), new Point(2, 6));
                    board.MovePiece(new Point(2, 5), new Point(0, 6));
                    board.MoveAndTakePiece(new Point(2, 6), new Point(0, 7));
                    board.MovePiece(new Point(0, 1), new Point(2, 2));
                    board.MovePiece(new Point(0, 7), new Point(2, 6));
                    board.MovePiece(new Point(2, 2), new Point(0, 1));
                    board.MoveAndTakePiece(new Point(2, 6), new Point(0, 5));
                    board.MovePiece(new Point(0, 6), new Point(2, 5));
                    board.MovePiece(new Point(0, 5), new Point(2, 6));
                    board.MovePiece(new Point(2, 5), new Point(0, 6));
                    board.MovePiece(new Point(2, 6), new Point(4, 7));
                    board.MovePiece(new Point(0, 1), new Point(2, 2));
                    board.MovePiece(new Point(4, 7), new Point(3, 5));
                    board.MovePiece(new Point(2, 2), new Point(0, 1));
                    board.MovePiece(new Point(3, 5), new Point(2, 7));
                    board.MovePiece(new Point(0, 1), new Point(2, 2));
                    board.MoveAndTakePiece(new Point(2, 7), new Point(0, 6));
                    board.MovePiece(new Point(2, 2), new Point(0, 1));
                    board.MovePiece(new Point(0, 6), new Point(2, 7));
                    board.MovePiece(new Point(0, 1), new Point(2, 2));
                    board.MovePiece(new Point(2, 7), new Point(3, 5));
                    board.MovePiece(new Point(2, 2), new Point(0, 1));
                    board.MovePiece(new Point(3, 5), new Point(4, 7));
                    board.MovePiece(new Point(0, 1), new Point(2, 2));
                    board.MovePiece(new Point(4, 7), new Point(5, 5));
                    board.MovePiece(new Point(2, 2), new Point(0, 1));
                    board.MovePiece(new Point(5, 5), new Point(7, 6));
                    board.MovePiece(new Point(0, 1), new Point(2, 2));

                    runButton.Invoke((MethodInvoker)delegate { runButton.Text = "Reset"; runButton.Enabled = true; });
                });
                thread.IsBackground = true;
                return thread;
            }
            else if (gameSelectionComboBox.SelectedIndex == 2)
            {
                Thread thread = new Thread(() =>
                {
                    board.MovePiece(new Point(1, 4), new Point(3, 4));
                    board.MovePiece(new Point(6, 4), new Point(4, 4));
                    board.MovePiece(new Point(0, 6), new Point(2, 5));
                    board.MovePiece(new Point(6, 3), new Point(5, 3));
                    board.MovePiece(new Point(0, 5), new Point(3, 2));
                    board.MovePiece(new Point(7, 2), new Point(3, 6));
                    board.MovePiece(new Point(0, 1), new Point(2, 2));
                    board.MovePiece(new Point(6, 6), new Point(5, 6));
                    board.MoveAndTakePiece(new Point(2, 5), new Point(4, 4));
                    board.MoveAndTakePiece(new Point(3, 6), new Point(0, 3));
                    board.MoveAndTakePiece(new Point(3, 2), new Point(6, 5));
                    board.MovePiece(new Point(7, 4), new Point(6, 4));
                    board.MovePiece(new Point(2, 2), new Point(4, 3));

                    runButton.Invoke((MethodInvoker)delegate { runButton.Text = "Reset"; runButton.Enabled = true; });
                });
                thread.IsBackground = true;
                return thread;
            }

            throw new ArgumentOutOfRangeException();
        }        
    }
}
