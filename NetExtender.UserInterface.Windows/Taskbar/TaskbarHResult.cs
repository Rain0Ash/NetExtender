// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.UserInterface.Windows.Taskbar
{
    /// <summary>HRESULT Wrapper</summary>
    public enum TaskbarHResult
    {
        /// <summary>E_NOINTERFACE</summary>
        NoInterface = -2147467262, // 0x80004002
        /// <summary>E_FAIL</summary>
        Fail = -2147467259, // 0x80004005
        /// <summary>TYPE_E_ELEMENTNOTFOUND</summary>
        TypeElementNotFound = -2147319765, // 0x8002802B
        /// <summary>The requested resources is read-only.</summary>
        AccessDenied = -2147287035, // 0x80030005
        /// <summary>NO_OBJECT</summary>
        NoObject = -2147221019, // 0x800401E5
        /// <summary>E_OUTOFMEMORY</summary>
        OutOfMemory = -2147024882, // 0x8007000E
        /// <summary>E_INVALIDARG</summary>
        InvalidArguments = -2147024809, // 0x80070057
        /// <summary>The requested resource is in use</summary>
        ResourceInUse = -2147024726, // 0x800700AA
        /// <summary>E_ELEMENTNOTFOUND</summary>
        ElementNotFound = -2147023728, // 0x80070490
        /// <summary>ERROR_CANCELLED</summary>
        Canceled = -2147023673, // 0x800704C7
        /// <summary>S_OK</summary>
        Ok = 0,
        /// <summary>S_FALSE</summary>
        False = 1,
        /// <summary>Win32 Error code: ERROR_CANCELLED</summary>
        Win32ErrorCanceled = 1223 // 0x000004C7
    }
}