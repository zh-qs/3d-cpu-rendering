using GKProject.Drawing;
using GKProject.Drawing.CameraModes;
using GKProject.Geometry;
using GKProject.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GKProject
{
    // contains fields representing chess board and methods to move and animate pieces
    public class ChessBoard
    {
        const string objFilePath = @"..\..\..\..\Chess_Scene.obj";

        const string boardName = "Board_Frame";

        const string whiteKingName = "White_King";
        const string whiteQueenName = "White_Queen";
        const string whiteBlackFieldBishopName = "White_Bishop";
        const string whiteWhiteFieldBishopName = "White_Bishop.001_White_Bishop";
        const string whiteBlackFieldKnightName = "White_Knight.001_White_Knight";
        const string whiteWhiteFieldKnightName = "White_Knight";
        const string whiteBlackFieldRookName = "White_Rook";
        const string whiteWhiteFieldRookName = "White_Rook.001_White_Rook";

        const string whiteAPawnName = "White_Pawn";
        const string whiteBPawnName = "White_Pawn.001_White_Pawn";
        const string whiteCPawnName = "White_Pawn.002_White_Pawn";
        const string whiteDPawnName = "White_Pawn.003_White_Pawn";
        const string whiteEPawnName = "White_Pawn.004_White_Pawn";
        const string whiteFPawnName = "White_Pawn.005_White_Pawn";
        const string whiteGPawnName = "White_Pawn.006_White_Pawn";
        const string whiteHPawnName = "White_Pawn.007_White_Pawn";

        const string blackKingName = "Black_King.001";
        const string blackQueenName = "Black_Queen.001";
        const string blackBlackFieldBishopName = "Black_Bishop.003_Black_Bishop.001";
        const string blackWhiteFieldBishopName = "Black_Bishop.002_Black_Bishop.001";
        const string blackBlackFieldKnightName = "Black_Knight.002_Black_Knight.001";
        const string blackWhiteFieldKnightName = "Black_Knight.003_Black_Knight.001";
        const string blackBlackFieldRookName = "Black_Rook.003_White_Rook.001";
        const string blackWhiteFieldRookName = "Black_Rook.002_White_Rook.001";

        const string blackAPawnName = "Black_Pawn.008_Black_Pawn.001";
        const string blackBPawnName = "Black_Pawn.009_Black_Pawn.001";
        const string blackCPawnName = "Black_Pawn.010_Black_Pawn.001";
        const string blackDPawnName = "Black_Pawn.011_Black_Pawn.001";
        const string blackEPawnName = "Black_Pawn.012_Black_Pawn.001";
        const string blackFPawnName = "Black_Pawn.013_Black_Pawn.001";
        const string blackGPawnName = "Black_Pawn.014_Black_Pawn.001";
        const string blackHPawnName = "Black_Pawn.015_Black_Pawn.001";

        Model model;
        Action render;
        Solid[,] board;
        Scene scene;

        public ICameraMode CameraMode { get; set; } = new StaticCameraMode();

        private ChessBoard(Model model, Action render, Scene scene) 
        {
            this.model = model;
            board = new Solid[8, 8];
            this.render = render;
            this.scene = scene;

            FillBoardWithPieces();
        }

        // board:
        //    01234567
        //   +--------+
        // 7 |W      B| 8
        // 6 |        | 7 black pieces
        // 5 |        | 6
        // 4 |        | 5
        // 3 |        | 4
        // 2 |        | 3
        // 1 |        | 2 white pieces
        // 0 |B      W| 1 
        //   +--------+
        //    ABCDEFGH  :real
        void FillBoardWithPieces()
        {
            board[0, 0] = model.GetSolid(whiteBlackFieldRookName);
            board[0, 1] = model.GetSolid(whiteWhiteFieldKnightName);
            board[0, 2] = model.GetSolid(whiteBlackFieldBishopName);
            board[0, 3] = model.GetSolid(whiteQueenName);
            board[0, 4] = model.GetSolid(whiteKingName);
            board[0, 5] = model.GetSolid(whiteWhiteFieldBishopName);
            board[0, 6] = model.GetSolid(whiteBlackFieldKnightName);
            board[0, 7] = model.GetSolid(whiteWhiteFieldRookName);

            board[1, 0] = model.GetSolid(whiteAPawnName);
            board[1, 1] = model.GetSolid(whiteBPawnName);
            board[1, 2] = model.GetSolid(whiteCPawnName);
            board[1, 3] = model.GetSolid(whiteDPawnName);
            board[1, 4] = model.GetSolid(whiteEPawnName);
            board[1, 5] = model.GetSolid(whiteFPawnName);
            board[1, 6] = model.GetSolid(whiteGPawnName);
            board[1, 7] = model.GetSolid(whiteHPawnName);

            board[6, 0] = model.GetSolid(blackAPawnName);
            board[6, 1] = model.GetSolid(blackBPawnName);
            board[6, 2] = model.GetSolid(blackCPawnName);
            board[6, 3] = model.GetSolid(blackDPawnName);
            board[6, 4] = model.GetSolid(blackEPawnName);
            board[6, 5] = model.GetSolid(blackFPawnName);
            board[6, 6] = model.GetSolid(blackGPawnName);
            board[6, 7] = model.GetSolid(blackHPawnName);

            board[7, 0] = model.GetSolid(blackWhiteFieldRookName);
            board[7, 1] = model.GetSolid(blackBlackFieldKnightName);
            board[7, 2] = model.GetSolid(blackWhiteFieldBishopName);
            board[7, 3] = model.GetSolid(blackQueenName);
            board[7, 4] = model.GetSolid(blackKingName);
            board[7, 5] = model.GetSolid(blackBlackFieldBishopName);
            board[7, 6] = model.GetSolid(blackWhiteFieldKnightName);
            board[7, 7] = model.GetSolid(blackBlackFieldRookName);

            board[0, 0].SetTranslation(new Vector3(-21, 0, 21));
            board[0, 1].SetTranslation(new Vector3(-15, 0, 21));
            board[0, 2].SetTranslation(new Vector3(-9, 0, 21));
            board[0, 3].SetTranslation(new Vector3(-3, 0, 21));
            board[0, 4].SetTranslation(new Vector3(3, 0, 21));
            board[0, 5].SetTranslation(new Vector3(9, 0, 21));
            board[0, 6].SetTranslation(new Vector3(15, 0, 21));
            board[0, 7].SetTranslation(new Vector3(21, 0, 21));

            board[1, 0].SetTranslation(new Vector3(-21, 0, 15));
            board[1, 1].SetTranslation(new Vector3(-15, 0, 15));
            board[1, 2].SetTranslation(new Vector3(-9, 0, 15));
            board[1, 3].SetTranslation(new Vector3(-3, 0, 15));
            board[1, 4].SetTranslation(new Vector3(3, 0, 15));
            board[1, 5].SetTranslation(new Vector3(9, 0, 15));
            board[1, 6].SetTranslation(new Vector3(15, 0, 15));
            board[1, 7].SetTranslation(new Vector3(21, 0, 15));

            board[6, 0].SetTranslation(new Vector3(-21, 0, -15));
            board[6, 1].SetTranslation(new Vector3(-15, 0, -15));
            board[6, 2].SetTranslation(new Vector3(-9, 0, -15));
            board[6, 3].SetTranslation(new Vector3(-3, 0, -15));
            board[6, 4].SetTranslation(new Vector3(3, 0, -15));
            board[6, 5].SetTranslation(new Vector3(9, 0, -15));
            board[6, 6].SetTranslation(new Vector3(15, 0, -15));
            board[6, 7].SetTranslation(new Vector3(21, 0, -15));

            board[7, 0].SetTranslation(new Vector3(-21, 0, -21));
            board[7, 1].SetTranslation(new Vector3(-15, 0, -21));
            board[7, 2].SetTranslation(new Vector3(-9, 0, -21));
            board[7, 3].SetTranslation(new Vector3(-3, 0, -21));
            board[7, 4].SetTranslation(new Vector3(3, 0, -21));
            board[7, 5].SetTranslation(new Vector3(9, 0, -21));
            board[7, 6].SetTranslation(new Vector3(15, 0, -21));
            board[7, 7].SetTranslation(new Vector3(21, 0, -21));

            for (int i = 0; i <= 1; ++i)
                for (int j = 0; j < 8; ++j)
                {
                    board[i, j].ModelDirection = new Vector3(1, 0, 0);
                    board[i, j].Visible = true;
                }

            for (int i = 6; i <= 7; ++i)
                for (int j = 0; j < 8; ++j)
                {
                    board[i, j].ModelDirection = new Vector3(-1, 0, 0);
                    board[i, j].Visible = true;
                }
        }

        public static ChessBoard BuildChessBoardAndFillModel(Model model, Action render, Scene scene)
        {
            model.AddSolids(ObjParser.ParseMeshes(objFilePath));
            return new ChessBoard(model, render,scene);
        }

        public void MovePiece(Point from, Point to)
        {
            Solid solid = board[from.X, from.Y];
            if (solid == null) throw new ArgumentException($"There is no piece at point {from}");

            if (board[to.X, to.Y] != null) throw new ArgumentException($"There is already a piece at point {to}. If you want to take this piece, use MoveAndTakePiece() method.");
            board[from.X, from.Y] = null;
            board[to.X, to.Y] = solid;

            AnimateMove(solid, GetModelSpaceTranslationVector(from, to));
        }

        public void ResetBoard()
        {
            board = new Solid[8, 8];
            FillBoardWithPieces();
            render();
        }

        public void MoveAndTakePiece(Point from, Point to)
        {
            Solid solid = board[from.X, from.Y];
            if (solid == null) throw new ArgumentException($"There is no piece at point {from}");

            if (board[to.X, to.Y] == null) throw new ArgumentException($"There is no piece at point {to} to take. If you want to only move a piece, use MovePiece() method.");
            Solid takenSolid = board[to.X, to.Y];
            board[from.X, from.Y] = null;
            board[to.X, to.Y] = solid;

            
            AnimateTake(takenSolid, solid, GetModelSpaceTranslationVector(from, to));
        }

        Vector3 GetModelSpaceTranslationVector(Point from, Point to)
        {
            return new Vector3(6 * (to.Y - from.Y), 0, 6 * (from.X - to.X));
        }


        const int steps = 40;
        void AnimateMove(Solid solid, Vector3 vector)
        {
            Vector3 step = vector / steps;
            float yChange = 0.03f;
            float smallAngle = InitialParameters.GetPieceRotationAngle() / (steps / 2);

            float policeAngle = MathF.PI * 4 / steps;
            Light policeLight = InitialParameters.GetPoliceLight(solid.CameraTarget);

            scene.Lights.Add(policeLight);

            Matrix4x4 rotation;

            solid.ModelDirection = step;

            CameraMode.MoveCameraAfterSolidHasMoved(scene, solid);

            for (int i=0;i<steps/2;i++)
            {
                rotation = RotationMatrixAroundAxisPerpendicularToVector(vector, i * smallAngle);
                solid.SetModelMatrix(rotation);
                solid.Translate(new Vector3(step.X, step.Y + (steps/2 - i - 1) * yChange, step.Z));

                policeLight.Position = solid.CameraTarget;
                policeLight.Direction = Vector3.Transform(InitialParameters.GetPoliceLightDirection(), Matrix4x4.Transpose(rotation * RotationY(i * policeAngle)));
                
                CameraMode.MoveCameraAfterSolidHasMoved(scene, solid);
                render();
            }
            for (int i = 0; i < steps/2; i++)
            {
                rotation = RotationMatrixAroundAxisPerpendicularToVector(vector, (steps / 2 - i - 1) * smallAngle);
                solid.SetModelMatrix(rotation);
                solid.Translate(new Vector3(step.X, step.Y - i * yChange, step.Z));
                policeLight.Position = solid.CameraTarget;
                policeLight.Direction = Vector3.Transform(InitialParameters.GetPoliceLightDirection(), Matrix4x4.Transpose(rotation * RotationY(i * policeAngle)));
                
                CameraMode.MoveCameraAfterSolidHasMoved(scene, solid);
                render();
            }

            scene.Lights.Remove(policeLight);
            render();
        }

        void AnimateTake(Solid takenSolid, Solid solid, Vector3 vector)
        {
            Vector3 step = vector / steps;
            Vector3 takenStep = new Vector3(0, -takenSolid.Height, 0) / steps;
            float yChange = 0.03f;
            float smallAngle = InitialParameters.GetPieceRotationAngle() / (steps / 2);

            float policeAngle = MathF.PI * 4 / steps;
            Light policeLight = InitialParameters.GetPoliceLight(solid.CameraTarget);

            scene.Lights.Add(policeLight);

            Matrix4x4 rotation;

            solid.ModelDirection = step;

            CameraMode.MoveCameraAfterSolidHasMoved(scene, solid);

            for (int i = 0; i < steps / 2; i++)
            {
                rotation = RotationMatrixAroundAxisPerpendicularToVector(vector, i * smallAngle);
                solid.SetModelMatrix(rotation);
                solid.Translate(new Vector3(step.X, step.Y + (steps / 2 - i - 1) * yChange, step.Z));
                takenSolid.Translate(takenStep);

                policeLight.Position = solid.CameraTarget;
                policeLight.Direction = Vector3.Transform(InitialParameters.GetPoliceLightDirection(), Matrix4x4.Transpose(rotation * RotationY(i * policeAngle)));

                CameraMode.MoveCameraAfterSolidHasMoved(scene, solid);
                render();
            }
            for (int i = 0; i < steps / 2; i++)
            {
                rotation = RotationMatrixAroundAxisPerpendicularToVector(vector, (steps / 2 - i - 1) * smallAngle);
                solid.SetModelMatrix(rotation);
                solid.Translate(new Vector3(step.X, step.Y - i * yChange, step.Z));
                takenSolid.Translate(takenStep);

                policeLight.Position = solid.CameraTarget;
                policeLight.Direction = Vector3.Transform(InitialParameters.GetPoliceLightDirection(), Matrix4x4.Transpose(rotation * RotationY(i * policeAngle)));

                CameraMode.MoveCameraAfterSolidHasMoved(scene, solid);
                render();
            }

            scene.Lights.Remove(policeLight);
            takenSolid.Visible = false;
            render();
        }

        Matrix4x4 RotationMatrixAroundAxisPerpendicularToVector(Vector3 vector, float angle)
        {
            Vector3 axis = Vector3.Normalize(Vector3.Cross(vector, Scene.WorldUp));
            float s = MathF.Sin(angle), c = MathF.Cos(angle);
            // we know that axis.Y == 0, because we are rotating chess pieces around axis parallel to OXZ plane
            return new Matrix4x4(
                axis.X * axis.X + (1 - axis.X * axis.X) * c, -axis.Z * s, axis.X * axis.Z * (1 - c), 0,
                axis.Z * s, c, -axis.X * s, 0,
                axis.X * axis.Z * (1 - c), axis.X * s, axis.Z * axis.Z + (1 - axis.Z * axis.Z) * c, 0,
                0, 0, 0, 1
                );
        }

        Matrix4x4 RotationY(float angle)
        {
            float s = MathF.Sin(angle), c = MathF.Cos(angle);
            return new Matrix4x4(
                c, 0, -s, 0,
                0, 1, 0, 0,
                s, 0, c, 0,
                0, 0, 0, 1
                );
        }

    }
}
