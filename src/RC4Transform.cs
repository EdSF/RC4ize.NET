using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections;

namespace RC4.NET
{
    public class RC4Transform : ICryptoTransform
    {
        #region Interface contract implementation
        bool ICryptoTransform.CanReuseTransform
        {
            get { return true; }
        }

        bool ICryptoTransform.CanTransformMultipleBlocks
        {
            get { return true; }
        }

        int ICryptoTransform.InputBlockSize
        {
            get { return 8; }
        }

        int ICryptoTransform.OutputBlockSize
        {
            get { return 8; }
        }

        int ICryptoTransform.TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            Crypt(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);

            return inputCount;
        }

        byte[] ICryptoTransform.TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            byte[] data = new byte[inputCount];
            Crypt(inputBuffer, inputOffset, data, 0, inputCount);

            return data;
        }

        void IDisposable.Dispose() { }
        #endregion 

        #region Transform implementation

        private byte[] box = new byte[256];

        public RC4Transform(byte[] rgbKey, byte[] rgbIV)
        {
            ScheduleBox(rgbKey, rgbIV);
        }

        private void InitializeBox()
        {
            int c;
            byte i;
            for (i = 0, c = 0; c < 256; c++, i++)
            {
                box[i] = i;
            }
        }

        private void ScheduleBox(byte[] rgbKey, byte[] rgbIV)
        {
            int c, keylength;
            byte i, j, swap;

            // glueing the key and the initialization vector together
            byte[] key = new byte[rgbKey.Length + rgbIV.Length];
            System.Buffer.BlockCopy(rgbKey, 0, key, 0, rgbKey.Length);
            System.Buffer.BlockCopy(rgbIV, 0, key, rgbKey.Length, rgbIV.Length);

            InitializeBox();

            for (i = j = 0, c = 0, keylength = key.Length;
                 c < 256;
                 i++, c++)
            {
                j = (byte)(j + box[i] + key[i % keylength]);

                swap = box[i];
                box[i] = box[j];
                box[j] = swap;
            }
        }

        private void Crypt(byte[] inputBuffer, int inputOffset, byte[] outputBuffer, int outputOffset, int inputCount)
        {
            // initialize vars
            int c;
            byte i, j, k, swap;

            // pseudo-random generation algorithm + implementation
            for (i = j = 0, c = 0; c < inputCount; c++)
            {
                i++;
                j = (byte)(j + box[i]);

                swap = box[i];
                box[i] = box[j];
                box[j] = swap;

                k = (byte)(box[i] + box[j]);

                outputBuffer[outputOffset + c] = 
                    (byte)(inputBuffer[inputOffset + c] ^ box[k]);
            }
        }

        public byte[] GetBox()
        {
            return box;
        }
        #endregion
    }
}
