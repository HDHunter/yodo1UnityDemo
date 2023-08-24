namespace Yodo1.Suit
{
    using System;
    using UnityEngine;

    public class TXTWriter : IDisposable
    {
#if !UNITY_FLASH
        private System.IO.StreamWriter m_file;
        private bool m_disposed = false;
#endif

        public TXTWriter(string fileName)
        {
#if !UNITY_FLASH
            try
            {
                m_file = new System.IO.StreamWriter(fileName, false, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Debug.LogError("Could not open: " + fileName + ", error: " + ex.Message);
            }
#endif
        }

        ~TXTWriter()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
#if !UNITY_FLASH
            GC.SuppressFinalize(this);
#endif
        }

        protected virtual void Dispose(bool disposing)
        {
#if !UNITY_FLASH
            if (!this.m_disposed)
            {
                if (disposing && m_file != null)
                {
                    m_file.Dispose();
                }

                m_disposed = true;
            }
#endif
        }

        public void WriteString(string val)
        {
#if !UNITY_FLASH
            if (m_file == null)
            {
                return;
            }

            m_file.Write(val);
            m_file.Write('\n');
#endif
        }
    }
}
