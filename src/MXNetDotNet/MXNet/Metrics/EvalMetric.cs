﻿using System;

// ReSharper disable once CheckNamespace
namespace MXNetDotNet
{

    public abstract class EvalMetric
    {

        #region Constructors

        protected EvalMetric(string name, int num = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            this.Name = name;
            this.Num = num;
        }

        #endregion

        #region Properties

        protected string Name
        {
            get;
        }

        protected int Num
        {
            get;
            set;
        }

        protected int NumInst
        {
            get;
            set;
        }

        protected float SumMetric
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public float Get()
        {
            return this.SumMetric / this.NumInst;
        }

        public void GetNameValue()
        {
        }

        public void Reset()
        {
            this.NumInst = 0;
            this.SumMetric = 0.0f;
        }

        public abstract void Update(NDArray labels, NDArray preds);

        #region Helpers

        protected static void CheckLabelShapes(NDArray labels, NDArray preds, bool strict = false)
        {
            if (strict)
            {
                Logging.CHECK_EQ(new Shape(labels.GetShape()), new Shape(preds.GetShape()));
            }
            else
            {
                Logging.CHECK_EQ(labels.Size, preds.Size);
            }
        }

        #endregion

        #endregion

    }

}
