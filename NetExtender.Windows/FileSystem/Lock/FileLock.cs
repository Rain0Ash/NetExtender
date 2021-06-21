// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using NetExtender.IO.FileSystem.Lock.Common;
using NetExtender.IO.FileSystem.Lock.Content;
using NetExtender.IO.FileSystem.Lock.Interfaces;

namespace NetExtender.IO.FileSystem.Lock
{
    public class FileLock : IFileLock
    {
        public TimeSpan Timeout { get; }

        public String Name { get; }

        private String Path { get; set; }
        
        public FileLock(String name, TimeSpan timeout)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), @"Name cannot be null or empty.");
            }
            
            Name = name;
            Path = LockHelper.GetFilePath(name);
            Timeout = timeout;
        }

        public Boolean TryAcquireLock()
        {
            if (!LockHelper.LockExists(Path))
            {
                return AcquireLock();
            }

            FileLockContent content = LockHelper.ReadLock(Path);

            //Someone else owns the lock
            if (content.GetType() == typeof(OtherProcessOwnsFileLockContent))
            {
                return false;
            }

            //the file no longer exists
            if (content.GetType() == typeof(MissingFileLockContent))
            {
                return AcquireLock();
            }
            
            DateTime writeTime = new DateTime(content.Timestamp);

            //This lock belongs to this process - we can reacquire the lock
            if (content.PID == Process.GetCurrentProcess().Id)
            {
                return AcquireLock();
            }

            //The lock has not timed out - we can't acquire it
            return Math.Abs((DateTime.Now - writeTime).TotalSeconds) > Timeout.TotalSeconds && AcquireLock();
        }


        public Boolean ReleaseLock()
        {
            //Need to own the lock in order to release it (and we can reacquire the lock inside the current process)
            if (LockHelper.LockExists(Path) && TryAcquireLock())
            {
                LockHelper.DeleteLock(Path);
            }

            return true;
        }

        private static FileLockContent CreateLockContent()
        {
            Process process = Process.GetCurrentProcess();
            
            return new FileLockContent
            {
                PID = process.Id,
                Timestamp = DateTime.Now.Ticks,
                ProcessName = process.ProcessName
            };
        }

        private Boolean AcquireLock()
        {
            return LockHelper.WriteLock(Path, CreateLockContent());
        }
    }
}