// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Network
{
    public static class HttpUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsClientError(this HttpStatusCode code)
        {
            return code is >= HttpStatusCode.BadRequest and < HttpStatusCode.InternalServerError;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsClientError(this HttpStatusCode? code)
        {
            return code is >= HttpStatusCode.BadRequest and < HttpStatusCode.InternalServerError;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsServerError(this HttpStatusCode code)
        {
            return code is >= HttpStatusCode.InternalServerError and < (HttpStatusCode) 600;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsServerError(this HttpStatusCode? code)
        {
            return code is >= HttpStatusCode.InternalServerError and < (HttpStatusCode) 600;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCustomError(this HttpStatusCode code)
        {
            return code >= (HttpStatusCode) 600;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCustomError(this HttpStatusCode? code)
        {
            return code >= (HttpStatusCode) 600;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetHttpStatusName(this HttpStatusCode code)
        {
            return GetHttpStatusName((Int32) code);
        }

        public static String GetHttpStatusName(Int32 code)
        {
            return code switch
            {
                100 => "Continue",
                101 => "Switching Protocols",
                102 => "Processing",
                103 => "Early Hints",
                200 => "OK",
                201 => "Created",
                202 => "Accepted",
                203 => "Non-Authoritative Information",
                204 => "No Content",
                205 => "Reset Content",
                206 => "Partial Content",
                207 => "Multi-Status",
                208 => "Already Reported",
                226 => "IM Used",
                300 => "Multiple Choices",
                301 => "Moved Permanently",
                302 => "Found",
                303 => "See Other",
                304 => "Not Modified",
                305 => "Use Proxy",
                306 => "Switch Proxy",
                307 => "Temporary Redirect",
                308 => "Permanent Redirect",
                400 => "Bad Request",
                401 => "Unauthorized",
                402 => "Payment Required",
                403 => "Forbidden",
                404 => "Not Found",
                405 => "Method Not Allowed",
                406 => "Not Acceptable",
                407 => "Proxy Authentication Required",
                408 => "Request Timeout",
                409 => "Conflict",
                410 => "Gone",
                411 => "Length Required",
                412 => "Precondition Failed",
                413 => "Payload Too Large",
                414 => "URI Too Long",
                415 => "Unsupported Media Type",
                416 => "Range Not Satisfiable",
                417 => "Expectation Failed",
                421 => "Misdirected Request",
                422 => "Unprocessable Entity",
                423 => "Locked",
                424 => "Failed Dependency",
                425 => "Too Early",
                426 => "Upgrade Required",
                427 => "Unassigned",
                428 => "Precondition Required",
                429 => "Too Many Requests",
                431 => "Request Header Fields Too Large",
                451 => "Unavailable For Legal Reasons",
                500 => "Internal Server Error",
                501 => "Not Implemented",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                504 => "Gateway Timeout",
                505 => "HTTP Version Not Supported",
                506 => "Variant Also Negotiates",
                507 => "Insufficient Storage",
                508 => "Loop Detected",
                510 => "Not Extended",
                511 => "Network Authentication Required",
                _ => "Unknown"
            };
        }

        public static String? GetContentType(String extension)
        {
            return extension switch
            {
                // Base content types
                ".html" => "text/html",
                ".css" => "text/css",
                ".js" => "text/javascript",
                ".xml" => "text/xml",
                // Common content types
                ".gzip" => "application/gzip",
                ".json" => "application/json",
                ".map" => "application/json",
                ".pdf" => "application/pdf",
                ".zip" => "application/zip",
                ".mp3" => "audio/mpeg",
                ".jpg" => "image/jpeg",
                ".gif" => "image/gif",
                ".png" => "image/png",
                ".svg" => "image/svg+xml",
                ".mp4" => "video/mp4",
                // Application content types
                ".atom" => "application/atom+xml",
                ".fastsoap" => "application/fastsoap",
                ".ps" => "application/postscript",
                ".soap" => "application/soap+xml",
                ".sql" => "application/sql",
                ".xslt" => "application/xslt+xml",
                ".zlib" => "application/zlib",
                // Audio content types
                ".aac" => "audio/aac",
                ".ac3" => "audio/ac3",
                ".ogg" => "audio/ogg",
                // Font content types
                ".ttf" => "font/ttf",
                // Image content types
                ".bmp" => "image/bmp",
                ".jpm" => "image/jpm",
                ".jpx" => "image/jpx",
                ".jrx" => "image/jrx",
                ".tiff" => "image/tiff",
                ".emf" => "image/emf",
                ".wmf" => "image/wmf",
                // Message content types
                ".http" => "message/http",
                ".s-http" => "message/s-http",
                // Model content types
                ".mesh" => "model/mesh",
                ".vrml" => "model/vrml",
                // Text content types
                ".csv" => "text/csv",
                ".plain" => "text/plain",
                ".richtext" => "text/richtext",
                ".rtf" => "text/rtf",
                ".rtx" => "text/rtx",
                ".sgml" => "text/sgml",
                ".strings" => "text/strings",
                ".url" => "text/uri-list",
                // Video content types
                ".H264" => "video/H264",
                ".H265" => "video/H265",
                ".mpeg" => "video/mpeg",
                ".raw" => "video/raw",
                _ => null
            };
        }
    }
}