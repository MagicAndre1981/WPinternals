﻿// Copyright (c) 2018, Rene Lergner - @Heathcliff74xda
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//
// Some of the classes and functions in this file were found online.
// Where possible the original authors are referenced.

using System.Collections;
using System.IO;
using System.IO.Compression;

namespace WPinternals.HelperClasses
{
    internal static class Compression
    {
        internal static Stream GetDecompressedStreamWithSeek(Stream InputStream)
        {
            long P = InputStream.Position;
            byte[] GZipHeader = new byte[3];
            InputStream.Read(GZipHeader, 0, 3);
            InputStream.Position = P;
            if (StructuralComparisons.StructuralEqualityComparer.Equals(GZipHeader, new byte[] { 0x1F, 0x8B, 0x08 }))
            {
                return new GZipStream(InputStream, CompressionMode.Decompress, false);
            }
            else
            {
                return InputStream;
            }
        }

        internal static bool IsCompressedStream(Stream InputStream)
        {
            byte[] GZipHeader = new byte[3];
            InputStream.Read(GZipHeader, 0, 3);
            return StructuralComparisons.StructuralEqualityComparer.Equals(GZipHeader, new byte[] { 0x1F, 0x8B, 0x08 });
        }

        internal static GZipStream GetDecompressedStream(Stream InputStream)
        {
            return new GZipStream(InputStream, CompressionMode.Decompress, false);
        }
    }
}
