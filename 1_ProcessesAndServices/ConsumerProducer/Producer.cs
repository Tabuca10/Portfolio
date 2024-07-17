using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ProducerConsumer
{
    internal class Producer : IDisposable
    {
        public static int sharedBufferSize = 256;
        public static string fillCountSemaphoreName = "fillCount";
        public static string emptyCountSemaphoreName = "emptyCount";
        public static string sharedMemoryName = "mysharedbuffer";
        private MemoryMappedFile shareMemory;
        private byte[] internalBuffer = new byte[1];
        private FileStream fileStream = File.OpenRead("myfiletoread.txt");
        private MemoryMappedFile sharedMemory;
        private MemoryMappedViewAccessor accessor;
        private int writePosition;
        private Semaphore fillCount;
        private Semaphore emptyCount;

        public Producer()
        {
            fillCount = new Semaphore(0, sharedBufferSize, fillCountSemaphoreName);
            emptyCount = new Semaphore(sharedBufferSize, sharedBufferSize, emptyCountSemaphoreName);
            sharedMemory = MemoryMappedFile.CreateOrOpen(sharedMemoryName, sharedBufferSize);
            accessor = sharedMemory.CreateViewAccessor(0, Producer.sharedBufferSize);
            writePosition = 0;
        }

        public void Dispose()
        {
            emptyCount.Dispose();
            fillCount.Dispose();
            fileStream.Dispose();
            accessor.Dispose();
            sharedMemory.Dispose();
        }

        internal void Start()
        {
            int bytesReaded = 0;
            do
            {
                bytesReaded = ProduceItem(internalBuffer);
                emptyCount.WaitOne(); // wait for emptyCount semaphore
                PutItemIntoBuffer(internalBuffer);
                fillCount.Release(); // release fillCount semaphore
            } while (bytesReaded > 0);
            emptyCount.WaitOne(); // wait for emptyCount semaphore
            PutItemIntoBuffer(new byte[] { 0x00 }); //Notify end off text
            fillCount.Release(); // release fillCount semaphore
        }

        private void PutItemIntoBuffer(byte[] data)
        {
            accessor.WriteArray(writePosition, data, 0, data.Length);
            Console.Write(Encoding.ASCII.GetString(data));
            writePosition++;
            writePosition = writePosition % sharedBufferSize;
        }

        private int ProduceItem(byte[] buffer)
        {
            return fileStream.Read(buffer, 0, buffer.Length);
        }
    }
}
