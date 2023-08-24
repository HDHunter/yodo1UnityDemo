namespace Yodo1.Suit
{
    using System;
    using System.IO;
    using UnityEngine;

    public class TXTReader : IDisposable
    {
#if UNITY_EDITOR
        private StringReader m_file;
        private string m_token;
#endif
        private bool m_silent = false;
        private bool m_disposed = false;

        public TXTReader(string fileName, bool silent = false)
        {
#if UNITY_EDITOR
            m_silent = silent;

            try
            {
                var r = new StreamReader(fileName);
                m_file = new StringReader(r.ReadToEnd());
            }
            catch (Exception ex)
            {
                if (!m_silent)
                {
                    Debug.LogWarning("Could not load: " + fileName + ", error: " + ex.Message);
                }
            }
#endif
        }

        ~TXTReader()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
#if UNITY_EDITOR
            GC.SuppressFinalize(this);
#endif
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.m_disposed)
            {
#if UNITY_EDITOR
                if (disposing && m_file != null)
                {
                    m_file.Dispose();
                }
#endif

                m_disposed = true;
            }
        }

        public bool NextRow()
        {
#if UNITY_EDITOR
            if (m_file == null)
            {
                return false;
            }

            string line = m_file.ReadLine();
            if (line == null)
            {
                // End of file
                return false;
            }

            m_token = line;
            return true;
#else
        return false;
#endif
        }

        public string ReadString()
        {
#if UNITY_EDITOR
            return m_token;
#else
        return "";
#endif
        }
    }
}