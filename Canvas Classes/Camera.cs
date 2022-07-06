using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class Camera
    {
        private string name;

        public IPoint Position { get; set; }
        public ITarget Target { get; set; }

        public Camera(IPoint position, ITarget target, string name)
        {
            this.Position = position;
            this.Target = target;

            this.name = name;
        }

        #region Moving

        private void Move(bool target, Vector<double> distance)
        {
            this.Invalidate();

            if (target)
            {
                this.Target.Position.X += distance[0];
                this.Target.Position.Y += distance[1];
                this.Target.Position.Z += distance[2];
            }
            else
            {
                this.Position.X += distance[0];
                this.Position.Y += distance[1];
                this.Position.Z += distance[2];
            }
        }

        public void MoveTarget(Vector<double> distance) => this.Move(true, distance);

        public void MoveSelf(Vector<double> distance) => this.Move(false, distance);

        #endregion

        public void SetTarget(ITarget target) => this.Target = target;

        public void Invalidate()
        {
            this.InvalidateViewMatrix();
            this.InvalidateProjectionMatrix();
            this.InvalidateCameraMatrix();
        }

        #region ViewMatrix

        private Matrix<double> viewMatrix;

        private bool viewMatrixCalculated = false;
        public void InvalidateViewMatrix() => this.viewMatrixCalculated = false;

        public Matrix<double> ViewMatrix 
        { 
            get
            {
                if (!this.viewMatrixCalculated)
                    this.CalculateViewMatrix();

                return this.viewMatrix;
            } 
        }

        public void CalculateViewMatrix()
        {
            Vector<double> normalizing = Vector<double>.Build.DenseOfArray(new double[] { 0.0, 0.0, 1.0 });
            Vector<double> up;
            Vector<double> forward;
            Vector<double> right;

            Vector<double> L = Functors.Distance(this.Position.ToArray(), this.Target.Position.ToArray());
            L = Functors.Versorize(L);

            if (Math.Abs(L[0]) == normalizing[0] && Math.Abs(L[1]) == normalizing[1] && Math.Abs(L[2]) == normalizing[2])
                up = Vector<double>.Build.DenseOfArray(new double[] { 1.0, 0.0, 0.0 });
            else
            {
                Vector<double> R = Functors.CrossProduct(L, Vector<double>.Build.Dense(new double[] { 0, 0, 1 }));
                R = Functors.Versorize(R);

                up = (-1) * Functors.Versorize(Functors.CrossProduct(L, R));
            }

            forward = Functors.Versorize(Functors.Distance(this.Target.Position.ToArray(), this.Position.ToArray()));
            right = Functors.Versorize(Functors.CrossProduct(up, forward));

            this.viewMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { right[0], up[0], forward[0], this.Position.X },
                { right[1], up[1], forward[1], this.Position.Y },
                { right[2], up[2], forward[2], this.Position.Z },
                { 0, 0, 0, 1}
            }).Inverse();

            this.viewMatrixCalculated = true;
        }

        #endregion

        #region ProjectionMatrix

        private Matrix<double> projectionMatrix;
        private Matrix<double> inverseProjectionMatrix;

        private bool projectionMatrixCalculated = false;
        public void InvalidateProjectionMatrix() => this.projectionMatrixCalculated = false;

        public Matrix<double> ProjectionMatrix
        {
            get
            {
                if (!this.projectionMatrixCalculated)
                    this.CalculateProjectionMatrix();

                return this.projectionMatrix;    
            }
        }

        public Matrix<double> InverseProjectionMatrix
        {
            get
            {
                if (!this.projectionMatrixCalculated)
                    this.CalculateProjectionMatrix();

                return this.inverseProjectionMatrix;
            }
        }

        public void CalculateProjectionMatrix()
        {
            // n = 1 f = 100
            double n = 1.0;
            double f = 100.0;
            double e = 1.0 / Math.Tan((double)Designer.Instance.Alpha * Math.PI / 180.0 / 2.0);
            double a = (double)Designer.Instance.Params.MainPictureBox.Height / (double)Designer.Instance.Params.MainPictureBox.Width;

            var M = Matrix<double>.Build;
            this.projectionMatrix = M.DenseOfArray(new double[,]
            {
                { e, 0.0, 0.0, 0.0 },
                { 0.0, e / a, 0.0, 0.0 },
                { 0.0, 0.0, (-1) * (f + n) / (f - n), (-2) * (f * n) / (f - n) },
                { 0.0, 0.0, -1.0, 0.0 }
            }
                );

            // this.inverseProjectionMatrix = this.projectionMatrix.Inverse();

            this.projectionMatrixCalculated = true;
        }

        #endregion

        #region CameraMatrix

        private Matrix<double> cameraMatrix;
        private Matrix<double> inverseCameraMatrix;

        private bool cameraMatrixCalculated = false;
        public void InvalidateCameraMatrix() => this.cameraMatrixCalculated = false;

        public Matrix<double> CameraMatrix
        {
            get
            {
                if (!this.cameraMatrixCalculated)
                    this.CalculateCameraMatrix();

                return this.cameraMatrix;
            }
        }

        public Matrix<double> InverseCameraMatrix
        {
            get
            {
                if (!this.cameraMatrixCalculated)
                    this.CalculateCameraMatrix();

                return this.inverseCameraMatrix;
            }
        }

        private void CalculateCameraMatrix()
        {
            if (!this.viewMatrixCalculated)
                this.CalculateViewMatrix();

            if (!this.projectionMatrixCalculated)
                this.CalculateProjectionMatrix();
            
            this.cameraMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { 1.0, 0.0, 0.0, 0.0 },
                { 0.0, 1.0, 0.0, 0.0 },
                { 0.0, 0.0, 1.0, 0.0 },
                { 0.0, 0.0, 0.0, 1.0 }
            });

            this.viewMatrix.Multiply(this.cameraMatrix, this.cameraMatrix);
            this.projectionMatrix.Multiply(this.cameraMatrix, this.cameraMatrix);
            this.inverseCameraMatrix = this.cameraMatrix.Inverse();

            this.cameraMatrixCalculated = true;
        }

        #endregion

        public override string ToString() => this.name;
    }
}
