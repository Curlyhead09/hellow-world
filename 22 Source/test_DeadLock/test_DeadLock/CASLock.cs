using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test_DeadLock
{
    public class CASLock : IDisposable // .NET IDisposable 처리 정리
                                       // ; https://www.sysnet.pe.kr/2/0/347
    {
        int _lockVariable = 0;
        bool _disposed = true;

        public IDisposable Lock()
        {
            while (Interlocked.CompareExchange(ref _lockVariable, 1, 0) != 0) { }
            _disposed = false;
            return this;
        }

        void Free(bool disposing)
        {
            _lockVariable = 0;
            _disposed = true;
        }

        public void Dispose()
        {
            Free(true);
            GC.SuppressFinalize(this);
        }

        ~CASLock()
        {
#if DEBUG
            if (false == _disposed)
            {
                throw new ApplicationException("CASLock.Dispose() was not called!");
            }
#endif

            Free(false);
        }
    }

    public class LockLevel : IDisposable
    {
        int _currentLockLevel = 0;
        int _oldLockLevel = 0;
        object _lockThis = new object();

        static int _lockClassLevel = 0;

        [ThreadStatic]
        static int _checkLockLevel = 0;

        bool _locked = false;

        public LockLevel()
        {
            _currentLockLevel = Interlocked.Increment(ref _lockClassLevel);
        }

        public IDisposable Lock()
        {
            if (_checkLockLevel > _currentLockLevel)
            {
                throw new ApplicationException("Deadlock may occur!");
            }

            _locked = true;

            _oldLockLevel = _checkLockLevel;
            _checkLockLevel = _currentLockLevel;

            Monitor.Enter(_lockThis);
            return this;
        }

        void Free(bool disposing)
        {
            if (_locked == true)
            {
                _locked = false;
                _checkLockLevel = _oldLockLevel;
                Monitor.Exit(_lockThis);
            }
        }

        public void Dispose()
        {
            Free(true);
            GC.SuppressFinalize(this);
        }

        ~LockLevel()
        {
            Free(false);
        }
    }

}
