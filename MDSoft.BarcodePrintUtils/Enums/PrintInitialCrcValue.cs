﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.BarcodePrintUtils.Enums;

public enum PrintInitialCrcValue
{
    Zeros,
    NonZero1 = 0xffff,
    NonZero2 = 0x1D0F,
}