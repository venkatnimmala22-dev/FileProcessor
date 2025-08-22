using System;

namespace FileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FileProcessor processor = new FileProcessor();
                processor.ProcessFile();
            }
            catch (Exception ex)
            {
                DatabaseLogger.LogException(ex);
            }
        }
    }
}

