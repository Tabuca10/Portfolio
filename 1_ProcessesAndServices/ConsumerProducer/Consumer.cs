using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ProducerConsumer
{
    internal class Consumer : IDisposable
    {
        private Semaphore fillCount;
        private Semaphore emptyCount;
        private MemoryMappedFile sharedMemory;
        private MemoryMappedViewAccessor accessor;
        private int readPosition;
        private byte[] internalBuffer = new byte[1];

        public Consumer()
        {
            fillCount = new Semaphore(0, Producer.sharedBufferSize, Producer.fillCountSemaphoreName);
            emptyCount = new Semaphore(Producer.sharedBufferSize, Producer.sharedBufferSize, Producer.emptyCountSemaphoreName);
            sharedMemory = MemoryMappedFile.CreateOrOpen(Producer.sharedMemoryName, Producer.sharedBufferSize);
            accessor = sharedMemory.CreateViewAccessor(0, Producer.sharedBufferSize);
            readPosition = 0;
        }

        public void Dispose()
        {
            emptyCount.Dispose();
            fillCount.Dispose();
            accessor.Dispose();
            sharedMemory.Dispose();
        }

        internal void Start()
        {
            do
            {
                fillCount.WaitOne(); // wait for fillCount semaphore
                ReadFromBuffer(internalBuffer);
                emptyCount.Release(); // release emptyCount semaphore
                Console.Write(Encoding.ASCII.GetString(internalBuffer));
            } while (internalBuffer[0] != 0x00);
        }

        private void ReadFromBuffer(byte[] internalBuffer)
        {
            accessor.ReadArray(readPosition, internalBuffer, 0, internalBuffer.Length);
            readPosition++;
            readPosition = readPosition % Producer.sharedBufferSize;
        }
    }
}
