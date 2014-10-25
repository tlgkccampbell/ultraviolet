﻿using System;

namespace TwistedLogik.Nucleus
{
    /// <summary>
    /// Represents a masked 32-bit integer.
    /// </summary>
    /// <remarks>Masking allows an integer to be stored, on average, with fewer than 4 bytes of memory. To do this, the integer value
    /// is treated as a sequence of bytes, and any bytes which have a value of zero are omitted from the output stream. Masking requires
    /// the integer value to be prepended with an additional byte of data, the masking byte, which tracks which integer bytes have non-zero
    /// values; this means that the size of a masked 32-bit integer is 2 bytes in the best case and 5 bytes in the worst case.</remarks>
    [CLSCompliant(false)]
    public struct MaskedUInt32
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaskedUInt32"/> structure.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public MaskedUInt32(UInt32 value)
        {
            Value = value;
        }

        /// <summary>
        /// Retrieves a human-readable string that represents the object.
        /// </summary>
        /// <returns>A human-readable string that represents the object.</returns>
        public override String ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Implicitly converts the masked integer to a 32-bit unsigned integer.
        /// </summary>
        /// <param name="masked">The masked integer to convert.</param>
        /// <returns>The converted integer.</returns>
        public static implicit operator UInt32(MaskedUInt32 masked)
        {
            return masked.Value;
        }

        /// <summary>
        /// Implicitly converts a 32-bit unsigned integer to a masked integer.
        /// </summary>
        /// <param name="value">The integer to convert.</param>
        /// <returns>The converted masked integer.</returns>
        public static implicit operator MaskedUInt32(UInt32 value)
        {
            return new MaskedUInt32(value);
        }

        /// <summary>
        /// Creates a copy of this integer with the specified byte set to the specified value.
        /// </summary>
        /// <param name="byteIndex">The index of the byte to set.</param>
        /// <param name="byteValue">The value to set in the specified byte.</param>
        public MaskedUInt32 WithByte(Int32 byteIndex, Byte byteValue)
        {
            var value = Value;
            value |= (uint)((uint)byteValue << (byteIndex * 8));
            return new MaskedUInt32(value);
        }

        /// <summary>
        /// Gets the mask for this value.
        /// </summary>
        /// <returns>The mask for this value.</returns>
        public Byte GetMask()
        {
            byte mask = 0;
            for (int i = 0; i < sizeof(int); i++)
                if (GetByte(i) != 0) mask |= (byte)(1 << i);
            return mask;
        }

        /// <summary>
        /// Gets the value of the byte with the specified index.
        /// </summary>
        /// <param name="ix">The index of the byte to retrieve.</param>
        /// <returns>The value of the byte with the specified index.</returns>
        public Byte GetByte(Int32 ix)
        {
            if (ix >= sizeof(int))
                return 0;

            var shift = (ix * 8);
            var mask = (ulong)0xFF << shift;
            return (byte)((Value & mask) >> shift);
        }

        /// <summary>
        /// Gets the size of the integer in bytes.
        /// </summary>
        /// <returns>The size of the integer in bytes.</returns>
        internal Int32 GetSizeInBytes()
        {
            var size = 1;
            if ((Value & 0x000000FF) != 0) size++;
            if ((Value & 0x0000FF00) != 0) size++;
            if ((Value & 0x00FF0000) != 0) size++;
            if ((Value & 0xFF000000) != 0) size++;
            return size;
        }

        /// <summary>
        /// Gets the underlying integer value.
        /// </summary>
        public readonly UInt32 Value;
    }
}